using Aplicaciones.Interfaces;
using Dominio;
using Dominio.Interfaces.Repositorios;
using System.Web.Mvc;

namespace Aplicacion.Servicios
{
    public class DocumentoServicio : IServicioBase<Documento>
    {
        private readonly IRepositorioBase<Documento> repoDocumento;

        public DocumentoServicio(IRepositorioBase<Documento> _repoDocumento)
        {
            repoDocumento = _repoDocumento;
        }

        public async Task<List<Documento>> Buscar(string Area)
        {
            return await repoDocumento.Buscar(Area);
        }

        public async Task<List<Documento>> Listar()
        {
            return await repoDocumento.Listar();
        }

       public async Task<bool> Guardar(Documento documentoObjeto)
        {
            return await repoDocumento.Guardar(documentoObjeto);
        }

        public async Task<bool> Actualizar(Documento documentoObjeto)
        {
            return await repoDocumento.Actualizar(documentoObjeto);
        }

        public async Task<bool> Eliminar(int documentoId)
        {
            return await repoDocumento.Eliminar(documentoId);
        }

        

        public async Task<Documento> DocumentoAActualizar(int DocId)
        {
            return await repoDocumento.DocumentoAActualizar(DocId);
        }

      
    }
}
