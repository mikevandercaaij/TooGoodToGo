
using Infrastructure.Contexts;

namespace Infrastructure.Repos.Impl
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Student> GetStudentByIdAsync(int id) => await _context.Students.FindAsync(id);
        public async Task AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }
    }
}
