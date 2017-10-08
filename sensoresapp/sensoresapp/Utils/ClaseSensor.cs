using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sensoresapp.Utils
{
    public class ClaseSensor
    {
        //01- Creo una clase para devolver a la view y que sea mas facil la lectura
        [Display(Name = "Id")]
        [Required(ErrorMessage = "(*) Id es requerido")]
        public int id { get; set; }

        [Display(Name = "IP")]
        [Required(ErrorMessage = "(*) IP es requerido")]
        public string ip { get; set; }

        [Display(Name = "Puerto")]
        [Required(ErrorMessage = "(*) Puerto es requerido")]
        public string puerto { get; set; }

        [Display(Name = "MAC")]
        [Required(ErrorMessage = "(*) MAC es requerido")]
        public string mac { get; set; }

        [Display(Name = "Ubicación")]
        [Required(ErrorMessage = "(*) Ubicación es requerido")]
        public string ubicacion { get; set; }

        [Display(Name = "Refresco")]
        [Required(ErrorMessage = "(*) Refresco es requerido")]
        public string refresco  { get; set; }
    }
}