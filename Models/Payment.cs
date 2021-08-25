using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbaCaZapada.Models
{
    public class Payment
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        [DisplayName("Suma")]
        public double Amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Data platii")]
        public DateTime? PaymentDate { get; set; }

    }
}
