namespace Infrastructure.Repos.Impl
{
    public class CanteenRepository : ICanteenRepository
    {
        private readonly ApplicationDbContext _context;
        public CanteenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Canteen?> GetCanteenByLocationAsync(CanteenLocationEnum location) => await _context.Canteens.Where(c => c.Location == location).FirstOrDefaultAsync();
        public async Task<IEnumerable<Canteen>> GetAllCanteensAsync() => await _context.Canteens.ToListAsync();
    }
}
