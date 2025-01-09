using HOTEL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HOTEL.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HOTEL2;Trusted_Connection=True;MultipleActiveResultSets=true"));
builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Create roles if they don't exist
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Customer" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Create Admin user if not exists
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
    string email = "Admin@Admin.com";
    string username = "ADMIN";
    string password = "admin123";

    var user = await userManager.FindByEmailAsync(email);
    if (user == null)
    {
        user = new Users
        {
            Email = email,
            UserName = username,
            FullName = "Admin"
        };

        // Create the admin user
        var createResult = await userManager.CreateAsync(user, password);
        if (createResult.Succeeded)
        {
            // Assign the user to the Admin role
            await userManager.AddToRoleAsync(user, "Admin");
        }
        else
        {
            // Log or handle any errors if creation fails
            foreach (var error in createResult.Errors)
            {
                Console.WriteLine($"Error creating user: {error.Description}");
            }
        }
    }

    // Assign the "Customer" role to all other users, excluding the admin user
    var allUsers = await userManager.Users.ToListAsync(); // Fetch all users asynchronously
    foreach (var existingUser in allUsers)
    {
        // Skip the Admin user and users that already have "Admin" or "Customer" role
        if (existingUser.UserName != username &&
            !await userManager.IsInRoleAsync(existingUser, "Admin") &&
            !await userManager.IsInRoleAsync(existingUser, "Customer"))
        {
            await userManager.AddToRoleAsync(existingUser, "Customer");
        }
    }
}

app.Run();
