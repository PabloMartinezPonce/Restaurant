using System;
using System.Collections.Generic;

namespace Restaurante.Model
{
    public class ProveedorDTO
    {
        public int id { get; set; }
        public string nombreContacto { get; set; }
        public string compania { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}