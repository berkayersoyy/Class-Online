
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using Newtonsoft.Json;
using WebAPI;
using WebAPI.Controllers;
using Xunit;

namespace WebApi.IntegrationTests.Controllers
{
    public class VideosControllerIntegrationTests : IClassFixture<CustomWebApplicationFactoryWithSql<Startup>>
    {
        private readonly HttpClient _client;

        public VideosControllerIntegrationTests(CustomWebApplicationFactoryWithSql<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost/api/");
        }
        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithValidData_ShouldReturnSuccessResult(string videoUri)
        {
            var expectedMessage = Messages.VideoAdded;
            var expectedStatusCode = HttpStatusCode.OK;
            var request = new Video
            {
                Description = "description which length more than 25",
                Extension = "mp4",
                Path = "path",
                Title = "title"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedMessage,actualResult.Message);
        }
        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithEmptyTitle_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Video
            {
                Description = "desc",
                Extension = "mp4",
                Path = "path",
                Title = String.Empty
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithNullTitle_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Video
            {
                Description = "desc",
                Extension = "mp4",
                Path = "path",
                Title = null
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithEmptyDescription_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Video
            {
                Description = String.Empty,
                Extension = "mp4",
                Path = "path",
                Title = "Title"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithNullDescription_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Video
            {
                Description = null,
                Extension = "mp4",
                Path = "path",
                Title = "Title"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithDescriptionLengthShorterThan25_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Video
            {
                Description = "012345678901234567890123",
                Extension = "mp4",
                Path = "path",
                Title = "Title"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithDescriptionLengthLongerThan250_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Video
            {
                Description = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678999",
                Extension = "mp4",
                Path = "path",
                Title = "Title"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithTitleLengthShorterThan5_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Video
            {
                Description = "desc",
                Extension = "mp4",
                Path = "path",
                Title = "Titl"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Contains(expectedMessage, actualResult.Message);
        }

        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithEmptyExtension_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Video
            {
                Description = "Description which length is more than 25",
                Extension = String.Empty,
                Path = "path",
                Title = "Title"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8,"application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode,actualStatusCode);
        }
        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithNullExtension_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Video
            {
                Description = "Description which length is more than 25",
                Extension = null,
                Path = "path",
                Title = "Title"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithEmptyPath_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Video
            {
                Description = "Description which length is more than 25",
                Extension = "mp4",
                Path = String.Empty,
                Title = "Title"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8,"application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.True(!actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Contains(expectedMessage, actualResult.Message);
        }
        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithNullPath_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Video
            {
                Description = "Description which length is more than 25",
                Extension = "mp4",
                Path = null,
                Title = "Title"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Contains(expectedMessage, actualResult.Message);
        }
        [Theory]
        [InlineData("videos/getall")]
        public async void GetList_WithValidData_ShouldReturnSuccessDataResultWithData(string videoUri)
        {
            //TODO Get all videos for check expected video count!
            var expectedMessage = Messages.GetVideos;
            var expectedStatusCode = HttpStatusCode.OK;

            var response = await _client.GetAsync(videoUri);

            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<DataResult<List<Video>>>(actualResultJson);
            var actualStatusCode = response.StatusCode;
            
            Assert.True(actualResult!=null);
            Assert.True(actualResult.Data!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedMessage,actualResult.Message);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(3,actualResult.Data.Count);
        }

        [Theory]
        [InlineData("videos/getbyid?videoid=1")]
        public async void Get_WithValidId_ShouldReturnSuccessDataResultWithData(string videoUri)
        {
            var expectedMessage = Messages.GetVideo;
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedVideoExtension = "mp4";

            var response = await _client.GetAsync(videoUri);

            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<DataResult<Video>>(actualResultJson);
            var actualStatusCode = response.StatusCode;

            Assert.True(actualResult!=null);
            Assert.True(actualResult.Data!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedMessage,actualResult.Message);
            Assert.Equal(expectedVideoExtension,actualResult.Data.Extension);
        }
        [Theory]
        [InlineData("videos/getbyid?videoid=")]
        public async void Get_WithEmptyId_ShouldReturnErrorDataResultWithoutData(string videoUri)
        {
            var expectedStatusCode = HttpStatusCode.BadRequest;

            var response = await _client.GetAsync(videoUri);

            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<DataResult<Video>>(actualResultJson);
            var actualStatusCode = response.StatusCode;

            Assert.True(actualResult != null);
            Assert.True(actualResult.Data == null);
            Assert.True(!actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Theory]
        [InlineData("videos/delete")]
        public async void Delete_WithValidData_ShouldReturnSuccessResult(string videoUri)
        {
            var expectedMessage = Messages.VideoDeleted;
            var expectedStatusCode = HttpStatusCode.OK;
            var request = new Video
            {
                Id = 1,
                Title = "Title 1",
                Extension = "mp4",
                Description = "Desc 1",
                Path = "videos"
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(videoUri, content);

            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedMessage,actualResult.Message);
        }

        [Theory]
        [InlineData("videos/update")]
        public async void Update_WithValidData_ShouldReturnSuccessDataResult(string videoUri)
        {
            var expectedMessage = Messages.VideoUpdated;
            var expectedStatusCode = HttpStatusCode.OK;
            var request = new Video
            {
                Id = 1,
                Title = "Title 3",
                Extension = "mp4",
                Description = "Desc 1",
                Path = "videos"
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(videoUri, content);

            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedMessage,actualResult.Message);
        }

    }
}