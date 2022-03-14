namespace Bienes_Raices_HAXA.Models
{
    /// <summary>
    /// Clase para mostrar las citas en el calendario
    /// </summary>
    public class Citas2
    {
        public int idCita { get; set; }
        public int idUsuario { get; set; }
        public int idPropiedad { get; set; }
        public string titulo { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFinal { get; set; }
    }
}