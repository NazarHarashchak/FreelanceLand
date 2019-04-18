using Backend;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Services;
using FreelanceLand.Models;
using MimeKit;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Xunit;

namespace Tests
{
    public class EmailServiceTests
    {
        [Theory]
        [InlineData("bekeguxuli@businesssource.net", "Test subject", null)]
        [InlineData("bekeguxuli@businesssource.net", null, "Test msg")]
        [InlineData(null, "Test subject", "Test msg")]
        public void SendEmail_ArgumentsNull_ThrowsArgumentNullExceptions(string email, string subj, string msg)
        {
            // Arrange
            var target = new EmailService();
            // Act
            Action act = () => target.SendEmailAsync(email, subj, msg);
            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }


        [Fact]
        public void SendEmail_ArgumentsNotNull_SendTextOnEmail()
        {
            // Arrange
            var target = new EmailService();
            // Act
            target.SendEmailAsync("bekeguxuli@businesssource.net", "Test subject", "Test msg");
            // Assert
            
        }
    }
}
