using Dominio;
using Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infraestructura.Datos.Repositorios
{
    public class DocumentoRepositorio : IRepositorioBase<Documento>
    {
        private readonly IConfiguration configuration;

        public DocumentoRepositorio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<Documento>> Buscar(string Area)
        {
            var cadenaSQL = new Conexion(configuration).GetConnectionString();

            var oLista = new List<Documento>();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_Buscar", conexion);
                cmd.Parameters.AddWithValue("Area", Area);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new Documento()
                        {
                            DocId = Convert.ToInt32(dr["DocId"]),
                            DocCodigo = dr["DocCodigo"].ToString(),
                            DocNombre = dr["DocNombre"].ToString(),
                            DocArea = dr["DocArea"].ToString(),
                            DocRuta = dr["DocRuta"].ToString(),
                        });

                    }
                }
            }
            return oLista;
        }

        public async Task<List<Documento>> Listar()
        {
            var cadenaSQL = new Conexion(configuration).GetConnectionString();
            var oLista = new List<Documento>();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetDocumentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new Documento()
                        {
                            DocId = Convert.ToInt32(dr["DocId"]),
                            DocCodigo = dr["DocCodigo"].ToString(),
                            DocNombre = dr["DocNombre"].ToString(),
                            DocArea = dr["DocArea"].ToString(),
                            DocRuta = dr["DocRuta"].ToString()

                        });

                    }
                }
            }
            return oLista;
        }
    }
}
