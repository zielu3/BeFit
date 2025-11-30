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
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var exercises = _context.Exercises
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .Where(e => e.UserId == userId);
            return View(await exercises.ToListAsync());
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = GetUserId();
            var exercise = await _context.Exercises
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            var userId = GetUserId();
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name");
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(t => t.UserId == userId), "Id", "StartTime");
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseDto dto)
        {
            if (ModelState.IsValid)
            {
                var exercise = new Exercise
                {
                    ExerciseTypeId = dto.ExerciseTypeId,
                    TrainingSessionId = dto.TrainingSessionId,
                    Weight = dto.Weight,
                    Sets = dto.Sets,
                    Reps = dto.Reps,
                    UserId = GetUserId()
                };
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var userId = GetUserId();
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", dto.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(t => t.UserId == userId), "Id", "StartTime", dto.TrainingSessionId);
            return View(dto);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = GetUserId();
            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            if (exercise == null)
            {
                return NotFound();
            }
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", exercise.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(t => t.UserId == userId), "Id", "StartTime", exercise.TrainingSessionId);
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExerciseTypeId,TrainingSessionId,Weight,Sets,Reps")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            var userId = GetUserId();
            var existingExercise = await _context.Exercises
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            
            if (existingExercise == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    exercise.UserId = userId;
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
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
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", exercise.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions.Where(t => t.UserId == userId), "Id", "StartTime", exercise.TrainingSessionId);
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = GetUserId();
            var exercise = await _context.Exercises
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();
            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}
