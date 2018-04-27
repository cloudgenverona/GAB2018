using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAB.Entities
{
    public class BeagleBoyCavernUI : ObservableObject
    {
        decimal roberyAmount;

        public decimal RoberyAmount
        {
            get
            {
                return roberyAmount;
            }
            set
            {
                Set(ref roberyAmount, value, "RoberyAmount");
            }
        }
    }
}
