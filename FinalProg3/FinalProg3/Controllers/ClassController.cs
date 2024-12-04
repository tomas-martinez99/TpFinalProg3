
using FinalProg3.Application.Interfaces;
using FinalProg3.Application.Models.Dto;
using FinalProg3.Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProg3.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        public ClassController(IClassService classService)
        {
            _classService = classService;
        }
        
        [HttpGet]
        public ActionResult<List<ClassDto>> GetAll()
        {
            return Ok(_classService.GetAll());
        }


        [HttpGet("{id}")]
        public ActionResult GetClassById(int id)
        {
            var classDetails = _classService.GetClassById(id);
            if (classDetails == null) return NotFound("Clase no encontrada.");
            return Ok(classDetails);
        }


        [HttpGet("{sport}")]
        public ActionResult <List<ClassDto>>GetClassBySport(string sport) {
            var clas = _classService.GetClassBySport(sport);
            if (clas == null || clas.Count == 0) return NotFound("No existen clases con ese nombre");
            return Ok(clas);
        }


        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public ActionResult AddClass(AddClassRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            }

            try
            {
                _classService.AddClass(request);
                return Ok("Clase agregada exitosamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); 
            }
        }


        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut]
        public ActionResult UpdateClass(int id, UpdateClassRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            }

            try
            {
                _classService.UpdateClass(id,request);
                return Ok("Clase actulizada exitosamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public ActionResult DeleteClass(int id)
        {
            var del = _classService.DeleteClass(id);
            if (del == false) return NotFound();
            return NoContent();
        }

        // Inscribir un alumno en una clase
        [Authorize(Roles = "Admin,Student")]
        [HttpPost("{classId}/EnrollStudent/{studentId}")]
        public ActionResult EnrollStudent(int classId, int studentId)
        {
            try
            {
                _classService.EnrollStudent(classId, studentId);
                return Ok("Alumno inscrito correctamente.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Asignar un profesor a una clase
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost("{classId}/AssignTeacher/{teacherId}")]
        public ActionResult AssignTeacher(int classId, int teacherId)
        {
            try
            {
                _classService.AssignTeacher(classId, teacherId);
                return Ok("Profesor asignado correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
