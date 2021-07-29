using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.DTOs;
using Newtonsoft.Json;
using WebAPI;
using Xunit;

namespace WebApi.IntegrationTests.Controllers
{
    public class AuthControllerIntegrationTests : IClassFixture<CustomWebApplicationFactoryWithSql<Startup>>
    {
        private readonly HttpClient _client;

        public AuthControllerIntegrationTests(CustomWebApplicationFactoryWithSql<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost/api/");
        }

        [Theory]
        [InlineData("auth/login")]
        public async void Login_WithValidData_ShouldReturnSuccessDataResult(string authUri)
        {
            var expectedMessage = Messages.AccessTokenCreated;
            var expectedStatusCode = HttpStatusCode.OK;
            var request = new UserForLoginDto
            {
                Email = "sampleemail1@domain.com",
                Password = "123456789"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(authUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<DataResult<AccessToken>>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode,actualStatusCode);
            Assert.Equal(expectedMessage,actualResult.Message);
        }
        [Theory]
        [InlineData("auth/login")]
        public async void Login_WithWrongEmail_ShouldReturnErrorDataResult(string authUri)
        {
            var expectedMessage = Messages.UserNotFound;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new UserForLoginDto
            {
                Email = "wrongemail1@domain.com",
                Password = "123456789"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(authUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<DataResult<User>>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(actualResult.Data==null);
            Assert.True(!actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(expectedMessage, actualResult.Message);
        }
        [Theory]
        [InlineData("auth/login")]
        public async void Login_WithWrongPassword_ShouldReturnErrorDataResult(string authUri)
        {
            var expectedMessage = Messages.PasswordError;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new UserForLoginDto
            {
                Email = "sampleemail1@domain.com",
                Password = "12345678911"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(authUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<DataResult<User>>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(actualResult.Data != null);
            Assert.True(!actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(expectedMessage, actualResult.Message);
        }
        [Theory]
        [InlineData("auth/register")]
        public async void Register_WithValidData_ShouldReturnSuccessDataResult(string authUri)
        {
            var expectedMessage = Messages.AccessTokenCreated;
            var expectedStatusCode = HttpStatusCode.OK;
            var request = new UserForRegisterDto
            {
                Email = "sampleemail5@domain.com",
                Password = "123456789",
                FirstName = "Belguzar",
                LastName = "Ersoy"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(authUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<DataResult<AccessToken>>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(expectedMessage, actualResult.Message);
        }
        [Theory]
        [InlineData("auth/register")]
        public async void Register_WithExistingEmail_ShouldReturnErrorResult(string authUri)
        {
            var expectedMessage = Messages.UserAlreadyExist;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var request = new UserForRegisterDto
            {
                Email = "sampleemail1@domain.com",
                Password = "123456789",
                FirstName = "Belguzar",
                LastName = "Ersoy"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(authUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(expectedMessage, actualResult.Message);
        }
    }
}