using FinalProg3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Domain.Interfaces
{
    public interface IClassRepository
    {
        List<Class> GetAll();
        List<Class> GetClassBySport(string sportName);
        Class? GetClassById(int id);
       // Class? GetClassBySchedule(DateTime Schedule);
        void AddClass(Class clas);
        void UpdateClass(Class clas);
        void DeleteClass(Class clas);
        bool ExistsBySportAndSchedule(string sportName, TimeSpan schedule);
        void SaveChanges();
    }
}
