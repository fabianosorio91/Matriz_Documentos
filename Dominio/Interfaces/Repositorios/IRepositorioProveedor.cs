namespace Dominio.Interfaces.Repositorios
{
    public interface IRepositorioProveedor<TEntidad>: IListarProveedor<TEntidad>, IBuscarProveedorIDProveedor<TEntidad>
    {
    }

    public interface IRepositorioProveedores<TEntidad> : IListarOrden<TEntidad>
    {
    }

}
