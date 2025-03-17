
using Microsoft.EntityFrameworkCore;

namespace ApiPrueba.Models
{
    public class BdRegistroEscolarContext : DbContext

    {
        public BdRegistroEscolarContext(DbContextOptions<BdRegistroEscolarContext> options) : base(options) { }

        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Maestro> Maestros { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación 1 a muchos: Maestro -> Asignaturas
            modelBuilder.Entity<Asignatura>()
                .HasOne(a => a.Maestro)
                .WithMany(m => m.Asignaturas)
                .HasForeignKey(a => a.MaestroId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación 1 a muchos: Curso -> Maestros
            modelBuilder.Entity<Maestro>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Maestros)
                .HasForeignKey(m => m.CursoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación 1 a muchos: Curso -> Alumnos
            modelBuilder.Entity<Alumno>()
                .HasOne(a => a.Curso)
                .WithMany(c => c.Alumnos)
                .HasForeignKey(a => a.CursoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
