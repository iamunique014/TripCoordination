using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TripCoordination.Areas.Identity.Data;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Data;
using TripCoordination.Data.Repository;
using TripCoordination.Data.Services;


var builder = WebApplication.CreateBuilder(args);


// Configure EF Core with your SQL Server connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));


// Add ASP.NET Core Identity with default options and role support
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to true if email confirmation is needed
    // Configure password requirements if desired:
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLogging();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITripRepository, TripRepository>();
builder.Services.AddTransient<ITripParticipantRepository, TripParticipantRepository>();
builder.Services.AddTransient<ITownRepository, TownRepository>();
builder.Services.AddTransient<IResidenceRepository, ResidenceRepository>();
builder.Services.AddTransient<ITripDestinationTownRepository, TripDestinationTownRepository>();
builder.Services.AddTransient<IProfileRepository, ProfileRepository>();
builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<ITripRequestRepository, TripRequestRepository>();
builder.Services.AddTransient<IRouteRequestRepository, RouteRequestRepository>();
builder.Services.AddTransient<IRouteRepository, RouteRepository>();
builder.Services.AddTransient<IStudentDashboardRepository, StudentDashboardRepository>();

builder.Services.AddRazorPages();

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

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    await DBseeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
}


app.Run();