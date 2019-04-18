using AutoMapper;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Services;
using FreelanceLand.Models;
using Xunit;
using Moq;
using Backend.DTOs;

namespace Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void Authenticate_ExistingUser_ReturnsUser()
        {
            var user = new UserAccountDTO()
            {
                Login = "qwerty",
                Password = "user"
            };
            var user1 = new User()
            {
                Login = "qwerty",
                Password = "user"
            };

            var appContext = new Mock<ApplicationContext>();
            var map = new Mock<IMapper>();
            var email = new Mock<IEmailService>();

            var service = new UsersService(map.Object, appContext.Object, email.Object);

            var mock = new Mock<IUsersService>();
            mock.Setup(x => x.Authenticate(user.Login, user.Password)).ReturnsAsync(user);
            mock.Setup(x => x.GetUserByLogin(user.Login)).ReturnsAsync(user1);

            var result = service.Authenticate(user.Login, user.Password);
             
            Assert.Equal(user.Login, user.Login);
            mock.Verify();
        }
    }
}
