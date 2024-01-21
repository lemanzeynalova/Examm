using Examm.Areas.Admin.Controllers.SliderVM;
using Examm.Contexts;
using Examm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examm.Areas.Admin.Controllers
{
    
    
        [Area("Admin")]
        //[Authorize(Roles = "SuperAdmin,Admin, Moderator")]
        public class SliderController : Controller
        {
            ExammDbContext _db { get; }
            public SliderController(ExammDbContext db)
            {
                _db = db;
            }

            public async Task<IActionResult> Index()
            {
               
                var items = await _db.Sliders.Select(s => new SliderListItemVM
                {
                    Position = s.Position,
                    Name = s.Name,
                    Image = s.Image,
                    Id = s.Id,
                }).ToListAsync();
                return View(items);
            }
            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Create(SliderCreateVM vm)
            {
                
                Slider slider = new Slider
                {
                    Position = vm.Position,
                    Name = vm.Name,
                    Image = vm.Image,
                    
                };
                await _db.Sliders.AddAsync(slider);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            public async Task<IActionResult> Delete(int? id)
            {
                TempData["Response"] = false;
                if (id == null) return BadRequest();
                var data = await _db.Sliders.FindAsync(id);
                if (data == null) return NotFound();
                _db.Sliders.Remove(data);
                await _db.SaveChangesAsync();
                TempData["Response"] = true;
                return RedirectToAction(nameof(Index));
            }
            public async Task<IActionResult> Update(int? id)
            {
                if (id == null || id <= 0) return BadRequest();
                var data = await _db.Sliders.FindAsync(id);
                if (data == null) return NotFound();
                return View(new SliderUpdateVM
                {
                    Image = data.Image,
                    Name = data.Name,
                    Position = data.Position
                });
            }
            [HttpPost]
            public async Task<IActionResult> Update(int? id, SliderUpdateVM vm)
            {
                
                var data = await _db.Sliders.FindAsync(id);
                if (data == null) return NotFound();
                data.Name = vm.Name;
                data.Image = vm.Image;
                data.Position = vm.Position;
               
                await _db.SaveChangesAsync();
                TempData["Response"] = true;
                return RedirectToAction(nameof(Index));
            }
        }
    }

