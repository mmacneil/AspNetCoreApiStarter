using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.Api.Core.Dto.UseCaseRequests;
using Xunit;

namespace Web.Api.IntegrationTests.Controllers
{
    public class AccountsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AccountsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanRegisterUserWithValidAccountDetails()
        {
            var httpResponse = await _client.PostAsync("/api/accounts", new StringContent(JsonConvert.SerializeObject(new RegisterUserRequest("John", "Doe", "jdoe@gmail.com", "johndoe", "Pa$$word1")), Encoding.UTF8, "application/json"));
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            dynamic result = JObject.Parse(stringResponse);
            Assert.True((bool) result.success);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CantRegisterUserWithInvalidAccountDetails()
        {
            var httpResponse = await _client.PostAsync("/api/accounts", new StringContent(JsonConvert.SerializeObject(new RegisterUserRequest("John", "Doe", "", "johndoe", "Pa$$word1")), Encoding.UTF8, "application/json"));
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Contains("'Email' is not a valid email address.", stringResponse);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }
    }
}

 

