using Blazored.LocalStorage;
using ComentarioSite.Services.ComentarioVotes.Classes;
using ComentarioSite.Services.Voting.Classes.ComentarioVotingService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RoadmapSite;
using RoadmapSite.Services.Authentication.Classes;
using RoadmapSite.Services.Authentication.Interfaces;
using RoadmapSite.Services.Comentario.Classes;
using RoadmapSite.Services.Comentario.Interfaces;
using RoadmapSite.Services.ComentarioVotes.Interfaces;
using RoadmapSite.Services.Node.Classes;
using RoadmapSite.Services.Node.Interfaces;
using RoadmapSite.Services.Registration.Classes;
using RoadmapSite.Services.Registration.Interfaces;
using RoadmapSite.Services.Roadmap.Classes;
using RoadmapSite.Services.Roadmap.Interfaces;
using RoadmapSite.Services.RoadmapVotes.Classes;
using RoadmapSite.Services.RoadmapVotes.Interfaces;
using RoadmapSite.Services.User.Classes;
using RoadmapSite.Services.User.Interfaces;
using RoadmapSite.Services.Voting.Classes.RoadmapVotingService;
using RoadmapSite.Services.Voting.Interfaces.ComentarioVotingService;
using RoadmapSite.Services.Voting.Interfaces.RoadmapVotingService;

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
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
