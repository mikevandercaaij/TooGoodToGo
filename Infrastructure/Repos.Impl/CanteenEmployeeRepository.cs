using Infrastructure.Contexts;

namespace Infrastructure.Repos.Impl
{
    public class CanteenEmployeeRepository : ICanteenEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public CanteenEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int id) => await _context.CanteenEmployees.FindAsync(id);
        public async Task AddCanteenEmployeeAsync(CanteenEmployee canteenEmployee)
        {
            await _context.CanteenEmployees.AddAsync(canteenEmployee);
            await _context.SaveChangesAsync();
        }
    }
}