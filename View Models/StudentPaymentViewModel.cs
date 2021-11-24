using AlbaCaZapada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbaCaZapada.View_Models
{
    public class StudentPaymentViewModel
    {
        public List<Student> Students { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
