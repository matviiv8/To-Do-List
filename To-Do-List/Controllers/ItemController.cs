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
            try
            {
                if (ModelState.IsValid)
                {
                    context.Add(item);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var itemToUpdate = context.Items.FirstOrDefault(x => x.Id == id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }
            return View(itemToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,IsCompleted,Date")] Item item)
        {
            if(id != item.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    context.Update(item);
                    await context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(item);
        }
    } 
}
