using Dominio;
using Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Datos.Repositorios
{
    public class ParametrosRepositorio : IRepositorioParametros<Parametros>
    {
        private readonly IConfiguration configuration;

        public ParametrosRepositorio(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public async Task<List<Parametros>> BuscarParametro(string tipoP)
        {
            
            var cadenaSQL = new Conexion(configuration).GetConnectionString();
            var oLista = new List<Parametros>();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetExtencionDoc", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TipoParametro", SqlDbType.VarChar).Value = tipoP;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new Parametros()
                        {
                            ID_PARAMETRO = Convert.ToInt32(dr["ID_PARAMETRO"]),
                            NOMBREPARAMETRO = dr["NOMBREPARAMETRO"].ToString(),
                            VALORPARAMETRO = dr["VALORPARAMETRO"].ToString()
                        });

                    }
                }
            }

            return oLista;
        }
    }
}
