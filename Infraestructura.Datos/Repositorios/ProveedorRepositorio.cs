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

        //crear proveedor
        public async Task<Proveedor> CreateAsync(Proveedor proveedor)
        {
            var cadenaSQL = new Conexion(configuration).GetConnectionString();//cadena conexion
            using (var conexion = new SqlConnection(cadenaSQL))

            {
                SqlCommand cmd = new SqlCommand("SP_PostProveedores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NOMBRE", proveedor.NOMBRE);
                cmd.Parameters.AddWithValue("@NIT", proveedor.NIT);
                cmd.Parameters.AddWithValue("@CONTACTO", proveedor.CONTACTO);
                cmd.Parameters.AddWithValue("@EMAIL", proveedor.EMAIL);
                cmd.Parameters.AddWithValue("@TELEFONO", proveedor.TELEFONO);
                cmd.Parameters.AddWithValue("@ESTADO", proveedor.ESTADO);

                await conexion.OpenAsync();
                cmd.ExecuteNonQuery();

                conexion.Close();
            }
            return proveedor;
        }


        //listar Proveedor
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
        //Eliminar proveedor
        public async Task<bool> Eliminar(int id)
        {
            bool EliminadoConExito = false;

            var cadenaSQL = new Conexion(configuration).GetConnectionString();
            using (var conexion = new SqlConnection(cadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_ELiminarProveedor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID_PROVEEDOR ", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();

            }

            return EliminadoConExito;
        }


        //Buscar Proveedor

        public async Task<Proveedor> BuscarProveedor(int id)
        {
            var cadenaSQL = new Conexion(configuration).GetConnectionString();
            var proveedor = new Proveedor();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_BuscarProveedor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID_PROVEEDOR ", SqlDbType.Int).Value = id;
             
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        proveedor.ID_PROVEEDOR = Convert.ToInt32(dr["ID_PROVEEDOR"]);
                        proveedor.NOMBRE = dr["NOMBRE"].ToString();
                        proveedor.NIT = dr["NIT"].ToString();
                        proveedor.CONTACTO = dr["CONTACTO"].ToString();
                        proveedor.EMAIL = dr["EMAIL"].ToString();
                        proveedor.TELEFONO = dr["TELEFONO"].ToString();
                        proveedor.ESTADO = dr["ESTADO"].ToString();                        
                    }
                }
            }

            return proveedor;
        }

        //Editar Proveedor
        public async Task<bool> EditarProveedor(Proveedor proveedor)
        {
            try
            {
                bool EliminadoConExito = true;

                var cadenaSQL = new Conexion(configuration).GetConnectionString();
                using (var conexion = new SqlConnection(cadenaSQL))
                {

                    SqlCommand cmd = new SqlCommand("SP_EditarProveedor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_PROVEEDOR", proveedor.ID_PROVEEDOR);
                    cmd.Parameters.AddWithValue("@NOMBRE", proveedor.NOMBRE);
                    cmd.Parameters.AddWithValue("@NIT", proveedor.NIT);
                    cmd.Parameters.AddWithValue("@CONTACTO", proveedor.CONTACTO);
                    cmd.Parameters.AddWithValue("@EMAIL", proveedor.EMAIL);
                    cmd.Parameters.AddWithValue("@TELEFONO", proveedor.TELEFONO);
                    cmd.Parameters.AddWithValue("@ESTADO", proveedor.ESTADO);

                    await conexion.OpenAsync();
                    cmd.ExecuteNonQuery();

                    conexion.Close();
                }

                return EliminadoConExito;

            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                throw;
            }
        }


    }
}
