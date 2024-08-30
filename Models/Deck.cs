using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace PRG.EVA.BlackJack.Models
{
    public class Deck
    {
        [Key]
        public int DeckId { get; set; } // Primary Key
        public BlackJackGame GameId { get; set; }
        // Eigenschap voor de collectie van Card objecten
        public List<Card> Cards { get; private set; }

        // Eigenschap voor de totale waarde van alle kaarten in het Deck
        public int TotalValue
        {
            get
            {
                int totalValue = 0;
                int aceCount = 0;

                foreach (var card in Cards)
                {
                    int cardValue = GetCardValue(card.Rank);
                    totalValue += cardValue;
                    if (card.Rank == Rank.Ace)
                    {
                        aceCount++;
                    }
                }

                // Adjust for Aces
                while (totalValue > 21 && aceCount > 0)
                {
                    totalValue -= 10;
                    aceCount--;
                }

                return totalValue;
            }
        }

        // Constructor om een nieuw Deck aan te maken
        public Deck()
        {
            Cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public void ResetDeck()
        {
            Cards.Clear();
        }

        private int GetCardValue(Rank rank)
        {
            return rank switch
            {
                Rank.Ace => 11,
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