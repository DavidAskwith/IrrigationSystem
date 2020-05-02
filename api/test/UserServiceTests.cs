using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Irrigation.Services;
using Irrigation.Entities;
using Irrigation.Helpers;

namespace Test
{
    public class UserServiceTests
    {
        private byte[] _passwordSalt;
        private byte[] _passwordHash;

        public UserServiceTests()
        {
            var password = "ValidPassword";
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                _passwordSalt = hmac.Key;
                _passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private void SeedUsers(DataContext context)
        {
            var users = new List<User>() {
                new User(){
                    UserId = 1,
                    FirstName = "Frank",
                    LastName = "Frankenstein",
                    Email = "FFrank",
                    PasswordHash = _passwordHash,
                    PasswordSalt = _passwordSalt
                },
                new User(){
                    UserId = 2,
                    FirstName = "Bill",
                    LastName = "Billington",
                    Email = "BBillington",
                    PasswordHash = _passwordHash,
                    PasswordSalt = _passwordSalt
                },
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        [Theory]
        [InlineData("", "ValidPassword")]
        [InlineData("  ", "ValidPassword")]
        [InlineData(null, "ValidPassword")]
        [InlineData("ValidEmail", null)]
        [InlineData("ValidEmail", "")]
        [InlineData("ValidEmail", " ")]
        // Email exists
        [InlineData("FFrank", "ValidPassword")]
        public void Create_InvalidEmailPassword_UserCreationException(string email, string password)
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var user = new User()
                {
                    UserId = 1,
                    FirstName = "Ted",
                    LastName = "Teddington",
                    Email = email
                };
                var service = new UserService(ctx);

                string expectedMsg;
                if (string.IsNullOrWhiteSpace(password))
                    expectedMsg = "Password is required";
                else if (email == "FFrank")
                    expectedMsg = $"Email \"{email}\" is already taken";
                else
                    expectedMsg = "Email is required";

                Exception ex = Assert.Throws<UserCreationException>(() => service.Create(user, password));

                Assert.Equal(expectedMsg, ex.Message);
            }
        }

        [Fact]
        public void Create_ValidEmailPassword_Created()
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var user = new User()
                {
                    FirstName = "Ted",
                    LastName = "Teddington",
                    Email = "TTedington"
                };
                var service = new UserService(ctx);

                var result = service.Create(user, "ValidPassword");

                Assert.IsType<User>(result);
            }
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        //needs valid hash [InlineData("FFrank", "InvalidPassword")]
        [InlineData("InvalidEmail", "ValidPassword")]
        public void Authenticate_InvalidEmailPasswords_Null(string email, string password)
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var service = new UserService(ctx);

                var result =  service.Authenticate(email, password);

                Assert.Null(result);
            }
        }

        [Fact]
        public void Authenticate_ValidPasswordEmail_AutehenticatedUser()
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var service = new UserService(ctx);

                var result =  service.Authenticate("FFrank", "ValidPassword");

                Assert.IsType<User>(result);
            }
        }


        [Fact]
        public void Update_UpdatePassword_Updated()
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var user = new User()
                {
                    UserId = 1,
                };
                var service = new UserService(ctx);
                var preUpdateUser =ctx.Users
                    .Where(u => u.UserId == user.UserId).First();
                var expectedHash = preUpdateUser.PasswordHash;
                var expectedSalt = preUpdateUser.PasswordSalt;

                service.Update(user, "TestPassword");

                var postUpdateUser = ctx.Users
                    .Where(u => u.UserId == user.UserId).First();
                Assert.Equal(postUpdateUser.PasswordHash, preUpdateUser.PasswordHash);
                Assert.Equal(postUpdateUser.PasswordSalt, preUpdateUser.PasswordSalt);
            }
        }

        [Fact]
        public void Update_ValidUpdates_Updated()
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var user = new User()
                {
                    UserId = 1,
                    FirstName = "Test FirstName",
                    LastName = "Test LastName",
                    Email = "TestEmail",
                };
                var service = new UserService(ctx);

                service.Update(user, "TestPassword");

                var postUpdateUser = ctx.Users
                    .Where(u => u.UserId == user.UserId).First();
                Assert.Equal(postUpdateUser.Email, user.Email);
                Assert.Equal(postUpdateUser.FirstName, user.FirstName);
                Assert.Equal(postUpdateUser.LastName, user.LastName);
            }
        }

        [Fact]
        public void Update_BlankFields_NoUpdates()
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var user = new User()
                {
                    UserId = 1,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    Email = string.Empty,
                    PasswordHash = new byte[] { 0x20, 0x20 },
                    PasswordSalt = new byte[] { 0x20, 0x20 },
                };
                var service = new UserService(ctx);

                service.Update(user, "");

                var postUpdateUser = ctx.Users
                    .Where(u => u.UserId == user.UserId).First();
                Assert.NotEqual(postUpdateUser.Email, string.Empty);
                Assert.NotEqual(postUpdateUser.FirstName, string.Empty);
                Assert.NotEqual(postUpdateUser.LastName, string.Empty);
                Assert.NotEqual(postUpdateUser.PasswordHash, user.PasswordHash);
                Assert.NotEqual(postUpdateUser.PasswordSalt, user.PasswordSalt);
            }
        }

        [Fact]
        public void Update_NullFields_NoUpdates()
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var user = new User()
                {
                    UserId = 1,
                    FirstName = null,
                    LastName = null,
                    Email = null,
                    PasswordHash = new byte[] { 0x20, 0x20 },
                    PasswordSalt = new byte[] { 0x20, 0x20 },
                };
                var service = new UserService(ctx);

                service.Update(user, null);

                var postUpdateUser = ctx.Users
                    .Where(u => u.UserId == user.UserId).First();
                Assert.NotNull(postUpdateUser.Email);
                Assert.NotNull(postUpdateUser.FirstName);
                Assert.NotNull(postUpdateUser.LastName);
                Assert.NotEqual(postUpdateUser.PasswordHash, user.PasswordHash);
                Assert.NotEqual(postUpdateUser.PasswordSalt, user.PasswordSalt);
            }
        }

        [Fact]
        public void Update_InvalidUser_UserUpdateException()
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var user = new User()
                {
                    UserId = 0,
                };
                var service = new UserService(ctx);

                Exception ex = Assert.Throws<UserUpdateException>(() => service.Update(user));
                Assert.Equal("User not found", ex.Message);
            }
        }

        [Fact]
        public void Update_EmailTaken_UserUpdateException()
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var user = new User()
                {
                    UserId = 1,
                    Email = "BBillington",
                };
                var service = new UserService(ctx);

                Exception ex = Assert.Throws<UserUpdateException>(() => service.Update(user));
                Assert.Equal($"Email \"{user.Email}\" is already taken", ex.Message);
            }
        }

        [Fact]
        public async void GetByIdAsync_ValidId_User()
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var service = new UserService(ctx);

                var user = await service.GetByIdAsync(1);

                Assert.IsType<User>(user);
                Assert.Equal(1, user.UserId);
            }
        }

        [Fact]
        public async void GetByIdAsync_InvalidId_User()
        {
            using (var ctx = new TestDataContext())
            {
                SeedUsers(ctx);
                var service = new UserService(ctx);

                var user = await service.GetByIdAsync(0);

                Assert.Null(user);
            }
        }
    }
}
