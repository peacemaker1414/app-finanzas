using Finanzas_Personales_App.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
builder.Services.AddDbContext<FinanzasDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSession();


// Configuración específica para Render
builder.WebHost.ConfigureKestrel(serverOptions => {
    serverOptions.ListenAnyIP(Int32.Parse(Environment.GetEnvironmentVariable("PORT") ?? "5000"));
});

// Configuración de autenticación CORREGIDA
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options => {
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
})
.AddGoogle(options => {
    options.ClientId = builder.Configuration["GOOGLE_CLIENT_ID"]; 
    options.ClientSecret = builder.Configuration["GOOGLE_CLIENT_SECRET"];
    options.CallbackPath = "/signin-google";
    options.AuthorizationEndpoint += "?prompt=consent&access_type=offline";
});

var app = builder.Build();

// Solo redirección HTTPS si no es Render
if (!app.Environment.IsEnvironment("Render"))
{
    app.UseHttpsRedirection();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
