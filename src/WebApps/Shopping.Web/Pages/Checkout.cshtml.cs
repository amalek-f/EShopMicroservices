namespace Shopping.Web.Pages
{
    public class CheckoutModel(IBasketService basketService, ILogger<CheckoutModel> logger)
        : PageModel
    {
        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = default!;

        public ShoppingCartModel Cart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Cart = await basketService.LoadUserBasket();
                Order = new BasketCheckoutModel
                {
                    UserName = Cart.UserName,
                    TotalPrice = Cart.TotalPrice
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading user basket");
                return RedirectToPage("/Error");
            }
            return Page();
        }

       
        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            logger.LogInformation("Checkout button clicked");

            Cart = await basketService.LoadUserBasket();

            if (!ModelState.IsValid)
            {
                //logger.LogWarning("Invalid model state");
                return Page();
            }
            // assuming customerId is passed in from the UI authentication user amalek
            Order.CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
            Order.UserName = Cart.UserName;
            Order.TotalPrice = Cart.TotalPrice;
            //try
            //{
            //    var response = await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));
            //    if (response.IsSuccess)
            //    {
            //        logger.LogInformation("Checkout successful");
            //        return RedirectToPage("Confirmation", "OrderSubmitted");
            //    }
            //    else
            //    {
            //        logger.LogWarning("Checkout failed");
            //        ModelState.AddModelError(string.Empty, "Checkout failed. Please try again.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError(ex, "Error during checkout");
            //    ModelState.AddModelError(string.Empty, "An error occurred while processing your order. Please try again later.");
            //}

            await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
       
    }
}
