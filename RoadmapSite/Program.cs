using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RoadmapSite;
using RoadmapSite.Services.Authentication;
using RoadmapSite.Services.Comentario;
using RoadmapSite.Services.ComentarioVotes;
using RoadmapSite.Services.Denuncia;
using RoadmapSite.Services.Node;
using RoadmapSite.Services.Registration;
using RoadmapSite.Services.Roadmap;
using RoadmapSite.Services.RoadmapVotes;
using RoadmapSite.Services.User;
using RoadmapSite.Services.Voting;

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
