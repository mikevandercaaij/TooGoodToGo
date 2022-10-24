namespace Infrastructure.Repos.Impl
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Student?> GetStudentByIdAsync(string id) => await _context.Students.Where(p => p.StudentNumber == id).FirstOrDefaultAsync();
        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }
    }
}