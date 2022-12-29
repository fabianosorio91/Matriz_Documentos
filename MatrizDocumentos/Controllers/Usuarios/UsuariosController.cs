 using Aplicacion.Servicios;
using Dominio;
using Infraestructura.Datos.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace MatrizDocumentos.Controllers.Usuarios
{
	public class Usuarios : Controller
	{
		private readonly IConfiguration configuration;

		public Usuarios(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		UsuarioServicio ServicioUsuario()
		{
			UsuarioRepositorio repo = new UsuarioRepositorio(configuration);
			UsuarioServicio servicio = new UsuarioServicio(repo);
			return servicio;
		}

		//get Login

		public async Task<IActionResult> Login()
		{
			ViewBag.visible = false;
			Autenticacion auto = new Autenticacion();

			return View(auto);
		}

		[HttpPost]
		public async Task<IActionResult> Acceder(Autenticacion auto)
		{
			var respuesta = await ServicioUsuario().Autenticar(new Usuario {USUARIO= auto.USUARIO, CONTRASEÑA = auto.CONTRASEÑA });

			if (respuesta.ID != 0 && respuesta.ID != null)
			{
				/*autenticacion
				var claims = new List<Claim> { 
				
					new
				}; */

				return RedirectToAction("Index", "Documentos");
			}
			else {
				ViewBag.visible = true;
				return View("Login");

			}
			
		}

	}
}

