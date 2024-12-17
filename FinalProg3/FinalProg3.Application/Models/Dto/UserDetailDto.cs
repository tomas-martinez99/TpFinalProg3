using FinalProg3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Models.Dto
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<ClassDto> classStudentInscripted { get; set; }
        public List<ClassDto> classTeacherInscripted { get; set; }


        public static UserDetailDto FromEntity(User user)
        {
            if (user == null) return null;
            return new UserDetailDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                classStudentInscripted = user.StudentClasses?.Select(c => new ClassDto
                {
                    Id = c.Id,
                    Sport = c.Sport?.SportName ?? "Deporte desconocido",
                    Schedule = c.Schedule,
                }).ToList() ?? new List<ClassDto>(),
                classTeacherInscripted = user.TeacherClasses?.Select(c => new ClassDto
                {
                    Id = c.Id,
                    Sport = c.Sport?.SportName ?? "Deporte desconocido",
                    Schedule = c.Schedule,
                }).ToList() ?? new List<ClassDto>(),
            };
        }
    }

        
}
