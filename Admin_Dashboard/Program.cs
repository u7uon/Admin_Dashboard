using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Admin_Dashboard;
using Admin_Dashboard.Service;
using Blazored.Toast;
using Microsoft.Extensions.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddBlazoredToast();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7081") });
builder.Services.AddScoped<HandleRequest>();
builder.Services.AddHttpClient("AuthHttpClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7081");
}).AddHttpMessageHandler<HandleRequest>();

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<BrandService>();
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();
