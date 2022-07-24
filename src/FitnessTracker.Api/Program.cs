using FluentValidation;
using FitnessTracker.Application;
using FitnessTracker.Contracts.Requests.Dtos.Authorization;
using FitnessTracker.Infrastructure.Persistance.Migrations;
using FitnessTracker.Interfaces;
using IAuthorizationHandler = FitnessTracker.Interfaces.IAuthorizationHandler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(ServiceLifetime.Singleton);
builder.Services.AddSingleton<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddSingleton<IValidator<RegisterRequest>, RegisterRequestValidator>();
builder.Services.AddSingleton<IAuthorizationHandler, AuthorizationHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
