using System;
namespace PRG.EVA.BlackJack.Models
{
    public class GameLog
    {
        public int Id { get; set; }  // Unieke id van de GameLog entiteit

        public string PlayOption { get; set; }  // Gekozen optie door de speler (Hit of Stand)

        public string CardSuit { get; set; }  // Soort van de getrokken kaart (indien van toepassing)

        public string CardRank { get; set; }  // Waarde van de getrokken kaart (indien van toepassing)

        public decimal CardTotal { get; set; }  // Totaal van de kaarten van de speler

        public decimal Wins { get; set; }  // Waarde van de uitbetaling (indien gelijkspel of winst)

        public string Result { get; set; }  // Resultaat van de poging (Won, Lost, Draw, etc.)

        public DateTime CreatedOn { get; set; } = DateTime.Now;  // Tijdstip van de log creatie
    }
}
