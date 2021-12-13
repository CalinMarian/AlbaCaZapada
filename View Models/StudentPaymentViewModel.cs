using AlbaCaZapada.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace AlbaCaZapada.View_Models
{
    public class StudentPaymentViewModel
    {
        [DisplayName("Grupa")]
        public string Group { get; set; }
        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public IEnumerable<Payment> Payments { get; set; }
    }
}
