
namespace Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Canteen> Canteens { get; set; } = null!;
        public DbSet<Package> Packages { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<CanteenEmployee> CanteenEmployees { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new Student[]
                {
                  new Student() {StudentId = 1, FirstName = "Mike", LastName = "van der Caaij", DateOfBirth = new DateTime(2000, 7, 18), StudentNumber = 2184147, EmailAddress = "m.vandercaaij@student.avans.nl", StudyCity = CityEnum.Breda, PhoneNumber = "0612345678"}
                });
        }
    }
}