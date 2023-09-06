using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NodeServices.Classes;
using RoadmapRepository.Classes;
using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;
using RoadmapServices.Classes;
using RoadmapServices.Interfaces;
using RoadmapServices.Validators;
using RoadmapServices.Validators.Interfaces;
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
builder.Services.AddSingleton<IRoadmapVotesRepository, RoadmapVotesRepository>();
builder.Services.AddSingleton<IComentarioRepository, ComentarioRepository>();
builder.Services.AddSingleton<IComentarioVotesRepository, ComentarioVotesRepository>();
builder.Services.AddSingleton<IDenunciaRepository, DenunciaRepository>();

//Services
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IRoadmapClassService, RoadmapClassService>();
builder.Services.AddSingleton<IRoadmapVotesService, RoadmapVotesService>();
builder.Services.AddSingleton<INodeService, NodeService>();
builder.Services.AddSingleton<IComentarioService, ComentarioService>();
builder.Services.AddSingleton<IComentarioVotesService, ComentarioVotesService>();
builder.Services.AddSingleton<IDenunciaService, DenunciaService>();


//Validators / MessageHandler
builder.Services.AddSingleton<IMessageHandler, MessageHandler>();
builder.Services.AddSingleton<IValidator<UserModel>, UserValidator>();
builder.Services.AddSingleton<IValidator<RoadmapClassModel>, RoadmapValidator>();

builder.Services.AddEndpointsApiExplorer();

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
					.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
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
app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();