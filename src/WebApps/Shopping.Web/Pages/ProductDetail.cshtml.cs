namespace Shopping.Web.Pages
{
    public class ProductDetailModel (ICatalogService catalogService,IBasketService basketService, ILogger<ProductDetailModel> logger)
        : PageModel
    {
        public ProductModel Product { get; set; } = default!;

        [BindProperty]
        public int Quantity { get; set; } = default!;

        [BindProperty]
        public string Color { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            logger.LogInformation("ProductDetail page visited for product {ProductId}", productId);
            var response = await catalogService.GetProduct(productId);
            if (response.Product == null)
            {
                return NotFound();
            }
            Product = response.Product;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            logger.LogInformation("Adding product {ProductId} to cart with quantity {Quantity} and color {Color}", productId, Quantity, Color);
            var productResponse = await catalogService.GetProduct(productId);
            if (productResponse.Product == null)
            {
                return NotFound();
            }
            var basket = await basketService.LoadUserBasket();
            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productResponse.Product.Id,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = Quantity,
                Color = Color
            });
            await basketService.StoreBasket(new StoreBasketRequest(basket));
            return RedirectToPage("Cart");
        }
    }
}
