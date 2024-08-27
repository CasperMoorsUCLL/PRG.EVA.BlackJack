namespace PRG.EVA.BlackJack.Models
{
    public class Card
    {
        // Eigenschap voor het soort kaart
        public Suit Suit { get; set; }

        // Eigenschap voor de waarde van de kaart
        public Rank Rank { get; set; }

        // Constructor om een kaart te initialiseren met een Suit en Rank
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        // Override ToString() voor een gebruiksvriendelijke representatie van de kaart
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}
