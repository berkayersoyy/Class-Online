
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Newtonsoft.Json;
using WebAPI;
using Xunit;

namespace WebApi.IntegrationTests.Controllers
{
    public class VideosControllerIntegrationTests : IClassFixture<CustomWebApplicationFactoryWithInMemory<TestStartup>>
    {
        private readonly HttpClient _client;

        public VideosControllerIntegrationTests(CustomWebApplicationFactoryWithInMemory<TestStartup> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async void Add_WithValidData_ShouldReturnSuccessResult()
        {
            var expectedResult = new SuccessResult("Video added.");
            var expectedStatusCode = HttpStatusCode.OK;
            _client.BaseAddress = new Uri("https://localhost/api/");
            var request = new Video
            {
                Description = "desc",
                Extension = "mp4",
                Path = "path",
                Title = "title"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("videos/add", content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<SuccessResult>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedResult.Message,actualResult.Message);
            Assert.Equal(expectedResult.Success,actualResult.Success);
        }
    }
}