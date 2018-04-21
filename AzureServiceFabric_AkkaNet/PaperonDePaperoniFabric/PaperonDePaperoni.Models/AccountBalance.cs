using System;
using System.Collections.Generic;

namespace PaperonDePaperoni.Models
{
    /// <summary>
    /// Storico dei movimenti bancari
    /// </summary>
    public class AccountBalance
    {
        public string BankAccount { get; set; }
        public string IBAN { get; set; }
        public List<BankRecords> Records { get; set; } = new List<BankRecords>();
    }

    /// <summary>
    /// Singolo movimento bancario
    /// </summary>
    public class BankRecords
    {
        public decimal CurrentMoney { get; set; }
        public DateTime Date { get; set; }
    }
}
