using System.Text.Json;
using System.Text.Json.Serialization;
using FitnessTracker.Application.Features;
using FitnessTracker.Contracts.Requests.Authorization;
using FitnessTracker.Contracts.Requests.Workouts;
using FitnessTracker.Infrastructure.Persistance;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services;
using FluentValidation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin();
        corsBuilder.AllowAnyHeader();
        corsBuilder.AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); });
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(ServiceLifetime.Scoped);
builder.Services.AddMemoryCache();
builder.Services.AddHealthChecks();

builder.Services.AddSingleton<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddSingleton<IValidator<RegisterRequest>, RegisterRequestValidator>();
builder.Services.AddSingleton<IValidator<RecordWorkoutRequest>, RecordWorkoutRequestValidator>();
builder.Services.AddSingleton<IValidator<UpdateWorkoutRequest>, UpdateWorkoutRequestValidator>();
builder.Services.AddScoped<IWorkoutService, WorkoutHandler>();
builder.Services.AddScoped<IWorkoutNamesService, WorkoutNamesHandler>();
builder.Services.AddScoped<IWorkoutGraphDataService, WorkoutGraphDataHandler>();
builder.Services.AddScoped<IAuthorizationService, UserHandler>();
builder.Services.AddScoped<IExerciseService, ExerciseHandler>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseCors();
app.Run();