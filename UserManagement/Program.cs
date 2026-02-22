using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using UserManagement.Filters;
using UserManagement.Middleware;
using UserManagement.Model.Data;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Bind JwtSettings (Options Pattern)
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

// Swagger
builder.Services.AddEndpointsApiExplorer();

const string securitySchemeName = "Bearer";

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UserManagement API",
        Version = "v1"
    });


    options.AddSecurityDefinition(securitySchemeName, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Enter your JWT token"
    });

    // .NET 10+ AddSecurityRequirement pattern
    //Faced issue here but resolved it somehow.!
    options.AddSecurityRequirement(doc =>
    {
        var scheme = new OpenApiSecuritySchemeReference("Bearer");

        return new OpenApiSecurityRequirement
        {
            [scheme] = new List<string>()
        };
    });
});

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddScoped<CustomActionFilter>();
builder.Services.AddScoped<CustomExceptionFilter>();
builder.Services.AddScoped<CustomAuthorizationFilter>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagement API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// Ensure auth middleware runs before protected endpoints
app.UseAuthentication();
app.UseAuthorization();

// Register custom middleware after authentication/authorization if it depends on the user,
// or before if it only needs to catch all exceptions. Keep it single-invocation.
app.UseMiddleware<PracticeMiddleware>();

app.MapControllers();

app.Run();