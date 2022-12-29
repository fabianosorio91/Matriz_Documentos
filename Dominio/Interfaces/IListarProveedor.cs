namespace Dominio.Interfaces
{
    public interface IListarProveedor<TEntidad>
    {
        Task<List<TEntidad>> ListarProveedor();
    }
}
