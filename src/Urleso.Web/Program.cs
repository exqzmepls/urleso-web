using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Urleso.Api.Client;
using Urleso.Web.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<Routes>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;
services.AddMudServices();

services.AddUrlesoApiClient(new Uri(builder.HostEnvironment.BaseAddress));

var host = builder.Build();

await host.RunAsync();
