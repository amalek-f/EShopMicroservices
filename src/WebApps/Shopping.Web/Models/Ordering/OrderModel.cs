namespace Shopping.Web.Models.Ordering;

public record OrderModel(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressModel ShippingAddress,
    AddressModel BillingAddress,
    PaymentModel PaymentMethod,
    OrderStatus Status,
    List<OrderItemModel> OrderItems);

public record AddressModel(string FirstName, string LastName, string EmailAdress, string AddressLine, string Country, string State, string ZipCode);
public record PaymentModel();
public record OrderItemModel(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);
public enum OrderStatus
{
    Draft = 1,
    Pending = 2,
    Completed = 3,
    Cancelled = 4
}

// wrapper classes.
public record GetOrdersResponse(PaginatedResult<OrderModel> Orders);
public record GetOrdersByNameResponse(IEnumerable<OrderModel> Orders);
public record GetOrdersByCustomerResponse(IEnumerable<OrderModel> Orders);



