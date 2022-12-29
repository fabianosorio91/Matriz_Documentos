using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dominio
{
    public class Proveedor
    {
        public int ID_PROVEEDOR { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(50)]
        public string NOMBRE { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(30)]
        public string NIT { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(50)]
        public string CONTACTO { get; set; }
        [EmailAddress(ErrorMessage = "Debe ingresar un Email válido")]
        [MaxLength(50)]
        public string EMAIL { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(50)]
        public string TELEFONO { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string ESTADO { get; set; }

    }
}
