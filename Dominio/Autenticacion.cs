using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
	public class Autenticacion
	{
		[Required(ErrorMessage = "El campo es requerido")]
		public string USUARIO { get; set; }
		[Required(ErrorMessage = "El campo es requerido")]
		public string CONTRASEÑA { get; set; }
		
	}
}
