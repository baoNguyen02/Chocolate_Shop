using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Chocolate_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages.Account.AddressShip
{
    public class CreateModel : PageModel
    {
        private readonly Chocolate_Shop.Models.Chocolate_ShopContext _context;

        public CreateModel(Chocolate_Shop.Models.Chocolate_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.AddressShip AddressShip { get; set; }

        [BindProperty]
        public Models.Account Account { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
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

            Account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == Account.AccountId);

            AddressShip.AccountId = Account.AccountId;

            _context.AddressShips.Add(AddressShip);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
