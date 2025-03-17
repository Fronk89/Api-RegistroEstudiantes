namespace ApiPrueba.Models
{
    public class Maestro
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }

        // Llave foránea a Curso
        public int CursoId { get; set; }
        public Curso? Curso { get; set; }

        // Relación con Asignaturas (1 maestro puede tener varias asignaturas)
        public List<Asignatura>? Asignaturas { get; set; }
    }
}
