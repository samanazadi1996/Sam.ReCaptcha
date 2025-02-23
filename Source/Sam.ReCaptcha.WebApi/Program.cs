using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();

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
        CaseSensitivityMode = StringComparison.OrdinalIgnoreCase
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

builder.Services.AddCaptcha(captchaOptions);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program
{
}
