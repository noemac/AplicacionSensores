using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sensoresapp.Utils
{
    public static class Utilities
    {
        /// <summary>
        /// Esta metodo convierte de TimeSpan a Datetime (lo devuelvo convertido a un string de todos modos)
        /// </summary>
        /// <param name="lectura"></param>
        /// <returns></returns>
        public static string convertirTimeStampADateTime(dynamic lectura)
        {
            var primeros10dig = Convert.ToDouble(Convert.ToString(lectura).Substring(0, 10));
            var respuesta = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(primeros10dig).ToString("dd/MM/yyyy HH:mm:ss");

            return respuesta;
        }

        internal static List<ClaseSensor> ConvertirALista(DataTable sensores)
        {
            List<ClaseSensor> _sensores = new List<ClaseSensor>();

            //Ordenar tabla por id
            if (sensores.Rows.Count > 0)
            {
                //  Convert DataTable to DataView
                DataView dv = sensores.DefaultView;
                //   Sort data
                dv.Sort = "id";
                //   Convert back your sorted DataView to DataTable
                sensores = dv.ToTable();
            }

            foreach (DataRow item in sensores.Rows)
            {
                var itemsensor = new ClaseSensor();
                itemsensor.id = Convert.ToInt32(item["id"]);
                itemsensor.ip = item["ip"].ToString();
                itemsensor.puerto = item["puerto"].ToString();
                itemsensor.mac = item["mac"].ToString();
                itemsensor.ubicacion = item["ubicacion"].ToString();
                itemsensor.refresco = item["refresco"].ToString();
                _sensores.Add(itemsensor);
            }

            return _sensores;
        }

        public static string ObtenerGeoLat(string coordenadas)
        {
            var arrayCoordenadas = coordenadas.Split(',');//las cordenadas que me tre de json las divido y tomo posicion invertida.
            return arrayCoordenadas[1];
        }

        public static object ObtenerGeoLong(string coordenadas)
        {
            var arrayCoordenadas = coordenadas.Split(',');
            return arrayCoordenadas[0];
        }

        /// <summary>
        /// Devuelve una lista de la tabla enviada como parametro
        /// </summary>
        /// <param name="sensores"></param>
        /// <returns></returns>
        public static SelectList LlenarDropDownList(DataTable sensores)
        {
            List<SelectListItem> _sensores = new List<SelectListItem>();
            _sensores.Add(new SelectListItem { Text = "Seleccione Sensor", Value = "0" });

            foreach (DataRow item in sensores.Rows)
            {
                _sensores.Add(new SelectListItem { Text = "Sensor" + item["id"], Value = item["id"].ToString() });
            }
            
            return new SelectList(_sensores, "Value", "Text");
        }
    }


}