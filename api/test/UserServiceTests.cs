using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Irrigation.Services;
using Irrigation.Entities;

namespace Test
{
    public class UserServiceTests
    {
        [Fact]
        public void Update_CanUpdatePassword_True()
        {
            using(var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var user = new User(){
                    UserId = 1,
                };
                var service = new UserService(ctx);
                var preUpdateUser = ctx.Users
                    .Where(u => u.UserId == user.UserId).First();
                var expectedHash = preUpdateUser.PasswordHash;
                var expectedSalt = preUpdateUser.PasswordSalt;

                service.Update(user, "TestPassword");

                var postUpdateUser = ctx.Users
                    .Where(u => u.UserId == user.UserId).First();

                Assert.True(postUpdateUser.PasswordHash == preUpdateUser.PasswordHash);
                Assert.True(postUpdateUser.PasswordSalt == preUpdateUser.PasswordSalt);
            }
        }

        [Fact]
        public void Update_()
        {
            using(var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var user = new User(){
                    UserId = 1,
                };
                var service = new UserService(ctx);
                var preUpdateUser = ctx.Users
                    .Where(u => u.UserId == user.UserId).First();
                var expectedHash = preUpdateUser.PasswordHash;
                var expectedSalt = preUpdateUser.PasswordSalt;

                service.Update(user, "TestPassword");

                var postUpdateUser = ctx.Users
                    .Where(u => u.UserId == user.UserId).First();

                Assert.True(postUpdateUser.PasswordHash == preUpdateUser.PasswordHash);
                Assert.True(postUpdateUser.PasswordSalt == preUpdateUser.PasswordSalt);
            }
        }

        private void SeedUsers(DataContext context) 
        {
            var users = new List<User>() {
                new User(){
                    UserId = 1,
                    FirstName = "Frank",
                    LastName = "Frankenstein",
                    Username = "FFrank",
                    PasswordHash = new byte[] { 0x20, 0x20 },
                    PasswordSalt = new byte[] { 0x20, 0x20 },
                },
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}