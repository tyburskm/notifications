﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotificationsWeb.Models;

namespace NotificationsWeb
{
    public class NotificationsController : Controller
    {
        private readonly notificationsContext _context;

        public NotificationsController(notificationsContext context)
        {
            _context = context;
        }

        // GET: Notifications
        public async Task<IActionResult> Index()
        {
            ViewBag.MissingParameters =_context.Notifications.Where(x => !x.Parameters.Any()).Select(x=>x.Id).ToList();
            return View(await _context.Notifications.ToListAsync());
        }

        // GET: Notifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notifications == null)
            {
                return NotFound();
            }

            return View(notifications);
        }

        public async Task<IActionResult> EditParameters(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameterId = await _context.Parameters
                .FirstOrDefaultAsync(x=>x.NotificationId == id);

            if (parameterId != null)
            {
                return RedirectToAction("Edit", nameof(Parameters), new { Id = parameterId.Id});
            }
            else
            {
                return RedirectToAction("Create", nameof(Parameters), new { nId = id });
            }

            //return View(notifications);
        }


        // GET: Notifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ValidFrom,ValidTo,Repeat,RunAtTime")] Notifications notifications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notifications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notifications);
        }

        // GET: Notifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications.FindAsync(id);
            if (notifications == null)
            {
                return NotFound();
            }
            return View(notifications);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ValidFrom,ValidTo,Repeat,RunAtTime")] Notifications notifications)
        {
            if (id != notifications.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notifications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificationsExists(notifications.Id))
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
            return View(notifications);
        }

        // GET: Notifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notifications == null)
            {
                return NotFound();
            }

            return View(notifications);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notifications = await _context.Notifications.FindAsync(id);
            _context.Notifications.Remove(notifications);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationsExists(int id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }
}
