using FinalProg3.Application.Interfaces;
using FinalProg3.Application.Models.Dto;
using FinalProg3.Application.Models.Request;
using FinalProg3.Domain.Entities;
using FinalProg3.Domain.Enum;
using FinalProg3.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _repository;
        private readonly ISportRepository _sportRepository;
        private readonly IUserRepository _userRepository;
        public ClassService(IClassRepository repository, ISportRepository sportRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _sportRepository = sportRepository;
            _userRepository = userRepository;
        }
        //Metodo 1 Traer todas las clases
        public List<ClassDto>GetAll()
        {
            return ClassDto.ToDTO(_repository.GetAll());
        }

        //Metodo 2 Traer una clase segun su id

        public ClassDetailDto GetClassById(int id)
        {
            var clas = _repository.GetClassById(id);

            if (clas == null)
                return null;

            return new ClassDetailDto
            {
                Id = clas.Id,
                SportName = clas.Sport != null ? clas.Sport.SportName : "No definido",
                Schedule = clas.Schedule,
                Students = clas.Students.Select(s => new UserDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email
                }).ToList(),
                Teachers = clas.Teachers.Select(t => new UserDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Email = t.Email
                }).ToList()
            };

        }

        //Metodo 3 Traer Todas las clases con el mismo nombre
        public List<ClassDto> GetClassBySport(string sport) 
        {
            var clas = ClassDto.ToDTO(_repository.GetClassBySport(sport));            
            return clas;
           
        }

        //Metodo 4 Crear una clase
        public void AddClass(AddClassRequest request)
        {
            var sport = _sportRepository.GetSportByName(request.Sport);
            var schedule = TimeSpan.Parse(request.Schedule);
            if (sport == null)
            {
                throw new ArgumentException($"El deporte '{request.Sport}' no existe.");
            }

            if (_repository.ExistsBySportAndSchedule(request.Sport, schedule))
            {
                throw new ArgumentException($"Ya esta inscripta la clase en este horario.");
            }
            var obj = new Class()
            {
                Sport = sport,
                Schedule = TimeSpan.Parse(request.Schedule)
            };
            _repository.AddClass(obj);
            _repository.SaveChanges();
        }
        //Metodo 5 Actualizar una clase
        public void UpdateClass(int id, UpdateClassRequest request)
        {
            var sport = _sportRepository.GetSportByName(request.Sport);
            if (sport == null)
            {
                throw new ArgumentException($"El deporte '{request.Sport}' no existe.");
            }
            var updateClassValidate = _repository.GetClassById(id);
            if (updateClassValidate == null)
            {
                throw new ArgumentException($"Clase inexistente");
            }
                if (updateClassValidate != null)
            {
                updateClassValidate.Sport= sport;
                updateClassValidate.Schedule= TimeSpan.Parse(request.Schedule);
                _repository.UpdateClass(updateClassValidate);
                _repository.SaveChanges();
            }
        }

        //Metodo 6 Borrar una clase
        public bool DeleteClass(int id)
        {
            var deleteClassValidate = _repository.GetClassById(id);
            if(deleteClassValidate != null)
            {
                _repository.DeleteClass(deleteClassValidate);
                _repository.SaveChanges();
                return true;
            }
            return false;
        }

        //Metodo 7 Agregar un alumno a una clase
        public void EnrollStudent(int classId, int studentId)
        {
            var clas = _repository.GetClassById(classId);
            var student = _userRepository.GetById(studentId);

            if (clas == null)
                throw new KeyNotFoundException($"Clase con ID {classId} no encontrada.");

            var studentAlreadyInClass = clas.Students.Any(s => s.Id == studentId);
            if (studentAlreadyInClass)
            {
                throw new InvalidOperationException("El estudiante ya esta inscripto en la clase");
            }

            if (student == null || student.UserType != UserType.Student)
                throw new KeyNotFoundException($"Estudiante con ID {studentId} no encontrado o no válido.");

            clas.Students.Add(student);
            _repository.UpdateClass(clas);
            _repository.SaveChanges();
        }

        //Metodo 8 asignar un profesor a una clase
        public void AssignTeacher(int classId, int teacherId)
        {
            var clas = _repository.GetClassById(classId);
            var teacher = _userRepository.GetById(teacherId);

            if (clas == null)
                throw new KeyNotFoundException($"Clase con ID {classId} no encontrada.");
            var teacherAlreadyInClass = clas.Teachers.Any(s => s.Id == teacherId);
            if (teacherAlreadyInClass)
            {
                throw new InvalidOperationException("El Profesor ya esta inscripto en la clase");
            }

            if (teacher == null || teacher.UserType != UserType.Teacher)
                throw new KeyNotFoundException($"Profesor con ID {teacherId} no encontrado o no válido.");

            clas.Teachers.Add(teacher);
            _repository.UpdateClass(clas);
            _repository.SaveChanges();
        }
    }
}
