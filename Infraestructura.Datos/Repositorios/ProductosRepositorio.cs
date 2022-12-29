using Dominio;
using Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infraestructura.Datos.Repositorios
{
    public class ProductosRepositorio : IRepositorioProductos<Productos>
    {
        private readonly IConfiguration _configuration;

        public ProductosRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Productos>> BuscarProductosDeOrden(long idOrdenCompra)
        {

            var oListaProducto = new List<Productos>();

            var cadenaSQL = new Conexion(_configuration).GetConnectionString();

            //Inserta Tabla TBL_ORDEN_COMPRA
            using (var conexion = new SqlConnection(cadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_BuscarProductosDeLaOrden", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_ORDENCOMPRA", SqlDbType.Int).Value = idOrdenCompra;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oListaProducto.Add(new Productos()
                        {
                            cantidad = Convert.ToInt32(dr["CANTIDAD"]),
                            descripcion = dr["NOMBREPRODUCTO"].ToString(),
                            totalantesiva = Convert.ToInt32(dr["TOTALANTESDEIVA"]),
                            valorunitario = Convert.ToInt32(dr["VALORUNITARIOANTESDEIVA"])
                        });
                    }
                }
            }

  
            return oListaProducto;
        }
    }
}
