using FitnessTracker.Application.Authorization;
using FitnessTracker.Application.Features.Workouts.Commands;
using FitnessTracker.Contracts.Requests.Authorization;
using FitnessTracker.Contracts.Requests.Workout;
using FitnessTracker.Infrastructure.Persistance.Migrations;
using FitnessTracker.Interfaces;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(ServiceLifetime.Singleton);
builder.Services.AddSingleton<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddSingleton<IValidator<RegisterRequest>, RegisterRequestValidator>();
builder.Services.AddSingleton<IValidator<RecordWorkoutRequest>, RecordWorkoutRequestValidator>();
builder.Services.AddSingleton<IWorkoutService, WorkoutHandler>();
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