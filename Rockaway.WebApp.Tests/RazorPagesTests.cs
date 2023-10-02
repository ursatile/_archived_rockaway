namespace Rockaway.WebApp.Tests;

public class RazorPagesTests {
	[Theory]
	[InlineData("/privacy")]
	[InlineData("/contact")]
	public async Task PageExists(string pageUrl) {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var response = await client.GetAsync(pageUrl);
		response.EnsureSuccessStatusCode();
	}
}