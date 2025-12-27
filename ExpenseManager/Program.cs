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

                    // Odczytuję wybór użytkownika i zapisuję go do zmiennej "input"
                    string input = Console.ReadLine();

                    // Przetwarzam wybór użytkownika za pomocą instrukcji switch
                    switch (input)
                    {
                        // W przypadku wyboru "1", wywołuję funkcję AddExpense
                        case "1":
                            AddExpense(db);
                        break;

                        // W przypadku wyboru "2", wyświetlam wszystkie wydatki z bazy danych
                        case "2":
                            Console.WriteLine("");
                        break;

                        // W przypadku wyboru "3", ustawiam zmienną exit na true, aby zakończyć pętlę i wyjść z programu
                        case "3": 
                        exit = true;    
                        break;

                        // W przypadku nieznanej opcji, informuję użytkownika
                        default:
                            Console.WriteLine("Nieznana opcja, spróbuj innych dostępnych opcji.");
                        break;
                    }

                }
            }
        }

        // Teraz przechodzimy do zdefiniowania funkcji pomocniczych 
        static void AddExpense(ExpenseContext db)
        {
            // Dodaję nowy wydatek do bazy danych
            Console.WriteLine("\n =-=-=-=-= DODAWANIE WYDATKU =-=-=-=-=\n");
            Console.WriteLine("Podaj opis (np. Zakupy): \n");

            // Odczytuję opis wydatku od użytkownika i przechowuję go w zmiennej "description"
            string description = Console.ReadLine();

            // pobieram kwotę z zabezpieczeniami przed błednym wpisem
            decimal amount = 0;
            bool validAmount = false;

            // Pętla do momentu uzyskania prawidłowej kwoty
            while (!validAmount)
            {
                // Proszę użytkownika o podanie kwoty
                Console.WriteLine("Podaj kwotę (np. 25,30): \n");

                // Odczytuję kwotę od użytkownika jako łańcuch znaków
                string amountInput = Console.ReadLine();

                // Próbuję przekonwertować łańcuch na liczbę dziesiętną i sprawdzam, czy jest dodatnia
                if (decimal.TryParse(amountInput, out amount) && amount > 0)
                {
                    validAmount = true;
                }

                // Jeśli konwersja się nie powiodła lub kwota nie jest dodatnia, informuję użytkownika o błędzie
                else
                {
                    Console.WriteLine("Nieprawidłowa kwota. Proszę podać dodatnią liczbę.");
                }
            }

            // Pobieram kategorię wydatku od użytkownika
            Console.WriteLine("Podaj kategorię (np. Jedzenie): ");

            // Odczytuję kategorię od użytkownika i przechowuję ją w zmiennej "category"
            string category = Console.ReadLine();

            // Tworzę nowy obiekt Expense z podanymi danymi
            var newExpense = new Expense
            {
                Date = DateTime.Now,
                Description = description,
                Amount = amount,
                Category = category
            };
        }
    }
}
