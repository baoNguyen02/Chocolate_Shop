using Chocolate_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages.Admin.Members
{
    public class IndexModel : PageModel
    {
        private readonly Chocolate_Shop.Models.Chocolate_ShopContext _context;

        public IndexModel(Chocolate_Shop.Models.Chocolate_ShopContext context)
        {
            _context = context;
        }

        public IList<Chocolate_Shop.Models.Account> Account { get; set; }

        public async Task OnGetAsync()
        {
            Account = await _context.Accounts.Include(x => x.Role).ToListAsync();
        }
    }
}
