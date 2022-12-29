using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dominio
{
    public class DatosProveedor
    { 
        public List<SelectListItem> Proveedors { get; set; }

    }
}
