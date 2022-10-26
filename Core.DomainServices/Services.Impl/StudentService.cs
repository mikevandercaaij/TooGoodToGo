using Core.Domain.Entities;

namespace Core.DomainServices.Services.Impl
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<bool> AddStudentAsync(Student student)
        {
            if (student == null)
                throw new Exception("Deze student bestaat niet!");

            await _studentRepository.AddStudentAsync(student);
            return true;
        }


        public async Task<Student?> GetStudentByIdAsync(string id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);

            if (student == null)
                throw new Exception("Deze student bestaat niet!");

            return student;
        }
    }
}
    