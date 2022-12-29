using Aplicacion.Servicios;
using Dominio;
using Infraestructura.Datos.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace MatrizDocumentos.Controllers.Documentos
{
    public class DocumentosController : Controller
    {
        private readonly IConfiguration configuration;

        public DocumentosController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        AreaServicio ServicioArea()
        {
            AreaRepositorio repo = new(configuration);
            AreaServicio servicio = new(repo);
            return servicio;
        }
        DocumentoServicio ServicioDocumento()
        {
            DocumentoRepositorio repo = new DocumentoRepositorio(configuration);
            DocumentoServicio servicioDocumento = new DocumentoServicio(repo);
            return servicioDocumento;
        }


        ParametrosServicio ServicioParametro()
        {
            ParametrosRepositorio repo = new(configuration);
            ParametrosServicio servicio = new(repo);
            return servicio;
        }

        public async Task<IActionResult> Index(IndexViewModel indexViewModel)
        {

            indexViewModel.Documento = await DocumentosAreas();

            if (indexViewModel.Area is not null)

            {
                indexViewModel.DocumentoList = await ServicioDocumento().Buscar(indexViewModel.Area);
            }
            else
            {
                indexViewModel.DocumentoList = await ServicioDocumento().Listar();

            }


            return View(indexViewModel);
        }

        public IActionResult Redireccionar(string url)
        {
            if (url == null)
            {
                return View("ErrorRuta");
            }

            if (!url.Contains("http"))
            {
                var name = Path.GetFileName(url);
                var extension = Path.GetExtension(url);

                Process process = new();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.WorkingDirectory = Path.GetDirectoryName(url);
                process.StartInfo.FileName = name;
                process.Start();
                return RedirectToAction("Index");
            }
            return Redirect(url);

        }


        private async Task<Documento> DocumentosAreas()
        {
            var Areas = await ServicioArea().Listar();
            List<SelectListItem> lstAreas = new();

            foreach (var area in Areas)
            {
                lstAreas.Add(new SelectListItem { Value = area.AreId.ToString(), Text = area.AreNombre });

            }

            Documento documentos = new();
            {
                documentos.Areas = lstAreas;
            }
            return documentos;
        }


        [HttpGet] //Se manda a la vista documento detalle la cual dependiendo de la validacion del id del documento recibido crea un nuevo campo
        //o se ejecuta El IBuscarDocumento
        public async Task<IActionResult> Documento_Detalle(int DocId)
        {
            //Lista desplegable para areas

            var Areas = await ServicioArea().Listar();
            List<SelectListItem> lstAreas = new();

            foreach (var area in Areas)
            {
                lstAreas.Add(new SelectListItem { Value = area.AreId.ToString(), Text = area.AreNombre });

            }

            // Lista desplegable para Extenciones

            var Extenciones= await ServicioParametro().BuscarParametro("Extension");
            List<SelectListItem> listaParametros = new();

            foreach (var parametro in Extenciones)
            {
                listaParametros.Add(new SelectListItem { Value = parametro.VALORPARAMETRO, Text = parametro.VALORPARAMETRO });
            }
            // fin feature
            IndexViewModel objetoDocumento = new IndexViewModel()
            {
                Documento = new Documento() { Areas = lstAreas, ListaExtenciones = listaParametros }

            };

            if (DocId != 0)
            {
               

              objetoDocumento.Documento =  await ServicioDocumento().DocumentoAActualizar(DocId);
              objetoDocumento.Documento.Areas= lstAreas;
              objetoDocumento.Documento.ListaExtenciones= listaParametros;
            }


          
            return View(objetoDocumento);
        }

        [HttpPost]// Si en el http get se recibe un id=0 entonces se habilita dentro de la  vista la opcion de crear
        public async Task<IActionResult> Documento_Detalle(IndexViewModel objetoDocumento)
        {
            if (objetoDocumento.Documento.DocId == 0)
            {
             await ServicioDocumento().Guardar(objetoDocumento.Documento);
            }
            else
            {
                await ServicioDocumento().Actualizar(objetoDocumento.Documento);
            }

            return RedirectToAction("Index", "Documentos");
        }



        [HttpGet]
        // recibe el Id del documento para posteriormente eliminarlo 
        public async Task<IActionResult> Eliminar(int DocId)
        {
            await ServicioDocumento().Eliminar(DocId);

            return RedirectToAction("Index", "Documentos");
        }
    }
}
