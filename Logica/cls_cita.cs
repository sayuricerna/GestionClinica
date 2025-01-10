using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestiónDeClinica.Datos;

namespace GestiónDeClinica.Logica
{
    public class cls_cita
    {
        private readonly conexion cn = new conexion();
        //Buscar Cita POR ID
        public dto_cita buscarPorId(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "select * from cita where IdCita=" + id;
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        lector.Read();
                        var cita = new dto_cita
                        {
                            IdCita = (int)lector["IdCita"],
                            FechaCita = (DateTime)lector["FechaCita"],
                            IdPaciente = (int)lector["IdPaciente"],
                            IdMedico = (int)lector["IdMedico"],
                            Motivo = lector["Motivo"].ToString(),
                        };
                        return cita;
                    }
                }
            }
        }
        //ACTUALIZAR   U
        public string actualizarCita(dto_cita cita)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "UPDATE Cita SET " +
                    $"FechaCita='{cita.FechaCita.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                    $"IdPaciente={cita.IdPaciente}," +
                    $"IdMedico={cita.IdMedico}," +
                    $"Motivo='{cita.Motivo}'" +
                    $"where IdCita={cita.IdCita}";

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

        //INSERTAR CITA  C
        public string insertarCita(dto_cita cita)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "INSERT INTO Cita (FechaCita, IdPaciente, IdMedico, Motivo) VALUES ('" +
                    cita.FechaCita.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                    cita.IdPaciente + "','" +
                    cita.IdMedico + "','" +
                    cita.Motivo + "')";

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
        /*RETORNA TODAS LAS CITAS  R*/
        public List<dto_cita> listarCitas()
        {
            var listaCitas = new List<dto_cita>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "SELECT IdCita, FechaCita, IdPaciente, IdMedico, Motivo, FechaCreacion FROM Cita";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var cita = new dto_cita
                            {
                                IdCita = (int)lector["IdCita"],
                                FechaCita = (DateTime)lector["FechaCita"],
                                IdPaciente = (int)lector["IdPaciente"],
                                IdMedico = (int)lector["IdMedico"],
                                Motivo = lector["Motivo"].ToString(),
                                FechaCreacion = (DateTime)lector["FechaCreacion"]
                            };
                            listaCitas.Add(cita);
                        }
                    }
                }
            }
            return listaCitas;
        }
        //Buscar Cita R
        public List<dto_cita> buscarCita(string texto)
        {
            var listaCitas = new List<dto_cita>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "SELECT IdCita, FechaCita, IdPaciente, IdMedico, Motivo, FechaCreacion FROM Cita " +
                    $"WHERE Motivo LIKE '%{texto}%'";
                
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var cita = new dto_cita
                            {
                                IdCita = (int)lector["IdCita"],
                                FechaCita = (DateTime)lector["FechaCita"],
                                IdPaciente = (int)lector["IdPaciente"],
                                IdMedico = (int)lector["IdMedico"],
                                Motivo = lector["Motivo"].ToString(),
                                FechaCreacion = (DateTime)lector["FechaCreacion"]
                            };
                            listaCitas.Add(cita);
                        }
                    }
                }
            }
            return listaCitas;
        }
        // Eliminar Cita D
        public bool eliminarCita(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = $"DELETE FROM Cita WHERE IdCita={id}";
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
