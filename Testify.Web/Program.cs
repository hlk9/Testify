using Blazored.LocalStorage;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using MudBlazor;
using MudBlazor.Services;
using System.Globalization;
using Testify.DAL.Models;
using Testify.Web.Components;
using Testify.Web.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Testify.Web.Data.Commons;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices(

    config =>
    {
        config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

        config.SnackbarConfiguration.PreventDuplicates = false;
        config.SnackbarConfiguration.NewestOnTop = false;
        config.SnackbarConfiguration.ShowCloseIcon = true;
        config.SnackbarConfiguration.VisibleStateDuration = 10000;
        config.SnackbarConfiguration.HideTransitionDuration = 500;
        config.SnackbarConfiguration.ShowTransitionDuration = 500;
        config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    }

    );
string apiKey = builder.Configuration["SendGrid:APIKey"];
builder.Services.AddSingleton(sp => new CommonServices(apiKey));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<QuestionTypeService>();
builder.Services.AddScoped<QuestionLevelService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<AnswerService>();
builder.Services.AddScoped<LecturerService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<ClassService>();
builder.Services.AddScoped<AccessService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<LevelService>();
builder.Services.AddScoped<ExamScheduleService>();
builder.Services.AddScoped<ExamService>();
builder.Services.AddScoped<ExamDetailService>();
builder.Services.AddScoped<ExamDetailQuestionService>();
builder.Services.AddScoped<SubmissionServices>();
builder.Services.AddScoped<AnswerSubmissionServices>();
builder.Services.AddScoped<ClassUserServices>();
builder.Services.AddScoped<ClassExamScheduleService>();

// Add response compression services
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // Enable compression over HTTPS
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
    {
        "text/plain",
        "text/css",
        "application/javascript",
        "text/html",
        "application/json",
        "application/xml"
    });
});

// Optionally configure Brotli and Gzip compression levels
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});


//BlazoredLocalStorage
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri("http://localhost:7128/")
});

var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("vi") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("vi"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
