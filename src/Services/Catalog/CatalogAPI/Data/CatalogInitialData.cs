using Marten.Schema;

namespace CatalogAPI.Data;

public class CatalogInitialData : IInitialData
{
    public Task Populate(IDocumentStore store, CancellationToken cancellationToken)
    {
        using var session = store.LightweightSession();
        if (session.Query<Product>().Any())
        {
            return Task.CompletedTask;
        }
        // Preconfigured data
        // Marten UPSERT will carter for existing recors
        session.Store(GetPreconfiguredProducts());
        
        return session.SaveChangesAsync(cancellationToken);
    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return
        [
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "iPhone X",
                Description = "This is an iPhone X.",
                Price = 1999.00M,
                Category = ["c1", "c2" ],
                ImageFile = "Iphone-x.png"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "iPhone 16",
                Description = "This is an iPhone 16.",
                Price = 2999.00M,
                Category = ["c1", "c3"],
                ImageFile = "iPhone-16.png"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "iPhone 13 Pro Max",
                Description = "This is an iPhone 13 Pro Max.",
                Price = 3999.00M,
                Category = ["c2", "c3"],
                ImageFile = "iPhone-13-ProMax.png"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "iPad 4",
                Description = "This is an iPad 4.",
                Price = 4999.00M,
                Category = ["c1", "c2", "c3"],
                ImageFile = "iPad-4.png"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Mac book",
                Description = "This is a Mac book.",
                Price = 5999.00M,
                Category = ["c1", "c2"],
                ImageFile = "Mac-book.png"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "iPad 3",
                Description = "This is an iPad 3.",
                Price = 4999.00M,
                Category = ["c1", "c2", "c3"],
                ImageFile = "iPad-3.png"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "iPhone 12",
                Description = "This is an iPhone 12.",
                Price = 5999.00M,
                Category = ["c1", "c2"],
                ImageFile = "iPhone-12.png"
            }
        ];
    }
}
