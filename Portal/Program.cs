using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Impl;
using Infrastructure.Repos.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ApplicationDatabaseTGTG");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

var userConnectionString = builder.Configuration.GetConnectionString("SecurityDatabaseTGTG");
builder.Services.AddDbContext<SecurityDbContext>(options => options.UseSqlServer(userConnectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredLength = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<SecurityDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "AuthorizationCookieTGTG";
        config.LoginPath = "/Account/Login";
    });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("Student", policy => policy.RequireClaim("Role", "Student"));
    config.AddPolicy("CanteenEmployee", policy => policy.RequireClaim("Role", "CanteenEmployee"));
});

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
builder.Services.AddScoped<ICanteenEmployeeRepository, CanteenEmployeeRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICanteenService, CanteenService>();
builder.Services.AddScoped<ICanteenEmployeeService, CanteenEmployeeService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
