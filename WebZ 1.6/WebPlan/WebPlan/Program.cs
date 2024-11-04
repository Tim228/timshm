using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using WebPlan.DAL;
using WebPlan.DAL.Interfaces;
using WebPlan.Domain.Entity;
using WebPlan.DAL.Repositories;
using WebPlan.Service.Interfaces;
using WebPlan.Service.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBaseRepository<DataRecord>, DataRecordRepository>();

builder.Services.AddScoped<IDataRecordService,DataRecordService>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllersWithViews();
//аутентификация с помощью куки
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie(options=>
    {
        options.LoginPath = "/";
        options.AccessDeniedPath = "/accesdenied";
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
        options.Scope.Add("email");
        options.SaveTokens = true;
        options.ClaimActions.MapJsonKey("email", "email");
    });

builder.Services.AddAuthorization();

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
