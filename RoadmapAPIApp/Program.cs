using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using NodeServices.Classes;
using RoadmapAPIApp.Request;
using RoadmapRepository.Classes;
using RoadmapRepository.Interfaces;
using RoadmapRepository.SqlDataAccess;
using RoadmapServices.Classes;
using RoadmapServices.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Cors
builder.Services.AddCors(policy =>
{
	policy.AddPolicy("OpenCorsPolicy", opt =>
		opt
		.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod());
});

//Dependency Injection
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();

//Repositories
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IRoadmapClassRepository, RoadmapClassRepository>();
builder.Services.AddSingleton<INodeRepository, NodeRepository>();

//Services
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IRoadmapClassService, RoadmapClassService>();
builder.Services.AddSingleton<INodeService, NodeService>();


builder.Services.AddEndpointsApiExplorer();


//Fluent Validation
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserRequest>());

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//SwaggerGen
builder.Services.AddSwaggerGen(x =>
{
	x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the bearer scheme",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey
	});
	x.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{new OpenApiSecurityScheme{Reference = new OpenApiReference
		{
			Id = "Bearer",
			Type = ReferenceType.SecurityScheme
		}}, new List<string>()}
	});
});

//Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(
		options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
					.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
				ValidateIssuer = false,
				ValidateAudience = false
			};
		});

// Authorization
builder.Services.AddAuthorization(options =>
{
	options.FallbackPolicy = new AuthorizationPolicyBuilder()
	.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
	.RequireAuthenticatedUser()
	.Build();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");

app.MapControllers();

app.Run();
