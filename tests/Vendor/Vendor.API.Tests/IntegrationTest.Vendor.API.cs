using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Vendor.API.Tests
{
  public class VendorAPIIntegrationTests
  {
    private readonly HttpClient _httpClient;
    private readonly TokenClient _tokenClient;

    public VendorAPIIntegrationTests()
    {
      var webhost = new WebHostBuilder()
      .UseUrls("http://*:8001")
      .UseStartup<Startup>();

      var server = new TestServer(webhost);
      _httpClient = server.CreateClient();

      var authority = Environment.GetEnvironmentVariable("IDENTITY_SERVER_AUTHORITY");
      var disco = DiscoveryClient.GetAsync(authority).Result;
      _tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
    }

    [Fact]
    public async void ShouldNotAllowAnonymousUser()
    {
      var result = await _httpClient.GetAsync("http://localhost:8001/api/values");
      Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [Fact]
    public async void ShouldReturnValuesForAuthenticatedUser()
    {
      var tokenResponse = _tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1").Result;
      _httpClient.SetBearerToken(tokenResponse.AccessToken);

      var result = await _httpClient.GetStringAsync("http://localhost:8001/api/values");
      Assert.Equal("[\"value1\",\"value2\"]", result);
    }
  }
}
