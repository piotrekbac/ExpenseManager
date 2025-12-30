using ExpenseManager;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Linq;
using System.Text;

// Piotr Bacior

namespace ExpenseManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ustawiam kodowanie konsoli na UTF-8, aby poprawnie wyświetlać znaki specjalne w języku polskim
            Console.OutputEncoding = Encoding.UTF8;

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
                    // Czyścę konsolę przed wyświetleniem menu, aby było czytelniej i schludniej
                    Console.Clear();

                    Console.WriteLine("=-=-=-=-= MENU PROGRAMU =-=-=-=-= ");
                    Console.WriteLine("1. Dodaj nowy wydatek.");
                    Console.WriteLine("2. Wyświetl wszystkie wydatki");
                    Console.WriteLine("3. Usuń wydatek");
                    Console.WriteLine("4. Edytuj wydatek");
                    Console.WriteLine("5. Raport kategorii");
                    Console.WriteLine("6. Eksportuj do pliku CSV");
                    Console.WriteLine("7. Wyjdź z programu");
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
                            ShowExpenses(db);
                        break;

                        // W przypadku wyboru "3", wywołuję funkcję DeleteExpense - usuwa wydatek z bazy danych
                        case "3": 
                                DeleteExpenses(db);
                        break;

                        // W przypadku wyboru "4", wywołuję funkcję EditExpense - edytuje wydatek w bazie danych
                        case "4":
                            EditExpenses(db);
                        break;

                        // W przypadku wyboru "5", wywołuję funkcję GetCategoryRaport - generuje raport kategorii wydatków
                        case "5":
                            GetCategoryRaport(db);
                            break;

                        // W przypadku wyboru "6", wywołuję funkcję ExportToCsv - eksportuje wydatki do pliku CSV
                        case "6":
                            ExportToCsv(db);
                        break;

                        // W przypadku wyboru "4", ustawiam zmienną exit na true, aby zakończyć pętlę i wyjść z programu
                        case "7":
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
            Console.WriteLine("\n =-=-=-=-= DODAWANIE WYDATKU =-=-=-=-=");
            Console.WriteLine("\nPodaj opis (np. Zakupy): \n");

            // Odczytuję opis wydatku od użytkownika i przechowuję go w zmiennej "description"
            string description = Console.ReadLine();

            // pobieram kwotę z zabezpieczeniami przed błednym wpisem
            decimal amount = 0;
            bool validAmount = false;

            // Pętla do momentu uzyskania prawidłowej kwoty
            while (!validAmount)
            {
                Console.WriteLine("____________________________");
                // Proszę użytkownika o podanie kwoty
                Console.WriteLine("\nPodaj kwotę (np. 25,30): \n");

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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nieprawidłowa kwota. Proszę podać dodatnią liczbę.");
                }
            }

            Console.WriteLine("____________________________");

            // Pobieram kategorię wydatku od użytkownika
            Console.WriteLine("\nPodaj kategorię (np. Jedzenie): \n");

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

            // Dodaję nowy wydatek do kontekstu bazy danych
            db.Expenses.Add(newExpense);

            // Zapisuję zmiany w bazie danych
            db.SaveChanges();

            Console.WriteLine("____________________________");
            Console.WriteLine("\nSukces! Wydatek został zapisany poprawnie.\n");
        }

        // Definiuję funkcję do wyświetlania wszystkich wydatków
        static void ShowExpenses(ExpenseContext db)
        {
            // Wyswietlam wszystkie wydatki z bazy danych
            Console.WriteLine("\n =-=-=-=-= LISTA WYDATKÓW =-=-=-=-=-=\n");

            // Tworzę listę wszystkich wydatków z bazy danych i przechowuję ją w zmiennej "expenses"
            var expenses = db.Expenses.ToList();

            // Sprawdzam, czy lista wydatków jest pusta
            if (expenses.Count == 0)
            {
                Console.WriteLine("Brak zapisanych wydatków.\n");
                return;
            }

            // Wyświetlam nagłówki kolumn dla listy wydatków
            Console.WriteLine("ID  |  Data      | Opis            | Kategoria  | Kwota ");
            Console.WriteLine("-----------------------------------------------------------");

            // Iteruję przez każdy wydatek w liście i wyświetlam jego szczegóły
            foreach (var expense in expenses)
            {
                Console.WriteLine($"{expense.Id, -3} | {expense.Date.ToShortDateString()} | {expense.Description, -15} | {expense.Category, -10} | {expense.Amount} zł");
            }

            Console.WriteLine("-----------------------------------------------------------\n");
        }

        // Definiuję funkcję do usuwania wydatków
        static void DeleteExpenses(ExpenseContext db)
        {
            // Pokazuję listę, żeby użytkownik mógł wybrać ID wydatku do usunięcia
            ShowExpenses(db);

            // Proszę użytkownika o podanie ID wydatku do usunięcia
            Console.WriteLine("Podaj ID wydatku do usunięcia: \n");

            // Odczytuję ID od użytkownika jako łańcuch znaków i przechowuję je w zmiennej "idString"
            string idString = Console.ReadLine();

            // Próbuję przekonwertować łańcuch na liczbę całkowitą
            if (int.TryParse(idString, out int id))
            {
                // Znajduję wydatek o podanym ID w bazie danych
                var expenseToDelete = db.Expenses.Find(id);

                // Jeśli wydatek został znaleziony, proszę o potwierdzenie usunięcia
                if (expenseToDelete != null)
                {
                    // Proszę o potwierdzenie usunięcia wydatku
                    Console.WriteLine($"Czy na pewno chcesz usunąć wydatek: {expenseToDelete.Description} ({expenseToDelete.Amount} zł)? (t/n)\n");

                    // Odczytuję odpowiedź użytkownika i przechowuję ją w zmiennej "confirm"
                    string confirm = Console.ReadLine();

                    // Jeśli użytkownik potwierdził usunięcie, usuwam wydatek z bazy danych
                    if (confirm.ToLower() == "t")
                    {
                        // Usuwam wydatek z kontekstu bazy danych i potem zapisuję zmiany
                        db.Expenses.Remove(expenseToDelete);
                        db.SaveChanges();

                        // Informuję użytkownika o pomyślnym usunięciu
                        Console.WriteLine("Usunięto pomyślnie.\n");
                    }

                    // Jeśli użytkownik anulował usunięcie, informuję go o tym
                    else
                    {
                        Console.WriteLine("Anulowano usunięcie.\n");
                    }
                }

                // Jeśli wydatek o podanym ID nie został znaleziony, informuję użytkownika
                else
                {
                    Console.WriteLine("Nie znaleziono wydatku o takim ID.\n");
                }
            }

            // Jeśli konwersja ID się nie powiodła, informuję użytkownika o błędzie
            else
            {
                Console.WriteLine("Niepoprawne ID. Spróbuj jeszcze raz.\n");
            }
        }

        // Definiuję funkcję do edytowania wydatków
        static void EditExpenses(ExpenseContext db)
        {
            Console.WriteLine("\n =-=-=-=-= EDYCJA WYDATKU =-=-=--=-=\n");

            // Pokazuję listę, żeby użytkownik mógł wybrać ID wydatku do edycji
            ShowExpenses(db);

            // Proszę użytkownika o podanie ID wydatku do edycji
            Console.WriteLine("Podaj ID wydatku do edycji: \n");

            // Próbuję przekonwertować wprowadzony tekst na liczbę całkowitą
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                // Dopasowuję wydatek o podanym ID z bazy danych i przechowuję go w zmiennej "expenseToEdit"
                var expenseToEdit = db.Expenses.Find(id);

                // Jeśli wydatek o podanym ID nie został znaleziony, informuję użytkownika i kończę funkcję
                if (expenseToEdit == null)
                {
                    Console.WriteLine("Nie znaleziono wydatku o takim ID. \n");
                    return;
                }

                // Proszę użytkownika o podanie nowych wartości dla opisu, kwoty i kategorii wydatku
                Console.WriteLine("Wpisz nową wartość lub naciśnij ENTER, aby zostawić starą wartość.\n");

                Console.WriteLine($"Nowy opis (obecnie: {expenseToEdit.Description}): \n");

                // Odczytuję nowy opis od użytkownika i przechowuję go w zmiennej "newDesc"
                string newDesc = Console.ReadLine();

                // Jeśli użytkownik wpisał nowy opis, aktualizuję właściwość Description wydatku
                if (!string.IsNullOrWhiteSpace(newDesc))
                {
                    // Aktualizuję opis wydatku na nową wartość
                    expenseToEdit.Description = newDesc;
                }

                // Proszę użytkownika o podanie nowej kwoty wydatku
                Console.WriteLine($"Nowa kwota (obecnie: {expenseToEdit.Amount}): \n");

                // Odczytuję nową kwotę od użytkownika jako łańcuch znaków
                string newAmountStr = Console.ReadLine();

                // Jeśli użytkownik wpisał nową kwotę, próbuję ją przekonwertować na liczbę dziesiętną i aktualizuję właściwość Amount wydatku
                if (!string.IsNullOrEmpty(newAmountStr))
                {
                    // Próbuję przekonwertować łańcuch na liczbę dziesiętną i sprawdzam, czy jest dodatnia
                    if (decimal.TryParse(newAmountStr, out decimal newAmount) && newAmount > 0)
                    {
                        // Aktualizuję kwotę wydatku na nową wartość
                        expenseToEdit.Amount = newAmount;
                    }

                    // Jeśli konwersja się nie powiodła lub kwota nie jest dodatnia, informuję użytkownika o błędzie
                    else
                    {
                        // Wyświetlam komunikat o błędzie, jeśli kwota jest nieprawidłowa
                        Console.WriteLine("Nieprawidłowa kwota. Edycja anulowana.\n");
                    }
                }

                // Proszę użytkownika o podanie nowej kategorii wydatku
                Console.WriteLine($"Nowa kategoria (obecnie: {expenseToEdit.Category}): \n");

                // Odczytuję nową kategorię od użytkownika i przechowuję ją w zmiennej "newCat"
                string newCat = Console.ReadLine();

                // Jeśli użytkownik wpisał nową kategorię, aktualizuję właściwość Category wydatku
                if (!string.IsNullOrWhiteSpace(newCat))
                {
                    // Aktualizuję kategorię wydatku na nową wartość
                    expenseToEdit.Category = newCat;
                }

                // Zapisuję zmiany w bazie danych
                db.SaveChanges();

                // Informuję użytkownika o pomyślnym zaktualizowaniu wydatku
                Console.WriteLine("Zaktualizowano pomyślnie!\n");
            }

            // Jeśli konwersja ID się nie powiodła, informuję użytkownika o błędzie
            else
            {
                Console.WriteLine("Niepoprawne ID.");
            }
        }

        // Definiuję funkcję do generowania raportu kategorii wydatków
        static void GetCategoryRaport(ExpenseContext db)
        {
            Console.WriteLine("\n=-=-=-=-= RAPORT KATEOGRII =-=-=-=-=\n");

            // Pobieramy wszystkie wydatki z bazy danych i przechowujemy je w zmiennej "allExpenses"
            var allExpenses = db.Expenses.ToList();

            // Grupuję wydatki według kategorii i obliczam łączną kwotę wydatków dla każdej kategorii w bieżącym miesiącu
            var raport = allExpenses
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    // Nazwa ketegorii, łączna kwota wydatków i liczba wydatków w tej kategorii
                    CategoryName = g.Key,
                    TotalAmount = g.Sum(e => e.Amount),
                    Count = g.Count()
                })
                // Konwertuję wynik na listę
                .ToList();

            // Itweruję przez każdą linię raportu i wyświetlam kategorię oraz łączną kwotę wydatków
            foreach (var group in raport)
            {
                Console.WriteLine($"Kategoria: {group.CategoryName}, Suma: {group.TotalAmount}, Ilość: {group.Count}");
            }

            Console.WriteLine("\n---------------------------------------\n");
            Console.WriteLine($"Łącznie wydano: {allExpenses.Sum(e => e.Amount)} zł\n\n\n");
        }

        // Definiuję funkcję do eksportu wydatków do pliku CSV 
        static void ExportToCsv(ExpenseContext db)
        {
            Console.WriteLine("\n =-=-=-=-= EKSPORT DO CSV =-=-=-=-=\n");

            // Pobieram wszystkie wydatki z bazy danych i przechowuję je w zmiennej "expenses"
            var expenses = db.Expenses.ToList();

            // Tworzę StringBuilder do budowania zawartości pliku CSV i przechowuję go w zmiennej "sb"
            var sb = new System.Text.StringBuilder();

            // Dodaję nagłówki kolumn do pliku CSV
            sb.AppendLine("Id,Data,Opis,Ilość,Kategoria, Kwota");

            // Iteruję przez każdy wydatek w liście i dodaję jego szczegóły do pliku CSV
            foreach (var e in expenses)
            {
                sb.AppendLine($"{e.Id},{e.Date.ToShortDateString()},{e.Description},{e.Amount},{e.Category}");
            }

            // Zapisuję zawartość StringBuilder do pliku o nazwie "expenses.csv"
            string fileName = "wydatki.csv";

            // Próbuję zapisać plik CSV i informuję użytkownika o sukcesie
            try
            {
                // Zapisuję zawartość StringBuilder do pliku CSV z kodowaniem UTF-8
                System.IO.File.WriteAllText(fileName, sb.ToString(), System.Text.Encoding.UTF8);

                // Wyświetlam komunikat o sukcesie
                Console.WriteLine($"Sukces! Dane zapisano w pliku: {fileName}");
                Console.WriteLine($"Ścieżka: {System.IO.Path.GetFullPath(fileName)}");
            }

            // Łapię wyjątki i informuję użytkownika o błędzie zapisu pliku
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd zapisu pliku: {ex.Message}");
            }
        }

        // Poniżej definiuję dodatkowe funkcje pomocnicze, w celu poprawy UX programu

        // Definiuję funkcję do wyświetlania nagłówków sekcji w konsoli
        static void PrintHeader(string title)
        {
            // Wyświetlam nagłówek sekcji z podanym tytułem w kolorze cyan 
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Wyświetlam tytuł sekcji otoczony dekoracyjnymi znakami
            Console.WriteLine($"=-=-=-=-=-=-= {title} =-=-=-=-=-=-=");

            // Resetuję kolor konsoli do domyślnego
            Console.ResetColor();

            // Wypisuję pustą linię dla lepszej czytelności
            Console.WriteLine();
        }

        // Definiuję funkcję do wyświetlania kolorowych komunikatów w konsoli
        static void PrintMessage(string message, ConsoleColor color)
        {
            // Ustawiam kolor tekstu w konsoli na podany kolor
            Console.ForegroundColor = color;

            // Wyświetlam podany komunikat w konsoli
            Console.WriteLine(message);

            // Resetuję kolor konsoli do domyślnego
            Console.ResetColor();
        }

        // Definiuję funkcję do oczekiwania na naciśnięcie klawisza przez użytkownika
        static void WaitForUser()
        {
            // Wyświetlam komunikat informujący użytkownika o naciśnięciu klawisza, aby kontynuować
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić do menu...");

            // Czekam na naciśnięcie klawisza przez użytkownika
            Console.ReadKey();
        }
    }
}
