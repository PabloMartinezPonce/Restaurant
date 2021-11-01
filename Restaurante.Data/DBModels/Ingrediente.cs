using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurante.Data.DBModels
{
    public partial class Ingrediente
    {
        public Ingrediente()
        {
            RelProductoReceta = new HashSet<RelProductoRecetum>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RutaImagen { get; set; }
        public double Stock { get; set; }
        public bool Activo { get; set; }
        public string TipoMedicion { get; set; }
        public string TipoMedicionStock { get; set; }

        public virtual ICollection<RelProductoRecetum> RelProductoReceta { get; set; }
    }
}
