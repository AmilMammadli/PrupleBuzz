using Microsoft.AspNetCore;
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
        private readonly IWebHostEnvironment _env;


        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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

            string ImageName = Guid.NewGuid() + category.FormFile.FileName;
            string path = Path.Combine(_env.WebRootPath, "assets/img", ImageName);
            using (FileStream filestrem = new FileStream(path, FileMode.Create))
            {
                category.FormFile.CopyTo(filestrem);
            }
            category.Image = ImageName;


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
