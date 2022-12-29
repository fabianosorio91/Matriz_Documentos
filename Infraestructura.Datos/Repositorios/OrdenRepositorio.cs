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
    public class OrdenRepositorio : IRepositorioOrden<OrdenCompra>
    {
        private readonly IConfiguration configuration;

        public OrdenRepositorio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<OrdenCompra>> ListarOrden(OrdenCompra data)
        {
            var oLista = new List<OrdenCompra>();
            try
            {
                var cadenaSQL = new Conexion(configuration).GetConnectionString();

                //Inserta Tabla TBL_ORDEN_COMPRA
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    await conexion.OpenAsync();
                    SqlCommand cmd = new SqlCommand("SP_InsertaOrden", conexion);

                    //Obtener el valor total del producto
                    int cont = 0;
                    int totals = data.tabla.Count();
                    long valortotal = 0;
                    foreach (var tbl in data.tabla)
                    {
                        cont++;
                        if (cont == totals)
                        {
                            valortotal = tbl.total;
                        }
                    }

                    cmd.Parameters.AddWithValue("id_ordencompra", data.id_ordencompra);
                    cmd.Parameters.AddWithValue("fechasolicitud", data.fechasolicitud);
                    cmd.Parameters.AddWithValue("Solicitante", data.Solicitante);
                    cmd.Parameters.AddWithValue("codigodelproyecto", data.codigodelproyecto);
                    cmd.Parameters.AddWithValue("codigopresupuestos", data.codigopresupuestos);
                    cmd.Parameters.AddWithValue("id_proveedor", data.id_proveedor);
                    cmd.Parameters.AddWithValue("garantia", data.garantia);
                    cmd.Parameters.AddWithValue("acuerdosdepago", data.acuerdosdepago);
                    cmd.Parameters.AddWithValue("acuerdosdeincumplimiento", data.incumplimiento);
                    cmd.Parameters.AddWithValue("acuerdosdeentregas", data.entregas);
                    cmd.Parameters.AddWithValue("acuerdosdeseguimiento", data.seguimiento);
                    cmd.Parameters.AddWithValue("soporteposventa", data.soporteposventa);
                    cmd.Parameters.AddWithValue("otros", data.otros);
                    cmd.Parameters.AddWithValue("valortotalorden", valortotal);

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

                //INSERTA TABLA TBL_PRODUCTOSORDENCOMPRA
                int con = 0;
                int total = data.tabla.Count();

                foreach (var tbl in data.tabla)
                {
                    con++;

                    if (con > 1 && con < total)
                    {
                        using (var conexion = new SqlConnection(cadenaSQL))
                        {
                            await conexion.OpenAsync();
                            SqlCommand cmd = new SqlCommand("SP_InsertarProducto", conexion);
                            cmd.Parameters.AddWithValue("nombreproducto", tbl.descripcion);
                            cmd.Parameters.AddWithValue("id_ordencompra", oLista[0].id_ordencompra);
                            cmd.Parameters.AddWithValue("cantidad", tbl.cantidad);
                            cmd.Parameters.AddWithValue("valorunitario", tbl.valorunitario);
                            cmd.Parameters.AddWithValue("totalantesdeiva", tbl.totalantesiva);

                            cmd.CommandType = CommandType.StoredProcedure;
                            using (var dr = await cmd.ExecuteReaderAsync())
                            {
                                if (con == total - 1)
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
                    }
                }

            }
            catch (Exception e)
            {
            }

            ExportarOrdenRepositorio repoex = new ExportarOrdenRepositorio(configuration);
            EnviarCorreo enviar = new EnviarCorreo(configuration);
            repoex.ExportarOrden(oLista);
            enviar.SendEmailAsync(oLista);

            return oLista;
        }
    }
}

