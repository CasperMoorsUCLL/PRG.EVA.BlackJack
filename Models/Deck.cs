using System.Collections.Generic;
using System.Linq;
namespace PRG.EVA.BlackJack.Models
{
    public class Deck
    {
        // Eigenschap voor de collectie van Card objecten
        public List<Card> Cards { get; private set; }

        // Eigenschap voor de totale waarde van alle kaarten in het Deck
        public int TotalValue
        {
            get
            {
                return Cards.Sum(card => GetCardValue(card.Rank));
            }
        }

        // Constructor om een nieuw Deck aan te maken
        public Deck()
        {
            Cards = new List<Card>();
        }

        // Methode om een kaart toe te voegen aan het Deck
        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        // Methode om het Deck te resetten, bijvoorbeeld wanneer een nieuw spel begint
        public void ResetDeck()
        {
            Cards.Clear();
        }

        // Hulpmethode om de waarde van een kaart op basis van de Rank te bepalen
        private int GetCardValue(Rank rank)
        {
            return rank switch
            {
                Rank.Ace => 11,       // Aas kan 11 of 1 zijn, afhankelijk van de context
                Rank.Two => 2,
                Rank.Three => 3,
                Rank.Four => 4,
                Rank.Five => 5,
                Rank.Six => 6,
                Rank.Seven => 7,
                Rank.Eight => 8,
                Rank.Nine => 9,
                Rank.Ten => 10,
                Rank.Jack => 10,
                Rank.Queen => 10,
                Rank.King => 10,
                _ => 0,
            };
        }

        internal void AddCard(int cardValue)
        {
            Cards.Add(Cards[cardValue]);
        }
    }
}
