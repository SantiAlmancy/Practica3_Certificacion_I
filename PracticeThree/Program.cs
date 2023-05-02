using UPB.PracticeThree.Middlewares;
using UPB.CoreLogic.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<PatientManager>(sp => new PatientManager("C:\\Users\\PC\\Documents\\Certificacion I\\patients.xml"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline. FOR LATER
app.UseGlobalExceptionHandler();
// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();
// app.UseCors();
// app.UseAuthentication();
// app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
