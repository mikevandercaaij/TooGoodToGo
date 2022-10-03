namespace Core.DomainServices
{
    public interface ICanteenEmployeeRepository
    {
        Task<IEnumerable<CanteenEmployee>> GetAllCanteenEmployeesAsync();

        Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int id);

        Task AddCanteenEmployeeAsync(CanteenEmployee canteenEmployee);

        Task DeleteCanteenEmployeeAsync(int id);

        Task UpdateCanteenEmployeeAsync(CanteenEmployee canteenEmployee);

    }
}