using ExpenseManager;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Linq;

// Piotr Bacior

namespace ExpenseManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Witam się z użytkownikiem 
            Console.WriteLine("Menadżer wydatków - Start Programu\n");
            Console.WriteLine("Autor - Piotr Bacior\n");

            using (var db = new ExpenseContext())
            {
                if (db.Database.CanConnect())
                {
                    Console.WriteLine("Sukces! Połączono z bazą danych.");

                    int count = db.Expenses.Count();
                    Console.WriteLine($"Aktualnie w bazie jest {count} wydatków.");
                }

                else
                {
                    Console.WriteLine("Błąd! Nie udało się połączyć z bazą.");
                }
            }

            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }
    }
}
