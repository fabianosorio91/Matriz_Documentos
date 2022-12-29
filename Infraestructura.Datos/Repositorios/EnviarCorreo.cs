using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using Dominio;
using System.Data.SqlClient;
using System.Data;
using DocumentFormat.OpenXml.Office2016.Excel;
using System.Web.Mvc;

namespace Infraestructura.Datos.Repositorios
{
    public class EnviarCorreo
    {
        private readonly IConfiguration configuration;

        public EnviarCorreo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task SendEmailAsync(List<OrdenCompra> oLista)
        {
            try
            {
                var correo = "";
                var asunto = "";
                var cuerpo = "";
                var cadenaSQL = new Conexion(configuration).GetConnectionString();

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    await conexion.OpenAsync();
                    SqlCommand cmd = new SqlCommand("SP_GetParametros", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            Dictionary<string, string> parametros = new Dictionary<string, string>()
                            {
                                { dr["NOMBREPARAMETRO"].ToString(), dr["VALORPARAMETRO"].ToString()},
                            };

                            if (parametros.TryGetValue("correo", out string rescorreo))
                            {
                                correo = rescorreo;
                            }

                            if (parametros.TryGetValue("asunto", out string resasunto))
                            {
                                asunto = resasunto;
                            }

                            if (parametros.TryGetValue("cuerpo", out string rescuerpo))
                            {
                                cuerpo = rescuerpo;
                            }
                        }
                    }
                }

                string EMAILADDRESS = configuration["EMAILADDRESS"];
                string Host = configuration["Host"];
                string Credentials = configuration["Credentials"];
                int Port = Convert.ToInt32(configuration["Port"]);
                string RutaOrden = configuration["RutaOrden"];
                long NumeroOrden = oLista[0].numerodelaorden;
                string NombreProveedor = oLista[0].nombre;

                // Credentials
                var credentials = new NetworkCredential(EMAILADDRESS, Credentials);

                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress(EMAILADDRESS, asunto),
                    Subject = asunto,
                    Body = cuerpo,
                    IsBodyHtml = true

                };

                string filePath = (@RutaOrden + "_" + NumeroOrden + "_" + NombreProveedor + ".xlsx");
                mail.Attachments.Add(new Attachment(GetStreamFile(filePath), Path.GetFileName(filePath), "application/excel"));

                string[] destinatario = correo.Split(';');

                foreach (string destinos in destinatario)
                {
                    mail.To.Add(new MailAddress(destinos));
                }

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = Port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = Host,
                    EnableSsl = false,
                    Credentials = credentials
                };

                // Send it...         
                client.Send(mail);

                //Eliminar el Archivo 
                File.Delete(filePath);


            }
            catch (Exception ex)
            {

            }

        }

        public Stream GetStreamFile(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                MemoryStream memStream = new MemoryStream();
                memStream.SetLength(fileStream.Length);
                fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);

                return memStream;
            }
        }

    }
}
