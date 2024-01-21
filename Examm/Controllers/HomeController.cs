using Examm.Contexts;
using Examm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Examm.Controllers
{
    public class HomeController : Controller
    {
        ExammDbContext _context { get; }
        public HomeController (ExammDbContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index()
        {
            return  View( await _context.Sliders.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}