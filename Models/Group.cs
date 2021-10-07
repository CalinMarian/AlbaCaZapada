using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbaCaZapada.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Grupa obligatorie")]
        [DisplayName("Grupa")]
        public string GroupName { get; set; }

        public virtual List<Student> Students { get; set; }
    }
}
