using FinalProg3.Application.Interfaces;
using FinalProg3.Application.Models.Dto;
using FinalProg3.Application.Models.Request;
using FinalProg3.Domain.Entities;
using FinalProg3.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Services
{
    public class SportService : ISportService
    {
        private readonly ISportRepository _sportRepository;
        public SportService(ISportRepository sportRpository)
        {
            _sportRepository = sportRpository;
        }

        //Metodo 1 Traer todos los deportes
        public List<SportDto> GetAll()
        {
            var sports = _sportRepository.GetAll();
            return SportDto.ToDTO(sports);
        }

        //Metodo 2 Traaer un deporte por su id

        public SportDto GetSportById(int id)
        {
            var sport = _sportRepository.GetSportById(id);

            if (sport == null)
            {
                throw new KeyNotFoundException($"Deporte con id: {id} no encontrado.");
            }

            return SportDto.ToDTO(sport);
        }

        //Metodo 3 Traer un deporte por su Nombre

        public SportDto GetSportByName(string name)
        {
            var sport = _sportRepository.GetSportByName(name);

            if (sport != null)
            {
                return SportDto.ToDTO(sport);
            }
            return null;
        }
        // Metodo 4 Agregar un deporte

        public void AddSport(AddSportRequest request)
        {
            if (_sportRepository.ExistsByName(request.Name))
            {
                throw new ArgumentException($"El deporte'{request.Name}' ya existe.");
            }

            var sport = new Sport
            {
                SportName = request.Name
            };

            _sportRepository.AddSport(sport);
        }

        // Metodo 5 Actualizar una clase
        public void UpdateSport(int id, UpdateSportRequest request)
        {
            var sport = _sportRepository.GetSportById(id);

            if (sport == null)
            {
                throw new KeyNotFoundException($"Deporte con id: {id} no encontrado.");
            }

            sport.SportName = request.Name;

            _sportRepository.UpdateSport(sport);
        }
        //Metodo 6 Borrar una clase
        public bool DeleteSport(int id)
        {
            var sport = _sportRepository.GetSportById(id);

            if (sport == null)
            {
                return false; // Retorna falso si no existe
            }

            _sportRepository.DeleteSport(sport);
            return true;
        }


    }
}
