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
        
        [DisplayName("Nume grupa")]
        public string GroupName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
