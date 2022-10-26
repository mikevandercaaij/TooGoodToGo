namespace Core.DomainServices.Services.Intf
{
    public interface ICanteenService
    {
        Task<Canteen?> GetCanteenByLocationAsync(CanteenLocationEnum location);
    }
}
