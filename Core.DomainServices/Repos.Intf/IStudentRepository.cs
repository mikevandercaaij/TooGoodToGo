namespace Core.DomainServices.Repos.Intf
{
    public interface IStudentRepository

    {
        Task<Student?> GetStudentByIdAsync(string id);
        Task AddStudentAsync(Student student);
    }
}