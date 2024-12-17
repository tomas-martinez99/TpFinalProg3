using FinalProg3.Application.Interfaces;
using FinalProg3.Application.Models.Dto;
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
        public UserDetailDto GetById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return null;

            return UserDetailDto.FromEntity(user);
        }

        public UserDto GetByName(string name)
        {
            var user = _userRepository.GetByName(name);
            return UserDto.FromEntity(user);
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
          var a= _userRepository.AddUser(obj);
            _userRepository.SaveChanges();
            return a;
            
        }

        public void UpdateUser (int id, UpdateUserRequest request)
        {
            var user = _userRepository.GetById(id);
            if ( user == null)
            {
                throw new ArgumentException("Usuario inexistente");
            }
            if (user != null)
            {
                user.Name= request.Name;
                user.Email= request.Email;
                user.Password= request.Password;
                user.UserType = request.UserType;
                _userRepository.UpdateUser(user);
                _userRepository.SaveChanges();
            }
            
        }

        public bool DeleteUser(int id)
        {
            var sport = _userRepository.GetById(id);

            if (sport == null)
            {
                return false; // Retorna falso si no existe
            }

            _userRepository.DeleteUser(sport);
            _userRepository.SaveChanges();
            return true;
        }
    }
}
