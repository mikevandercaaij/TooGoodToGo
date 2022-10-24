namespace Core.DomainServices.Services.Intf
{
    public interface ICanteenService
    {
        Task<IEnumerable<Canteen>> GetAllCanteensAsync();
        Task<Canteen?> GetCanteenByLocationAsync(CanteenLocationEnum location);
    }
}
