using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class SecurityDbContext : DbContext
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {

        }
    }
}
