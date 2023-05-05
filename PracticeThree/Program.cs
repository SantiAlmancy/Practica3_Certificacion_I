using UPB.PracticeThree.Middlewares;
using UPB.CoreLogic.Managers;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

IConfiguration Configuration = configurationBuilder.Build();
string siteTitle = Configuration.GetSection("Title").Value;

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = siteTitle
    });
});

Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddTransient<PatientManager>(sp => new PatientManager(Configuration.GetSection("Location").Value));

var app = builder.Build();

// Configure the HTTP request pipeline. FOR LATER
app.UseGlobalExceptionHandler(Log.Logger);
// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();
// app.UseCors();
// app.UseAuthentication();
// app.UseAuthorization();
/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
