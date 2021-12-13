using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AlbaCaZapada.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nume grupa")]
        public string GroupName { get; set; }

        [DisplayName("Grupa activa")]
        public bool IsActive { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
