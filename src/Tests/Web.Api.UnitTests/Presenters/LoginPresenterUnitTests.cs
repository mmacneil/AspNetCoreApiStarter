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
        public void Handle_GivenSuccessfulUseCaseResponse_ContainsOKHttpStatusCode()
        {
            // arrange
            var presenter = new LoginPresenter();

            // act
            presenter.Handle(new LoginResponse(new Token("", "", 0), true));

            // assert
            Assert.Equal((int)HttpStatusCode.OK, presenter.ContentResult.StatusCode);
        }

        [Fact]
        public void Handle_GivenSuccessfulUseCaseResponse_ContainsToken()
        {
            // arrange
            const string accessToken = "777888AAABBB";
            var presenter = new LoginPresenter();

            // act
            presenter.Handle(new LoginResponse(new Token("1", accessToken, 0), true));

            // assert
            dynamic data = JsonConvert.DeserializeObject(presenter.ContentResult.Content);
            Assert.Equal(accessToken, data.accessToken.Value);
        }

        [Fact]
        public void Handle_GivenFailedUseCaseResponse_ContainsErrors()
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
