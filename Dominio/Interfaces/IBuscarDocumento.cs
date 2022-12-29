using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IBuscarDocumento<TEntidad>
    {
        // sirve para traer la informacion del documento a la hora de hacer la actualizacion
        Task<TEntidad> DocumentoAActualizar(int DocId);
    }
}
