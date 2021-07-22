using System.Collections.Generic;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using Moq;
using Xunit;

namespace Business.UnitTests
{
    public class AuthManagerUnitTests
    {
        private Mock<IAuthService> _mock;

        public AuthManagerUnitTests()
        {
            _mock = new Mock<IAuthService>();
        }

        [Fact]
        public void Register_WithValidData_ShouldReturnSuccess()
        {
            var userToRegister = SampleUsers()[0];
            _mock.Setup(x => x.Register(It.IsAny<UserForRegisterDto>())).Returns(new SuccessDataResult<User>(userToRegister));
            var authService = _mock.Object;
            var expected = new SuccessDataResult<User>(userToRegister);
            var actual = authService.Register(It.IsAny<UserForRegisterDto>());
            Assert.True(actual!=null);
            Assert.Equal(expected.GetType(),actual.GetType());
            Assert.Equal(expected.Data.Id,actual.Data.Id);
        }
        [Fact]
        public void Login_WithValidData_ShouldReturnSuccess()
        {
            var userToLogin= SampleUsers()[0];
            _mock.Setup(x => x.Login(It.IsAny<UserForLoginDto>())).Returns(new SuccessDataResult<User>(userToLogin));
            var authService = _mock.Object;
            var expected = new SuccessDataResult<User>(userToLogin);
            var actual = authService.Login(It.IsAny<UserForLoginDto>());
            Assert.True(actual != null);
            Assert.Equal(expected.GetType(), actual.GetType());
            Assert.Equal(expected.Data.Id, actual.Data.Id);
        }

        [Fact]
        public void UserExists_WithExistingUser_ShouldReturnSuccess()
        {
            _mock.Setup(x => x.UserExists(It.IsAny<string>())).Returns(new SuccessResult());
            var authService = _mock.Object;
            var expected = new SuccessResult();
            var actual = authService.UserExists(It.IsAny<string>());
            Assert.True(actual!=null);
            Assert.Equal(expected.GetType(),actual.GetType());
        }

        [Fact]
        public void UserExists_WhichUserNotExist_ShouldReturnError()
        {
            _mock.Setup(x => x.UserExists(It.IsAny<string>())).Returns(new ErrorResult());
        }
        private List<User> SampleUsers()
        {
            List<User> users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "sampleEmail@hotmail.com",
                    FirstName = "sampleName",
                    LastName = "sampleLastName",
                    PasswordHash = new byte[]{},
                    PasswordSalt = new byte[]{},
                    Status = true
                },
                new User
                {
                    Id = 2,
                    Email = "sampleEmail2@hotmail.com",
                    FirstName = "sampleName",
                    LastName = "sampleLastName",
                    PasswordHash = new byte[]{},
                    PasswordSalt = new byte[]{},
                    Status = true
                },
                new User
                {
                    Id = 3,
                    Email = "sampleEmail3@hotmail.com",
                    FirstName = "sampleName",
                    LastName = "sampleLastName",
                    PasswordHash = new byte[]{},
                    PasswordSalt = new byte[]{},
                    Status = true
                },
            };
            return users;
        }

    }
}