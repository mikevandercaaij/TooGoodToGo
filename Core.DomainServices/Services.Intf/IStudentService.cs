namespace Core.DomainServices.Services.Intf
{
    public interface IStudentService
    {
        Task<Student?> GetStudentByIdAsync(string id);
        Task AddStudentAsync(Student student);
    }
}
