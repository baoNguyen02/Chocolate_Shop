using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages.Account.Orders
{
    public class EditModel : PageModel
    {
        private readonly Chocolate_Shop.Models.Chocolate_ShopContext _context;

        public EditModel(Chocolate_Shop.Models.Chocolate_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? quantity)
        {
            if (id == null || quantity == null)
            {
                return NotFound();
            }

            Order = await _context.Orders
                .Include(o => o.Account).
                Include(o => o.Product).
                FirstOrDefaultAsync(m => m.OrderId == id);

            if (Order == null)
            {
                return NotFound();
            }

            Order.QuantityPerUnit += quantity;
            Order.UnitPrice = Order.Product.Price * Order.QuantityPerUnit;

            _context.Orders.Update(Order);
            await _context.SaveChangesAsync();

            if (Order.QuantityPerUnit == 0)
            {
                _context.Orders.Remove(Order);
                await _context.SaveChangesAsync();
                return Redirect("./Index");
            }

            return Redirect("./Index");
        }
    }
}
