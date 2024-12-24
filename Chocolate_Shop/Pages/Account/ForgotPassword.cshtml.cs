using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;
using Chocolate_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages.Account
{

    public class ForgotPasswordModel : PageModel
    {
        private readonly Chocolate_ShopContext _context;

        public ForgotPasswordModel(Chocolate_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string emailInput { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var acc = await _context.Accounts.
                SingleOrDefaultAsync(a => a.Gmail.Equals(emailInput));
            if (acc == null)
            {
                ViewData["msg"] = "Gmail doesn't exist!";
                return Page();
            }

            int randomCode = RandomNumber();
            HttpContext.Session.SetInt32("RandomNumber", randomCode);
            HttpContext.Session.SetInt32("RandomNumber_Timeout", (int)DateTime.Now.AddMinutes(60).Subtract(DateTime.Now).TotalSeconds);
            int accountId = await _context.Accounts
                                .Where(a => a.Gmail.Equals(emailInput))
                                .Select(a => a.AccountId)
                                .FirstOrDefaultAsync();
            HttpContext.Session.SetInt32("AccountId", accountId);
            string linkEmail = "https://localhost:7109/Account/ChangePassword?code=" + randomCode;

            if (SendEmail(emailInput, linkEmail) == false)
            {
                return Page();
            }

            return Redirect("./SendEmailSuccess");

        }
        public bool SendEmail(string email, string linkEmail)
        {
            string recipientEmail = email;
            string senderEmail = "baonguyenadzz@gmail.com"; // Replace with your email address
            string senderPassword = "wuur zbgn tebu hmqt"; // Replace with your email password

            string subject = "[ChocoLux] Reset your password";
            string body = "We have received your request to reset your password. " +
                "Please re-enter your new password by clicking this link:\n\n "
                + linkEmail +
                "\n\nThe link will expire within 1 hour.\n\nThank you for trusting our store.";

            try
            {
                // Configure SMTP client
                SmtpClient client = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true,
                };

                // Create and send email message
                MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                ViewData["msg"] = ex.Message;
                return false;
            }

            return true;
        }

        public int RandomNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);

            return randomNumber;
        }

    }
}
