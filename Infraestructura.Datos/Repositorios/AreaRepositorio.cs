using Dominio;
using Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infraestructura.Datos.Repositorios
{

    public class AreaRepositorio : IRepositorioAreas<Area>
    {
        private readonly IConfiguration configuration;

        public AreaRepositorio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<Area>> Listar()
        {
            var cadenaSQL = new Conexion(configuration).GetConnectionString();
            var oLista = new List<Area>();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetAreas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new Area()
                        {
                            AreId = Convert.ToInt32(dr["AreId"]),
                            AreNombre = dr["AreNombre"].ToString()

                        });

                    }
                }
            }

            return oLista;
        }
    }
}

