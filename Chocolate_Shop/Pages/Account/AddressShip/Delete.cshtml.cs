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
    public class DeleteModel : PageModel
    {
        private readonly Chocolate_Shop.Models.Chocolate_ShopContext _context;

        public DeleteModel(Chocolate_Shop.Models.Chocolate_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.AddressShip AddressShip { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AddressShip = await _context.AddressShips
                .Include(a => a.Account).FirstOrDefaultAsync(m => m.AddressShipId == id);

            if (AddressShip == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AddressShip = await _context.AddressShips.FindAsync(id);

            if (AddressShip != null)
            {
                _context.AddressShips.Remove(AddressShip);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
