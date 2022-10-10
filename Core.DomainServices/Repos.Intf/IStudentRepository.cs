namespace Core.DomainServices.Repos.Intf
{
    public interface IStudentRepository

    {
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
    }
}