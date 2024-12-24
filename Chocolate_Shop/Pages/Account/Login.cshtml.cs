using Chocolate_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Chocolate_Shop.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly Chocolate_ShopContext _context;

        public LoginModel(Chocolate_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Account Account { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var acc = await _context.Accounts.
                SingleOrDefaultAsync(a => a.Gmail.Equals(Account.Gmail)
                && a.Password.Equals(Account.Password));
            if (acc == null)
            {
                ViewData["msg"] = "Username or Password is invalid";
                return Page();
            }
            if (acc.Status != 1)
            {
                ViewData["msg"] = "Your account has been banned!";
                return Page();
            }

            HttpContext.Session.SetString("User", JsonSerializer.Serialize(acc));

            return Redirect("~/Index");
        }
    }
}
