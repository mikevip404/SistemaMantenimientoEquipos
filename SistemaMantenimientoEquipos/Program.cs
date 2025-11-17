using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaMantenimientoEquipos.Data;
using SistemaMantenimientoEquipos.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SistemaMantenimientoEquiposContextConnection") ?? throw new InvalidOperationException("Connection string 'SistemaMantenimientoEquiposContextConnection' not found.");

builder.Services.AddDbContext<SistemaMantenimientoEquiposContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SistemaMantenimientoEquiposContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Apply migrations and seed roles and assign Admin to first user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SistemaMantenimientoEquiposContext>();
    await context.Database.MigrateAsync();
    await SeedRolesAsync(services);
    await AssignAdminToFirstUserAsync(services);
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

async Task SeedRolesAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roleNames = { "Admin", "Coordinador", "Usuario" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

async Task AssignAdminToFirstUserAsync(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Check if Admin role exists
    if (!await roleManager.RoleExistsAsync("Admin"))
        return;

    // Check if any user has Admin role
    var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
    if (adminUsers.Any())
        return; // Admin already exists

    // Find the first user and assign Admin role
    var firstUser = userManager.Users.FirstOrDefault();
    if (firstUser != null)
    {
        await userManager.AddToRoleAsync(firstUser, "Admin");
    }
}
