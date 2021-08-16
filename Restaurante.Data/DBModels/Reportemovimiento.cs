using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Web
{
    public partial class Reportemovimiento
    {
        public int Id { get; set; }
        public string TipoMovimiento { get; set; }
        public string Modulo { get; set; }
        public string RegistroAfectado { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorActual { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
