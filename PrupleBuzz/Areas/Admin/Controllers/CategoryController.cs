using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrupleBuzz.DAL;
using PrupleBuzz.Models;

namespace PrupleBuzz.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _context.Categories.AnyAsync(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "It is alreay exist!");
                return View();
            }
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

           // return Json(category);
        }
        public IActionResult Update()
        {
            return View();
        }

    }
}
