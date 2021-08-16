using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Web
{
    public partial class Mesa
    {
        public Mesa()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int Id { get; set; }
        public string Mesa1 { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
