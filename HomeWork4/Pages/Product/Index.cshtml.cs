using HomeWork4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeWork4.Pages.Product
{
    public class IndexModel : PageModel
    {

        private readonly ProductDbContext _context;

        public IndexModel(ProductDbContext context)
        {
            _context = context;
        }   

        public List<Entities.Product> Products { get; set; }
        public string Info { get; set; }
        public bool isEdit { get; set; } = false;

        public void OnGet(string info = "", int id = -1, bool edit = false)
        {
            Products = _context.Products.ToList();
            Info = info;
            if (id != -1)
                Product = _context.Products.SingleOrDefault(p => p.Id == id);
            isEdit = edit;
        }

        [BindProperty]
        public Entities.Product Product { get; set; }

        public IActionResult OnPost()
        {
            if(Product != null)
            {
                _context.Products.Add(Product);
                _context.SaveChanges();
                Info = $"'{Product.Name}' succesfully added";
                return RedirectToPage("Index", new {info = Info});
            }
            return RedirectToPage("Index", new {info = "Data is null"});
        }

        public IActionResult OnGetDelete(int id)
        {
            var pr = _context.Products.SingleOrDefault(p => p.Id == id);
            _context.Products.Remove(pr);
            _context.SaveChanges();
            return RedirectToPage("Index", new { info = "Product successfully deleted" });
        }

        public IActionResult OnGetStartEdit(int iid)
        {
            var pro = _context.Products.SingleOrDefault(p => p.Id == iid);
            if (pro != null){
                return RedirectToPage("Index", new {id = iid , edit = true});
            }
            return RedirectToPage("Index", new { info = "Could not find product" });
        }

        public IActionResult OnPostEdit(int id)
        {
            if (Product != null)
            {
                var pr = _context.Products.SingleOrDefault(pro => pro.Id == id);
                if (pr != null)
                {
                    pr.Name = Product.Name; 
                    pr.Price = Product.Price;
                    pr.Discount = Product.Discount;
                    _context.SaveChanges();
                    return RedirectToPage("Index", new { info = "Product succesfully edited" });
                }
                return RedirectToPage("Index", new { info = "Could not find product" });
            }
            return RedirectToPage("Index", new { info = "Problem accured. Product was not edited" });
        }

    }
}
