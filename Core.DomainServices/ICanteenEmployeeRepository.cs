namespace Core.DomainServices
{
    public interface ICanteenEmployeeRepository
    {
        Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int id);

        Task AddCanteenEmployeeAsync(CanteenEmployee canteenEmployee);
    }
}