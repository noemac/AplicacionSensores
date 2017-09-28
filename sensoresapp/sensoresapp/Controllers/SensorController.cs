using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sensoresapp.Models;
using System.Data;

namespace sensoresapp.Controllers
{
    public class SensorController : Controller
    {
        // GET: Sensor
        [Authorize]
        public ActionResult Index()
        {


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
        public async System.Threading.Tasks.Task<ActionResult> Buscar(Sensor parametrosBusqueda)
        {
            #region listar sensores en grilla directamente
            // URL: http://techfunda.com/howto/305/consuming-external-web-api-in-asp-net-mvc
            //string url = "http://192.168.0.173:8080/granja/sensores";

            var anio1 = parametrosBusqueda.FechaDesde.Year;
            var mes1 = parametrosBusqueda.FechaDesde.Month;
            var dia1 = parametrosBusqueda.FechaDesde.Day;
            var hora1 = parametrosBusqueda.FechaDesde.Hour;
            var minuto1 = parametrosBusqueda.FechaDesde.Minute;

            var anio2 = parametrosBusqueda.FechaHasta.Year;
            var mes2 = parametrosBusqueda.FechaHasta.Month;
            var dia2 = parametrosBusqueda.FechaHasta.Day;
            var hora2 = parametrosBusqueda.FechaHasta.Hour;
            var minuto2 = parametrosBusqueda.FechaHasta.Minute;

            ViewBag.Mensaje = string.Empty;

            string url = 
            string.Format("http://192.168.0.173:8080/granja/sensores/fecha?date1={0}-{1}-{2} {3}:{4}:00&date2={5}-{6}-{7} {8}:{9}:00",
            anio1,
            mes1,
            dia1,
            hora1,
            minuto1,
            anio2,
            mes2,
            dia2,
            hora2,
            minuto2
            );

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);

                    var jsonPuro = data;


                    if (table.Rows.Count>0)
                    {
                        DataRow[] resultBusquedaPorIdSensor = null;

                        //parametrosBusqueda.Id == 0 traeme todos los resultados sin filtrar.
                        if (parametrosBusqueda.Id > 0)
                        {
                            resultBusquedaPorIdSensor = table.Select("id_sensor = " + parametrosBusqueda.Id + "");
                        }
                        else
                        {
                            resultBusquedaPorIdSensor = table.Select();
                        }

                        if (resultBusquedaPorIdSensor.Count() > 0)
                        {
                            ViewBag.Mensaje = string.Empty;

                            var tableFiltrada = resultBusquedaPorIdSensor.CopyToDataTable();

                            System.Web.UI.WebControls.GridView gView = new System.Web.UI.WebControls.GridView();
                            gView.DataSource = tableFiltrada;
                            gView.DataBind();
                            using (System.IO.StringWriter sw = new System.IO.StringWriter())
                            {
                                using (System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw))
                                {
                                    gView.RenderControl(htw);
                                    ViewBag.GridViewString = sw.ToString();
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Mensaje = "Sin Resultados";
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "Sin Resultados";
                    }
                    
                }
            }

            /**/
            #endregion

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
