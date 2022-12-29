using Aplicacion.Servicios;
using Aspose.Cells;
using Dominio;
using Infraestructura.Datos.Repositorios;
using MatrizDocumentos.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Syncfusion.XlsIO;
using static Infraestructura.Datos.Repositorios.Alerta.Enum;
using System.Web;
using System.Web.Mvc.Async;
using OfficeOpenXml.Sorting;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.VisualBasic;

namespace OrdendeCompra.Controllers.Ordenes
{
    public class OrdenesController : BaseController
    {


        private readonly IConfiguration configuration;

        public OrdenesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        ProveedorServicio ServicioProveedor()
        {
            ProveedorRepositorio repo = new ProveedorRepositorio(configuration);
            ProveedorServicio servicioProveedor = new ProveedorServicio(repo);
            return servicioProveedor;
        }

        OrdenServicio ServicioOrden()
        {
            OrdenRepositorio repo = new OrdenRepositorio(configuration);
            OrdenServicio servicioOrden = new OrdenServicio(repo);
            return servicioOrden;
        }

        QueryServicio ServicioQueryOrden()
        {
            QueryOrdenRepositorio repo = new QueryOrdenRepositorio(configuration);
            QueryServicio servicioQueryOrden = new QueryServicio(repo);
            return servicioQueryOrden;
        }

        ProductosServicio ServicioProducto()
        {
            ProductosRepositorio repo = new ProductosRepositorio(configuration);
            ProductosServicio productosServicio = new ProductosServicio(repo);

            return productosServicio;
        }


        ParametrosServicio ServicioParametro()
        {
            ParametrosRepositorio repo = new(configuration);
            ParametrosServicio servicio = new(repo);
            return servicio;
        }



        public async Task<DatosProveedor> ObtenerProveedores()
        {
            var Provedors = await ServicioProveedor().ListarProveedor();
            List<SelectListItem> lstProvedors = new();

            foreach (var proveedores in Provedors)
            {
                lstProvedors.Add(new SelectListItem { Value = proveedores.ID_PROVEEDOR.ToString(), Text = proveedores.NOMBRE });

            }

            DatosProveedor provedores = new();
            {
                provedores.Proveedors = lstProvedors;
                
            }
            return provedores;
        }

        public async Task<IActionResult> ObtenerOrden(IndexViewModel indexViewModel, OrdenCompra data)
        {
            #region Descargar Orden de Compra
            var oLista = new List<OrdenCompra>();
         
            if (data.tabla == null)
            {
                var queryOrden = await ServicioQueryOrden().QueryOrden(data);

                oLista.Add(new OrdenCompra()
                {
                    id_ordencompra = Convert.ToInt64(queryOrden[0].id_ordencompra),
                    fechasolicitud = Convert.ToDateTime(queryOrden[0].fechasolicitud),
                    Solicitante = queryOrden[0].Solicitante.ToString(),
                    codigodelproyecto = Convert.ToInt64(queryOrden[0].codigodelproyecto),
                    numerodelaorden = Convert.ToInt64(queryOrden[0].numerodelaorden),
                    codigopresupuestos = queryOrden[0].codigopresupuestos.ToString(),

                    id_proveedor = Convert.ToInt32(queryOrden[0].id_proveedor),
                    nombre = queryOrden[0].nombre.ToString(),
                    nit = queryOrden[0].nit.ToString(),
                    contacto = queryOrden[0].contacto.ToString(),
                    email = queryOrden[0].email.ToString(),
                    telefono = Convert.ToInt64(queryOrden[0].telefono),

                    garantia = queryOrden[0].garantia.ToString(),
                    acuerdosdepago = queryOrden[0].acuerdosdepago.ToString(),
                    incumplimiento = queryOrden[0].incumplimiento.ToString(),
                    entregas = queryOrden[0].entregas.ToString(),
                    seguimiento = queryOrden[0].seguimiento.ToString(),
                    soporteposventa = queryOrden[0].soporteposventa.ToString(),
                    otros = queryOrden[0].otros.ToString(),
                    valortotalorden = Convert.ToInt64(queryOrden[0].valortotalorden),
                });



                int cont = 0;
                foreach (var prd in queryOrden)
                {
                    cont++;
                    if (cont > 1)
                    {
                        //CantFilas = CantFilas + 1;
                        oLista.Add(new OrdenCompra()
                        {
                            descripcion = prd.descripcion,
                            cantidad = prd.cantidad,
                            valorunitario = prd.valorunitario,
                            totalantesiva = prd.totalantesiva
                        });
                    }
                }
            }
            else
            {
                // Validacion para Cuando sea un update  y no un insert
                //
                if (data.id_ordencompra != 0)
                {
                    var orden = await ServicioOrden().ActualizarOrdenCompra(data);
                }
                else
                {
                    var orden = await ServicioOrden().ListarOrden(data);
                }


            }

            ExcelEngine excelEngine = new ExcelEngine();
            string RutaPlantillaOrden = configuration["RutaPlantillaOrden"];
            string RutaOrden = configuration["RutaOrden"];
            string NombreOrden = configuration["NombreOrden"];
            long NumeroOrden = data.numerodelaorden;
            string NombreProveedor = data.nombre;

            FileStream inputStream = new FileStream(RutaPlantillaOrden, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(inputStream);
            IWorksheet worksheet = workbook.Worksheets[0];

            if (data.tabla == null)
            {
                //Datos Generales
                worksheet.Range["B6"].Value = oLista[0].fechasolicitud.ToString();
                worksheet.Range["B7"].Value = oLista[0].Solicitante.ToString();
                worksheet.Range["B8"].Value = oLista[0].codigodelproyecto.ToString();
                worksheet.Range["B9"].Value = oLista[0].numerodelaorden.ToString();
                worksheet.Range["B10"].Value = oLista[0].codigopresupuestos.ToString();

                //Datos del Proveedor
                worksheet.Range["B13"].Value = oLista[0].nombre.ToString();
                worksheet.Range["B14"].Value = oLista[0].nit.ToString();
                worksheet.Range["B15"].Value = oLista[0].contacto.ToString();
                worksheet.Range["B16"].Value = oLista[0].email.ToString();
                worksheet.Range["B17"].Value = oLista[0].telefono.ToString();

                NombreOrden = (NombreOrden + oLista[0].numerodelaorden.ToString() + "_" + oLista[0].nombre.ToString());
                //Datos del Producto
                int CantFilas = 20;
                int cont = 0;
                int totals = oLista.Count() - 1;
                int tot = 0;
                //Insertar Filas
                if (totals > 5)
                {
                    int cantinsertar = totals - 5;
                    worksheet.InsertRow(25, cantinsertar, ExcelInsertOptions.FormatAsBefore);
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
                        worksheet.Range["A" + CantFilas].Value = tbl.descripcion;
                        worksheet.Range["D" + CantFilas].Value = tbl.cantidad.ToString(); ;
                        worksheet.Range["E" + CantFilas].Value = tbl.valorunitario.ToString();
                        worksheet.Range["F" + CantFilas].Value = tbl.totalantesiva.ToString();
                    }
                }

                // Total          
                worksheet.Range["F" + tot].Value = oLista[0].valortotalorden.ToString();

                var pos1 = tot + 1;
                var pos2 = tot + 2;
                var pos3 = tot + 3;
                var pos4 = tot + 4;
                var pos5 = tot + 5;
                var pos6 = tot + 6;
                var pos7 = tot + 7;

                //Garantias
                worksheet.Range["B" + pos1].Value = (oLista[0].garantia);
                worksheet.Range["B" + pos2].Value = (oLista[0].acuerdosdepago);
                worksheet.Range["B" + pos3].Value = (oLista[0].incumplimiento);
                worksheet.Range["B" + pos4].Value = (oLista[0].entregas);
                worksheet.Range["B" + pos5].Value = (oLista[0].seguimiento);
                worksheet.Range["B" + pos6].Value = (oLista[0].soporteposventa);
                worksheet.Range["B" + pos7].Value = (oLista[0].otros);


                //Alerta
                int con = oLista.Count();

                if (con <= 1)
                {
                    Alert(configuration["MsgError"], NotificationType.error);
                }
                else
                {
                    Alert(configuration["MsgSucces"], NotificationType.Exito);
                }

            }

            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");       
            fileStreamResult.FileDownloadName = NombreOrden + ".xlsx";

            return fileStreamResult;
            
            #endregion
        }


    
        // Controladores para la CRUD

