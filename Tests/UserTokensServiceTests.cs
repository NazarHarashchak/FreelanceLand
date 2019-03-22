using Backend.DTOs;
using Backend.Interfaces.ServiceInterfaces;
using Backend.Services;
using FreelanceLand.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using AutoMapper;

namespace Tests
{
    public class UserTokensServiceTests
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext db;
       
        [Fact]
        public void PassingTest()
        {
            UserAccountDTO user = new UserAccountDTO();
            
            IUserTokensService _userTokensService = new UserTokensService(_mapper,db);

            Assert.Equal("?", funct(1));

        }

        public string funct(int i)
        {
            if (i < 0)
                return "!";
            else
                return "?";
        }
        
    }
}
