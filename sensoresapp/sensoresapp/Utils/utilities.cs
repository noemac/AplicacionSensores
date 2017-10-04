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