        public async Task<IActionResult> ListarOrdenes(IndexViewModel indexViewModel)
        {
            indexViewModel.OrdenCompraList = await ServicioOrden().Listar();




            return View(indexViewModel);
        }


      
        public async Task<IActionResult> Index(int id_ordencompra)
        {
            ///Lista para obtener las monedas
            var Extenciones = await ServicioParametro().BuscarParametro("Moneda");
            List<SelectListItem> listaParametros = new();
           
            foreach (var parametro in Extenciones)
            {
                listaParametros.Add(new SelectListItem { Value = parametro.VALORPARAMETRO, Text = parametro.VALORPARAMETRO });
            }
            ////////////////
            IndexViewModel objetoOrden = new IndexViewModel()
            {
                OrdenCompra = new OrdenCompra() { ListaMoneda = listaParametros }
            };

            objetoOrden.DatosProveedor = await ObtenerProveedores();
            


            if (objetoOrden.Proveedor is not null)

            {
                //indexViewModel.ProveedorList = await ServicioProveedor().Buscar(indexViewModel.Proveedor);
            }
            else
            {
                objetoOrden.ProveedorList = await ServicioProveedor().ListarProveedor();
              

            }


            /// agregacion  para el update de la orden
            if (id_ordencompra != 0)
            {
                objetoOrden.OrdenCompra = await ServicioOrden().BuscarOrdenCompra(id_ordencompra); // traer la orden de compra
                long idProveedor = objetoOrden.OrdenCompra.id_proveedor; // Id para traer a los provedor correspondientes a la orden
                objetoOrden.OrdenCompra.tabla = await ServicioProducto().BuscarProductosDeOrden(id_ordencompra); // Un elemento orden de compra que contiene los productos
                objetoOrden.DatosProveedor = await ObtenerProveedores(); // obtener la lista desplegable de provedores
                objetoOrden.Proveedor = await ServicioProveedor().BuscarProvedorIdProveedor(idProveedor); // obtener a los provedores que corresponden a la orden
                objetoOrden.OrdenCompra.ListaMoneda = listaParametros;
            }

            return View(objetoOrden);

        }

       
        // recibe el Id de la orden para posteriormente eliminarlo 
        public async Task<IActionResult> Eliminar(int id_ordencompra)
        {
            await ServicioOrden().Eliminar(id_ordencompra);

            return RedirectToAction("ListarOrdenes", "Ordenes");
        }


      

    }
}