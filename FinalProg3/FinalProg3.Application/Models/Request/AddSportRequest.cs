﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Models.Request
{
    public class AddSportRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
