using System.Threading.Tasks;
using Moq;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.Services;
using Web.Api.Core.UseCases;
using Xunit;

namespace Web.Api.Core.UnitTests.UseCases
{
    public class LoginUseCaseUnitTests
    {
        [Fact]
        public async void Handle_GivenValidCredentials_ShouldReturnTrue()
        {
            // arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.FindByName(It.IsAny<string>())).Returns(Task.FromResult(new User("", "", "")));

            mockUserRepository.Setup(repo => repo.CheckPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(factory => factory.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new AccessToken("", 0)));

            var mockTokenFactory = new Mock<ITokenFactory>();
            mockTokenFactory.Verify(factory => factory.GenerateToken(32), Times.Never);

            var useCase = new LoginUseCase(mockUserRepository.Object, mockJwtFactory.Object, mockTokenFactory.Object);

            var mockOutputPort = new Mock<IOutputPort<LoginResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<LoginResponse>()));

            // act
            var response = await useCase.Handle(new LoginRequest("userName", "password","127.0.0.1"), mockOutputPort.Object);

            // assert
            Assert.True(response);
        }

        [Fact]
        public async void Handle_GivenIncompleteCredentials_ShouldReturnFalse()
        {
            // arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.FindByName(It.IsAny<string>())).Returns(Task.FromResult(new User("", "", "")));

            mockUserRepository.Setup(repo => repo.CheckPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(false));

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(factory => factory.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new AccessToken("", 0)));

            var mockTokenFactory = new Mock<ITokenFactory>();
            mockTokenFactory.Verify(factory => factory.GenerateToken(32), Times.Never);

            var useCase = new LoginUseCase(mockUserRepository.Object, mockJwtFactory.Object, mockTokenFactory.Object);

            var mockOutputPort = new Mock<IOutputPort<LoginResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<LoginResponse>()));

            // act
            var response = await useCase.Handle(new LoginRequest("", "password","127.0.0.1"), mockOutputPort.Object);

            // assert
            Assert.False(response);
        }


        [Fact]
        public async void Handle_GivenUnknownCredentials_ShouldReturnFalse()
        {
            // arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.FindByName(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            mockUserRepository.Setup(repo => repo.CheckPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(factory => factory.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new AccessToken("", 0)));

            var mockTokenFactory = new Mock<ITokenFactory>();
            mockTokenFactory.Verify(factory => factory.GenerateToken(32), Times.Never);

            var useCase = new LoginUseCase(mockUserRepository.Object, mockJwtFactory.Object, mockTokenFactory.Object);

            var mockOutputPort = new Mock<IOutputPort<LoginResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<LoginResponse>()));

            // act
            var response = await useCase.Handle(new LoginRequest("", "password","127.0.0.1"), mockOutputPort.Object);

            // assert
            Assert.False(response);
        }

        [Fact]
        public async void Handle_GivenInvalidPassword_ShouldReturnFalse()
        {
            // arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.FindByName(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            mockUserRepository.Setup(repo => repo.CheckPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(false));

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(factory => factory.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new AccessToken("", 0)));

            var mockTokenFactory = new Mock<ITokenFactory>();
            mockTokenFactory.Verify(factory => factory.GenerateToken(32), Times.Never);

            var useCase = new LoginUseCase(mockUserRepository.Object, mockJwtFactory.Object, mockTokenFactory.Object);

            var mockOutputPort = new Mock<IOutputPort<LoginResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<LoginResponse>()));

            // act
            var response = await useCase.Handle(new LoginRequest("", "password","127.0.0.1"), mockOutputPort.Object);

            // assert
            Assert.False(response);
        }
    }
}
