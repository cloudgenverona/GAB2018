using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAB.Entities
{
   public class NephewUI : ObservableObject
    {
        decimal amount;
        public decimal Amount
        {
            get
            {
                return amount;
            }
            set
            {
                Set(ref amount, value, "Amount");
            }
        }
    }
}
