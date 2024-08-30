using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRG.EVA.BlackJack.Models;
using System.Linq;
using System.Threading.Tasks;

public class CardsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CardsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var card = await _context.Card.ToListAsync();
        return View(card);
    }
    // GET: Cards/Delete/5
    public async Task<IActionResult> Delete(int cardId)
    {
        if (cardId == null)
        {
            return NotFound();
        }

        var card = await _context.Card
            .FirstOrDefaultAsync(m => m.CardId == cardId);

        if (card == null)
        {
            return NotFound();
        }

        return View(card);
    }

    // POST: Cards/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int cardId)
    {
        var card = await _context.Card.FindAsync(cardId);
        if (card != null)
        {
            _context.Card.Remove(card);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }


    private bool CardsExists(int cardId)
    {
        return _context.Card.Any(e => e.CardId == cardId);
    }
}