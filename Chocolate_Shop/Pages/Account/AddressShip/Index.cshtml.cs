using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chocolate_Shop.Models;

namespace Chocolate_Shop.Pages.Account.AddressShip
{
    public class IndexModel : PageModel
    {
        private readonly Chocolate_Shop.Models.Chocolate_ShopContext _context;

        public IndexModel(Chocolate_Shop.Models.Chocolate_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Account Account { get; set; }

        public IList<Models.AddressShip> AddressShip { get; set; }

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
            }

            AddressShip = await _context.AddressShips
                .Include(a => a.Account)
                .Where(a => a.AccountId == Account.AccountId)
                .ToListAsync();

            return Page();
        }
    }
}
