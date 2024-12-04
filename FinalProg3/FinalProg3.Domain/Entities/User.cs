using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProg3.Domain.Enum;
using System.Collections.ObjectModel;

namespace FinalProg3.Domain.Entities
{
    public class User
    {
        [Key] // Hace que la id sea la clave principal dentro de la BD
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public UserType UserType { get; set; }
        [Required]
        public bool UserState { get; set; } = true;

        public ICollection<Class> StudentClasses { get; set; }
        public ICollection<Class> TeacherClasses { get; set; }

    }
}
