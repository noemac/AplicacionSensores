using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using sensoresapp.Models;
using System.Threading.Tasks;
using sensoresapp.Utils;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace sensoresapp.Utils
{
    public class API
    {
        private const string IP_SERVIDOR = "http://192.168.0.173:8080/granja/";
        private const string URL_sensores = IP_SERVIDOR + "/sensores";
        private const string URL_sensores_Update = IP_SERVIDOR + "/sensores/update";
        private const string URL_sensores_Create = IP_SERVIDOR + "/sensores";
        private const string URL_sensores_Delete = IP_SERVIDOR + "/sensores/";


        /// <summary>
        /// Obtiene todos los sensores y devuelve una datatable
        /// </summary>
        /// <returns></returns>
        public static DataTable getSensores()
        {
            //Consultar web api, traer todos los sensors
            string url = URL_sensores;

            var table = new DataTable();

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);

                    var jsonPuro = data;
                }
            }

            return table;
        }

        public static List<ClaseSensorRegistro> BuscarRegistros(Sensor parametrosBusqueda)
        {

            //04- Creo una lista de mi clase ClaseSensor
            List<ClaseSensorRegistro> resultadosparaview = new List<ClaseSensorRegistro>();

            // listar sensores en grilla directamente
            // URL: http://techfunda.com/howto/305/consuming-external-web-api-in-asp-net-mvc
            //string url = "http://192.168.0.173:8080/granja/sensores";

            //Convertimos ahora ToLocalTime, porque al ser que que trabajamos con timestamp, que usa la hora universal
            //toma en cuenta que tenemos 3 horas menos en buenos aires GMT-3
            //https://greenwichmeantime.com/time-zone/south-america/argentina/

            parametrosBusqueda.FechaDesde = parametrosBusqueda.FechaDesde.ToLocalTime();
            parametrosBusqueda.FechaHasta = parametrosBusqueda.FechaHasta.ToLocalTime();

            var anio1 = parametrosBusqueda.FechaDesde.Year;
            var mes1 = parametrosBusqueda.FechaDesde.Month.ToString("00");
            var dia1 = parametrosBusqueda.FechaDesde.Day.ToString("00");
            var hora1 = parametrosBusqueda.FechaDesde.Hour.ToString("00");
            var minuto1 = parametrosBusqueda.FechaDesde.Minute.ToString("00");

            var anio2 = parametrosBusqueda.FechaHasta.Year;
            var mes2 = parametrosBusqueda.FechaHasta.Month.ToString("00");
            var dia2 = parametrosBusqueda.FechaHasta.Day.ToString("00");
            var hora2 = parametrosBusqueda.FechaHasta.Hour.ToString("00");
            var minuto2 = parametrosBusqueda.FechaHasta.Minute.ToString("00");

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
            //httpClient
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);

                    var jsonPuro = data;

                    //**********************************************************************
                    //Convierto de Json a una lista de dynamics, (para poder leer los items)
                    List<dynamic> results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(jsonPuro);

                    if (results.Count > 0)
                    {
                        //Filtro por id de sensor con LINQ
                        var resultadofiltrado = new List<dynamic>();
                        if (parametrosBusqueda.Id != 0)
                        {
                            resultadofiltrado = (from item in results where item.id_sensor == parametrosBusqueda.Id select item).ToList();
                        }
                        else
                        {
                            resultadofiltrado = (from item in results select item).ToList();
                        }


                        if (resultadofiltrado.Count > 0)
                        {
                            //05- recorro cada item que tenga results, que es la conversión de json a lista de dynamic
                            foreach (var item in results)
                            {
                                //creo una instancia por cada registro de sensor
                                var itemSensor = new ClaseSensorRegistro();

                                itemSensor.id = item.id;
                                itemSensor.fechalectura = Utilities.convertirTimeStampADateTime(item.lectura); //funcion para convertir timestamp a datetime
                                //var itemSensor.fechalectura = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(1372061224000 / 1000d)).ToLocalTime();
                                Console.WriteLine(itemSensor.fechalectura); // Prints: 6/24/2013 10:07:04 AM

                                itemSensor.temperatura = item.temperatura;
                                itemSensor.humedad = item.humedad;
                                itemSensor.amoniaco = item.amoniaco;
                                itemSensor.id_sensor = item.id_sensor;

                                //07- agrego a la lista el item creado
                                resultadosparaview.Add(itemSensor);

                            }
                        }

                    }
                    //**********************************************************************

                }
            }


            return resultadosparaview;
        }

 

        /// <summary>
        /// Trae sensor por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ClaseSensor getSensoresPorId(int id)
        {
            DataTable ListaDeSensores = API.getSensores();

            //Consulta con LINQ
            ClaseSensor resultado = (from item in ListaDeSensores.AsEnumerable()
                                           where Convert.ToInt32(item["id"]) == id
                                           select new ClaseSensor
                                           {
                                               id = Convert.ToInt32(item["id"]),
                                               ip = item["ip"].ToString(),
                                               mac = item["mac"].ToString(),
                                               puerto = item["puerto"].ToString(),
                                               refresco = item["refresco"].ToString(),
                                               ubicacion = item["ubicacion"].ToString()
                                           }).FirstOrDefault();

            return resultado;
        }


        public static bool ActualizarSensor(ClaseSensor model)
        {
            bool PudoActualizar = false;

            //Consultar web api, traer todos los sensors
            string url = URL_sensores_Update;

            var table = new DataTable();

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //System.Net.Http.HttpContent itemParaActualizar = new system.Net.Http.HttpContent();



                var jsonString = "{" +
                    "\"id\": "+ model.id +"," +
                    "\"ip\": \""+model.ip+"\"," +
                    "\"puerto\": "+ model.puerto +"," +
                    "\"mac\": \""+model.mac+"\"," +
                    "\"ubicacion\": \""+model.ubicacion+"\"," +
                    "\"refresco\": "+model.refresco +" " +
                    "}";

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                System.Net.Http.HttpResponseMessage response = client.PutAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    PudoActualizar = true;
                }
            }

            return PudoActualizar;
        }


        public static bool CrearSensor(ClaseSensor model)
        {
            bool PudoCrear = false;

            //Consultar web api, traer todos los sensors
            string url = URL_sensores_Create;

            var table = new DataTable();

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //System.Net.Http.HttpContent itemParaActualizar = new system.Net.Http.HttpContent();



                var jsonString = "{" +
                    "\"ip\": \"" + model.ip + "\"," +
                    "\"puerto\": " + model.puerto + "," +
                    "\"mac\": \"" + model.mac + "\"," +
                    "\"ubicacion\": \"" + model.ubicacion + "\"," +
                    "\"refresco\": " + model.refresco + " " +
                    "}";

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                System.Net.Http.HttpResponseMessage response = client.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    PudoCrear = true;
                }
            }

            return PudoCrear;
        }

        public static bool EliminarSensor(int id)
        {
            bool PudoEliminar = false;

            //Consultar web api, traer todos los sensors
            string url = URL_sensores_Delete + id;

            var table = new DataTable();

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpResponseMessage response = client.DeleteAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    PudoEliminar = true;
                }
            }

            return PudoEliminar;

        }
    }
}