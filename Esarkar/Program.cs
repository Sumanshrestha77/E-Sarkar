using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Esarkar.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MeroDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MeroDbContext") ?? throw new InvalidOperationException("Connection string 'MeroDbContext' not found.")));
var connectionString = builder.Configuration.GetConnectionString("EsarkarDbContextConnection") ?? throw new InvalidOperationException("Connection string 'EsarkarDbContextConnection' not found.");

builder.Services.AddDbContext<EsarkarDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<EsarkarUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<EsarkarDbContext>();

// Add services to the container.
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
