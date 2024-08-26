using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRG.EVA.BlackJack.Models;

namespace PRG.EVA.BlackJack.Controllers
{
    public class GameLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameLogs.ToListAsync());
        }

        // GET: GameLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameLog = await _context.GameLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameLog == null)
            {
                return NotFound();
            }

            return View(gameLog);
        }

        // GET: GameLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameLog = await _context.GameLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameLog == null)
            {
                return NotFound();
            }

            return View(gameLog);
        }

        // POST: GameLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameLog = await _context.GameLogs.FindAsync(id);
            _context.GameLogs.Remove(gameLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameLogExists(int id)
        {
            return _context.GameLogs.Any(e => e.Id == id);
        }
    }
}
