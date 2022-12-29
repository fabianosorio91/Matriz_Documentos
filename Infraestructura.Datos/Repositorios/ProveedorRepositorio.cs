using Dominio;
using Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infraestructura.Datos.Repositorios
{

    public class ProveedorRepositorio : IRepositorioProveedor<Proveedor>
    {
        private readonly IConfiguration configuration;

        public ProveedorRepositorio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

      

        public async Task<List<Proveedor>> ListarProveedor()
        {
            var cadenaSQL = new Conexion(configuration).GetConnectionString();
            var oLista = new List<Proveedor>();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetProveedores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new Proveedor()
                        {
                            ID_PROVEEDOR = Convert.ToInt32(dr["ID_PROVEEDOR"]),
                            NOMBRE = dr["NOMBRE"].ToString(),
                            NIT = dr["NIT"].ToString(),
                            CONTACTO = dr["CONTACTO"].ToString(),
                            EMAIL = dr["EMAIL"].ToString(),
                            TELEFONO = dr["TELEFONO"].ToString(),
                            ESTADO = dr["ESTADO"].ToString()

                        });

                    }
                }
            }

            return oLista;
        }


        //metodo para traer a la vista los proveedores que correspondan a la orden de compra
        public async Task<Proveedor> BuscarProvedorIdProveedor(long idProveedor)
        {
            string consultaDato = $"SELECT * FROM TBL_PROVEEDOR WHERE ID_PROVEEDOR = {idProveedor}";
            {
                var cadenaSQL = new Conexion(configuration).GetConnectionString();
                var oLista = new Proveedor();

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    await conexion.OpenAsync();
                    SqlCommand cmd = new SqlCommand(consultaDato, conexion);
                    SqlDataReader lector = cmd.ExecuteReader();


                    while (await lector.ReadAsync())
                    {
                        oLista = new Proveedor()
                        {
                            ID_PROVEEDOR = Convert.ToInt32(lector.GetValue(0)),
                            NOMBRE = lector.GetValue(1).ToString(),
                            NIT = lector.GetValue(2).ToString(),
                            CONTACTO = lector.GetValue(3).ToString(),
                            EMAIL = lector.GetValue(4).ToString(),
                            TELEFONO = lector.GetValue(5).ToString(),
                            ESTADO = lector.GetValue(6).ToString()

                        };

                    }
                }
                return oLista;

            }
        }

    
    }
}

