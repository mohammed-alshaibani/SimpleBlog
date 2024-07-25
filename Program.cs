using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Hubs;
using SimpleBlog.Models;
using SimpleBlog.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbContextBlog>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("firstConnection"))
    );
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DbContextBlog>().AddDefaultTokenProviders();


builder.Services.AddScoped <IRepositiry<Post> ,GenericRepositiry<Post>>();

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<NotificationHub>("/notificationHub");
app.Run();
