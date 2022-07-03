using Microsoft.AspNetCore.Mvc;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    public class ItemController : Controller
    {
        private ItemContext context;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,IsCompleted,Date")] Item item)
        {
            if (ModelState.IsValid)
            {
               context.Add(item);
               context.SaveChanges();
               return RedirectToAction(nameof(Index));
            }
            return View(item);
        }
    }
}
