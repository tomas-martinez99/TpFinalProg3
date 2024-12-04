using FinalProg3.Application.Interfaces;
using FinalProg3.Application.Models.Request;
using FinalProg3.Domain.Entities;
using FinalProg3.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Services
{
    public class AuthenticateService : ICustomAuthenticateService
    {
        private readonly IUserRepository _userRepository;
        private readonly AutenticacionServiceOptions _options;


        public AuthenticateService(IUserRepository userRepository, IOptions<AutenticacionServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        public User? ValidateUser(AuthenticateRequest authenticateRequest)
        {
            if (string.IsNullOrEmpty(authenticateRequest.Name) || string.IsNullOrEmpty(authenticateRequest.Password))
            {
                return null;
            }

            var user = _userRepository.GetByName(authenticateRequest.Name);

            if (authenticateRequest.Name == user.Name && authenticateRequest.Password == user.Password)
            {
                return user;
            }

            return null;
        }

        public string? Authenticate(AuthenticateRequest authenticateRequest)
        {
            var user = ValidateUser(authenticateRequest);

            if (user == null)
            {
                return null;
            }

            //Paso 2: Crear el token
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey)); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            //Los claims son datos en clave -> valor que nos permite guardar data del usuario.
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString())); //"sub" es una key estándar que significa unique user identifier, es decir, si mandamos el id del usuario por convención lo hacemos con la key "sub".
            claimsForToken.Add(new Claim("given_name", user.Name));
            claimsForToken.Add(new Claim(ClaimTypes.Role, user.UserType.ToString())); //quiere usar la API por lo general lo que espera es que se estén usando estas keys.

            var jwtSecurityToken = new JwtSecurityToken( //agregar using System.IdentityModel.Tokens.Jwt; Acá es donde se crea el token con toda la data que le pasamos antes.
            _options.Issuer,
            _options.Audience,
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            credentials);
            //Pasamos el token a string
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();
        }
        public class AutenticacionServiceOptions
        {
            public const string AutenticacionService = "AutenticacionService";

            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
    }
}
