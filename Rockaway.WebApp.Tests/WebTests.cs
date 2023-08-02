using AngleSharp;
using Newtonsoft.Json;
using Rockaway.WebApp.Data.Entities;
using Shouldly;

namespace Rockaway.WebApp.Tests;

public class WebTests {
	[Fact]
	public async Task GET_Root_Returns_OK() {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var response = await client.GetAsync("/status");
		response.EnsureSuccessStatusCode();
	}

	[Theory]
	[InlineData("/artists")]
	[InlineData("/venues")]
	public async Task Pages_Use_Correct_Layouts(string path) {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var response = await client.GetAsync(path);
		var html = await response.Content.ReadAsStringAsync();
		var angleSharp = BrowsingContext.New(Configuration.Default);
		var dom = await angleSharp.OpenAsync(req => req.Content(html));
		dom.QuerySelector("header").ShouldNotBeNull();
		dom.QuerySelector("footer").ShouldNotBeNull();
	}


	[Fact]
	public async Task GET_Artists_Returns_OK() {
		// Arrange
		var factory = new TestFactory();
		var artist = new Artist { Name = "Test Artists" }; 
		factory.DbContext.Artists.Add(artist);
		await factory.DbContext.SaveChangesAsync();

		// Act
		var client = factory.CreateClient();
		var response = await client.GetAsync("/artists");
		response.EnsureSuccessStatusCode();

		//// Assert
		//var json = await response.Content.ReadAsStringAsync();
		//var data = JsonConvert.DeserializeObject<List<Artist>>(json);
		//var result = data.Single();
		//result.Name.ShouldBe("Test Artists");
	}
}