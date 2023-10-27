var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Register UserManager service.
// Register IPersonDataAccess and its implementation.
builder.Services.AddScoped<BusinessLogicLayer.Interfaces.IPersonDataAccess, DataAccessLayer.Managers.PersonDataManager>();
builder.Services.AddScoped<BusinessLogicLayer.Managers.UserManager>();
builder.Services.AddScoped<BusinessLogicLayer.Managers.HashingManager>();
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

app.UseAuthorization();

// Custom middleware to redirect to the Login page by default
app.MapGet("/", async (HttpContext context) =>
{
    context.Response.Redirect("/Login");
});

app.MapRazorPages();

app.Run();