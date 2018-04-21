using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaperonDePaperoni.Web.Models
{
    public class BankChangesViewModel
    {
        [Range(0.0, Double.MaxValue), DataType(DataType.Currency)]
        public decimal Deposit { get; set; }

        [Range(0.0, Double.MaxValue), DataType(DataType.Currency)]
        public decimal Withdraw { get; set; }

        public CurrentActorStateViewModel CurrenteActorState { get; set; }
    }
}
