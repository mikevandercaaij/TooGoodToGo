
using Core.Domain.Entities;
using System.Net;
using System.Xml.Linq;

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
                    Name = "Appel",
                    ContainsAlcohol = false,
                    Picture = new WebClient().DownloadData("https://appleridgeorchards.com/wp-content/uploads/2021/05/Apple-Picking-Icon-1-400x400-1.png")
                },
                new Product()
                {
                    ProductId = 2,
                    Name = "Bier",
                    ContainsAlcohol = true,
                    Picture = new WebClient().DownloadData("https://www.arjenverhuurt.nl/wp-content/uploads/rentman/jupiler-krat-24-x-25-cl-19625.jpg")
                },
                new Product()
                {
                    ProductId = 3,
                    Name = "Pakje melk",
                    ContainsAlcohol = false,
                    Picture = new WebClient().DownloadData("https://cdn1.sph.harvard.edu/wp-content/uploads/sites/30/2012/09/milk.jpg")
                },
                new Product()
                {
                    ProductId = 4,
                    Name = "Eiersalade",
                    ContainsAlcohol = false,
                    Picture = new WebClient().DownloadData("https://therecipecritic.com/wp-content/uploads/2019/02/besteggsalad-500x500.jpg")
                },
                new Product()
                {
                    ProductId = 5,
                    Name = "Broodje gezond",
                    ContainsAlcohol = false,
                    Picture = new WebClient().DownloadData("https://www.bakkerbart.nl/media/catalog/product/cache/afb382f2589f4527c76899f7685dfe75/b/a/bartje_gezond_1.jpg")
                },
                new Product()
                {
                    ProductId = 6,
                    Name = "Blikje Cola",
                    ContainsAlcohol = false,
                    Picture = new WebClient().DownloadData("https://www.shopbijalbatros.nl/media/catalog/product/cache/cd55d730358cf47f86f915a351923ecb/2/0/200.000343-voorkant.jpg_1.jpg")
                },
                new Product()
                {
                    ProductId = 7,
                    Name = "Croissant",
                    ContainsAlcohol = false,
                    Picture = new WebClient().DownloadData("https://www.okokorecepten.nl/i/recepten/kookboeken/2009/kook-ook-brood/zelfgebakken-croissants-500.jpg")
                },
                new Product()
                {
                    ProductId = 8,
                    Name = "Appelmoes",
                    ContainsAlcohol = false,
                    Picture = new WebClient().DownloadData("https://ilovefoodwine.nl/content/uploads/2021/03/shutterstock_177416972-500x375.jpg")
                },
                new Product()
                {
                    ProductId = 9,
                    Name = "Wijn",
                    ContainsAlcohol = true,
                    Picture = new WebClient().DownloadData("https://cdn.webshopapp.com/shops/29951/files/378792565/image.jpg")
                },
                new Product()
                {
                    ProductId = 10,
                    Name = "Koffie",
                    ContainsAlcohol = false,
                    Picture = new WebClient().DownloadData("https://image.gezondheid.be/XTRA/123m-koffie-espresso-27-8.jpg")
                },
            });
        }
    }
}