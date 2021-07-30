using Cafe.Data;
using System;
using Xunit;

namespace Cafe.Tests
{
    public class LoginControllerTest
    {
        [Fact]
        public void FailTest()
        {
            var loginController = new LoginController<TestDbContext>(Cafe.Models.AccessLevel.Admin);
            Assert.Equal(4, loginController.LoginCounter);
            Assert.Equal(1,loginController.Users.Length);
            Assert.Equal(false, loginController.TryLogin(1, "12"));
            Assert.Equal(3,loginController.LoginCounter);
            Assert.Equal(true, loginController.TryLogin(1, "12345"));
            Assert.Equal(4, loginController.LoginCounter);
            Assert.Equal(false, loginController.TryLogin(1, "12"));
            Assert.Equal(false, loginController.TryLogin(1, "12"));
            Assert.Equal(false, loginController.TryLogin(1, "12"));
            Assert.Equal(false, loginController.TryLogin(1, "12"));
            Assert.Equal(0, loginController.LoginCounter);
            Assert.Equal(false, loginController.TryLogin(1, "12345"));
            Assert.Equal(0, loginController.LoginCounter);

        }
    }
}
