namespace Dominio.Interfaces.Repositorios
{
    public interface IRepositorioProveedor<TEntidad>: 
        IListarProveedor<TEntidad>, 
        ICreate<TEntidad>, 
        IEliminar

    {
        Task<Proveedor> BuscarProveedor(int id);
        Task<bool> EditarProveedor(Proveedor proveedor);
    }
    public interface IRepositorioProveedores<TEntidad> : 
        IListarOrden<TEntidad>
    {     

    }
}
