using FinalProg3.Application.Models.Dto;
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
        UserDetailDto? GetById(int id);
        int AddUser(UserAddRequest user);

        UserDto? GetByName(string name);

        void UpdateUser(int id, UpdateUserRequest request);

        bool DeleteUser(int id);
    }
}
