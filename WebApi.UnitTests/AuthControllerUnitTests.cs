using System;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using Xunit;

namespace WebApi.UnitTests
{
    public class AuthControllerUnitTests
    {
        private AuthController _authController;
        private Mock<IAuthService> _authService;

        public AuthControllerUnitTests()
        {
            _authService = new Mock<IAuthService>();
            _authController = new AuthController(_authService.Object);
        }
        [Fact]
        public void Login_WithWrongEmail_ShouldReturnBadRequestObjectModel()
        {
            _authService.Setup(x => x.Login(It.IsAny<UserForLoginDto>())).Returns(new ErrorDataResult<User>());
            var result = _authController.Login(It.IsAny<UserForLoginDto>());
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Login_WithWrongPassword_ShouldReturnBadRequestObjectModel()
        {
            _authService.Setup(x => x.Login(It.IsAny<UserForLoginDto>())).Returns(new ErrorDataResult<User>());
            var result = _authController.Login(It.IsAny<UserForLoginDto>());
            Assert.IsType<BadRequestObjectResult>(result);
        }
       
        [Fact]
        public void Login_WithValidData_ShouldReturnOkObjectModel()
        {
            _authService.Setup(x => x.Login(It.IsAny<UserForLoginDto>())).Returns(new SuccessDataResult<User>());
            var result = _authController.Login(new UserForLoginDto{Email = "aaa@aa.ca",Password = "1231231232"}) as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
