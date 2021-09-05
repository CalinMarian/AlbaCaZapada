using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbaCaZapada.Models
{
    public class Student

    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nume obligatoriu")]
        [DisplayName("Nume elev")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CNP obligatoriu")]
        [Range(0,double.MaxValue)]
        public double CNP { get; set; }

        [Required(ErrorMessage = "Data nasterii obligatorie")]
        [DataType(DataType.Date)]
        [DisplayName("Data Nasterii")]
        public DateTime? BirthDate { get; set; }

        [DisplayName("Parinte 1")]
        public string FirstParent { get; set; }
        
        [DisplayName("Parinte 2")]
        public string SecondParent { get; set; }

        [DisplayName("Alte detalii")]
        public string Details { get; set; }

        public virtual IEnumerable<Payment> Payments { get; set; }

    }
   
}
