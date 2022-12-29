using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class OrdenCompra
    {
        //Datos Orden
        public long numerodelaorden { get; set; }
        public long id_ordencompra { get; set; }
        public DateTime fechasolicitud { get; set; }
        public string Solicitante { get; set; }
        public long codigodelproyecto { get; set; }
        public string codigopresupuestos { get; set; }

        public long valortotalorden { get; set; }

        //Proveedor
        public long id_proveedor { get; set; }

        public string nombre { get; set; }
        public string nit { get; set; }
        public string contacto { get; set; }
        public string email { get; set; }
        public long telefono { get; set; }

        //Datos Comerciales
        public string garantia { get; set; }
        public string acuerdosdepago { get; set; }
        public string incumplimiento { get; set; }
        public string entregas { get; set; }
        public string seguimiento { get; set; }
        public string soporteposventa { get; set; }
        public string otros { get; set; }


        // Datos Producto
        public string descripcion { get; set; }
        public long cantidad { get; set; }
        public long valorunitario { get; set; }
        public long totalantesiva { get; set; }
        public long total { get; set; }


        
        public List<Productos> tabla { get; set; }

        public List<string> ordenes { get; set; }

        public List<SelectListItem> ListaMoneda { get; set; }


        public string Moneda { get; set; }

    }
}
