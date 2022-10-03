using Infrastructure.Contexts;

namespace Infrastructure.Repos.Impl
{
    public class PackageRepository : IPackageRepository
    {
        private readonly ApplicationDbContext _context;

        public PackageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Package> GetPackageByIdAsync(int id) => await _context.Packages.FindAsync(id);

        public async Task<IEnumerable<Package>> GetAllPackagesAsync() => await _context.Packages.ToListAsync();
        public async Task AddPackageAsync(Package package)
        {
            await _context.Packages.AddAsync(package);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatePackageAsync(Package package)
        {
            _context.Packages.Update(package);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePackageAsync(int id)
        {
            var package = _context.Packages.Find(id);

            if (package != null)
            {
                _context.Packages.Remove(package);
                await _context.SaveChangesAsync();
            }
        }
    }
}