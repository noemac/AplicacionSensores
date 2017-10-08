using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sensoresapp.Utils
{
    public class ClaseSensorRegistro
    {
        //01- Creo una clase para devolver a la view y que sea mas facil la lectura
            public int id { get; set; }
            public string fechalectura { get; set; }
            public int temperatura { get; set; }
            public int humedad { get; set; }
            public double amoniaco { get; set; }
            public int id_sensor { get; set; }
    }
}