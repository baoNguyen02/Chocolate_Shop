using Chocolate_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly Chocolate_ShopContext _context;

        public IndexModel(ILogger<IndexModel> logger, Chocolate_ShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public IList<Product> Product { get; set; }

        [BindProperty]
        public Models.Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("User") == null)
            {
                return Redirect("/Account/Login");
            }

            Account = System.Text.Json.JsonSerializer.Deserialize<Models.Account>(HttpContext.Session.GetString("User"));

            if (Account == null)
            {
                ViewData["msg"] = "You have not logged in yet.";
                return Page();
            }
            else
            {
                Account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == Account.AccountId);
            }

            Models.Product product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (product != null)
            {
                Order order = new Order
                {
                    ProductId = product.ProductId,
                    QuantityPerUnit = 1,
                    UnitPrice = product.Price,
                    AccountId = Account.AccountId,
                    OrderDate = DateTime.Now,
                    Type = 0
                };


                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            Product = await _context.Products.ToListAsync();
            return Page();
        }
    }
}
