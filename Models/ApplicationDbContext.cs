using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PRG.EVA.BlackJack.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Eigenschap om de lijst van GameLog objecten te beheren
        public DbSet<GameLog> GameLogs { get; set; }
        public DbSet<Card> Card { get; set; }
        // public DbSet<Deck> Deck { get; set; }

        // Constructor met DbContextOptions parameter
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)  // Aanroepen van de base class constructor met de meegegeven opties
        {
        }
    }
}