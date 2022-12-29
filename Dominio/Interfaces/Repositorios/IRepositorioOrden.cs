namespace Dominio.Interfaces.Repositorios
{
    public interface IRepositorioOrden<TEntidad> : IListarOrden<TEntidad>, IBuscarOrdenCompra<TEntidad>, IActualizarOrdenCompra<TEntidad>, IListar<TEntidad>, IEliminar
    {
    }

}
