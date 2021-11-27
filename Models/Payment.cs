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

        public int Fee = 10;

        [DisplayName("Suma (RON)")]
        public double Amount { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data platii")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Luna obligatorie")]
        [DisplayName("Luna")]
        public string Month { get; set; }

        [Required(ErrorMessage = "Zile obligatorii")]
        [DisplayName("Zile lucratoare")]
        public int WorkingDaysInMonth { get; set; }

        [DisplayName("Zile Prezenta")]
        public int DaysInSchool { get; set; }

        [DisplayName("Zile Absenta")]
        public int DaysOutSchool 
        { 
            get { return WorkingDaysInMonth - DaysInSchool; } 
        }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

    }

}
