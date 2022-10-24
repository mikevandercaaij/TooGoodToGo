namespace Core.DomainServices.Services.Impl
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task AddStudentAsync(Student student) => await _studentRepository.AddStudentAsync(student);
        public async Task<Student?> GetStudentByIdAsync(string id) => await _studentRepository.GetStudentByIdAsync(id);
    }
}
    