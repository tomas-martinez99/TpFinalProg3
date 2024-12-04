using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Domain.Entities
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Sport Sport { get; set; }

        // Relación con Alumnos
        public ICollection<User> Students { get; set; } = new List<User>();

        // Relación con Profesores
        public ICollection<User> Teachers { get; set; } = new List<User>();


        public TimeSpan Schedule { get; set; }
    }
}
