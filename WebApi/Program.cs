using static HotChocolate.SchemaBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("ApplicationDatabaseTGTG");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

var userConnectionString = builder.Configuration.GetConnectionString("SecurityDatabaseTGTG");
builder.Services.AddDbContext<SecurityDbContext>(options => options.UseSqlServer(userConnectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<SecurityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters.ValidateAudience = false;
    options.TokenValidationParameters.ValidateIssuer = false;
    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["BearerTokens:Key"]));
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

builder.Services.AddGraphQLServer().RegisterService<PackageRepository>().AddQueryType<Query>();


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.MapGraphQL();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();