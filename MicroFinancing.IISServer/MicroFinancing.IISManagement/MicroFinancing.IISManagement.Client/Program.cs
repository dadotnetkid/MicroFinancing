using MicroFinancing.IISManagement.Client.Clients;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddRefitClient<IAppPoolClient>()
       .ConfigureHttpClient(c =>
       {
           var baseAddress = builder.HostEnvironment.BaseAddress;
           c.BaseAddress = new Uri(baseAddress);
       });

await builder.Build().RunAsync();
