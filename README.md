# Sam.ReCaptcha - A Powerful CAPTCHA Solution for ASP.NET Core

[![Badge CI](https://github.com/samanazadi1996/Sam.ReCaptcha/workflows/.NET/badge.svg)](https://github.com/samanazadi1996/Sam.ReCaptcha/actions)
[![Badge NuGet](https://img.shields.io/nuget/vpre/Sam.ReCaptcha.svg)](https://www.nuget.org/packages/Sam.ReCaptcha)

`Sam.ReCaptcha` is a robust package for implementing CAPTCHA in ASP.NET Core applications. It supports various CAPTCHA techniques, including text-based and math-based CAPTCHAs, helping to prevent automated attacks and enhance application security.

---

## 🚀 Features
- **Multiple CAPTCHA Types**: Supports text and math CAPTCHAs
- **Easy Configuration**: Customize fonts, colors, text size, noise effects, and background
- **Expiration Time Management**: Control the validity duration of CAPTCHA
- **Case Sensitivity Control**: Configure case sensitivity for input validation
- **Customizable Visual Effects**: Rotate characters, add noise, and draw interference lines for enhanced security

---

## 📦 Installation

To add `Sam.ReCaptcha` to your project, run the following command:

```sh
 dotnet add package Sam.ReCaptcha
```

---

## 🔧 Usage

### 1. Register the CAPTCHA Service in `Program.cs`

First, configure `Sam.ReCaptcha` in `Program.cs`:

```csharp
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

var builder = WebApplication.CreateBuilder(args);

// Add required services
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();

// CAPTCHA Configuration
var captchaOptions = new CaptchaOptions
{
    CaptchaVariant = CaptchaTypes.DefaultCaptcha,
    MathCaptchaOptions = new MathCaptchaOptions
    {
        MinValue = 10,
        MaxValue = 20,
    },
    TextCaptchaOptions = new TextCaptchaOptions()
    {
        AllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
        CodeLength = 5,
        CaseSensitivityMode = StringComparison.OrdinalIgnoreCase,
    },
    Font = ReCaptchaFonts.Timetwist,
    ExpirationTimeInMinutes = 5,

    ImageOptions = new CaptchaImageOptions
    {
        MinFontSize = 30,
        MaxFontSize = 36,
        TextColor = Color.Black,
        ShadowColor = Color.Gray,
        LetterSpacing = 30,
        ShadowPositionOffset = new PointF(2, 2),
        Rotation = new RotationOptions
        {
            MinRotation = -10,
            MaxRotation = 10
        }
    },

    LineDistortion = new LineDistortionOptions
    {
        LineColor = Color.Gray,
        LineCount = 8
    },

    NoiseEffect = new NoiseEffectOptions
    {
        NoiseDensity = 200,
        NoiseColor = Color.Silver
    },

    GradientBackground = new GradientBackgroundOptions
    {
        GradientStops =
        [
            new ColorStop(0, Color.LightBlue),
            new ColorStop(0.5f, Color.White),
            new ColorStop(1, Color.LightGray)
        ]
    }
};

// Register CAPTCHA service
builder.Services.AddCaptcha(captchaOptions);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

---

### 2. Create an API for CAPTCHA Generation and Validation

In `CaptchaController.cs`, add the following endpoints for generating and validating CAPTCHAs:

```csharp
using Microsoft.AspNetCore.Mvc;
using Sam.ReCaptcha.Services;

namespace Sam.ReCaptcha.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CaptchaController(ICaptchaService captchaService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<FileContentResult> ReCaptcha(Guid id)
    {
        var bytes = await captchaService.GenerateCaptchaImageAsync(id);
        return File(bytes, "image/jpeg");
    }

    [HttpGet("{id:guid}")]
    public async Task<bool> Validate(Guid id, [FromQuery] string code)
    {
        return await captchaService.Validate(id, code);
    }
}
```

---

## 🛠 Frontend Integration

### 1. Get CAPTCHA Image
You can retrieve the CAPTCHA image in your frontend using a `GET` request:

```html
<img id="captchaImage" src="https://your-api-url.com/Captcha/ReCaptcha/{id}" />
```

### 2. Validate CAPTCHA Code
To verify the entered code, send a `GET` request:

```sh
GET https://your-api-url.com/Captcha/Validate/{id}?code=12345
```

---

## 📌 Conclusion

`Sam.ReCaptcha` is a powerful and flexible solution for adding CAPTCHA to ASP.NET Core applications. With extensive configuration options, visual effects, and security enhancements, this package provides an efficient way to prevent automated attacks and ensure the security of your application. 🚀

