namespace MiPrimerCRUD.Models
{
    
    public class Asignatura
    {
        public int Id { get; set; } 
        public string Nombre { get; set; }
        //relacion a otra
        public long? CursoId { get; set; }   
        public Curso? Curso { get; set; }
    }
}
