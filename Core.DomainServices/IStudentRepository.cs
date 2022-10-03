namespace Core.DomainServices
{
    public interface IStudentRepository

    {
        Task AddStudentAsync(Student student);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
    }
}