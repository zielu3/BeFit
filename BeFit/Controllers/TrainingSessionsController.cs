using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;

namespace BeFit.Controllers
{
    [Authorize]
    public class TrainingSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: TrainingSessions
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var sessions = _context.TrainingSessions.Where(t => t.UserId == userId);
            return View(await sessions.ToListAsync());
        }

        // GET: TrainingSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = GetUserId();
            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (trainingSession == null)
            {
                return NotFound();
            }

            return View(trainingSession);
        }

        // GET: TrainingSessions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingSessionDto dto)
        {
            if (ModelState.IsValid)
            {
                var trainingSession = new TrainingSession
                {
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    UserId = GetUserId()
                };
                _context.Add(trainingSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: TrainingSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = GetUserId();
            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (trainingSession == null)
            {
                return NotFound();
            }
            return View(trainingSession);
        }

        // POST: TrainingSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime")] TrainingSession trainingSession)
        {
            if (id != trainingSession.Id)
            {
                return NotFound();
            }

            var userId = GetUserId();
            var existingSession = await _context.TrainingSessions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            
            if (existingSession == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    trainingSession.UserId = userId;
                    _context.Update(trainingSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingSessionExists(trainingSession.Id))
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
            return View(trainingSession);
        }

        // GET: TrainingSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = GetUserId();
            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (trainingSession == null)
            {
                return NotFound();
            }

            return View(trainingSession);
        }

        // POST: TrainingSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();
            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (trainingSession != null)
            {
                _context.TrainingSessions.Remove(trainingSession);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TrainingSessionExists(int id)
        {
            return _context.TrainingSessions.Any(e => e.Id == id);
        }
    }
}
