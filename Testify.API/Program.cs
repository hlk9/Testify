using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var jwtSecretKey = builder.Configuration["ApplicationSettings:JWT_Secret"]; // Get the secret key from appsettings.json
var key = Encoding.UTF8.GetBytes(jwtSecretKey);


builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // Compress responses even over HTTPS
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
    {
        "application/json",      // For JSON responses
        "application/xml",       // For XML responses
        "text/plain",            // If you're returning plain text
        "text/html",             // For any HTML content
        "application/javascript",
        // Add any other MIME types if needed
    });
});

// Optionally configure Brotli and Gzip compression providers
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest; // Change to Fastest if you want faster compression with lower ratio
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Optimal; // Use Optimal or Fastest based on your needs
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Set this to true in production if you're using HTTPS
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,  // You can adjust to true and set issuer if needed
        ValidateAudience = false, // You can adjust to true and set audience if needed
        ClockSkew = TimeSpan.Zero // Reduce or eliminate clock skew if needed
    };
});

var app = builder.Build();
app.UseResponseCompression();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
