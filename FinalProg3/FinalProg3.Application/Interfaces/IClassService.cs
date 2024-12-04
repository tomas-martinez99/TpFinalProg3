using FinalProg3.Application.Models.Dto;
using FinalProg3.Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Interfaces
{
    public interface IClassService
    {
        List<ClassDto> GetAll();
        ClassDetailDto GetClassById(int id);
        List<ClassDto> GetClassBySport(string sport);
        void AddClass(AddClassRequest request);
        void UpdateClass(int id, UpdateClassRequest request);
        bool DeleteClass(int id);
        void EnrollStudent(int classId, int studentId);
        void AssignTeacher(int classId, int teacherId);
        

    }
}
