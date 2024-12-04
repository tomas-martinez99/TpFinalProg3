using FinalProg3.Application.Models.Dto;
using FinalProg3.Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Interfaces
{
    public interface ISportService
    {
        List<SportDto> GetAll();
        SportDto GetSportById(int id);
        SportDto GetSportByName(string name);
        void AddSport(AddSportRequest request);
        void UpdateSport(int id, UpdateSportRequest request);
        bool DeleteSport(int id);

    }
}
