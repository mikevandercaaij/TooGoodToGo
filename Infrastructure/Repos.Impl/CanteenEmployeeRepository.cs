namespace Infrastructure.Repos.Impl
{
    public class CanteenEmployeeRepository : ICanteenEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public CanteenEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CanteenEmployee?> GetCanteenEmployeeByIdAsync(string id) => await _context.CanteenEmployees.Where(c => c.EmployeeId == id).FirstOrDefaultAsync();
        public async Task AddCanteenEmployeeAsync(CanteenEmployee canteenEmployee)
        {
            await _context.CanteenEmployees.AddAsync(canteenEmployee);
            await _context.SaveChangesAsync();
        }
    }
}