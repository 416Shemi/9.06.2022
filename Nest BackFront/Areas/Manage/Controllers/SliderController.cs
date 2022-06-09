using Microsoft.AspNetCore.Mvc;
using Nest_BackFront.DAL;
using Nest_BackFront.Models;
using Nest_BackFront.Utilies;
using Nest_BackFront.Utilies.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nest_BackFront.Areas.manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private AppDbContext _context { get; }

        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Sliders.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Slider slider)
        {
            if (slider.Photo.CheckSize(4000))
            {
                ModelState.AddModelError("Photo", "Faylın ölçüsü 4000 kb-dan az olmalıdır");
                return View();
            }
            if (!slider.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "Fayl şəkil olmalıdır");
                return View();
            }
            slider.Image = await slider.Photo.SaveFileAsync(Path.Combine(Constant.ImagePath, "slider"));
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.Find(id);
            if (slider == null) return NotFound();
            if (System.IO.File.Exists(Path.Combine(Constant.ImagePath, "slider", slider.Image)))
            {
                System.IO.File.Delete(Path.Combine(Constant.ImagePath, "slider", slider.Image));
            }
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
