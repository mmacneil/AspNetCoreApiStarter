using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Web.Api.IntegrationTests.Controllers
{
    public class AuthControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AuthControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanLoginWithValidCredentials()
        {
            var httpResponse = await _client.PostAsync("/api/auth/login", new StringContent(JsonConvert.SerializeObject(new Models.Request.LoginRequest{UserName = "mmacneil", Password = "Rhcp1234"}), Encoding.UTF8, "application/json"));
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            dynamic result = JObject.Parse(stringResponse);
            Assert.NotNull(result.accessToken.token);
            Assert.Equal(7200,(int)result.accessToken.expiresIn);
            Assert.NotNull(result.refreshToken);
        }

        [Fact]
        public async Task CantLoginWithInvalidCredentials()
        {
            var httpResponse = await _client.PostAsync("/api/auth/login", new StringContent(JsonConvert.SerializeObject(new Models.Request.LoginRequest { UserName = "unknown", Password = "Rhcp1234" }), Encoding.UTF8, "application/json"));
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Contains("Invalid username or password.", stringResponse);
            Assert.Equal(HttpStatusCode.Unauthorized, httpResponse.StatusCode);
        }
    }
}


