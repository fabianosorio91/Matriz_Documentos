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
    public class UsuarioServicio : IServicioUsuario<Usuario>
    {
        private readonly IRepositorioUsuario<Usuario> _repositorioUsuario;
        //contructor
        public UsuarioServicio(IRepositorioUsuario<Usuario> repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

		

		public async Task<Usuario> Autenticar(Usuario usuario)
		{
			return await _repositorioUsuario.Autenticar(usuario);
		}
	

	}
}
