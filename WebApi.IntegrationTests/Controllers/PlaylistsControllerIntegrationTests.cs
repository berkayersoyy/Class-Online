using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using Newtonsoft.Json;
using WebAPI;
using Xunit;

namespace WebApi.IntegrationTests.Controllers
{
    //TODO authentication tests need to be added.
    public class PlaylistsControllerIntegrationTests:IClassFixture<CustomWebApplicationFactoryWithSql<Startup>>
    {
        private readonly HttpClient _client;

        public PlaylistsControllerIntegrationTests(CustomWebApplicationFactoryWithSql<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost/api/");
        }

        [Theory]
        [InlineData("playlists/add")]
        public async void Add_WithValidData_ShouldReturnSuccessResult(string videoUri)
        {
            var expectedMessage = Messages.PlaylistAdded;
            var expectedStatusCode = HttpStatusCode.OK;
            var request = new Playlist
            {
                VideoList = "10,11"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8,"application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedMessage, actualResult.Message);
        }
        [Theory]
        [InlineData("playlists/add")]
        public async void Add_WithEmptyVideoList_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Playlist
            {
                VideoList = String.Empty
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
        [InlineData("playlists/add")]
        public async void Add_WithNullVideoList_ShouldReturnErrorResult(string videoUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new Playlist
            {
                VideoList = null
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
        [InlineData("playlists/getall")]
        public async void GetList_WithValidData_ShouldReturnSuccessDataResult(string videoUri)
        {
            var expectedMessage = Messages.GetPlaylists;
            var expectedStatusCode = HttpStatusCode.OK;

            var response = await _client.GetAsync(videoUri);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<DataResult<List<Playlist>>>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(actualResult.Data!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(4,actualResult.Data.Count);
            Assert.Equal(expectedMessage, actualResult.Message);
        }
        [Theory]
        [InlineData("playlists/getbyid?playlistid=1")]
        public async void Get_WithValidId_ShouldReturnSuccessDataResult(string videoUri)
        {
            var expectedMessage = Messages.GetPlaylist;
            var expectedId = 1;
            var expectedStatusCode = HttpStatusCode.OK;

            var response = await _client.GetAsync(videoUri);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<DataResult<Playlist>>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(actualResult.Data != null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(expectedId,actualResult.Data.Id);
            Assert.Equal(expectedMessage, actualResult.Message);
        }
        [Theory]
        [InlineData("playlists/getbyid?playlistid=")]
        public async void Get_WithEmptyId_ShouldReturnErrorDataResult(string videoUri)
        {
            var expectedStatusCode = HttpStatusCode.BadRequest;

            var response = await _client.GetAsync(videoUri);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<DataResult<Playlist>>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(actualResult.Data == null);
            Assert.True(!actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("playlists/update")]
        public async void Update_WithValidData_ShouldReturnSuccessResult(string videoUri)
        {
            var expectedMessage = Messages.PlaylistUpdated;
            var expectedStatusCode = HttpStatusCode.OK;
            var request = new Playlist
            {
                Id = 1,
                VideoList = "1,2,3,4"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Contains(expectedMessage, actualResult.Message);
        }
        [Theory]
        [InlineData("playlists/delete")]
        public async void Delete_WithValidData_ShouldReturnSuccessResult(string videoUri)
        {
            var expectedMessage = Messages.PlaylistDeleted;
            var expectedStatusCode = HttpStatusCode.OK;
            var request = new Playlist
            {
                Id = 1,
                VideoList = "1,2"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(videoUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Contains(expectedMessage, actualResult.Message);
        }
    }
}