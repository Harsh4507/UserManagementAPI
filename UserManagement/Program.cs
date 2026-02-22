using Microsoft.OpenApi;
using UserManagement.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register OpenAPI/Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UserManagement API",
        Version = "v1",
        Description = "Swagger UI for UserManagement API"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
    // Set RoutePrefix = string.Empty to serve the UI at app root: https://localhost:<port>/
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagement API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<PracticeMiddleware>();
app.CustomMiddleware();
app.UseAuthorization();

app.MapControllers();

//app.Use(async (context, next) => {
//    await context.Response.WriteAsync("Hey It's started.!");
//    await next(context);
//});


//app.Run(async (context) => {
//    await context.Response.WriteAsync("Rhino.!");
//});

app.Run();
