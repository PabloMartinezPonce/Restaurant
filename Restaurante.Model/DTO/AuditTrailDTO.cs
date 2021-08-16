using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurante.Model
{
    public class AuditTrailDTO
    {
        public int id { get; set; }
        public string tipoMovimiento { get; set; }
        public string modulo { get; set; }
        public string registroAfectado { get; set; }
        public string valorAnterior { get; set; }
        public string valorActual { get; set; }
        public string responsable { get; set; }
        public DateTime fecha { get; set; }
    }
}