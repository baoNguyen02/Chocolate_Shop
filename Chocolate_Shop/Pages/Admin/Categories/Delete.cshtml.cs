using Chocolate_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Chocolate_Shop.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly Chocolate_ShopContext _context;

        public DeleteModel(Chocolate_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Categories { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Categories = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                int oldCategoryId = category.CategoryId;

                Category newCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == "Empty category");

                if (newCategory == null)
                {
                    newCategory = new Category
                    {
                        CategoryName = "Empty category",
                        Description = "Description for New Category"
                    };


                    _context.Categories.Add(newCategory);
                    await _context.SaveChangesAsync();
                }


                List<Product> products = await _context.Products.Where(x => x.CategoryId == oldCategoryId).ToListAsync();
                foreach (var product in products)
                {
                    product.CategoryId = newCategory.CategoryId;
                }

                if (category.CategoryName != "Empty category")
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
