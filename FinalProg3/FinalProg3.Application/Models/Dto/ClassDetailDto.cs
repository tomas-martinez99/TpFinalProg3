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



        public static ClassDetailDto FromEntity(Class clas)
        {
            if (clas == null) return null;

            return new ClassDetailDto
            {
                Id = clas.Id,
                SportName = clas.Sport?.SportName ?? "No definido",
                Schedule = clas.Schedule,
                Students = clas.Students.Select(s => new UserDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email
                }).ToList(),
                Teachers = clas.Teachers.Select(t => new UserDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Email = t.Email
                }).ToList()
            };
        }
    }
}
