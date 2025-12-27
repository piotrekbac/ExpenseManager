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

            // Tworzę instancję kontekstu bazy danych i sprawdzam połączenie
            using (var db = new ExpenseContext())
            {
                // jeżeli połączenie się powiedzie, wyświetlam komunikat o połączeniu
                if (db.Database.CanConnect())
                {
                    Console.WriteLine("Sukces! Połączono z bazą danych.");

                    // definiuję zmienną count, która przechowuje liczbę wydatków w bazie danych
                    int count = db.Expenses.Count();
                    Console.WriteLine($"Aktualnie w bazie jest {count} wydatków.");
                }

                // jeżeli połączenie się nie powiedzie, wyświetlam komunikat o błędzie 
                else
                {
                    Console.WriteLine("Błąd! Nie udało się połączyć z bazą.");
                }
            }

            // Czekam na naciśnięcie klawisza przez użytkownika przed zakończeniem programu
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }
    }
}
