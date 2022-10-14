using Core.DomainServices.Repos.Intf;
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
        public async Task<Package?> GetPackageByIdAsync(int id) {
            
            return await _context.Packages
                .Where(p => p.PackageId == id)
                .Include(p => p.Products)
                .Include(p => p.Canteen)
                .Include(p => p.ReservedBy)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Package>> GetAllPackagesAsync() {
            return await _context.Packages
                .Include(p => p.Products)
                .Include(p => p.Canteen)
                .Include(p => p.ReservedBy)
                .ToListAsync();
        }
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