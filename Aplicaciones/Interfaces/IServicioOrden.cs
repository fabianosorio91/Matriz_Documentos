using Dominio.Interfaces;

namespace Aplicaciones.Interfaces
{
    internal interface IServicioOrden<TEntidad> : IListarOrden<TEntidad>, IBuscarOrdenCompra<TEntidad>, IActualizarOrdenCompra<TEntidad>, IListar<TEntidad>, IEliminar
    {
    }

}
