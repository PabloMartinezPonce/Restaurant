using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurante.Model
{
    public class CuentaDTO
    {
        public int id { get; set; }
        public bool cuentaActiva { get; set; }
        public DateTime fechaApertura { get; set; }
        public DateTime fechaCierre { get; set; }
        public string descripcion { get; set; }
        public int idMesa { get; set; }
        public int idEmpleado { get; set; }

        // View
        public string nombreProducto { get; set; }
        public int unidadesProducto { get; set; }

        public virtual UsuarioDTO Usuarios { get; set; }
        public virtual MesaDTO Mesas { get; set; }
    }
}