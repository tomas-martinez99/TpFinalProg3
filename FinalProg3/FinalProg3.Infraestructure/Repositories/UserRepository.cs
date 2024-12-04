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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User? GetById(int id)
        {
            return _context.Users.SingleOrDefault(u => u.Id == id);
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

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();

        }

        public bool ExistsByName(string name)
        {
            return _context.Users.Any(u => u.Name == name);
        }
    }

}
