using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DataAccess.IntegrationTests
{
    public class UserIntegrationTests:IDisposable
    {
        private NorthwindContext _context;
        public UserIntegrationTests()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkSqlServer().BuildServiceProvider();
            var options = new DbContextOptionsBuilder<NorthwindContext>();
            options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=TrainerHome_{Guid.NewGuid().ToString()};Trusted_Connection=true;multipleactiveresultsets=true").UseInternalServiceProvider(serviceProvider);
            _context = new NorthwindContext(options.Options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void Add_Should_Be_Valid()
        {
            Seed(_context);
            var actual = _context.Users.ToList();
            var expected = SampleUsers().Count;
            Assert.True(actual!=null);
            Assert.Equal(expected,actual.Count);
        }
        private List<User> SampleUsers()
        {
            List<User> users = new List<User>
            {
                new User
                {
                    Email = "sampleEmail@hotmail.com",
                    FirstName = "sampleName",
                    LastName = "sampleLastName",
                    PasswordHash = new byte[]{},
                    PasswordSalt = new byte[]{},
                    Status = true
                },
                new User
                {
                    Email = "sampleEmail2@hotmail.com",
                    FirstName = "sampleName",
                    LastName = "sampleLastName",
                    PasswordHash = new byte[]{},
                    PasswordSalt = new byte[]{},
                    Status = true
                },
                new User
                {
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
        private void Seed(NorthwindContext context)
        {
            context.AddRange(SampleUsers());
            context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}