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
    public class ParametersController : Controller
    {
        private readonly notificationsContext _context;

        public ParametersController(notificationsContext context)
        {
            _context = context;
        }

        // GET: Parameters
        public async Task<IActionResult> Index()
        {
            var notificationsContext = _context.Parameters.Include(p => p.Notification);
            return View(await notificationsContext.ToListAsync());
        }

        // GET: Parameters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameters = await _context.Parameters
                .Include(p => p.Notification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parameters == null)
            {
                return NotFound();
            }

            return View(parameters);
        }

        // GET: Parameters/Create
        public IActionResult Create()
        {
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Name");
            return View();
        }

        // POST: Parameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NotificationId,Title,NotificationText,BackgroundColor,BackgroundImage,TextColor,Maximized,Width,Height,Autosize,Gradient")] Parameters parameters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parameters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Name", parameters.NotificationId);
            return View(parameters);
        }

        // GET: Parameters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameters = await _context.Parameters.FindAsync(id);
            if (parameters == null)
            {
                return NotFound();
            }
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Name", parameters.NotificationId);
            return View(parameters);
        }

        // POST: Parameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NotificationId,Title,NotificationText,BackgroundColor,BackgroundImage,TextColor,Maximized,Width,Height,Autosize,Gradient")] Parameters parameters)
        {
            if (id != parameters.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parameters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParametersExists(parameters.Id))
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
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Name", parameters.NotificationId);
            return View(parameters);
        }

        // GET: Parameters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameters = await _context.Parameters
                .Include(p => p.Notification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parameters == null)
            {
                return NotFound();
            }

            return View(parameters);
        }

        // POST: Parameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parameters = await _context.Parameters.FindAsync(id);
            _context.Parameters.Remove(parameters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParametersExists(int id)
        {
            return _context.Parameters.Any(e => e.Id == id);
        }
    }
}
