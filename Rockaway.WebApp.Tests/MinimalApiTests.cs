namespace Rockaway.WebApp.Tests;

public class MinimalApiTests {
	[Fact]
	public async Task StatusEndpointExists() {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var response = await client.GetAsync("/status");
		response.EnsureSuccessStatusCode();
	}
}
