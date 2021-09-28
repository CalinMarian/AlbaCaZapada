using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlbaCaZapada.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Suma obligatorie")]
        [DisplayName("Suma (RON)")]
        public double Amount { get; set; }
        
        [Required(ErrorMessage = "Data obligatorie")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data platii")]
        public DateTime PaymentDate { get; set; }

        [DisplayName("Nr. Zile Prezenta")]
        public int DaysInSchool { get; set; }

        [DisplayName("Nr. Zile Absenta")]
        public int DaysOutSchool { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

    }
}
