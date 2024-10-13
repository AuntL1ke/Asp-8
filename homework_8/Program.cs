using DataAccess.Data;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using homework_8.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CarDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("CarDbContext")));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDefaultIdentity<User>(options=>options.SignIn.RequireConfirmedAccount=true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarDbContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SessionData>();



var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;
    Seeder.SeedRoles(serviceProvider).Wait();
    Seeder.SeedAdmin(serviceProvider).Wait();
}

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
app.UseSession();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
