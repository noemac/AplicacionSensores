using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sensoresapp.Models
{
    public class Sensor
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Fecha Desde")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime FechaDesde { get; set; }

        [Display(Name = "Fecha Hasta")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime FechaHasta { get; set; }
    }
}