using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiónDeClinica.Datos
{
    public class dto_paciente
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Genero { get; set; }
        public string Cedula { get; set; }
        public DateTime FechaNac { get; set; }


    }
}
