using FinalProg3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Domain.Interfaces
{
    public interface IUserRepository
    {
        User? GetById(int  id);
        int AddUser(User user);
        User? GetByName(string name);
        bool ExistsByName(string name);

        void UpdateUser(User user);
        void DeleteUser(User User);
        void SaveChanges();
    }
}
