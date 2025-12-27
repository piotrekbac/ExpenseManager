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
            Console.WriteLine("Menadżer wydatków v1.0- Start Programu\n");
            Console.WriteLine("Autor - Piotr Bacior\n");

            // Tworzę kontekst bazy danych
            using (var db = new ExpenseContext())
            {
                // Upewniam się, że baza danych jest utworzona
                db.Database.EnsureCreated();

                // Tworzę zmienną sterującą pętlą menu o nazwie "exit"
                bool exit = false;

                // Główna pętla programu 
                while (!exit)
                {
                    Console.WriteLine("=-=-=-=-= MENU PROGRAMU =-=-=-=-= ");
                    Console.WriteLine("1. Dodaj nowy wydatek.");
                    Console.WriteLine("2. Wyświetl wszystkie wydatki");
                    Console.WriteLine("3. Wyjdź z programu");
                    Console.Write("Wybierz opcję: ");

                    string input = Console.ReadLine();

                    
                }
            }

                // Czekam na naciśnięcie klawisza przed zamknięciem programu
                Console.WriteLine("\nNaciśnij dowolny klawisz, aby zamknąć...");
            Console.ReadKey();
        }
    }
}
