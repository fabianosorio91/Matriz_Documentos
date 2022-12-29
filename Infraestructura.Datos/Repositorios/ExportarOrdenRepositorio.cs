using Dominio;
using Microsoft.Extensions.Configuration;
using Aspose.Cells;
using Syncfusion.XlsIO;
using NuGet.ContentModel;
using System.Diagnostics;
using System.Web.Mvc;

namespace Infraestructura.Datos.Repositorios
{
    public class ExportarOrdenRepositorio
    {
        private readonly IConfiguration configuration;

        public ExportarOrdenRepositorio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ExportarOrden(List<OrdenCompra> oLista)
        {

            //Creates a new instance for ExcelEngine
            ExcelEngine excelEngine = new ExcelEngine();

            try
            {
                string RutaPlantillaOrden = configuration["RutaPlantillaOrden"];
                string RutaOrden = configuration["RutaOrden"];
                long NumeroOrden = oLista[0].numerodelaorden;
                string NombreProveedor = oLista[0].nombre;
                //Leer la plantilla de Excel
                FileStream inputStream = new FileStream(@RutaPlantillaOrden, FileMode.Open, FileAccess.Read);
                IWorkbook workbook = excelEngine.Excel.Workbooks.Open(inputStream);

                Workbook workbooks = new Workbook(inputStream.Name);
                Worksheet worksheet = workbooks.Worksheets[0];


                // Configuración de las opciones de formato
                InsertOptions insertOptions = new InsertOptions();
                insertOptions.CopyFormatType = CopyFormatType.SameAsAbove;

                //Datos Generales
                worksheet.Cells["B6"].PutValue(oLista[0].fechasolicitud);
                worksheet.Cells["B7"].PutValue(oLista[0].Solicitante);
                worksheet.Cells["B8"].PutValue(oLista[0].codigodelproyecto);
                worksheet.Cells["B9"].PutValue(oLista[0].numerodelaorden);
                worksheet.Cells["B10"].PutValue(oLista[0].codigopresupuestos);

                //Datos del Proveedor
                worksheet.Cells["B13"].PutValue(oLista[0].nombre);
                worksheet.Cells["B14"].PutValue(oLista[0].nit);
                worksheet.Cells["B15"].PutValue(oLista[0].contacto);
                worksheet.Cells["B16"].PutValue(oLista[0].email);
                worksheet.Cells["B17"].PutValue(oLista[0].telefono);

                //Datos del Producto
                int CantFilas = 20;
                int cont = 0;
                int totals = oLista.Count() - 1;
                int tot = 0;
                //Insertar Filas
                if (totals > 5)
                {
                    int cantinsertar = totals - 5;
                    worksheet.Cells.InsertRows(25, cantinsertar, insertOptions);
                    tot = 26 + cantinsertar;
                }
                else
                {
                    tot = 26;
                }

                foreach (var tbl in oLista)
                {
                    cont++;
                    if (cont > 1)
                    {
                        CantFilas = CantFilas + 1;
                        worksheet.Cells["A" + CantFilas].PutValue(tbl.descripcion);
                        worksheet.Cells["D" + CantFilas].PutValue(tbl.cantidad);
                        worksheet.Cells["E" + CantFilas].PutValue(tbl.valorunitario);
                        worksheet.Cells["F" + CantFilas].PutValue(tbl.totalantesiva);
                    }
                }

                // Total          
                worksheet.Cells["F" + tot].PutValue(oLista[0].valortotalorden);

                var pos1 = tot + 1;
                var pos2 = tot + 2;
                var pos3 = tot + 3;
                var pos4 = tot + 4;
                var pos5 = tot + 5;
                var pos6 = tot + 6;
                var pos7 = tot + 7;

                //Garantias
                worksheet.Cells["B" + pos1].PutValue(oLista[0].garantia);
                worksheet.Cells["B" + pos2].PutValue(oLista[0].acuerdosdepago);
                worksheet.Cells["B" + pos3].PutValue(oLista[0].incumplimiento);
                worksheet.Cells["B" + pos4].PutValue(oLista[0].entregas);
                worksheet.Cells["B" + pos5].PutValue(oLista[0].seguimiento);
                worksheet.Cells["B" + pos6].PutValue(oLista[0].soporteposventa);
                worksheet.Cells["B" + pos7].PutValue(oLista[0].otros);

                //Guardar
                workbooks.Save(@RutaOrden + "_" + NumeroOrden + "_" + NombreProveedor + ".xlsx");

                workbook.Close();
                inputStream.Close();


            }

            catch (Exception ex)
            {

            }


        }
    }

}