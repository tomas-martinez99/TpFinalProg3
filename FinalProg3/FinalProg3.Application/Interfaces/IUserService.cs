using FinalProg3.Application.Models.Request;
using FinalProg3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Interfaces
{
    public interface IUserService
    {
        User? GetById(int id);
        int AddUser(UserAddRequest user);

        User? GetByName(string name);

        bool DeleteUser(int id);
    }
}
