using System.Collections.Generic;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Moq;
using Xunit;

namespace Business.UnitTests
{
    public class UserManagerUnitTests
    {
        private Mock<IUserService> _mock;

        public UserManagerUnitTests()
        {
            _mock = new Mock<IUserService>();
        }

        [Fact]
        public void GetClaims_Should_Return_All_Claims_For_User()
        {
            _mock.Setup(x => x.GetClaims(It.IsAny<User>()))
                .Returns(new SuccessDataResult<List<OperationClaim>>(SampleOperationClaims()));
            var userService = _mock.Object;
            var expected = SampleOperationClaims();
            var actual = userService.GetClaims(It.IsAny<User>());
            Assert.True(actual!=null);
            Assert.Equal(expected.Count,actual.Data.Count);
        }

        [Fact]
        public void GetClaims_Should_Return_SuccessDataResult()
        {
            _mock.Setup(x => x.GetClaims(It.IsAny<User>()))
                .Returns(new SuccessDataResult<List<OperationClaim>>(SampleOperationClaims()));
            var userService = _mock.Object;
            var expected = new SuccessDataResult<List<OperationClaim>>(SampleOperationClaims());
            var actual = userService.GetClaims(It.IsAny<User>());
            Assert.True(actual != null);
            Assert.Equal(expected.GetType(), actual.GetType());
        }

        [Fact]
        public void GetByMail_Should_Return_Valid_User()
        {
            var users = SampleUsers();
            var userDto = users[0];
            _mock.Setup(x => x.GetByMail(It.IsAny<string>())).Returns(new SuccessDataResult<User>(userDto));
            var userService = _mock.Object;
            var expected = userDto;
            var actual = userService.GetByMail(It.IsAny<string>());
            Assert.True(actual!=null);
            Assert.Equal(expected.Email,actual.Data.Email);
            Assert.Equal(expected.Id,actual.Data.Id);
        }

        private List<OperationClaim> SampleOperationClaims()
        {
            List<OperationClaim> operationClaims = new List<OperationClaim>
            {
                new OperationClaim
                {
                    Id = 1,
                    Name = "Claim1"
                },
                new OperationClaim
                {
                    Id = 2,
                    Name = "Claim2"
                },
                new OperationClaim
                {
                    Id = 3,
                    Name = "Claim3"
                },
                new OperationClaim
                {
                    Id = 4,
                    Name = "Claim4"
                },
            };
            return operationClaims;
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