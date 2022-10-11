namespace Core.DomainServices.Services.Intf
{
    public interface ICanteenService
    {
        Task<IEnumerable<Canteen>> GetAllCanteensAsync();
        Task<Canteen?> GetCanteenByIdAsync(int id);
    }
}
