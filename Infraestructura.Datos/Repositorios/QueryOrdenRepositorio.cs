using Dominio;
using Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System;
using System.Reflection;
using System.Diagnostics.Contracts;


namespace Infraestructura.Datos.Repositorios
{
    public class QueryOrdenRepositorio : IRepositorioQueryOrden<OrdenCompra>
    {
        private readonly IConfiguration configuration;

        public QueryOrdenRepositorio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<OrdenCompra>> QueryOrden(OrdenCompra data)
        {
            var oLista = new List<OrdenCompra>();
            try
            {
                var cadenaSQL = new Conexion(configuration).GetConnectionString();

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    await conexion.OpenAsync();
                    SqlCommand cmd = new SqlCommand("SP_GetOrden", conexion);
        
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            oLista.Add(new OrdenCompra()
                            {
                                id_ordencompra = Convert.ToInt64(dr["id_ordencompra"]),
                                fechasolicitud = Convert.ToDateTime(dr["fechasolicitud"]),
                                Solicitante = dr["Solicitante"].ToString(),
                                codigodelproyecto = Convert.ToInt64(dr["codigodelproyecto"]),
                                numerodelaorden = Convert.ToInt64(dr["numerodelaorden"]),
                                codigopresupuestos = dr["codigopresupuestos"].ToString(),

                                id_proveedor = Convert.ToInt32(dr["id_proveedor"]),
                                nombre = dr["nombre"].ToString(),
                                nit = dr["nit"].ToString(),
                                contacto = dr["contacto"].ToString(),
                                email = dr["email"].ToString(),
                                telefono = Convert.ToInt64(dr["telefono"]),

                                garantia = dr["garantia"].ToString(),
                                acuerdosdepago = dr["acuerdosdepago"].ToString(),
                                incumplimiento = dr["acuerdosdeincumplimiento"].ToString(),
                                entregas = dr["acuerdosdeentregas"].ToString(),
                                seguimiento = dr["acuerdosdeseguimiento"].ToString(),
                                soporteposventa = dr["soporteposventa"].ToString(),
                                otros = dr["otros"].ToString(),
                                valortotalorden = Convert.ToInt64(dr["valortotalorden"]),
                            });
                        }
                    }
                }

                        using (var conexion = new SqlConnection(cadenaSQL))
                        {
                            await conexion.OpenAsync();
                            SqlCommand cmd = new SqlCommand("SP_GetProductos", conexion);

                            cmd.Parameters.AddWithValue("id_ordencompra", oLista[0].id_ordencompra);

                            cmd.CommandType = CommandType.StoredProcedure;
                            using (var dr = await cmd.ExecuteReaderAsync())
                            {
                                
                                    while (await dr.ReadAsync())
                                    {
                                        oLista.Add(new OrdenCompra()
                                        {
                                            descripcion = dr["nombreproducto"].ToString(),
                                            cantidad = Convert.ToInt64(dr["cantidad"]),
                                            valorunitario = Convert.ToInt64(dr["valorunitarioantesdeiva"]),
                                            totalantesiva = Convert.ToInt64(dr["totalantesdeiva"]),

                                        });
                                    }                               
                            }
                        }
                                  
            }
            catch (Exception e)
            {
            }

            return oLista;
        }
    }
}

