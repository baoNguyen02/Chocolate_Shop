using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly Chocolate_Shop.Models.Chocolate_ShopContext _context;

        public IndexModel(Chocolate_Shop.Models.Chocolate_ShopContext context)
        {
            _context = context;
        }

        public IList<Models.Order> Order { get; set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Orders
                    .Include(od => od.Account)
                    .Include(od => od.Product)
                    .Include(od => od.AddressShip)
                    .Where(od => od.Type != 0)
                    .ToListAsync();
        }
    }
}
