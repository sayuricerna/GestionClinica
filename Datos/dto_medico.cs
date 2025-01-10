using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiónDeClinica.Datos
{
    public class dto_medico
    {
        public int IdMedico { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Especialidad { get; set; }
        public string Email { get; set; }
    }
}
