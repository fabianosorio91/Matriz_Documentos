namespace Dominio.Interfaces
{

    public interface IListarOrden<TEntidad>
    {
        Task<List<TEntidad>> ListarOrden(OrdenCompra data);
    }
}




