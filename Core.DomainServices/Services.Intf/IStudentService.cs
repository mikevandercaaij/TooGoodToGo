namespace Core.DomainServices.Services.Intf
{
    public interface IStudentService
    {
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
    }
}
