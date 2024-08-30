using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
namespace PRG.EVA.BlackJack.Models
{
    public class GameLog
    {
        public int Id { get; set; }
        public string PlayOption { get; set; }
        public string CardSuit { get; set; }
        public string CardRank { get; set; }
        public decimal CardTotal { get; set; } 
        public decimal Wins { get; set; }
        public string Result { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
       }
}