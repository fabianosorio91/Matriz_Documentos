namespace Dominio.Interfaces.Repositorios
{
    public interface IRepositorioBase<TEntidad> : IListar<TEntidad>, IBuscar<TEntidad>
    {
    }
}
