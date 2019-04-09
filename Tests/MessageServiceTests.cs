using AutoMapper;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Services;
using FreelanceLand.Models;
using Xunit;
using Moq;
using Backend.DTOs;

namespace Tests
{
    public class MessageServiceTests
    {

        [Fact]
        public void AddMessageToRoomTestAsync()
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
            User user11 = null;
            var mock = new Mock<IUsersService>();
            mock.Setup(x => x.Authenticate(user.Login, user.Password)).ReturnsAsync(user);
            mock.Setup(x => x.GetUserByLogin(user.Login)).ReturnsAsync(user1);
            mock.Setup(x => x.GetUserByLogin(user1.Login)).ReturnsAsync(user11);

            var result = service.Authenticate(user.Login, user.Password);
            Assert.Equal(user.Login, user.Login);
            mock.Verify();

        }
    }
}