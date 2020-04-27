using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotificationsWeb.Models;

namespace NotificationsWeb
{
    public class PcsController : Controller
    {
        private readonly notificationsContext _context;

        public PcsController(notificationsContext context)
        {
            _context = context;
        }

        // GET: Pcs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pcs.ToListAsync());
        }

        // GET: Pcs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pcs = await _context.Pcs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pcs == null)
            {
                return NotFound();
            }

            return View(pcs);
        }

        // GET: Pcs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pcs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PcName")] Pcs pcs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pcs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pcs);
        }

        // GET: Pcs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pcs = await _context.Pcs.FindAsync(id);
            if (pcs == null)
            {
                return NotFound();
            }
            return View(pcs);
        }

        // POST: Pcs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PcName")] Pcs pcs)
        {
            if (id != pcs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pcs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PcsExists(pcs.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pcs);
        }

        // GET: Pcs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pcs = await _context.Pcs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pcs == null)
            {
                return NotFound();
            }

            return View(pcs);
        }

        // POST: Pcs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pcs = await _context.Pcs.FindAsync(id);
            _context.Pcs.Remove(pcs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PcsExists(int id)
        {
            return _context.Pcs.Any(e => e.Id == id);
        }
    }
}
