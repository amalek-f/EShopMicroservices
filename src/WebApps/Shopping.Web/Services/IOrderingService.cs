namespace Shopping.Web.Services;

public interface IOrderingService
{
    [Get("/ordering-service/orders?pageNumber={pageIndex}&pageSize={pageSize}\"")]
    Task<GetOrdersResponse> GetOrders(int pageIndex = 1, int? pageSize=10);
  
    [Get("/ordering-service/orders/{orderName}")]
    Task<GetOrdersByNameResponse> GetOrdersByName(string orderName);
   
    [Get("/ordering-service/orders/customer/{customerId}")]
    Task<GetOrdersByCustomerResponse> GetOrderByCustomer(Guid customerId);
}
