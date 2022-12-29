using Aplicacion.Servicios;
using Dominio;
using Infraestructura.Datos.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace MatrizDocumentos.Controllers.Proveedores
{
    public class Proveedores : Controller
    {
        private readonly IConfiguration configuration;

        public Proveedores(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        ProveedorServicio ServicioProveedor()
        {
            ProveedorRepositorio repo = new ProveedorRepositorio(configuration);
            ProveedorServicio servicio = new ProveedorServicio(repo);
            return servicio;
        }


        // GET: Proveedores
        public async Task<IActionResult> Index(IndexViewModel indexViewModel)
        {
            indexViewModel.ProveedorList = await ServicioProveedor().ListarProveedor();

            Console.WriteLine (indexViewModel.ProveedorList);
            return View(indexViewModel);
        }
        public async Task<IActionResult> Cancelar()
        {       
            return View();
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Proveedores/Create
        [HttpGet]
        public async Task<IActionResult> CrearProveedor()
        {
            ViewBag.Visible = false;
            Proveedor proveedor = new Proveedor();
            return View(proveedor);
        }

        // POST: Proveedores/Create
        [HttpPost]
        public async Task<IActionResult> CrearProveedor(Proveedor proveedor)

        {
            if (ModelState.IsValid)
            {
                IndexViewModel indexVM = new IndexViewModel();
                indexVM.ProveedorList = await ServicioProveedor().ListarProveedor();
                var proveedorEncontrado = indexVM.ProveedorList.Where(x => x.NIT == proveedor.NIT).FirstOrDefault();

                if (proveedorEncontrado != null)
                {
                    ViewBag.Visible = true;
                    return View(proveedor);
                }

                ViewBag.Visible = false;
                await ServicioProveedor().CreateAsync(proveedor);
                return RedirectToAction("index", "Ordenes");


            }
            ViewBag.Visible = false;
            return View(proveedor);

        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Proveedores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proveedores/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
                
       
        public async Task<IActionResult> Eliminar(int ID_PROVEEDOR)
        {
            await ServicioProveedor().Eliminar(ID_PROVEEDOR);

            return RedirectToAction("Index", "Proveedores");
        }

        public async Task<IActionResult> EditarProveedor(int ID_PROVEEDOR)
        {
            Proveedor proveedor = await ServicioProveedor().BuscarProveedor(ID_PROVEEDOR);
            ViewBag.Visible = false;

            return View(proveedor);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProveedor(Proveedor proveedor)
        {
            await ServicioProveedor().EditarProveedor(proveedor);
            return RedirectToAction("Index" , "Proveedores");
        }

    }
}
