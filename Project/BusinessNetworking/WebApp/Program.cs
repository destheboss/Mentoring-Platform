using BusinessLogicLayer.Managers;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddRazorPages();

// Register UserManager service.
// Register IPersonDataAccess and its implementation.
builder.Services.AddScoped<BusinessLogicLayer.Interfaces.IAuthenticationDataAccess, DataAccessLayer.Managers.AuthenticationDataManager>();
builder.Services.AddScoped<BusinessLogicLayer.Interfaces.IPersonDataAccess, DataAccessLayer.Managers.PersonDataManager>();
builder.Services.AddScoped<AuthenticationManager>();
builder.Services.AddScoped<HashingManager>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<PasswordStrengthChecker>();
builder.Services.AddScoped<BusinessLogicLayer.Interfaces.IMeetingDataAccess, DataAccessLayer.Managers.MeetingDataManager>();

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

app.MapRazorPages();

app.Run();