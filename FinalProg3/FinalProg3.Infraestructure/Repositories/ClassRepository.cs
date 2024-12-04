using FinalProg3.Domain.Entities;
using FinalProg3.Domain.Interfaces;
using FinalProg3.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Infraestructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public List<Class> GetAll()
        {
            return _context.Classs.Include(c => c.Sport).ToList();
        }
        public Class? GetClassById(int id)
        {
            return _context.Classs.Include(c => c.Sport) // Incluye el deporte relacionado
        .Include(c => c.Students) // Incluye los estudiantes relacionados
        .Include(c => c.Teachers) // Incluye los profesores relacionados
        .FirstOrDefault(c => c.Id == id);
        }
        public List<Class> GetClassBySport(string sportName)
        {
            return _context.Classs.Include(p => p.Sport).Where(p=>p.Sport.SportName == sportName).ToList();
        }

        public void AddClass(Class clas)
        {
            _context.Classs.Add(clas);
            _context.SaveChanges();
        }

        public void UpdateClass(Class clas)
        {
            _context.Classs.Update(clas);
            _context.SaveChanges();
        }
        public void DeleteClass(Class clas)
        {
            _context.Classs.Remove(clas);
            _context.SaveChanges();
        }

        public bool ExistsBySportAndSchedule(string sportName, TimeSpan schedule)
        {
            return _context.Classs.Any(c => c.Sport.SportName == sportName && c.Schedule == schedule);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }

}
