using Microsoft.AspNetCore.Mvc;
using PRG.EVA.BlackJack.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace PRG.EVA.BlackJack.Controllers
{
    public class BlackJackController : Controller
    {
        private static BlackJackGame? _game;
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public BlackJackController(HttpClient httpClient, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        // InitGame action
        public async Task<IActionResult> InitGame(decimal bet)
        {
            _game = new BlackJackGame(bet);
            _game.Status = GameStatus.Playing;

            // Dealer krijgt onmiddellijk twee kaarten
            var firstCardTask = GetCardFromApiAsync();
            var secondCardTask = GetCardFromApiAsync();

            var dealerCards = await Task.WhenAll(firstCardTask, secondCardTask);

            _game.DealerDeck.AddCard(dealerCards[0]); // Eerste kaart zichtbaar voor de speler
            _game.DealerDeck.AddCard(dealerCards[1]); // Tweede kaart blijft geheim

            ViewBag.HiddenCard = dealerCards[1]; // De tweede kaart opslaan in een ViewBag om geheim te houden

            return View(_game);
        }

        // Play action
        public async Task<IActionResult> Play(string option)
        {
            if (_game == null)
            {
                ViewBag.Result = "GameError";
                return View("Play");
            }

            if (_game.Status != GameStatus.Playing)
            {
                ViewBag.Result = _game.Status.ToString();
                return View("Play", _game);
            }

            if (option == "H")
            {
                // Speler kiest voor een nieuwe kaart
                var newCard = await GetCardFromApiAsync();
                _game.PlayerDeck.AddCard(newCard);

                ViewBag.TotalPlayer = _game.PlayerDeck.TotalValue;

                if (_game.PlayerDeck.TotalValue > 21)
                {
                    _game.Status = GameStatus.Lost;
                    ViewBag.Result = "Verloren";
                    ViewBag.Wins = 0;
                    ViewBag.TotalDealer = _game.DealerDeck.TotalValue;

                    // Bewaar het resultaat van het spel in de database
                    //await SaveGameLog(0, "Hit", newCard.Suit.ToString(), newCard.Rank.ToString(), _game.PlayerDeck.TotalValue, 0, ViewBag.Result);
                    if (_game.Status == GameStatus.Won)
                    {
                        ViewBag.Result = "Gewonnen";
                        ViewBag.Wins = _game.Bet * 2;
                        await SaveGameLog(0, option, "", "", _game.PlayerDeck.TotalValue, ViewBag.Wins, ViewBag.Result);
                    }
                    else if (_game.Status == GameStatus.Lost)
                    {
                        ViewBag.Result = "Verloren";
                        ViewBag.Wins = 0;
                        await SaveGameLog(0, option, "", "", _game.PlayerDeck.TotalValue, ViewBag.Wins, ViewBag.Result);
                    }
                    else
                    {
                        ViewBag.Result = "Gelijkgespeeld";
                        ViewBag.Wins = _game.Bet;
                        await SaveGameLog(0, option, "", "", _game.PlayerDeck.TotalValue, ViewBag.Wins, ViewBag.Result);
                    }
                    return View("Play", _game);
                }
                return View("Play", _game);
            }
            else if (option == "S" || _game.PlayerDeck.TotalValue >= 21)
            {
                // Speler kiest voor Stand
                //while (_game.DealerDeck.TotalValue < 17)
                //{
                //    var dealerCard = await GetCardFromApiAsync();
                //    _game.DealerDeck.AddCard(dealerCard);
                //}

                ViewBag.TotalPlayer = _game.PlayerDeck.TotalValue;
                ViewBag.TotalDealer = _game.DealerDeck.TotalValue;

                _game.UpdateGameStatus();

                if (_game.Status == GameStatus.Won)
                {
                    ViewBag.Result = "Gewonnen";
                    ViewBag.Wins = _game.Bet * 2;
                    await SaveGameLog(0, option, "", "", _game.PlayerDeck.TotalValue, ViewBag.Wins, ViewBag.Result);
                }
                else if (_game.Status == GameStatus.Lost)
                {
                    ViewBag.Result = "Verloren";
                    ViewBag.Wins = 0;
                    await SaveGameLog(0, option, "", "", _game.PlayerDeck.TotalValue, ViewBag.Wins, ViewBag.Result);
                }
                else
                {
                    ViewBag.Result = "Gelijkgespeeld";
                    ViewBag.Wins = _game.Bet;
                    await SaveGameLog(0, option, "", "", _game.PlayerDeck.TotalValue, ViewBag.Wins, ViewBag.Result);
                }

                return View("Play", _game);
            }
            else
            {
                ViewBag.Result = "UnknownOption";
                ViewBag.Wins = 0;
                ViewBag.TotalPlayer = _game.PlayerDeck.TotalValue;
                ViewBag.TotalDealer = _game.DealerDeck.TotalValue;
                return View("Play", _game);
            }
        }

        // Methode om een kaart op te halen via de API
        private async Task<Card> GetCardFromApiAsync()
        {
            var response = await _httpClient.GetAsync("https://mgp32-api.azurewebsites.net/blackjack/getcard");
            response.EnsureSuccessStatusCode();

            var card = await response.Content.ReadFromJsonAsync<Card>();
            return card ?? throw new Exception("Failed to retrieve card from API");
        }
        public async Task<IActionResult> SaveGameLog(int Id, string playOption, string CardSuit, string CardRank, decimal CardTotal, decimal wins, string result)
        {
            // Verzamel de benodigde gegevens
            var gameLog = new GameLog
            {
                Id = Id,
                PlayOption = playOption,
                CardSuit = CardSuit,
                CardRank = CardRank,
                CardTotal = CardTotal,
                Wins = wins,
                Result = result
            };

            // Voeg de GameLog toe aan de database
            _context.GameLogs.Add(gameLog);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}