
namespace Shopping.Web.Pages;

public class ProductListModel (IBasketService basketService, ICatalogService catalogService, ILogger<ProductListModel> logger)
    : PageModel
{
    public IEnumerable<string> CategoryList { get; private set; } = [];
    public IEnumerable<ProductModel> ProductList { get; private set; } = [];

    [BindProperty(SupportsGet = true)]
    public string SelectedCategory { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string categoryName)
    {
        var response = await catalogService.GetProducts();

        CategoryList = response.Products.SelectMany(p => p.Category)
            .Distinct()
            .OrderBy(c => c);

        logger.LogInformation("ProductList page visited with {CategoryCount} categories", CategoryList.Count());

        if (string.IsNullOrEmpty(categoryName))
        {
            ProductList = response.Products;
        }
        else
        {
            ProductList = response.Products.Where(p => p.Category.Contains(categoryName));
            SelectedCategory = categoryName;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        logger.LogInformation("Adding product {ProductId} to cart", productId);
        var productResponse = await catalogService.GetProduct(productId);
        var basket = await basketService.LoadUserBasket();
        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productResponse.Product.Id,
            ProductName = productResponse.Product.Name,
            Price = productResponse.Product.Price,
            Quantity = 1,
            Color = "Black"
        });
        await basketService.StoreBasket(new StoreBasketRequest(basket));
        return RedirectToPage("Cart");
    }
}
