namespace Ordering.Application.Features.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest)   
    : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedRsult<OrderDto> Orders);

