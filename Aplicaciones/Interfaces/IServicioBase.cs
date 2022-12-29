
using Dominio.Interfaces;

namespace Aplicaciones.Interfaces
{
    internal interface IServicioBase<TEntidad>: IListar<TEntidad>, IBuscar<TEntidad>
    {   
    }
}
