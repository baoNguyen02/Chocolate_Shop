using Chocolate_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Chocolate_Shop.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly Chocolate_ShopContext _context;

        public ProfileModel(Chocolate_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Account Account { get; set; }

        [BindProperty]
        public string currentPass { get; set; }

        [BindProperty]
        public string newPass { get; set; }

        [BindProperty]
        public string rePass { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
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
            else
            {
                Account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == Account.AccountId);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                string gender = Request.Form["gender"];

                Models.Account auth = System.Text.Json.JsonSerializer.Deserialize<Models.Account>(HttpContext.Session.GetString("User"));
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == auth.AccountId);
                if (currentPass != null && account != null)
                {
                    if (account.Password != currentPass)
                    {
                        ViewData["fail"] = "Wrong password!";
                        return Page();
                    }
                    else
                    {
                        if (!rePass.Equals(newPass) || rePass == null)
                        {
                            ViewData["fail"] = "Your confirm password is wrong";
                            return Page();
                        }
                        account.Password = newPass;
                        _context.Accounts.Update(account);
                        await _context.SaveChangesAsync();
                    }
                }

                if (account != null)
                {
                    account.Gmail = Account.Gmail;
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
                    account.AccountImage = Account.AccountImage;

                    await _context.SaveChangesAsync();

                    HttpContext.Session.Remove("User");
                    HttpContext.Session.SetString("User", System.Text.Json.JsonSerializer.Serialize(auth));

                    ViewData["success"] = "Update Successful";
                }
                else
                {
                    ViewData["fail"] = "Account not found";
                }
            }
            catch (Exception ex)
            {
                ViewData["fail"] = ex.Message;
            }
            return Page();
        }
    }
}
