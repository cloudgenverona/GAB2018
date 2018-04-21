using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaperonDePaperoni.Web.Models
{
    public class CurrentActorStateViewModel
    {
        [DataType(DataType.Currency)]
        public decimal Bank { get; set; }
        [DataType(DataType.Currency)]
        public decimal ZioPaperone { get; set; }
        [DataType(DataType.Currency)]
        public decimal BandaBassotti { get; set; }
        [DataType(DataType.Currency)]
        public decimal Qui { get; set; }
        [DataType(DataType.Currency)]
        public decimal Quo { get; set; }
        [DataType(DataType.Currency)]
        public decimal Qua { get; set; }
    }
}
