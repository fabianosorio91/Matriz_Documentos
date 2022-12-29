using Dominio.Interfaces;

namespace Aplicaciones.Interfaces
{
    internal interface IServicioProveedors<TEntidad>: IListarProveedor<TEntidad>, IBuscarProveedorIDProveedor<TEntidad>
    {
    }

}
