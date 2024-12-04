using FinalProg3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Models.Dto
{
    public class ClassDto
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del deporte es obligatorio.")]
        public string Sport { get; set; }

        [Required(ErrorMessage = "El horario es obligatorio.")]
        public TimeSpan Schedule { get; set; }
        public static ClassDto ToDTO(Class entity)
        {
            ClassDto dto = new ClassDto();

            dto.Id = entity.Id;
            dto.Sport = entity.Sport?.SportName ?? "Deporte desconocido";
            dto.Schedule = entity.Schedule;
            return dto;
        }

        public static List<ClassDto> ToDTO(List<Class> entities)
        {
            List<ClassDto> listClassDto = new List<ClassDto>();


            foreach (var item in entities)
            {
                listClassDto.Add(ToDTO(item));
            }

            return listClassDto;

        }

        public static ClassDto ToUpdateDTO(Class entity)
        {
            ClassDto dto = new ClassDto();

            dto.Id = entity.Id;
            dto.Sport = entity.Sport?.SportName ?? "Deporte desconocido";
            dto.Schedule = entity.Schedule;
            return dto;

        }
    }
}
