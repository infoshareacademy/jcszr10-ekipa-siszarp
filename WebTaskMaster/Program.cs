using Manage_tasks_Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebTaskMaster.Data;
 

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddIdentity<Company, IdentityRole>(options =>
{
    // Obowiazkowe potwierdzenie poczty na razie = false;
    options.SignIn.RequireConfirmedAccount = false;
    //Haslo powinno zawierac cyfry 
    options.Password.RequireDigit = true;
    //Haslo powinno zawierac minimum 8 symboli 
    options.Password.RequiredLength = 8;
    //Haslo powinno zawierac przynajmniej 1 wielka litere
    options.Password.RequireUppercase = true;
    //Ilosc prob wejsca do konta  (po 5 probach konto bedzie zablokowane)
    options.Lockout.MaxFailedAccessAttempts = 5;
    // Mowimy ze konto bedzie zablokowane w ciagu 5 minut
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //Email kazdego usera powinien byc unikatowy
    options.User.RequireUniqueEmail = true;


})
    // dodajemy entity schowek (mowimy "wykorzystaj baze danych dla identity)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    // mowimy identity ze checmy wykorzystywac role 
    .AddRoles<IdentityRole>();
     

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();
 



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

// podwlaczamy autentyfikacje i autoryzacje
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var pendingMigrations = applicationDbContext.Database.GetPendingMigrations().ToList();
    if (pendingMigrations.Any())
    {
        applicationDbContext.Database.Migrate();
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
