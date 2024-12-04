using FinalProg3.Application.Interfaces;
using FinalProg3.Application.Models.Dto;
using FinalProg3.Application.Models.Request;
using FinalProg3.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProg3.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly ISportService _portService;
        public SportController(ISportService portService)
        {
            _portService = portService;
        }

        [HttpGet]
        public ActionResult <List<SportDto>> GetAll()
        {
            return Ok(_portService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult GetSportById(int id)
        {
            try
            {
                var sport = _portService.GetSportById(id);
                return Ok(sport);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);  // Responder con un 404 si no se encuentra el deporte
            }
        }

        [HttpGet("{name}")]

        public ActionResult GetSportByName(string name)
        {
            var sport = _portService.GetSportByName(name);
            if (sport == null) return NotFound("Deporte no encontrado");
            return Ok(sport);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddSport([FromBody]AddSportRequest request)
        {
            try
            {
                _portService.AddSport(request);
                return Ok("Deporte creado correctamente");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);  // Responder con un 404 si no se encuentra el deporte
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult UpdateSport(int id,UpdateSportRequest request)
        {
            try
            {
                _portService.UpdateSport(id, request);
                return Ok("Actualizacion relizada correctamente");

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }


        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult DeleteSport(int id)
        {
            var del = _portService.DeleteSport(id);
            if (del == false) return NotFound();
            return NoContent();
        }
    }
}
