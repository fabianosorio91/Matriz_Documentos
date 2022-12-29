using Aplicacion.Interfaces;
using Dominio;
using Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aplicacion.Servicios
{
    public class ParametrosServicio : IServicioParametros<Parametros>
    {
        private readonly IRepositorioParametros<Parametros> repoParametros;

        public ParametrosServicio(IRepositorioParametros<Parametros> _repoParametros)
        {
            repoParametros = _repoParametros;
        }

        public async Task<List<Parametros>> BuscarParametro(string tipoP)
        {
           return await repoParametros.BuscarParametro(tipoP);
        }
    }
}
