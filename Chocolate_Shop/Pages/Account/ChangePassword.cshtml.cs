using Chocolate_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages.Account
{
    public class ChangePasswordModel : PageModel
    {
        private readonly Chocolate_ShopContext _context;

        public ChangePasswordModel(Chocolate_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string newPass { get; set; }

        [BindProperty]
        public string rePass { get; set; }
        public IActionResult OnGet(int Code)
        {
            var randomNumber = HttpContext.Session.GetInt32("RandomNumber");
            var timeout = HttpContext.Session.GetInt32("RandomNumber_Timeout");
            var currentTime = (int)DateTime.Now.Subtract(DateTime.Now).TotalSeconds;
            if (timeout != null && currentTime > timeout)
            {
                ViewData["msg"] = "Your link has expired. please resend!";
                return Redirect("./ForgotPassword");
            }
            if (Code != randomNumber)
            {
                return Redirect("./Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            var acc = await _context.Accounts.
                SingleOrDefaultAsync(a => a.AccountId == accountId);

            if (acc == null)
            {
                return RedirectToPage("./Login");
            }

            if (!newPass.Equals(rePass))
            {
                ViewData["msg"] = "Your confirm password is wrong";
                return Page();
            }

            acc.Password = newPass;
            _context.Accounts.Update(acc);
            await _context.SaveChangesAsync();


            HttpContext.Session.Remove("RandomNumber");
            HttpContext.Session.Remove("RandomNumber_Timeout");
            HttpContext.Session.Remove("AccountId");

            return RedirectToPage("./Login");

        }
    }
}
