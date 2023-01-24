using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using RoadmapAPIApp.Request;
using RoadmapRepository.Classes;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;
using RoadmapServices;
using RoadmapServices.User;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen(x =>
//{
//	x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//	{
//		Description = "JWT Authorization header using the bearer scheme",
//		Name = "Authorization",
//		In = ParameterLocation.Header,
//		Type = SecuritySchemeType.ApiKey
//	});
//	x.AddSecurityRequirement(new OpenApiSecurityRequirement
//	{
//		{new OpenApiSecurityScheme{Reference = new OpenApiReference
//		{
//			Id = "Bearer",
//			Type = ReferenceType.SecurityScheme
//		}}, new List<string>()}
//	});
//});

//Fluent Validation
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserModel>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RoadmapModel>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<NodeModel>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserRequest>());

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
//builder.Services.AddAuthorization(options =>
//{
//	options.FallbackPolicy = new AuthorizationPolicyBuilder()
//	.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
//	.RequireAuthenticatedUser()
//	.Build();
//});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure
app.UseSwagger();
app.UseSwaggerUI();

//app.UseAuthentication();
//app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
