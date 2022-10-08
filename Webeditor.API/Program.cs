using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Webeditor.Infra;
using Webeditor.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddInfrastructure();
builder.AddAuthentication();

builder.Services.AddControllers().AddJsonOptions(x =>
  x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
  {
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
  }));

builder.Services.AddHealthChecks();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
  option.SwaggerDoc("v1", new OpenApiInfo { Title = "WEBEditor 2 API", Version = "v1" });
  option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "Bearer"
  });
  option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
  });
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions()
{
  FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"UploadedFiles")),
  RequestPath = new PathString(""),
  ServeUnknownFileTypes = true,
});

app.UseCors("corsapp");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
