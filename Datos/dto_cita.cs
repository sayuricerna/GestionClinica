using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiónDeClinica.Datos
{
    public class dto_cita
    {
        public int IdCita { get; set; }
        public DateTime FechaCita { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public string Motivo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
