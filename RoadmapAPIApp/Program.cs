using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using RoadmapRepository.Data;
using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUserData, UserData>();

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

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserModel>());

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
