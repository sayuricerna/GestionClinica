using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestiónDeClinica.Datos;
using System.Data;
using GestiónDeClinica.Presentacion;
namespace GestiónDeClinica.Logica
{
    public class cls_medico
    {
        private readonly conexion cn = new conexion();
        
        public List<dto_medico> leer()
        {
            var listamedico = new List<dto_medico>();
            using (var varconexion = cn.obtenerConexion())
            {
                string cadena = "select * from medico";
                using (var comando = new SqlCommand(cadena, varconexion))
                {
                    varconexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listamedico.Add(new dto_medico
                            {
                                IdMedico = Convert.ToInt32(lector["IdMedico"]),
                                Nombre = lector["Nombre"].ToString()
                            });
                        }
                    }
                }
            }
            return listamedico;
        }

        public dto_medico buscarPorId(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "select * from medico where IdMedico=" + id;
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        lector.Read();
                        var medico = new dto_medico
                        {
                            IdMedico = (int)lector["IdMedico"],
                            Nombre = lector["Nombre"].ToString(),
                            Telefono = lector["Telefono"].ToString(),
                            Direccion = lector["Direccion"].ToString(),
                            Especialidad = lector["Especialidad"].ToString(),
                            Email = lector["Email"].ToString(),
                        };
                        return medico;
                    }
                }
            }
        }
        //ACTUALIZAR   U
        public string actualizarMedico(dto_medico medico)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "UPDATE Medico SET " +
                    $"Nombre='{medico.Nombre}'," +
                    $"Telefono='{medico.Telefono}'," +
                    $"Direccion='{medico.Direccion}'," +
                    $"Especialidad='{medico.Especialidad}'," +
                    $"Email='{medico.Email}' " +
                    $"WHERE IdMedico={medico.IdMedico}";


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
        public string insertarMedico(dto_medico medico)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "INSERT INTO Medico (Nombre, Telefono, Direccion, Especialidad, Email) VALUES ('" +
                    medico.Nombre + "','" +
                    medico.Telefono + "','" +
                    medico.Direccion + "','" +
                    medico.Especialidad + "','" +
                    medico.Email + "')";

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

        public List<dto_medico> listarMedicos()
        {
            var listaMedicos = new List<dto_medico>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "SELECT IdMedico, Nombre, Telefono, Direccion, Especialidad, Email FROM Medico";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var medico = new dto_medico
                            {
                                IdMedico = (int)lector["IdMedico"],
                                Nombre = lector["Nombre"].ToString(),
                                Telefono = lector["Telefono"].ToString(),
                                Direccion = lector["Direccion"].ToString(),
                                Especialidad = lector["Especialidad"].ToString(),
                                Email = lector["Email"].ToString(),
                            };
                            listaMedicos.Add(medico);
                        }
                    }
                }
            }
            return listaMedicos;
        }

        public List<dto_medico> buscarMedico(string texto)
        {
            var listaMedicos = new List<dto_medico>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "SELECT IdMedico, Nombre, Telefono, Direccion, Especialidad, Email FROM Medico " +
                    $"WHERE Nombre LIKE '%{texto}%'";

                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var medico = new dto_medico
                            {
                                IdMedico = (int)lector["IdMedico"],
                                Nombre = lector["Nombre"].ToString(),
                                Telefono = lector["Telefono"].ToString(),
                                Direccion = lector["Direccion"].ToString(),
                                Especialidad = lector["Especialidad"].ToString(),
                                Email = lector["Email"].ToString(),
                            };
                            listaMedicos.Add(medico);
                        }
                    }
                }
            }
            return listaMedicos;
        }
        public bool eliminarMedico(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = $"DELETE FROM Medico WHERE IdMedico={id}";
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
