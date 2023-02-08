using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RoadmapSite;
using RoadmapSite.Services.Authentication.Classes;
using RoadmapSite.Services.Authentication.Interfaces;
using RoadmapSite.Services.Registration.Classes;
using RoadmapSite.Services.Registration.Interfaces;
using RoadmapSite.Services.Roadmap.Classes;
using RoadmapSite.Services.Roadmap.Interfaces;
using RoadmapSite.Services.RoadmapVotes.Classes;
using RoadmapSite.Services.RoadmapVotes.Interfaces;
using RoadmapSite.Services.User.Classes;
using RoadmapSite.Services.User.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Dependency Injection
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IRoadmapService, RoadmapService>();
builder.Services.AddScoped<IRoadmapVotesService, RoadmapVotesService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
