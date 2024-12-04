using FinalProg3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Models.Dto
{
    public class SportDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string SportName { get; set; }

        // Mapeo de entidad a DTO
        public static SportDto ToDTO(Sport entity)
        {
            return new SportDto
            {
                Id = entity.Id,
                SportName = entity.SportName
            };
        }

        public static List<SportDto> ToDTO(List<Sport> entities)
        {
            return entities.Select(ToDTO).ToList();  // Usa LINQ para simplificar
        }

        // Método inverso: Mapeo de DTO a entidad
        public static Sport ToEntity(SportDto dto)
        {
            return new Sport
            {
                Id = dto.Id,
                SportName = dto.SportName
            };
        }

        public static List<Sport> ToEntity(List<SportDto> dtos)
        {
            return dtos.Select(ToEntity).ToList();  // Usa LINQ para simplificar
        }
    }
}
