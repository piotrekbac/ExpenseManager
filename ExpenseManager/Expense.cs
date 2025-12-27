using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager
{
    // Zaczynam od klasy Expense - reprezentuje pojedynczy wydatek w systemie zarządzania wydatkami 
    public class Expense
    {
        // poniżej znajdują się właściwości klasy Expense 
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }     // Using decimal for currency representation 
        public string Category { get; set; }
    }
}
