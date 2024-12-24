using Chocolate_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages.Account.Orders
{
    public class IndexModel : PageModel
    {
        private readonly Chocolate_ShopContext _context;

        public IndexModel(Chocolate_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<Order> Order { get; set; }

        [BindProperty]
        public Models.Account Account { get; set; }

        [BindProperty]
        public IList<Models.AddressShip> AddressShip { get; set; }

        [BindProperty]
        public decimal totalPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedAddressShipId { get; set; }

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
                    Where(o => o.AccountId == Account.AccountId && o.Type == 0).ToListAsync();

                totalPrice = (decimal)Order.Sum(o => o.UnitPrice);

                AddressShip = await _context.AddressShips.
                    Where(a => a.AccountId == Account.AccountId).ToListAsync();

            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
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
                    Where(o => o.AccountId == Account.AccountId && o.Type == 0).ToListAsync();

                foreach (var order in Order)
                {
                    order.AddressShipId = SelectedAddressShipId;
                    order.Type = 1;
                }

                await _context.SaveChangesAsync();
            }

            return Redirect("~/Index");
        }
    }
}
