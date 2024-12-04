using FinalProg3.Application.Interfaces;
using FinalProg3.Application.Models.Request;
using FinalProg3.Domain.Entities;
using FinalProg3.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetByName(string name)
        {
            return _userRepository.GetByName(name);
        }

        public int AddUser(UserAddRequest request)
        {

            if (_userRepository.ExistsByName(request.Name))
            {
                throw new ArgumentException($"El usuario con el nombre: '{request.Name}' ya existe.");
            }
            var obj = new User()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                UserType = request.UserType,
            };
            return _userRepository.AddUser(obj);
        }

        public bool DeleteUser(int id)
        {
            var sport = _userRepository.GetById(id);

            if (sport == null)
            {
                return false; // Retorna falso si no existe
            }

            _userRepository.DeleteUser(sport);
            return true;
        }
    }
}
