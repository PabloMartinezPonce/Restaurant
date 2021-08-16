using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Web
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Venta = new HashSet<Venta>();
        }

        public int Id { get; set; }
        public bool? CuentaActiva { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
        public string Descripcion { get; set; }
        public int IdMesa { get; set; }
        public int IdEmpleado { get; set; }

        // View
        public string NombreProducto { get; set; }
        public int UnidadesProducto { get; set; }

        public virtual Usuario IdEmpleadoNavigation { get; set; }
        public virtual Mesa IdMesaNavigation { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
