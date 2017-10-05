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

namespace sensoresapp.Controllers
{
    public class SensorController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            //ViewBag.resultado = null;

            ////Obtener sensores
            DataTable sensores = API.getSensores();

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

            sensoresapp.Utils.ClaseSensor hola = new ClaseSensor();
            hola.id = 1;
            hola.ip = "127.0.0.1";
            hola.mac = "127.0.0.2";
            hola.puerto = "127";
            hola.refresco = "1000";
            hola.ubicacion = "12425454,5314231";

            return View(hola);
        }

        // GET: Sensor/Details/5
        public ActionResult Detalle(int id)
        {
            ViewBag.IdSensor = id;

            return View();
        }


        // GET: Sensor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sensor/Create
        public ActionResult Create()
        {
            return View();
        }









        // GET: Sensor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sensor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sensor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sensor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
