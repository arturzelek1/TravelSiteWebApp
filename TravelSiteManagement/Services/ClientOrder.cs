using TravelSiteWeb.ViewModel;
public interface IClientOrderService
{
    IEnumerable<ClientOrderViewModel> GetClientOrders(int ClientID, int ReservationID);
}