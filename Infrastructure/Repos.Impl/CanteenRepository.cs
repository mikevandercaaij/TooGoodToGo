using Infrastructure.Contexts;

namespace Infrastructure.Repos.Impl
{
    public class CanteenRepository : ICanteenRepository
    {
        private readonly ApplicationDbContext _context;
        public CanteenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Canteen> GetCanteenByIdAsync(int id) => await _context.Canteens.FindAsync(id);
        public async Task<IEnumerable<Canteen>> GetAllCanteensAsync() => await _context.Canteens.ToListAsync();
    }
}
