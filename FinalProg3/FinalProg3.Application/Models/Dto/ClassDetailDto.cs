using FinalProg3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Models.Dto
{
    public class ClassDetailDto
    {
        public int Id { get; set; }
        public string SportName { get; set; }
        public TimeSpan Schedule { get; set; }
        public List<UserDto> Students { get; set; }
        public List<UserDto> Teachers { get; set; }

        
        
    }
}
