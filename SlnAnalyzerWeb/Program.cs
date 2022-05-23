using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using SlnAnalyzerWeb.Services;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args,
    ContentRootPath = null
});

builder.Services.AddSingleton<ISlnService, SlnService>();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    }));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin());

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "sln-analyzer-frontend", "build")
    ),
    RequestPath = ""
});

app.UseRouting();

app.MapControllers();

app.Run();