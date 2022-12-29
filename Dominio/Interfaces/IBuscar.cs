namespace Dominio.Interfaces
{
    public interface IBuscar<TEntidad>
    {
        Task<List<TEntidad>> Buscar(string Area);
    }
}
