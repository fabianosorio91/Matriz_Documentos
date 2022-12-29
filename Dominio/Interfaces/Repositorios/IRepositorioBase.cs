namespace Dominio.Interfaces.Repositorios
{
    public interface IRepositorioBase<TEntidad> : IListar<TEntidad>, IBuscar<TEntidad>, IGuardar<TEntidad>, IActualizar<TEntidad>, IEliminar, IBuscarDocumento<TEntidad>
    {
    }
}
