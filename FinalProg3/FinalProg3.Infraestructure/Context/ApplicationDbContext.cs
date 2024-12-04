using Microsoft.EntityFrameworkCore;
using FinalProg3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProg3.Domain.Enum;

namespace FinalProg3.Infraestructure.Context
{
    public class ApplicationDbContext: DbContext
    {
        
        public DbSet<Class> Classs { get; set; }
        public DbSet<Sport> Sports { get; set; }

        public DbSet<User> Users { get; set; }

            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Class>()
            .HasMany(c => c.Students)
            .WithMany(u => u.StudentClasses)  // Establece la propiedad de navegación correspondiente en User
            .UsingEntity<Dictionary<string, object>>(
                 "ClassStudent",
                  j => j.HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ClassStudent_User")
                        .OnDelete(DeleteBehavior.Cascade),
                  j => j.HasOne<Class>()
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .HasConstraintName("FK_ClassStudent_Class")
                        .OnDelete(DeleteBehavior.Cascade)
            );

            modelBuilder.Entity<Class>()
                .HasMany(c => c.Teachers)
                .WithMany(u => u.TeacherClasses)  // Establece la propiedad de navegación correspondiente en User
                .UsingEntity<Dictionary<string, object>>(
                    "ClassTeacher",
                    j => j.HasOne<User>()
                          .WithMany()
                          .HasForeignKey("UserId")
                          .HasConstraintName("FK_ClassTeacher_User")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Class>()
                          .WithMany()
                          .HasForeignKey("ClassId")
                          .HasConstraintName("FK_ClassTeacher_Class")
                          .OnDelete(DeleteBehavior.Cascade)
                );


            base.OnModelCreating(modelBuilder);
        }


    }
}
