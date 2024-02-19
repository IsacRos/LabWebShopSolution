using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

await builder.Build().RunAsync();

builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredLocalStorage();