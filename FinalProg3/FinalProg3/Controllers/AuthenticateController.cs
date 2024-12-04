using FinalProg3.Application.Interfaces;
using FinalProg3.Application.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FinalProg3.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICustomAuthenticateService _customAuthenticateService;

        public AuthenticateController(IConfiguration config, ICustomAuthenticateService customAuthenticateService)
        {
            _config = config;
            _customAuthenticateService = customAuthenticateService;
        }

        [HttpPost] //Vamos a usar un POST ya que debemos enviar los datos para hacer el login
        public ActionResult<string> Autenticar(AuthenticateRequest authenticationRequest) //Enviamos como parámetro la clase que creamos arriba
        {
            string? token = _customAuthenticateService.Authenticate(authenticationRequest); //Lo primero que hacemos es llamar a una función que valide los parámetros que enviamos.

            if (token.IsNullOrEmpty()) return BadRequest("Incorrect user or password");
            return Ok(token);
        }

    }
}
