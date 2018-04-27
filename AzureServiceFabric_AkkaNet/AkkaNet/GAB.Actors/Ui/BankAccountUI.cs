using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAB.Entities
{
   public class BankAccountUI : ObservableObject
    {
        decimal balance;
        public decimal Balance
        {
            get
            {
                return balance;
            }
            set
            {
                Set(ref balance, value, "Balance");
            }
        }
    }
   
}
