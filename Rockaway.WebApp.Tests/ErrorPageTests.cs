using System.Net;
using Microsoft.AspNetCore.Hosting;
using Shouldly;

namespace Rockaway.WebApp.Tests;

public class ErrorPageTests {
	[Fact]
	public async Task App_Uses_Custom_Error_Page_In_Production_Mode() {
		var factory = new WebApplicationFactory<Program>()
			.WithWebHostBuilder(builder => builder.UseEnvironment("Production"));
		var client = factory.CreateClient();
		var response = await client.GetAsync("/exception");
		response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
		var content = await response.Content.ReadAsStringAsync();
		content.ShouldContain("Sorry, something went wrong.");
	}

	[Fact]
	public async Task App_Uses_Custom_Error_Page_In_Development_Mode() {
		var factory = new WebApplicationFactory<Program>()
			.WithWebHostBuilder(builder => builder.UseEnvironment("Development"));
		var client = factory.CreateClient();
		var response = await client.GetAsync("/exception");
		response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
		var content = await response.Content.ReadAsStringAsync();
		content.ShouldContain("ROCKAWAY_TEST_EXCEPTION");
	}
}
