using Dominio;
using Dominio.Interfaces;

namespace Aplicaciones.Interfaces
{
    internal interface IServicioProveedors<TEntidad>: 
        IListarProveedor<TEntidad>, 
        ICreate<TEntidad>,
        IEliminar
    {
        Task<Proveedor> BuscarProveedor(int id);
        Task<bool> EditarProveedor(Proveedor proveedor);
    }

}
