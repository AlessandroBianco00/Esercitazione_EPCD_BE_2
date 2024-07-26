using HotelWebApp;
using HotelWebApp.Interfaces;
using HotelWebApp.Services;
using HotelWebApp.Services.Dao;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        // pagina alla quale l'utente sarà indirizzato se non è stato già riconosciuto
        opt.LoginPath = "/Account/AuthPage";
    });

builder.Services
    .AddAuthorization(opt => {
        opt.AddPolicy(Policies.LoggedIn, cfg => cfg.RequireAuthenticatedUser());
        opt.AddPolicy(Policies.IsAdmin, cfg => cfg.RequireRole("Admin"));
        opt.AddPolicy(Policies.IsBaseUser, cfg => cfg.RequireRole("User"));
    });

builder.Services
    .RegisterDAOs()
    .AddScoped<DbContext>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<IReservationQueryService, ReservationQueryService>()
    .AddScoped<IPasswordEncoder, PasswordEncoder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
