using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RoadmapBlazor;
using RoadmapBlazor.Services.Authentication;
using RoadmapBlazor.Services.Comentario;
using RoadmapBlazor.Services.ComentarioVotes;
using RoadmapBlazor.Services.Denuncia;
using RoadmapBlazor.Services.Node;
using RoadmapBlazor.Services.Registration;
using RoadmapBlazor.Services.Roadmap;
using RoadmapBlazor.Services.RoadmapVotes;
using RoadmapBlazor.Services.User;
using RoadmapBlazor.Services.Voting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Dependency Injection
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IRoadmapService, RoadmapService>();
builder.Services.AddScoped<IRoadmapVotesService, RoadmapVotesService>();
builder.Services.AddScoped<IComentarioVotesService, ComentarioVotesService>();
builder.Services.AddScoped<INodeService, NodeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoadmapVotingService, RoadmapVotingService>();
builder.Services.AddScoped<IComentarioVotingService, ComentarioVotingService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<IDenunciaService, DenunciaService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
