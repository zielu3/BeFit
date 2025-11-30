using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;

namespace BeFit.Controllers
{
    [Authorize]
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: Stats
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var fourWeeksAgo = DateTime.Now.AddDays(-28);

            var stats = await _context.Exercises
                .Include(e => e.ExerciseType)
                .Include(e => e.TrainingSession)
                .Where(e => e.UserId == userId && e.TrainingSession.StartTime >= fourWeeksAgo)
                .GroupBy(e => e.ExerciseType.Name)
                .Select(g => new ExerciseStats
                {
                    ExerciseName = g.Key,
                    Count = g.Count(),
                    TotalReps = g.Sum(e => e.Sets * e.Reps),
                    AverageWeight = g.Average(e => e.Weight),
                    MaxWeight = g.Max(e => e.Weight)
                })
                .OrderByDescending(s => s.Count)
                .ToListAsync();

            return View(stats);
        }
    }
}
