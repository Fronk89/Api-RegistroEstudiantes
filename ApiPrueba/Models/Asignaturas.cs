namespace ApiPrueba.Models
{
    public class Asignatura
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Maestro? Maestro { get; set; }

        // Llave foránea a Maestro
        public int MaestroId { get; set; }
      
    }
}
