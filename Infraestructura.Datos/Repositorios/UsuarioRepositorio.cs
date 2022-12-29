using DocumentFormat.OpenXml.Office.Word;
using Dominio;
using Dominio.Interfaces.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Datos.Repositorios
{

	public class UsuarioRepositorio : IRepositorioUsuario<Usuario>
	{
		private readonly IConfiguration configuration;

		public UsuarioRepositorio(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public async Task<Usuario> Autenticar(Usuario usuario)
		{
			var cadenaSQL = new Conexion(configuration).GetConnectionString();//cadena conexion
			using (var conexion = new SqlConnection(cadenaSQL))

			{
				SqlCommand cmd = new SqlCommand("SP_ValidarUsuario", conexion);
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("@USUARIO", usuario.USUARIO);
				cmd.Parameters.AddWithValue("@CONTRASEÑA", usuario.CONTRASEÑA);

				await conexion.OpenAsync();
				var datos = cmd.ExecuteScalar();
				usuario.ID = Convert.ToInt32(datos);		

			}

			return usuario;
		}



	}
}



