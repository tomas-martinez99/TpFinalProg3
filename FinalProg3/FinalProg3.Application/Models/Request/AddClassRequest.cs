using FinalProg3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Models.Request
{
    public class AddClassRequest
    {
        [Required]
        [MaxLength(20)]
        public string Sport { get; set; }
        [Required]
        [RegularExpression(@"^(?:[01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "La hora debe estar en formato hh:mm.")]
        public string Schedule { get; set; }

    }
}
