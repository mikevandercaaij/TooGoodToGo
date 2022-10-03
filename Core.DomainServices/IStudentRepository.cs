namespace Core.DomainServices
{
    public interface IStudentRepository

    {
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudent(Student student);
    }
}