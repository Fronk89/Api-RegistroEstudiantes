using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiPrueba.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int EncargadoId { get; set; }
        public int CantidadAlumnos { get; set; }

        // Relación con Maestros y Alumnos
        public List<Maestro>? Maestros { get; set; }
        public List<Alumno>? Alumnos { get; set; }
    }
}
