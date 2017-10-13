using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sensoresapp.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using sensoresapp.Utils;
using Newtonsoft.Json;

namespace sensoresapp.Controllers
{
    public class SensorController : Controller
    {
   
        public ActionResult GraficoHistorico()
        {
            return View();
        }

        public PartialViewResult ObtenerGrafico(int id)
        {
            var nombreDePartialView = string.Empty;

            switch (id)
            {
                case 1:
                    nombreDePartialView = "~/Views/Shared/_Humedad.cshtml";
                    break;
                case 2:
                     nombreDePartialView = "~/Views/Shared/_Temperatura.cshtml";
                    break;
                case 3:
                    nombreDePartialView = "~/Views/Shared/_Amoniaco.cshtml";
                    break;
            }
            return PartialView(nombreDePartialView);
        }

        public ActionResult GraficoVivo()
        {
            return View();
        }
        public ActionResult Humedad()
        {
            return View();
        }
        public ActionResult Amoniaco()
        {
            return View();
        }
        public ActionResult Temperatura()
        {
            return View();
        }

        [Authorize]
        public ActionResult Index()
        {
            //ViewBag.resultado = null;

            ////Obtener sensores
            DataTable sensores = API.getSensores();

            /*trabajandon con JSON linq Generic*/

            var reqUsers = from item in sensores.AsEnumerable()
                           select new
                           {
                               Id = item["id"],
                               PlaceName = "Sensor" + item["id"],
                               GeoLong = Utilities.ObtenerGeoLong(item["ubicacion"].ToString()),
                               GeoLat = Utilities.ObtenerGeoLat(item["ubicacion"].ToString())
                           };




            ViewBag.Ubicaciones = JsonConvert.SerializeObject(reqUsers); 

            ViewBag.resultado = Utilities.ConvertirALista(sensores);
            ////Popular ddl
            //ViewBag.SensoresDDL = Utilities.LlenarDropDownList(sensores);

            return View();
        }

        // GET: Sensor
        [Authorize]
        public ActionResult Buscador()
        {
            ViewBag.resultado = null;

            //Obtener sensores
            DataTable sensores = API.getSensores();

            //Popular ddl
            ViewBag.SensoresDDL = Utilities.LlenarDropDownList(sensores);

            return View();
        }

        // POST: Sensor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buscador(Sensor parametrosBusqueda)
        {
            ViewBag.resultado = null;

            //Buscamos los registros
            ViewBag.resultado = API.BuscarRegistros(parametrosBusqueda);

            //Obtengo cantidad de resultados
            var cantidadderesultados = (ViewBag.resultado as List<ClaseSensorRegistro>).Count;
            //Verifico la cantidad de resultados
            if (cantidadderesultados > 0)
            {
                ViewBag.CantidadResultados = "<h3>Cantidad de Resultados: " + cantidadderesultados + "</h3>";
            }
            else
            {
                ViewBag.resultado = null; // Para ocultar tabla en view
                ViewBag.Mensaje = "Sin Resultados";
            }


            //Obtener sensores
            DataTable sensores = API.getSensores();

            //Cargar dropdownlist
            ViewBag.SensoresDDL = Utilities.LlenarDropDownList(sensores);

            return View();
        }


        public ActionResult Editar(int id)
        {
            ViewBag.IdSensor = id;
            ViewBag.Actualizo = false;
            ViewBag.Mensaje = string.Empty;

            ClaseSensor sensorSeleccionado = API.getSensoresPorId(id);

            return View(sensorSeleccionado);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ClaseSensor model, string returnUrl)
        {
            returnUrl = null;

            if (ModelState.IsValid)
            {
                bool PudoActualizar = API.ActualizarSensor(model);
                if (PudoActualizar)
                {
                    ViewBag.Actualizo = true;
                    ViewBag.Mensaje = "Actualizó Correctamente, Presione Volver a lista";
                }
            }


            // If we got this far, something failed, redisplay form
            return View(model);



        }

        // GET: Sensor/Details/5
        public ActionResult Detalle(int id)
        {

            ViewBag.IdSensor = id;
            ViewBag.Actualizo = false;
            ViewBag.Mensaje = string.Empty;

            ClaseSensor sensorSeleccionado = API.getSensoresPorId(id);

            return View(sensorSeleccionado);
        }


        // GET: Sensor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sensor/Create
        public ActionResult Crear()
        {
            ViewBag.IdSensor = "";
            ViewBag.Creo = false;
            ViewBag.Mensaje = string.Empty;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(ClaseSensor model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                bool PudoCrear = API.CrearSensor(model);
                if (PudoCrear)
                {
                    ViewBag.Creo = true;
                    ViewBag.Mensaje = "Sensor Creado Correctamente, Presione Volver a lista";
                }

                //ViewBag.IdSensor = "";
                //ViewBag.Creo = false;
                //ViewBag.Mensaje = string.Empty;
            }

            return View();
        }

        // GET: Sensor/Delete/5
        public ActionResult Eliminar(int id)
        {
            bool PudoEliminar = API.EliminarSensor(id);
            if (PudoEliminar)
            {
                ViewBag.Mensaje = "Eliminó Correctamente, Presione Volver a lista";
            }
            else
            {
                ViewBag.Mensaje = "No se pudo eliminar, Presione Volver a lista";
            }

            //ViewBag.IdSensor = id;
            //ViewBag.Elimino = false;
            //ViewBag.Mensaje = string.Empty;
            return View();
        }

    }
}
