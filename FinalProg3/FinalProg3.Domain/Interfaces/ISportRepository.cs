using FinalProg3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Domain.Interfaces
{
    public interface ISportRepository
    {
        List<Sport> GetAll();
        Sport? GetSportByName(string sportname);
        Sport? GetSportById(int id);
        void AddSport(Sport sport);
        void UpdateSport(Sport sport);
        void DeleteSport(Sport sport);

        bool ExistsByName(string name);

        void SaveChanges();
    }
}
