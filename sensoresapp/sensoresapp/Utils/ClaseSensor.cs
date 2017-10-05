using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sensoresapp.Utils
{
    public class ClaseSensor
    {
        //01- Creo una clase para devolver a la view y que sea mas facil la lectura
        public int id { get; set; }
        public string ip { get; set; }
        public string puerto { get; set; }
        public string mac { get; set; }
        public string ubicacion { get; set; }
        public string refresco  { get; set; }
    }
}