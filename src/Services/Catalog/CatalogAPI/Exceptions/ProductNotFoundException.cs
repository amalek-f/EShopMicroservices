using BuildingBlocks.Exceptions;

namespace CatalogAPI.Exceptions;

public class ProductNotFoundException(Guid Id) : NotFoundException("Product", Id)
{
}
