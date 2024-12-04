using FinalProg3.Domain.Entities;
using FinalProg3.Domain.Interfaces;
using FinalProg3.Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Infraestructure.Repositories
{
    public class SportRepository: ISportRepository
    {
        private readonly ApplicationDbContext _context;
        public SportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Sport> GetAll()
        {
            return _context.Sports.ToList();
        }
        public Sport? GetSportById(int id)
        {
            return _context.Sports.FirstOrDefault(p => p.Id == id);
        }
        public Sport? GetSportByName(string name)
        {
            return _context.Sports.FirstOrDefault(p => p.SportName == name);
        }

        public void AddSport(Sport sport)
        {
            _context.Sports.Add(sport);
            _context.SaveChanges();
        }

        public void UpdateSport(Sport sport)
        {
            _context.Sports.Update(sport);
            _context.SaveChanges();
        }
        public void DeleteSport(Sport sport)
        {
            _context.Sports.Remove(sport);
            _context.SaveChanges();
        }

        public bool ExistsByName(string name)
        {
            return _context.Sports.Any(s => s.SportName == name);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
