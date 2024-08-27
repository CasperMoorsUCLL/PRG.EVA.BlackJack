using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
namespace PRG.EVA.BlackJack.Models
{
    public class GameLog
    {
        public int Id { get; set; }  // GameLog id

        public string PlayOption { get; set; }  // Gekozen optie(Hit of Stand)

        public string CardSuit { get; set; }  // Soort van de kaart

        public string CardRank { get; set; }  // Waarde van de kaart

        public decimal CardTotal { get; set; }  // Totaal van de kaarten van de speler

        public decimal Wins { get; set; }  // Waarde van de uitbetaling

        public string Result { get; set; }  // Resultaat van de poging

        public DateTime CreatedOn { get; set; } = DateTime.Now;  // Tijdstip van de log creatie

       }

}
