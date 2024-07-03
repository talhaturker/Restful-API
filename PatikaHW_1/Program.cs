using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyRestApi.Services;
using MyRestApi.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SparePartService>();
builder.Services.AddControllers().AddNewtonsoftJson(); // Newtonsoft.Json desteği ekleniyor

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
