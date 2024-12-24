using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages.Account.Orders
{
    public class PurchaseOrderModel : PageModel
    {
        private readonly Chocolate_Shop.Models.Chocolate_ShopContext _context;

        public PurchaseOrderModel(Chocolate_Shop.Models.Chocolate_ShopContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Models.Account Account { get; set; }
        public IList<Models.Order> Order { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
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
                Order = await _context.Orders.
                    Include(o => o.Account).
                    Include(o => o.Product).
                    Include(o => o.AddressShip).
                    Where(o => o.AccountId == Account.AccountId && o.Type != 0).ToListAsync();
            }
            return Page();
        }
    }
}
