namespace Core.DomainServices.Services.Intf
{
    public interface IStudentService
    {
        Task<Student?> GetStudentByIdAsync(string id);
        Task<bool> AddStudentAsync(Student student);
    }
}