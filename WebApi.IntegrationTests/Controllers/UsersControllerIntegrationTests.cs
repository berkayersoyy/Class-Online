using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Newtonsoft.Json;
using WebAPI;
using Xunit;

namespace WebApi.IntegrationTests.Controllers
{
    public class UsersControllerIntegrationTests:IClassFixture<CustomWebApplicationFactoryWithSql<Startup>>
    {
        private readonly HttpClient _client;

        public UsersControllerIntegrationTests(CustomWebApplicationFactoryWithSql<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost/api/");
        }

        [Theory]
        [InlineData("users/add")]
        public async void Add_WithValidData_ShouldReturnSuccessResult(string userUri)
        {
            var expectedMessage = Messages.UserAdded;
            var expectedStatusCode = HttpStatusCode.OK;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123456789",out passwordHash,out passwordSalt);
            var request = new User
            {
                FirstName = "berkay",
                LastName = "ersoy",
                Email = "newmail@domain.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult!=null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedMessage,actualResult.Message);
            Assert.Equal(expectedStatusCode,actualStatusCode);
        }
        [Theory]
        [InlineData("users/add")]
        public async void Add_WithEmptyFirstName_ShouldReturnErrorResult(string userUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123456789", out passwordHash, out passwordSalt);
            var request = new User
            {
                FirstName = String.Empty,
                LastName = "ersoy",
                Email = "newmail@domain.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("users/add")]
        public async void Add_WithNullFirstName_ShouldReturnErrorResult(string userUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123456789", out passwordHash, out passwordSalt);
            var request = new User
            {
                FirstName = null,
                LastName = "ersoy",
                Email = "newmail@domain.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("users/add")]
        public async void Add_WithFirstNameLengthShorterThan2_ShouldReturnErrorResult(string userUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123456789", out passwordHash, out passwordSalt);
            var request = new User
            {
                FirstName = "b",
                LastName = "ersoy",
                Email = "newmail@domain.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("users/add")]
        public async void Add_WithFirstNameLengthLongerThan15_ShouldReturnErrorResult(string userUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123456789", out passwordHash, out passwordSalt);
            var request = new User
            {
                FirstName = "berkayberkayberk",
                LastName = "ersoy",
                Email = "newmail@domain.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("users/add")]
        public async void Add_WithEmptyLastName_ShouldReturnErrorResult(string userUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123456789", out passwordHash, out passwordSalt);
            var request = new User
            {
                FirstName = "berkay",
                LastName = String.Empty,
                Email = "newmail@domain.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("users/add")]
        public async void Add_WithNullLastName_ShouldReturnErrorResult(string userUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123456789", out passwordHash, out passwordSalt);
            var request = new User
            {
                FirstName = "berkay",
                LastName = null,
                Email = "newmail@domain.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("users/add")]
        public async void Add_WithLastNameLengthShorterThan2_ShouldReturnErrorResult(string userUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123456789", out passwordHash, out passwordSalt);
            var request = new User
            {
                FirstName = "berkay",
                LastName = "e",
                Email = "newmail@domain.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("users/add")]
        public async void Add_WithLastNameLengthLongerThan15_ShouldReturnErrorResult(string userUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123456789", out passwordHash, out passwordSalt);
            var request = new User
            {
                FirstName = "berkay",
                LastName = "berkayberkayberk",
                Email = "newmail@domain.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("users/add")]
        public async void Add_WithInvalidEmail_ShouldReturnErrorResult(string userUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("123456789", out passwordHash, out passwordSalt);
            var request = new User
            {
                FirstName = "berkay",
                LastName = "ersoy",
                Email = "newmail",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        /*[Theory]
        [InlineData("users/add")]
        public async void Add_WithPasswordLengthShorterThan9_ShouldReturnErrorResult(string userUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("12345678", out passwordHash, out passwordSalt);
            var request = new User
            {
                FirstName = "berkay",
                LastName = "ersoy",
                Email = "newmail",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("users/add")]
        public async void Add_WithPasswordLengthLongerThan15_ShouldReturnErrorResult(string userUri)
        {
            var expectedMessage = Messages.ValidationFailed;
            var expectedStatusCode = HttpStatusCode.BadRequest;
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("1234567891234567", out passwordHash, out passwordSalt);
            var request = new User
            {
                FirstName = "berkay",
                LastName = "ersoy",
                Email = "newmail",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(!actualResult.Success);
            Assert.Contains(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }*/
        [Theory]
        [InlineData("users/update")]
        public async void Update_WithValidData_ShouldReturnSuccessResult(string userUri)
        {
            var expectedMessage = Messages.UserUpdated;
            var expectedStatusCode = HttpStatusCode.OK;
            byte[] passwordHash1, passwordSalt1;
            HashingHelper.CreatePasswordHash("123456789", out passwordHash1, out passwordSalt1);
            var request = new User
            {
                Id = 1,
                FirstName = "berkay",
                LastName = "ersoy",
                Email = "sampleemail1@domain.com",
                Status = true,
                PasswordHash = passwordHash1,
                PasswordSalt = passwordSalt1
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("users/delete")]
        public async void Delete_WithValidData_ShouldReturnSuccessResult(string userUri)
        {
            var expectedMessage = Messages.UserDeleted;
            var expectedStatusCode = HttpStatusCode.OK;
            byte[] passwordHash1, passwordSalt1;
            HashingHelper.CreatePasswordHash("123456789", out passwordHash1, out passwordSalt1);
            var request = new User
            {
                Id = 2,
                FirstName = "betul",
                LastName = "ersoy",
                Email = "sampleemail2@domain.com",
                Status = true,
                PasswordHash = passwordHash1,
                PasswordSalt = passwordSalt1
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(userUri, content);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<Result>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        [Theory]
        [InlineData("users/getbymail?email=sampleemail1@domain.com")]
        public async void GetByMail_WithValidEmail_ShouldReturnSuccessDataResult(string userUri)
        {
            var expectedMessage = Messages.GetUser;
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedFirstName = "berkay";
            var expectedLastName = "ersoy";
            var expectedId = 3;

            var response = await _client.GetAsync(userUri);
            var actualStatusCode = response.StatusCode;
            var actualResultJson = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<DataResult<User>>(actualResultJson);

            Assert.True(actualResult != null);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedMessage, actualResult.Message);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal(expectedFirstName,actualResult.Data.FirstName);
            Assert.Equal(expectedLastName, actualResult.Data.LastName);
            Assert.Equal(expectedId,actualResult.Data.Id);
        }
    }
}