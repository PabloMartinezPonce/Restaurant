using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Model.Model
{
    public class LoginRequestModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string eMail { get; set; }
        public bool esCorreo { get; set; }
        public bool view { get; set; }
        public bool enable { get; set; }
    }
}
