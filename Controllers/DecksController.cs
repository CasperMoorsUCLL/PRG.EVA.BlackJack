//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PRG.EVA.BlackJack.Models;
//using System.Linq;
//using System.Threading.Tasks;

//public class DecksController : Controller
//{
//    private readonly ApplicationDbContext _context;

//    public DecksController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    //GET
//    public async Task<IActionResult> Index()
//    {
//        var deck = await _context.Deck.Include(d => d.Cards).ToListAsync();
//        return View(deck);
//    }

//    // GET: Decks/Delete/5
//    public async Task<IActionResult> Delete(int? deckId)
//    {
//        if (deckId == null)
//        {
//            return NotFound();
//        }

//        var deck = await _context.Deck
//            .FirstOrDefaultAsync(m => m.DeckId == deckId);
//        if (deck == null)
//        {
//            return NotFound();
//        }

//        return View(deck);
//    }

//    // POST: Decks/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int deckId)
//    {
//        var deck = await _context.Deck.FindAsync(deckId);
//        if (deck != null)
//        {
//            _context.Deck.Remove(deck);
//            await _context.SaveChangesAsync();
//        }

//        return RedirectToAction(nameof(Index));
//    }
//    private bool DecksExists(int deckId)
//    {
//        return _context.Deck.Any(e => e.DeckId == deckId);
//    }
//}
