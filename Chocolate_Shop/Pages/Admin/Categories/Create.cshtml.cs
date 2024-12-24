using Chocolate_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace Chocolate_Shop.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        private readonly Chocolate_ShopContext _context;

        public CreateModel(Chocolate_ShopContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Category Categories { get; set; } = default!;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Categories == null || Categories == null)
            {
                return Page();
            }

            _context.Categories.Add(Categories);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
