using AlbaCaZapada.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AlbaCaZapada.View_Models
{
    public class StudentPaymentViewModel
    {
        [DisplayName("Grupa")]
        public string Group { get; set; }
        public IEnumerable<SelectListItem> Groups { get; set; }

        public IEnumerable<Student> Students { get; set; }
        
        public IEnumerable<Payment> Payments { get; set; }
    }
}
