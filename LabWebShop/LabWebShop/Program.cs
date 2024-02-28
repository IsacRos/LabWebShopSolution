using Blazored.LocalStorage;
using BookingSystem.Server.Data;
using LabWebShop.Components;
using LabWebShop.Data;
using LabWebShop.Models;
using LabWebShop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<ICheckOutService, CheckOutService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazorBootstrap();



var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddDbContext<WebShopDbContext>(options =>
    options.UseMongoDB(mongoDbSettings?.AtlasURI ?? "", mongoDbSettings?.DatabaseName ?? ""));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(mongoDbSettings?.AtlasURI ?? "", mongoDbSettings?.DatabaseName ?? "");

builder.Services.AddScoped<ICartService, CartService>();

builder.Services.Configure<IdentityOptions>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequiredUniqueChars = 2;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequiredLength = 4;
    o.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(o =>
{
    o.Cookie.HttpOnly = true;
    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    o.LoginPath = "/login-page";
    o.AccessDeniedPath = "/Identity/Account/AccessDenied";
    o.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(LabWebShop.Client._Imports).Assembly);

app.UseAuthentication();
app.UseAuthorization();

app.Run();
