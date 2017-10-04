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
        // GET: Sensor
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.resultado = null;

            //Obtener sensores
            DataTable sensores = API.getSensores();

            //Popular ddl
            ViewBag.SensoresDDL = Utilities.LlenarDropDownList(sensores);

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



        // POST: Sensor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buscar(Sensor parametrosBusqueda)
        {
            ViewBag.resultado = null;

            //Buscamos los registros
            ViewBag.resultado = API.BuscarRegistros(parametrosBusqueda);

            //Obtengo cantidad de resultados
            var cantidadderesultados = (ViewBag.resultado as List<ClaseSensor>).Count; 
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

            return View("Index");
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
