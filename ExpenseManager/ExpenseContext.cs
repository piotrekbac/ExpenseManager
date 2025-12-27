using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Piotr Bacior

namespace ExpenseManager
{
    // ExpenseContext - kontekst bazy danych dla zarządzania wydatkami
    public class ExpenseContext : DbContext
    {
        // DbSet reprezentujący kolekcję wydatków w bazie danych 
        public DbSet<Expense> Expenses { get; set; }

        // Konfiguracja kontekstu bazy danych
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Użycie bazy danych SQLite, będzie zapisywana w pliku o nazwie expenses.db 
            optionsBuilder.UseSqlite("Data Source=expenses.db");
        }
    }
}
