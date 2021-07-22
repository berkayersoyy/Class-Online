using System.Collections.Generic;
using System.Linq;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Moq;
using Xunit;

namespace DataAccess.UnitTests
{
    public class UserUnitTests
    {
        private Mock<IUserDal> _mock;
        public UserUnitTests()
        {
            _mock = new Mock<IUserDal>();
        }

        [Fact]
        public void GetAll_Should_Return_All_Valid()
        {
            _mock.Setup(x => x.GetAll(null)).Returns(SampleUsers());
            var userDal = _mock.Object;
            var expected = SampleUsers().Count;
            var actual = userDal.GetAll();
            Assert.True(actual != null);
            Assert.Equal(expected, actual.Count);
        }

        [Fact]
        public void Get_Should_Return_1()
        {
            var users = SampleUsers();
            var userDto = users[0];
            _mock.Setup(x => x.Get(x => x.Id == 1)).Returns(userDto);
            var userDal = _mock.Object;
            var expected = users.FirstOrDefault(x => x.Id == 1);
            var actual = userDal.Get(x => x.Id == 1);
            Assert.True(actual != null);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Email, actual.Email);
        }
        [Fact]
        public void GetClaims_Should_Return_Valid_Claims()
        {
            _mock.Setup(x => x.GetClaims(It.IsAny<User>())).Returns(SampleOperationClaims());
            var userDal = _mock.Object;
            var expected = SampleOperationClaims();
            var actual = userDal.GetClaims(It.IsAny<User>());
            Assert.True(actual!=null);
            Assert.Equal(expected.Count,actual.Count);
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