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



    }
}
