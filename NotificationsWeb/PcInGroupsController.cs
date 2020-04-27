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
    public class PcInGroupsController : Controller
    {
        private readonly notificationsContext _context;

        public PcInGroupsController(notificationsContext context)
        {
            _context = context;
        }

        // GET: PcInGroups
        public async Task<IActionResult> Index()
        {
            var notificationsContext = _context.PcInGroup.Include(p => p.Group).Include(p => p.Pc);
            return View(await notificationsContext.ToListAsync());
        }

        // GET: PcInGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pcInGroup = await _context.PcInGroup
                .Include(p => p.Group)
                .Include(p => p.Pc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pcInGroup == null)
            {
                return NotFound();
            }

            return View(pcInGroup);
        }

        // GET: PcInGroups/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName");
            ViewData["Pcid"] = new SelectList(_context.Pcs, "Id", "PcName");
            return View();
        }

        // POST: PcInGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupId,Pcid")] PcInGroup pcInGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pcInGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName", pcInGroup.GroupId);
            ViewData["Pcid"] = new SelectList(_context.Pcs, "Id", "PcName", pcInGroup.Pcid);
            return View(pcInGroup);
        }

        // GET: PcInGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pcInGroup = await _context.PcInGroup.FindAsync(id);
            if (pcInGroup == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName", pcInGroup.GroupId);
            ViewData["Pcid"] = new SelectList(_context.Pcs, "Id", "PcName", pcInGroup.Pcid);
            return View(pcInGroup);
        }

        // POST: PcInGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupId,Pcid")] PcInGroup pcInGroup)
        {
            if (id != pcInGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pcInGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PcInGroupExists(pcInGroup.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName", pcInGroup.GroupId);
            ViewData["Pcid"] = new SelectList(_context.Pcs, "Id", "PcName", pcInGroup.Pcid);
            return View(pcInGroup);
        }

        // GET: PcInGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pcInGroup = await _context.PcInGroup
                .Include(p => p.Group)
                .Include(p => p.Pc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pcInGroup == null)
            {
                return NotFound();
            }

            return View(pcInGroup);
        }

        // POST: PcInGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pcInGroup = await _context.PcInGroup.FindAsync(id);
            _context.PcInGroup.Remove(pcInGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PcInGroupExists(int id)
        {
            return _context.PcInGroup.Any(e => e.Id == id);
        }
    }
}
