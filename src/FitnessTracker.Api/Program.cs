using System.Text.Json;
using System.Text.Json.Serialization;
using FitnessTracker.Application.Authorization;
using FitnessTracker.Application.Features.Exercises;
using FitnessTracker.Application.Features.Workouts;
using FitnessTracker.Contracts.Requests.Authorization;
using FitnessTracker.Contracts.Requests.Workouts;
using FitnessTracker.Infrastructure.Persistance;
using FitnessTracker.Interfaces;
using FluentValidation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); });
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(ServiceLifetime.Singleton);
builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddSingleton<IValidator<RegisterRequest>, RegisterRequestValidator>();
builder.Services.AddSingleton<IValidator<RecordWorkoutRequest>, RecordWorkoutRequestValidator>();
builder.Services.AddSingleton<IValidator<UpdateWorkoutRequest>, UpdateWorkoutRequestValidator>();
builder.Services.AddSingleton<IWorkoutService, WorkoutHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, UserHandler>();
builder.Services.AddSingleton<IExerciseRepository, ExerciseRepository>();
builder.Services.AddSingleton<IExerciseService, ExerciseHandler>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors();
app.Run();