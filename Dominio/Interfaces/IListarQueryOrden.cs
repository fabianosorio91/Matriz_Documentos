namespace Dominio.Interfaces
{

    public interface IListarQueryOrden<TEntidad>
    {
        //Task<List<TEntidad>> ListarOrden();
        Task<List<TEntidad>> QueryOrden(OrdenCompra data);

    }
}




