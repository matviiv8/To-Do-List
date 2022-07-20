using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.Models;
using Microsoft.EntityFrameworkCore;

namespace To_Do_List.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemContext context;

        public ItemController(ItemContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Items.ToListAsync());
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
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }
    }
}
