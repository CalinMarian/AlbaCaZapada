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
        [Required]
        [DisplayName("Nume elev")]
        public string Name { get; set; }
        [Required]
        public int CNP { get; set; }

    }

}
