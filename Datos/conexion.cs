using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiónDeClinica.Datos
{
    internal class conexion
    {
        private readonly string varconexion = "Server=localhost\\SQLEXPRESS;Database=GestionDeClinica;Trusted_Connection=True";
        public SqlConnection obtenerConexion()
        {
            return new SqlConnection(varconexion);
        }
    }
}
