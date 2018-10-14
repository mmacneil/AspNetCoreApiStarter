using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Presenters;
using Xunit;

namespace Web.Api.UnitTests.Presenters
{
    public class LoginPresenterUnitTests
    {
        [Fact]
        public void Handle_GivenSuccessfulUseCaseResponse_SetsOKHttpStatusCode()
        {
            // arrange
            var presenter = new LoginPresenter();

            // act
            presenter.Handle(new LoginResponse(new AccessToken("", 0),"", true));

            // assert
            Assert.Equal((int)HttpStatusCode.OK, presenter.ContentResult.StatusCode);
        }

        [Fact]
        public void Handle_GivenSuccessfulUseCaseResponse_SetsAccessToken()
        {
            // arrange
            const string token = "777888AAABBB";
            var presenter = new LoginPresenter();

            // act
            presenter.Handle(new LoginResponse(new AccessToken(token, 0),"", true));

            // assert
            dynamic data = JsonConvert.DeserializeObject(presenter.ContentResult.Content);
            Assert.Equal(token, data.accessToken.token.Value);
        }

        [Fact]
        public void Handle_GivenFailedUseCaseResponse_SetsErrors()
        {
            // arrange
            var presenter = new LoginPresenter();

            // act
            presenter.Handle(new LoginResponse(new[] { new Error("", "Invalid username/password") }));

            // assert
            var data = JsonConvert.DeserializeObject<IEnumerable<Error>>(presenter.ContentResult.Content);
            Assert.Equal((int)HttpStatusCode.Unauthorized, presenter.ContentResult.StatusCode);
            Assert.Equal("Invalid username/password", data.First().Description);
        }
    }
}
