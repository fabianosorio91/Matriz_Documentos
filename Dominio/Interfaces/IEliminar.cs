namespace Dominio.Interfaces
{
    public interface IEliminar
        {
            Task<bool> Eliminar(int id);
        }
}
