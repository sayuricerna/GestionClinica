using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestiónDeClinica.Datos;
using GestiónDeClinica.Presentacion;

namespace GestiónDeClinica.Logica
{
    public class cls_paciente
    {
        private readonly conexion cn = new conexion();
        public List<dto_paciente> leer()
        {
            var listapaciente = new List<dto_paciente>();
            using (var varconexion = cn.obtenerConexion())
            {
                string cadena = "select * from paciente";
                using (var comando = new SqlCommand(cadena, varconexion))
                {
                    varconexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listapaciente.Add(new dto_paciente
                            {
                                IdPaciente = Convert.ToInt32(lector["IdPaciente"]),
                                Cedula = lector["Cedula"].ToString()
                            });
                        }
                    }
                }
            }
            return listapaciente;
        }

        public dto_paciente buscarPorId(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "select * from paciente where IdPaciente=" + id;
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        lector.Read();
                        var paciente = new dto_paciente
                        {
                            IdPaciente = (int)lector["IdPaciente"],
                            Nombre = lector["Nombre"].ToString(),
                            Email = lector["Email"].ToString(),
                            Direccion = lector["Direccion"].ToString(),
                            Telefono = lector["Telefono"].ToString(),
                            Genero = lector["Genero"].ToString(),
                            Cedula = lector["Cedula"].ToString(),
                            FechaNac = (DateTime)lector["FechaNac"]
                        };
                        return paciente;
                    }
                }
            }
        }
        //ACTUALIZAR   U
        public string actualizarPaciente(dto_paciente paciente)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "UPDATE Paciente SET " +
                    $"Nombre='{paciente.Nombre}'," +
                    $"Email='{paciente.Email}'," +
                    $"Direccion='{paciente.Direccion}'," +
                    $"Telefono='{paciente.Telefono}'," +
                    $"Genero='{paciente.Genero}'," +
                    $"Cedula='{paciente.Cedula}'," +
                    $"FechaNac='{paciente.FechaNac:yyyy-MM-dd}' " +
                    $"WHERE IdPaciente={paciente.IdPaciente}";


                using (var comando = new SqlCommand(cadena, conexion))
                {
                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        return "ok";
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
        }
        public string insertarPaciente(dto_paciente paciente)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "INSERT INTO Paciente (Nombre, Email, Direccion, Telefono, Genero, Cedula, FechaNac) VALUES ('" +
                    paciente.Nombre + "','" +
                    paciente.Email + "','" +
                    paciente.Direccion + "','" +
                    paciente.Telefono + "','" +
                    paciente.Genero + "','" +
                    paciente.Cedula + "','" +
                    paciente.FechaNac.ToString("yyyy-MM-dd") + "')";

                using (var comando = new SqlCommand(cadena, conexion))
                {
                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        return "ok";
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
        }

        public List<dto_paciente> listarPacientes()
        {
            var listaPacientes = new List<dto_paciente>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "SELECT * FROM Paciente";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var paciente = new dto_paciente
                            {
                                IdPaciente = (int)lector["IdPaciente"],
                                Nombre = lector["Nombre"].ToString(),
                                Email = lector["Email"].ToString(),
                                Direccion = lector["Direccion"].ToString(),
                                Telefono = lector["Telefono"].ToString(),
                                Genero = lector["Genero"].ToString(),
                                Cedula = lector["Cedula"].ToString(),
                                FechaNac = (DateTime)lector["FechaNac"]
                            };
                            listaPacientes.Add(paciente);
                        }
                    }
                }
            }
            return listaPacientes;
        }

        public List<dto_paciente> buscarPaciente(string texto)
        {
            var listaPacientes = new List<dto_paciente>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "SELECT IdPaciente, Nombre, Telefono, Direccion, Especialidad, Email FROM Paciente " +
                    $"WHERE Nombre LIKE '%{texto}%'";

                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var paciente = new dto_paciente
                            {
                                IdPaciente = (int)lector["IdPaciente"],
                                Nombre = lector["Nombre"].ToString(),
                                Email = lector["Email"].ToString(),
                                Direccion = lector["Direccion"].ToString(),
                                Telefono = lector["Telefono"].ToString(),
                                Genero = lector["Genero"].ToString(),
                                Cedula = lector["Cedula"].ToString(),
                                FechaNac = (DateTime)lector["FechaNac"]
                            };
                            listaPacientes.Add(paciente);
                        }
                    }
                }
            }
            return listaPacientes;
        }
        public bool eliminarPaciente(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = $"DELETE FROM Paciente WHERE IdPaciente={id}";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }




    }
}
