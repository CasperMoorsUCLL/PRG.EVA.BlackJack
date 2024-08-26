namespace PRG.EVA.BlackJack.Models
{
    public class BlackJackGame
    {
        // Eigenschappen van het spel
        public Deck DealerDeck { get; private set; }
        public Deck PlayerDeck { get; private set; }
        public GameStatus Status { get; set; }
        public decimal Bet { get; private set; }

        // Constructor om een nieuw spel te initialiseren
        public BlackJackGame(decimal initialBet)
        {
            DealerDeck = new Deck();
            PlayerDeck = new Deck();
            Status = GameStatus.Playing;
            Bet = initialBet;
        }

        // Methode om een nieuwe inzet te plaatsen
        public void PlaceBet(decimal newBet)
        {
            Bet = newBet;
        }

        // Methode om het spel te updaten op basis van de huidige situatie
        public void UpdateGameStatus()
        {
            if (PlayerDeck.TotalValue > 21)
            {
                Status = GameStatus.Lost;
            }
            else if (DealerDeck.TotalValue > 21)
            {
                Status = GameStatus.Won;
            }
            else if (PlayerDeck.TotalValue == DealerDeck.TotalValue)
            {
                Status = GameStatus.Draw;
            }
            else if (PlayerDeck.TotalValue > DealerDeck.TotalValue)
            {
                Status = GameStatus.Won;
            }
            else
            {
                Status = GameStatus.Lost;
            }
        }

        // Methode om een kaart toe te voegen aan de hand van de speler
        public void PlayerHit(int cardValue)
        {
            if (Status == GameStatus.Playing)
            {
                PlayerDeck.AddCard(cardValue);
                UpdateGameStatus();
            }
        }

        // Methode om een kaart toe te voegen aan de hand van de dealer
        public void DealerHit(int cardValue)
        {
            if (Status == GameStatus.Playing)
            {
                DealerDeck.AddCard(cardValue);
                UpdateGameStatus();
            }
        }

        // Methode om het spel te resetten
        public void ResetGame(decimal newBet)
        {
            DealerDeck.ResetDeck();
            PlayerDeck.ResetDeck();
            Status = GameStatus.Playing;
            Bet = newBet;
        }
    }
}
