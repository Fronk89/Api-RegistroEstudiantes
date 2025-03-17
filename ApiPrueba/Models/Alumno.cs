namespace ApiPrueba.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateOnly Nacimiento { get; set; }
        public bool Sexo { get; set; }
        public string? TelefonoPadres { get; set; }

        // Llave foránea a Curso
        public int CursoId { get; set; }
        public Curso? Curso { get; set; }
    }
}
