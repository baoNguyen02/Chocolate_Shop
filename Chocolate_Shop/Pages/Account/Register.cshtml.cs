using Chocolate_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly Chocolate_ShopContext _context;

        public RegisterModel(Chocolate_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Account Account { get; set; }

        [BindProperty]
        public string rePass { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            string gender = Request.Form["gender"];

            var acc = await _context.Accounts.
                SingleOrDefaultAsync(a => a.Gmail.Equals(Account.Gmail));
            if (Account.Gmail == null)
            {
                ViewData["msg"] = "Your input is invalid";
                return Page();
            }
            else if (acc != null)
            {
                ViewData["msg"] = "Gmail is already in use. Try a different Gmail.";
                return Page();
            }
            else if (!rePass.Equals(Account.Password))
            {
                ViewData["msg"] = "Your confirm password is wrong";
                return Page();
            }
            else
            {
                Models.Account account = new Models.Account();
                account.RoleId = 2;
                account.Gmail = Account.Gmail;
                account.Password = Account.Password;
                account.FirstName = Account.FirstName;
                account.LastName = Account.LastName;
                if (gender == "true")
                {
                    account.Gender = true;
                }
                else if (gender == "false")
                {
                    account.Gender = false;
                }
                else
                {
                    account.Gender = null;
                }

                account.Phone = Account.Phone;
                account.Address = Account.Address;
                account.BirthDay = Account.BirthDay;
                account.AccountImage = null;
                account.Status = 1;


                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                return Redirect("~/Account/Login");
            }
        }
    }
}
