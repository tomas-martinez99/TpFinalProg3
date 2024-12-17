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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User? GetById(int id)
        {
            return _context.Users
         .Include(u => u.StudentClasses) // Cargar clases donde el usuario es estudiante
         .Include(u => u.TeacherClasses) // Cargar clases donde el usuario es profesor
         .ThenInclude(c => c.Sport)      // Cargar el deporte asociado a las clases (si aplica)
         .SingleOrDefault(u => u.Id == id);
        }
        public User? GetByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Name == name);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();

        }

        public bool ExistsByName(string name)
        {
            return _context.Users.Any(u => u.Name == name);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

}
