using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WALLE.Shared.Services;
using WALLE.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the WALLE.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
