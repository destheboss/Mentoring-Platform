using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Managers;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Cookie expiration time
        options.SlidingExpiration = true; // Refresh the expiration time if the user is active
        options.Cookie.IsEssential = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Send the cookie only over HTTPS
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Login");
    options.Conventions.AllowAnonymousToPage("/Home");
    options.Conventions.AllowAnonymousToPage("/Events");
    options.Conventions.AllowAnonymousToPage("/Register");
});

// Register services.
builder.Services.AddScoped<IAuthenticationDataAccess, AuthenticationDataManager>();
builder.Services.AddScoped<IAnnouncementDataAccess, AnnouncementDataManager>();
builder.Services.AddScoped<IPersonDataAccess, PersonDataManager>();
builder.Services.AddScoped<IMeetingDataAccess, MeetingDataManager>();
builder.Services.AddScoped<IHashingManager, HashingManager>();
builder.Services.AddScoped<AuthenticationManager>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<ISuggestionDataAccess, SuggestionDataManager>();
builder.Services.AddScoped<ISuggestionManager, SuggestionManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapGet("/", async (HttpContext context) =>
{
    context.Response.Redirect("/Home");
});

app.MapRazorPages().RequireAuthorization();

app.Run();