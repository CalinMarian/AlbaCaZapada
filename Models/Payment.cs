using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbaCaZapada.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Taxa obligatorii")]
        [DisplayName("Taxa/zi")]
        public decimal Fee { get; set; }

        [Required(ErrorMessage = "Suma obligatorie")]
        [DisplayName("Suma (RON)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Data platii este obligatorie")]
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
        public int DaysOutSchool { get; set; }

        [DisplayName("Suma datorata")]
        public decimal AmountOwed { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

    }

}
