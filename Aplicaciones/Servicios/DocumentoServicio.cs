using Aplicaciones.Interfaces;
using Dominio;
using Dominio.Interfaces.Repositorios;

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

    }
}
