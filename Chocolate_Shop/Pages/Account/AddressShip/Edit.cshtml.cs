using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chocolate_Shop.Models;

namespace Chocolate_Shop.Pages.Account.AddressShip
{
    public class EditModel : PageModel
    {
        private readonly Chocolate_Shop.Models.Chocolate_ShopContext _context;

        public EditModel(Chocolate_Shop.Models.Chocolate_ShopContext context)
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AddressShip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressShipExists(AddressShip.AddressShipId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AddressShipExists(int id)
        {
            return _context.AddressShips.Any(e => e.AddressShipId == id);
        }
    }
}
