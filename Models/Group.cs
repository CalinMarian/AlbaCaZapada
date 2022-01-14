using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AlbaCaZapada.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nume grupa obligatorie")]
        [DisplayName("Nume grupa")]
        public string GroupName { get; set; }

        [DisplayName("Grupa activa")]
        public bool IsActive { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
