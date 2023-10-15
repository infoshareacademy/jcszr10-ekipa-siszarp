using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebTaskMaster.Areas.Identity.Data;
using WebTaskMaster.Areas.Identity.Pages;
using WebTaskMaster.Data;




using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Manage_tasks_Database.Context;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataBaseContext>(options =>
options.UseSqlServer(connectionString));


builder.Services.AddAuthentication("MyCookieAuth")
	.AddCookie(options =>
	{
		options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
		options.SlidingExpiration = true;
		options.AccessDeniedPath = "/Account/Forbidden/";
		options.LoginPath = "/Account/Login";

	});




builder.Services.AddDatabaseDeveloperPageExceptionFilter();



// Add services to the container.
builder.Services
	.AddControllersWithViews()
	.AddRazorRuntimeCompilation();
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services
	.AddScoped<IUserService, UserService>()
	.AddScoped<ITeamService, TeamService>();

builder.Services.AddScoped<IAccountService, AccountService>();

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

using (var scope = app.Services.CreateScope())
{
	var autorizationDbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
	autorizationDbContext.Database.Migrate();
}

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
