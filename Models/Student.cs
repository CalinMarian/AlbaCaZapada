using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbaCaZapada.Models
{
    public class Student

    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nume obligatoriu")]
        [DisplayName("Nume elev")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Data nasterii obligatorie")]
        //[DataType(DataType.Date)]
        //[DisplayName("Data Nasterii")]
        //public DateTime? BirthDate { get; set; }

        [DisplayName("Parinte 1")]
        public string FirstParent { get; set; }

        [DisplayName("Parinte 2")]
        public string SecondParent { get; set; }

        [DisplayName("Alte detalii")]
        public string Details { get; set; }

        [DisplayName("Actualmente in gradinita")]
        public bool InSchool { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public bool Indebted { get; set; } = false;
        public double Balance { get; set; } = 0;

        [ForeignKey("Gruop")]
        public int GroupId { get; set; }

        [DisplayName("Grupa")]
        public virtual Group Group { get; set; }
    }

}
