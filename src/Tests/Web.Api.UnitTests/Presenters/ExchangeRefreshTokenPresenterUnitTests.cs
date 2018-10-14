using System.Net;
using Newtonsoft.Json;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Presenters;
using Xunit;

namespace Web.Api.UnitTests.Presenters
{
    public class ExchangeRefreshTokenPresenterUnitTests
    {
        [Fact]
        public void Handle_GivenSuccessfulUseCaseResponse_SetsAccessToken()
        {
            // arrange
            const string token = "777888AAABBB";
            var presenter = new ExchangeRefreshTokenPresenter();

            // act
            presenter.Handle(new ExchangeRefreshTokenResponse(new AccessToken(token, 0), "", true));

            // assert
            dynamic data = JsonConvert.DeserializeObject(presenter.ContentResult.Content);
            Assert.Equal(token, data.accessToken.token.Value);
        }

        [Fact]
        public void Handle_GivenSuccessfulUseCaseResponse_SetsRefreshToken()
        {
            // arrange
            const string token = "777888AAABBB";
            var presenter = new ExchangeRefreshTokenPresenter();

            // act
            presenter.Handle(new ExchangeRefreshTokenResponse(null, token, true));

            // assert
            dynamic data = JsonConvert.DeserializeObject(presenter.ContentResult.Content);
            Assert.Equal(token, data.refreshToken.Value);
        }

        [Fact]
        public void Handle_GivenFailedUseCaseResponse_SetsError()
        {
            // arrange
            var presenter = new ExchangeRefreshTokenPresenter();

            // act
            presenter.Handle(new ExchangeRefreshTokenResponse(false,"Invalid Token."));

            // assert
            var data = JsonConvert.DeserializeObject(presenter.ContentResult.Content);
            Assert.Equal((int)HttpStatusCode.BadRequest, presenter.ContentResult.StatusCode);
            Assert.Equal("Invalid Token.", data);
        }
    }
}
