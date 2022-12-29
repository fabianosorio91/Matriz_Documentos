
using Dominio.Interfaces;

namespace Aplicaciones.Interfaces
{
    internal interface IServicioBase<TEntidad>: IListar<TEntidad>, IBuscar<TEntidad>, IGuardar<TEntidad>, IActualizar<TEntidad>, IEliminar, IBuscarDocumento<TEntidad>
    {   
    }
}
