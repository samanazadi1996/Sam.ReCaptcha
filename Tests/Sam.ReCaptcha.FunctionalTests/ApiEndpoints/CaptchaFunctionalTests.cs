using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace Sam.ReCaptcha.FunctionalTests.ApiEndpoints;

[Collection("CaptchaFunctionalTests")]
public class CaptchaFunctionalTests(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient client = factory.CreateClient();

    [Fact]
    public async Task GetCaptchaImage_WithValidId_ShouldReturnImage()
    {
        // Arrange
        var id = Guid.NewGuid();
        var url = $"/Captcha/ReCaptcha/{id}";

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.Content.Headers.ContentType?.MediaType.ShouldBe("image/jpeg");
        (await response.Content.ReadAsByteArrayAsync()).Length.ShouldBeGreaterThan(0);
    }


    [Fact]
    public async Task ValidateCaptcha_WithInvalidCode_ShouldReturnFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var url = $"/Captcha/Validate/{id}?code=INVALID";

        // Act
        var response = await client.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<bool>();

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.ShouldBeFalse();
    }

}