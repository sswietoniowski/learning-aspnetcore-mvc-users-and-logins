using learning_aspnetcore_mvc_users_and_logins.Configurations.Options;
using learning_aspnetcore_mvc_users_and_logins.DataAccess;
using learning_aspnetcore_mvc_users_and_logins.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<PasswordHasherOptions>(
    builder.Configuration.GetSection("PasswordHasherOptions"));
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IAccountManager, AccountManager>();

// More info here: https://learn.microsoft.com/en-us/aspnet/core/security/cookie-sharing?view=aspnetcore-6.0
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

//    options.LoginPath = $"/Account/Login";
//    options.LogoutPath = $"/Account/Logout";
//    //options.AccessDeniedPath = $"/Account/AccessDenied";
//});

// More info here: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.Cookie.Name = "__Host-my-app";
        o.Cookie.SameSite = SameSiteMode.Strict;
        o.LoginPath = "/Account/Login";
        o.LogoutPath = "/Account/Logout";
        //o.Events.OnRedirectToLogin = (context) =>
        //{
        //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //    return Task.CompletedTask;
        //};
    });

builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
