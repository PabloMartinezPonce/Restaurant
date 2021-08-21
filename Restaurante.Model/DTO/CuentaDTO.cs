using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Restaurante.Model
{
    public class CuentaDTO
    {
        [NotMapped]
        public decimal total { get; set; }
        [NotMapped]
        public string NombreEmpleado { get; set; }
    }
}