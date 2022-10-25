using System.Net;

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
            modelBuilder.Entity<Canteen>().HasData(new Canteen[]
            {
                new Canteen()
                {
                    CanteenId = 1,
                    City = CityEnum.Breda,
                    Location = CanteenLocationEnum.LA,
                    ServesWarmMeals = false
                },
                new Canteen()
                {
                    CanteenId = 2,
                    City = CityEnum.Breda,
                    Location = CanteenLocationEnum.LD,
                    ServesWarmMeals = true
                },
                new Canteen()
                {
                    CanteenId = 3,
                    City = CityEnum.Breda,
                    Location = CanteenLocationEnum.HA,
                    ServesWarmMeals = true
                },
                new Canteen()
                {
                    CanteenId = 4,
                    City = CityEnum.Breda,
                    Location = CanteenLocationEnum.HB,
                    ServesWarmMeals = false
                },
                new Canteen()
                {
                    CanteenId = 5,
                    City = CityEnum.DenBosch,
                    Location = CanteenLocationEnum.HC,
                    ServesWarmMeals = false
                },
                new Canteen()
                {
                    CanteenId = 6,
                    City = CityEnum.Tilburg,
                    Location = CanteenLocationEnum.HD,
                    ServesWarmMeals = true
                }
            });

            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Broodje gezond",
                    ContainsAlcohol = false,
                    Picture = ReadStream(new HttpClient().GetStreamAsync("https://lunchroom28.nl/wp-content/uploads/2021/11/broodje-gezond.jpg").Result)

                },
                new Product()
                {
                    ProductId = 2,
                    Name = "Tosti",
                    ContainsAlcohol = false,
                    Picture = ReadStream(new HttpClient().GetStreamAsync("https://d1mlbwr23caxox.cloudfront.net/public/sites/default/files/recipe-images/italiaanse-tosti_1.jpg?VersionId=iTZSc.tVjriNS8ShQSvjoP0qtzYbvmoh").Result)
                },
                new Product()
                {
                    ProductId = 3,
                    Name = "Pizza",
                    ContainsAlcohol = false,
                    Picture = ReadStream(new HttpClient().GetStreamAsync("https://www.thespruceeats.com/thmb/t7yCtb3norW0m37YBfJOcS7Qd_w=/1000x1000/smart/filters:no_upscale()/prosciutto-pizza-4844358-hero-04-c0a6f73057ce4fed88982b75a5c2c8e1.jpg").Result)
                },
                new Product()
                {
                    ProductId = 4,
                    Name = "Bier",
                    ContainsAlcohol = true,
                    Picture = ReadStream(new HttpClient().GetStreamAsync("https://www.shape.com/thmb/o5FUbsfmic5_-U1ES1S51PaQpxM=/1000x1000/smart/filters:no_upscale():focal(499x0:501x2)/low-calorie-beers-44b18824f4fa4313af94afa2d7148180.png").Result)
                },
                new Product()
                {
                    ProductId = 5,
                    Name = "Blikje Cola",
                    ContainsAlcohol = false,
                    Picture = ReadStream(new HttpClient().GetStreamAsync("https://images.unsplash.com/photo-1514178255089-603d3a35b24a?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTB8fGNvY2ElMjBjb2xhJTIwY2FufGVufDB8fDB8fA%3D%3D&w=1000&q=80").Result)

                },
                new Product()
                {
                    ProductId = 6,
                    Name = "Flesje water",
                    ContainsAlcohol = false,
                    Picture = ReadStream(new HttpClient().GetStreamAsync("https://www.verywellfit.com/thmb/r23Iyl6xjjk9lnG9ydKB95y5HxQ=/1000x1000/smart/filters:no_upscale()/water-56bb320b3df78c0b136eb382.jpg").Result)
                },
                new Product()
                {
                    ProductId = 7,
                    Name = "Wijn",
                    ContainsAlcohol = true,
                    Picture = ReadStream(new HttpClient().GetStreamAsync("https://media-production.procook.io/c82518737b051eb60d4dedc31c0fd9af.jpg").Result)
                },
                new Product()
                {
                    ProductId = 8,
                    Name = "Koffie",
                    ContainsAlcohol = false,
                    Picture = ReadStream(new HttpClient().GetStreamAsync("https://drivu.s3.eu-west-1.amazonaws.com/uploads/menu_item/image/123678/thumb_Americano.jpg").Result)
                },
                new Product()
                {
                    ProductId = 9,
                    Name = "Falafel",
                    ContainsAlcohol = false,
                    Picture = ReadStream(new HttpClient().GetStreamAsync("https://www.cleaneatingmag.com/wp-content/uploads/2015/11/falafelpitasandwich.jpg?crop=1:1&width=1000").Result)
                },
                new Product()
                {
                    ProductId = 10,
                    Name = "Hamburger",
                    ContainsAlcohol = false,
                    Picture = ReadStream(new HttpClient().GetStreamAsync("https://montrealpizza.com.cy/wp-content/uploads/2021/11/Hamburger-Bu.jpg").Result)
                },
            });
        }

        public static byte[] ReadStream(Stream stream)
        {
            byte[] Buffer = new byte[16 * 1024];
            using (MemoryStream MS = new MemoryStream())
            {
                int Read;
                while ((Read = stream.Read(Buffer, 0, Buffer.Length)) > 0)
                {
                    MS.Write(Buffer, 0, Read);
                }
                return MS.ToArray();
            }
        }
    }
}