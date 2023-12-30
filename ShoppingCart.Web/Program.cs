using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ShoppingCart.Web;
using ShoppingCart.Web.Services;
using ShoppingCart.Web.Services.Contracts;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7288/") });
builder.Services.AddMudServices();

builder.Services.AddScoped<IProductService, ProductService>();

await builder.Build().RunAsync();
