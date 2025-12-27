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

            // Dokonuję refaktoryzacji kodu, aby wymuszał utworzenie bazy danych i wyświetlanie odpowiednich komunikatów o błedach

            // Próbuję połączyć się z bazą danych i wyświetlić odpowiednie komunikaty
            try
            {
                // Tworzę kontekst bazy danych i sprawdzam połączenie
                using (var db = new ExpenseContext())
                {
                    // Upewniam się, że baza danych jest utworzona - jeśli nie istnieje, zostanie utworzona
                    db.Database.EnsureCreated();

                    // Sprawdzam, czy mogę połączyć się z bazą danych
                    if (db.Database.CanConnect())
                    {
                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");
                        Console.WriteLine("Sukces! Połączono z bazą danych.\n");
                        Console.WriteLine($"Lokalizacja bazy: {System.IO.Path.GetFullPath("expences.db")}\n");

                        // Liczenie i wyświetlanie liczby wydatków w bazie danych
                        int count = db.Expenses.Count();
                        Console.WriteLine($"Aktualnie w bazie jest {count} wydatków.\n");
                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");
                    }

                    // Jeśli nie udało się połączyć, wyświetlam odpowiedni komunikat
                    else
                    {
                        Console.WriteLine("Błąd. CanConnect zwróciło false, ale nie rzuciło żadnego wyjątku.");
                    }
                }
            }

            // Łapię wszelkie wyjątki i wyświetlam komunikaty o błędach
            catch (Exception ex)
            {
                // Wyświetlam komunikat o błędzie w kolorze czerwonym 
                Console.ForegroundColor = ConsoleColor.Red;                 
                Console.WriteLine("UWAGA! Wystąpił krytyczyny błąd: ");
                Console.WriteLine(ex.Message);

                // Jeśli istnieje wyjątek wewnętrzny, wyświetlam jego szczegóły
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Szczegóły błędu wewnętrznego: ");
                    Console.WriteLine(ex.InnerException.Message);
                }

                Console.ResetColor();       // Resetuję kolor konsoli do domyślnego
            }

            // Czekam na naciśnięcie klawisza przed zamknięciem programu
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zamknąć...");
            Console.ReadKey();
        }
    }
}
