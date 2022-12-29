using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dominio
{
    public class Documento
    {
        public int DocId { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string DocCodigo { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string DocNombre { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string DocArea { get; set; }
        public List<SelectListItem> Areas { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]

        public List<SelectListItem> Proveedors { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]

        public List<string> Proveedores { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]

        public string DocRuta { get; set; }
        public string AreValor { get; set; }
        /// añadidura nueva
        /// 

        public string DocExtension { get; set; }
        public string DocUsuCrea { get; set; }
        //feature: lista parametros, lista areas 

        public List<SelectListItem> ListaExtenciones { get; set; }
    }
}
