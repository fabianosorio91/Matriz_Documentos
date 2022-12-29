using Aplicaciones.Interfaces;
using Dominio;
using Dominio.Interfaces.Repositorios;

namespace Aplicacion.Servicios
{
    public class AreaServicio : IServicioAreas<Area>
    {
        private readonly IRepositorioAreas<Area> repositorioAreas;

        public AreaServicio(IRepositorioAreas<Area> _repositorioAreas)
        {
            repositorioAreas = _repositorioAreas;
        }

        public async Task<List<Area>> Listar()
        {
            return await repositorioAreas.Listar();
        }
    }
}
