using Chocolate_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Chocolate_ShopContext _context;

        public IndexModel(Chocolate_ShopContext context)
        {
            _context = context;
        }

        public IList<Category> Categories { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return Redirect("/Account/Login");
            }

            Models.Account Account = System.Text.Json.JsonSerializer.Deserialize<Models.Account>(HttpContext.Session.GetString("User"));

            if (Account.RoleId != 1)
            {
                return Redirect("~/Index");
            }
            else
            {
                if (_context.Categories != null)
                {
                    Categories = await _context.Categories.ToListAsync();
                }
            }
            return Page();
        }
    }
}
