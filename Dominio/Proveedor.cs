using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dominio

{
    public class Proveedor
    {    
        public int ID_PROVEEDOR { get; set; }
        public string NOMBRE { get; set; }

        public string NIT { get; set; }

        public string CONTACTO { get; set; }

        public string EMAIL { get; set; }

        public string TELEFONO { get; set; }

        public string ESTADO { get; set; }


        
    }
}
