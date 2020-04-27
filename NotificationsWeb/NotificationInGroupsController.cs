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
    public class NotificationInGroupsController : Controller
    {
        private readonly notificationsContext _context;

        public NotificationInGroupsController(notificationsContext context)
        {
            _context = context;
        }

        // GET: NotificationInGroups
        public async Task<IActionResult> Index()
        {
            var notificationsContext = _context.NotificationInGroup.Include(n => n.Group).Include(n => n.Notification);
            return View(await notificationsContext.ToListAsync());
        }

        // GET: NotificationInGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationInGroup = await _context.NotificationInGroup
                .Include(n => n.Group)
                .Include(n => n.Notification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notificationInGroup == null)
            {
                return NotFound();
            }

            return View(notificationInGroup);
        }

        // GET: NotificationInGroups/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName");
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Name");
            return View();
        }

        // POST: NotificationInGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NotificationId,GroupId")] NotificationInGroup notificationInGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notificationInGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName", notificationInGroup.GroupId);
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Name", notificationInGroup.NotificationId);
            return View(notificationInGroup);
        }

        // GET: NotificationInGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationInGroup = await _context.NotificationInGroup.FindAsync(id);
            if (notificationInGroup == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName", notificationInGroup.GroupId);
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Name", notificationInGroup.NotificationId);
            return View(notificationInGroup);
        }

        // POST: NotificationInGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NotificationId,GroupId")] NotificationInGroup notificationInGroup)
        {
            if (id != notificationInGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificationInGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificationInGroupExists(notificationInGroup.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName", notificationInGroup.GroupId);
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Name", notificationInGroup.NotificationId);
            return View(notificationInGroup);
        }

        // GET: NotificationInGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationInGroup = await _context.NotificationInGroup
                .Include(n => n.Group)
                .Include(n => n.Notification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notificationInGroup == null)
            {
                return NotFound();
            }

            return View(notificationInGroup);
        }

        // POST: NotificationInGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notificationInGroup = await _context.NotificationInGroup.FindAsync(id);
            _context.NotificationInGroup.Remove(notificationInGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationInGroupExists(int id)
        {
            return _context.NotificationInGroup.Any(e => e.Id == id);
        }
    }
}
