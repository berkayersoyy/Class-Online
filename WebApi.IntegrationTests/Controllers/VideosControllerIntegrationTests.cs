
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Newtonsoft.Json;
using WebAPI;
using Xunit;

namespace WebApi.IntegrationTests.Controllers
{
    public class VideosControllerIntegrationTests : IClassFixture<CustomWebApplicationFactoryWithInMemory<Startup>>
    {
        private readonly HttpClient _client;

        public VideosControllerIntegrationTests(CustomWebApplicationFactoryWithInMemory<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost/api/");
        }

        [Theory]
        [InlineData("videos/add")]
        public async void Add_WithValidData_ShouldReturnSuccessResult(string videoUri)
        {
            var expectedResult = new SuccessResult("Video added.");
            var expectedStatusCode = HttpStatusCode.OK;
            var request = new Video
            {
                Description = "desc",
                Extension = "mp4",
                Path = "path",
                Title = "title"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<SuccessResult>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedResult.Message,actualResult.Message);
            Assert.Equal(expectedResult.Success,actualResult.Success);
        }

        [Theory]
        [InlineData("videos/getall")]
        public async void GetList_WithValidData_ShouldReturnSuccessDataResultWithData(string videoUri)
        {
            var expectedResult = new SuccessDataResult<List<Video>>();
            var expectedStatusCode = HttpStatusCode.OK;

            var response = await _client.GetAsync(videoUri);

            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<SuccessDataResult<List<Video>>>(actualResultJson);
            var actualStatusCode = response.StatusCode;
            
            Assert.True(actualResult!=null);
            Assert.True(actualResult.Data!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedResult.GetType(),actualResult.GetType());
            Assert.Equal(3,actualResult.Data.Count);
        }

        [Theory]
        [InlineData("videos/getbyid?videoid=1")]
        public async void Get_WithValidId_ShouldReturnSuccessDataResultWithData(string videoUri)
        {
            var expectedResult = new SuccessDataResult<Video>();
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedVideoExtension = "mp4";

            var response = await _client.GetAsync(videoUri);

            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<SuccessDataResult<Video>>(actualResultJson);
            var actualStatusCode = response.StatusCode;

            Assert.True(actualResult!=null);
            Assert.True(actualResult.Data!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedResult.GetType(),actualResult.GetType());
            Assert.Equal(expectedVideoExtension,actualResult.Data.Extension);
        }

        [Theory]
        [InlineData("videos/delete")]
        public async void Delete_WithValidData_ShouldReturnSuccessResult(string videoUri)
        {
            var expectedResult = new SuccessResult("Video deleted.");
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
            var actualResult = JsonConvert.DeserializeObject<SuccessResult>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedResult.GetType(),actualResult.GetType());
            Assert.Equal(expectedResult.Message,actualResult.Message);
        }

        [Theory]
        [InlineData("videos/update")]
        public async void Update_WithValidData_ShouldReturnSuccessDataResult(string videoUri)
        {
            var expectedResult = new SuccessResult("Video updated.");
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
            var actualResult = JsonConvert.DeserializeObject<SuccessResult>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedResult.GetType(),actualResult.GetType());
            Assert.Equal(expectedResult.Message,actualResult.Message);
        }

    }
}