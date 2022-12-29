namespace Dominio.Interfaces
{
    public interface IListar<TEntidad>
    {
        Task<List<TEntidad>> Listar();

    }
}
