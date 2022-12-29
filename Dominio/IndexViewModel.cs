using System.Web.Mvc;

namespace Dominio
{
    public class IndexViewModel
    {
        public Documento Documento { get; set; }
        
        public Proveedor Proveedor { get; set; }
        public DatosProveedor DatosProveedor { get; set; }
        public List<Documento> DocumentoList { get; set; }
        public List<Proveedor> ProveedorList { get; set; }
        
        public Parametros Parametros { get; set; }
        
        public string Area { get; set; }

      // Utilizado para ordenes de compra
      public OrdenCompra OrdenCompra { get; set; }
      
      public List<OrdenCompra> OrdenCompraList { get; set; }


    }
}
