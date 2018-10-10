using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Api.Controllers;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.Services;
using Web.Api.Core.UseCases;
using Web.Api.Presenters;
using Xunit;

namespace Web.Api.UnitTests.Controllers
{
    public class AuthControllerUnitTests
    {
        [Fact]
        public async void Login_Returns_Ok_When_Use_Case_Succeeds()
        {
            // arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.FindByName(It.IsAny<string>()))
                .Returns(Task.FromResult(new User("", "", "")));
            mockUserRepository
                .Setup(repo => repo.CheckPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory
                .Setup(repo => repo.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new Token("", "", 0)));

            // fakes
            var outputPort = new LoginPresenter();
            var useCase = new LoginUseCase(mockUserRepository.Object, mockJwtFactory.Object,null);

            var controller = new AuthController(useCase, outputPort);

            // act
            var result = await controller.Login(new Models.Request.LoginRequest { UserName = "userName", Password = "password" });

            // assert
            var statusCode = ((ContentResult)result).StatusCode;
            Assert.True(statusCode.HasValue && statusCode.Value == (int)HttpStatusCode.OK);
        }
    }
}
