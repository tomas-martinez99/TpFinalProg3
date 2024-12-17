using FinalProg3.Application.Interfaces;
using FinalProg3.Application.Models.Request;
using FinalProg3.Application.Services;
using FinalProg3.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProg3.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        
        public ActionResult GetByName(string name)
        {
            var user = _userService.GetByName(name);
            if (user == null) return NotFound("Usuario no encontrado.");
            return Ok(user);
        }
        [HttpGet]
        public ActionResult GetById (int id)
        {
            var userDetail = _userService.GetById(id);
            if (userDetail == null) return NotFound("Usuario no encontrado.");
            return Ok(userDetail);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserAddRequest request)
        {
            try
            {
                var sport = _userService.AddUser(request);
                return Ok(sport);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);  // Responder con un 404 si no se encuentra el deporte
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
           
        }

        [HttpPut]
        public ActionResult Update(int id, UpdateUserRequest request)
        {
            try
            {
                _userService.UpdateUser(id, request);
                return Ok("Usuario actualizado");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        [HttpDelete]
        public ActionResult DeleteUser(int id)
        {
            var del = _userService.DeleteUser(id);
            if (del == false) return NotFound();
            return NoContent();
        }
    }
}
