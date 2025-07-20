namespace CatalogAPI.Features.GetProductById;

public record GetProdutByIdResult(Product Product);
public record GetProductByIdQuery(Guid Id) : IQuery<GetProdutByIdResult>;
internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProdutByIdResult>
{
    public async Task<GetProdutByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        return product is null ? throw new ProductNotFoundException(query.Id) : new GetProdutByIdResult(product);
    }
}
