using DocumentFormat.OpenXml.Drawing;
using Dominio;
using Dominio.Interfaces.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        //Feature: Actualizar, eliminar, guardar

        //metodo que Guarda ingresa inserta en la base de datos
        public async Task<bool> Guardar(Documento documentoObjeto)
        {
            bool InsercionExitosa = false;
            DateTime date = DateTime.Now;

            var cadenaSQL = new Conexion(configuration).GetConnectionString();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_InsertarDocumentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DocCodigo", SqlDbType.VarChar).Value = documentoObjeto.DocCodigo;
                cmd.Parameters.Add("@DocNombre", SqlDbType.VarChar).Value = documentoObjeto.DocNombre;
                cmd.Parameters.Add("@DocArea", SqlDbType.Int).Value = documentoObjeto.DocArea;
                cmd.Parameters.Add("@DocExtension", SqlDbType.VarChar).Value = documentoObjeto.DocExtension;
                cmd.Parameters.Add("@DocUsuCrea", SqlDbType.VarChar).Value = documentoObjeto.DocUsuCrea;
                cmd.Parameters.Add("@DocFechaCrea", SqlDbType.VarChar).Value = date.ToString();
                cmd.Parameters.Add("@DocRuta", SqlDbType.VarChar).Value = documentoObjeto.DocRuta;



                cmd.ExecuteNonQuery();
            }



            return InsercionExitosa;
        }

        //metodo que actualiza los campos de la base de datos
        public async Task<bool> Actualizar(Documento documentoObjeto)
        {
            bool ActualizacionExitosa = false;

            DateTime date = DateTime.Now;

            var cadenaSQL = new Conexion(configuration).GetConnectionString();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_ActualizarDocumentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DocId", SqlDbType.Int).Value = documentoObjeto.DocId;
                cmd.Parameters.Add("@DocCodigo", SqlDbType.VarChar).Value = documentoObjeto.DocCodigo;
                cmd.Parameters.Add("@DocNombre", SqlDbType.VarChar).Value = documentoObjeto.DocNombre;
                cmd.Parameters.Add("@DocArea", SqlDbType.Int).Value = documentoObjeto.DocArea;
                cmd.Parameters.Add("@DocExtension", SqlDbType.VarChar).Value = documentoObjeto.DocExtension;
                cmd.Parameters.Add("@DocUsuModifica", SqlDbType.VarChar).Value = documentoObjeto.DocUsuCrea;
                cmd.Parameters.Add("@DocFechaModifica", SqlDbType.VarChar).Value = date.ToString();
                cmd.Parameters.Add("@DocRuta", SqlDbType.VarChar).Value = documentoObjeto.DocRuta;

                cmd.ExecuteNonQuery();
            }

            return ActualizacionExitosa;
        }

        //metodo que elimina un documento

        public async Task<bool> Eliminar(int documentoId)
        {
            bool EliminadoConExito = false;


            var cadenaSQL = new Conexion(configuration).GetConnectionString();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_EliminarDocumento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DocId", SqlDbType.Int).Value = documentoId;
                cmd.ExecuteNonQuery();

            }

            return EliminadoConExito;
        }


        //Metodo que trae la informacion del documento a la hora de elegir editar

        public async Task<Documento> DocumentoAActualizar(int DocId)
        {
            string consultaDato = $"SELECT * FROM Documentos WHERE DocId = {DocId}";
            {
                var cadenaSQL = new Conexion(configuration).GetConnectionString();
                var oLista = new Documento();

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    await conexion.OpenAsync();
                    SqlCommand cmd = new SqlCommand(consultaDato, conexion);
                    SqlDataReader lector = cmd.ExecuteReader();


                    while (await lector.ReadAsync())
                    {
                        oLista = new Documento()
                        {
                            DocId = Convert.ToInt32(lector.GetValue(0)),
                            DocCodigo = lector.GetValue(1).ToString(),
                            DocNombre = lector.GetValue(2).ToString(),
                            DocArea = lector.GetValue(3).ToString(),
                            DocExtension = lector.GetValue(4).ToString(),
                            DocRuta = lector.GetValue(10).ToString(),

                            DocUsuCrea = lector.GetValue(6).ToString()

                        };

                    }
                }
                return oLista;
            }
        }

      
    }
}
