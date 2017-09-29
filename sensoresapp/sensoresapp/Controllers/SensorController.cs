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
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            ViewBag.resultado = null;

            //Consultar web api, traer todos los sensors
            string url = "http://192.168.0.173:8080/granja/sensores";

            var table = new DataTable();

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);

                    var jsonPuro = data;
                }
            }

            //Popular ddl
            List<SelectListItem> _sensores = new List<SelectListItem>();
            _sensores.Add(new SelectListItem { Text = "Seleccione Sensor", Value = "0" });

            foreach (DataRow item in table.Rows)
            {
                _sensores.Add(new SelectListItem { Text = "Sensor" + item["id"], Value = item["id"].ToString() });
            }

            ViewBag.SensoresDDL = new SelectList(_sensores, "Value", "Text");

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

        //01- Creo una clase para devolver a la view y que sea mas facil la lectura
        public class ClaseSensor
        {
            public int id { get; set; }
            public string fechalectura { get; set; }
            public int temperatura { get; set; }
            public int humedad { get; set; }
            public double amoniaco { get; set; }
            public int id_sensor { get; set; }
        }

        // POST: Sensor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Buscar(Sensor parametrosBusqueda)
        {
            ViewBag.resultado = null;

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
            ViewBag.CantidadResultados = string.Empty;

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

                    //**********************************************************************
                    //Convierto de Json a una lista de dynamics, (para poder leer los items)
                    List<dynamic> results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(jsonPuro);

                    if (results.Count > 0)
                    {
                        //Filtro por id de sensor con LINQ
                        var resultadofiltrado = (from item in results where item.id_sensor == parametrosBusqueda.Id select item).ToList();

                        if (resultadofiltrado.Count > 0)
                        {
                            ViewBag.Mensaje = string.Empty;
                            ViewBag.CantidadResultados = "<h3>Cantidad de Resultados: " + resultadofiltrado.Count() + "</h3>";

                            //04- Creo una lista de mi clase ClaseSensor
                            List<ClaseSensor> resultadosparaview = new List<ClaseSensor>();

                            //05- recorro cada item que tenga results, que es la conversión de json a lista de dynamic
                            foreach (var item in results)
                            {
                                //creo una instancia por cada registro de sensor
                                var itemSensor = new ClaseSensor();

                                itemSensor.id = item.id;
                                // itemSensor.fechalectura = convertirTimeStampADateTime(item.lectura); //funcion para convertir timestamp a datetime
                                //var itemSensor.fechalectura = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(1372061224000 / 1000d)).ToLocalTime();
                                Console.WriteLine(itemSensor.fechalectura); // Prints: 6/24/2013 10:07:04 AM

                                itemSensor.temperatura = item.temperatura;
                                itemSensor.humedad = item.humedad;
                                itemSensor.amoniaco = item.amoniaco;
                                itemSensor.id_sensor = item.id_sensor;

                                //07- agrego a la lista el item creado
                                resultadosparaview.Add(itemSensor);

                            }

                            //08- Devuelvo el item a viewbag con el listado---> Ahora vamos a la View a "Views\Home\Index.cshtml"
                            ViewBag.resultado = resultadosparaview;
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

                    //**********************************************************************



                    //volcar data como json a viewbag
                    #region CODIGO VIEJO
                    //ViewBag.listado = data;

                    //if (table.Rows.Count>0)
                    //{
                    //    DataRow[] resultBusquedaPorIdSensor = null;

                    //    //parametrosBusqueda.Id == 0 traeme todos los resultados sin filtrar.
                    //    if (parametrosBusqueda.Id > 0)
                    //    {
                    //        resultBusquedaPorIdSensor = table.Select("id_sensor = " + parametrosBusqueda.Id + "");
                    //    }
                    //    else
                    //    {
                    //        resultBusquedaPorIdSensor = table.Select();
                    //    }

                    //    if (resultBusquedaPorIdSensor.Count() > 0)
                    //    {
                    //        ViewBag.Mensaje = string.Empty;
                    //        ViewBag.CantidadResultados = "<h3>Cantidad de Resultados: " + resultBusquedaPorIdSensor.Count() + "</h3>";

                    //        var tableFiltrada = resultBusquedaPorIdSensor.CopyToDataTable();

                    //        System.Web.UI.WebControls.GridView gView = new System.Web.UI.WebControls.GridView();
                    //        gView.DataSource = tableFiltrada;
                    //        gView.DataBind();
                    //        using (System.IO.StringWriter sw = new System.IO.StringWriter())
                    //        {
                    //            using (System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw))
                    //            {
                    //                gView.RenderControl(htw);
                    //                ViewBag.GridViewString = sw.ToString();
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        ViewBag.Mensaje = "Sin Resultados";
                    //    }
                    //}
                    //else
                    //{
                    //    ViewBag.Mensaje = "Sin Resultados";
                    //}
                    #endregion  CODIGO VIEJO

                }
            }

            /**/
            #endregion


            //Consultar web api, traer todos los sensores, por SEGUNDA VEZ, por perder los valores
            //que habia cargado previamente en index
            url = "http://192.168.0.173:8080/granja/sensores";

            var table2 = new DataTable();

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    table2 = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);

                    var jsonPuro = data;
                }
            }

            //Popular ddl
            List<SelectListItem> _sensores = new List<SelectListItem>();
            _sensores.Add(new SelectListItem { Text = "Seleccione Sensor", Value = "0" });

            foreach (DataRow item in table2.Rows)
            {
                _sensores.Add(new SelectListItem { Text = "Sensor" + item["id"], Value = item["id"].ToString() });
            }

            ViewBag.SensoresDDL = new SelectList(_sensores, "Value", "Text");

            return View("Index");
        }

        /// <summary>
        /// Esta metodo convierte de TimeSpan a Datetime (lo devuelvo convertido a un string de todos modos)
        /// </summary>
        /// <param name="lectura"></param>
        /// <returns></returns>
        private string convertirTimeStampADateTime(dynamic lectura)
        {
            double timestamp = (double)lectura;
            DateTime lectura_date1 = new DateTime(Convert.ToInt64(timestamp));
            DateTime lecturafechadatetime = Convert.ToDateTime(lectura_date1);
            //var lecturafecha = Convert.ToDateTime(lectura_date1).ToString("dd-mm-yyyy HH:mm:ss");            

            return lecturafechadatetime.ToString();
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
