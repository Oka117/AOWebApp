using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AOWebApp.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AOWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AOWebAppContext") ??
    throw new InvalidOperationException("Connection string 'AOWebAppContext' not found.")));

// This builder object gets created at runtime. Use this object to add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
