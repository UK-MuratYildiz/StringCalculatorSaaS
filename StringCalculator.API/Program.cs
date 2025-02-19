
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using StringCalculator.Application.Handlers;
using StringCalculator.Domain.Interfaces;
using StringCalculator.Infrastructure.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StringCalculatorAPI", Version = "v1" });
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddNumbersCommandHandler).Assembly));
builder.Services.AddScoped<IDelimiterParser, DelimiterParser>();
builder.Services.AddScoped<INumberValidator, NumberValidator>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StringCalculatorAPI v1"));
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();