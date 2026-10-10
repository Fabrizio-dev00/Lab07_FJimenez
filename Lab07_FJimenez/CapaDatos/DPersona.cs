using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class DPersona
    {
        private List<Persona> LeerPersonas(SqlDataReader lector)
        {
            List<Persona> personas = new List<Persona>();

            while (lector.Read())
            {
                Persona persona = new Persona();
                persona.IdPersona = Convert.ToInt32(lector["IdPersona"]);
                persona.Nombres = lector["Nombres"].ToString();
                persona.Apellidos = lector["Apellidos"].ToString();
                persona.Dni = lector["Dni"].ToString();
                personas.Add(persona);
            }

            return personas;
        }

        public List<Persona> Listar()
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_ListarPersonas", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    return LeerPersonas(lector);
                }
            }
        }

        public List<Persona> Buscar(string texto)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_BuscarPersonas", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Texto", texto);

                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    return LeerPersonas(lector);
                }
            }
        }

        public int Insertar(Persona persona)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_InsertarPersona", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Nombres", persona.Nombres);
                comando.Parameters.AddWithValue("@Apellidos", persona.Apellidos);
                comando.Parameters.AddWithValue("@Dni", persona.Dni);

                conexion.Open();
                return comando.ExecuteNonQuery();
            }
        }

        public int Actualizar(Persona persona)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_ActualizarPersona", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdPersona", persona.IdPersona);
                comando.Parameters.AddWithValue("@Nombres", persona.Nombres);
                comando.Parameters.AddWithValue("@Apellidos", persona.Apellidos);
                comando.Parameters.AddWithValue("@Dni", persona.Dni);

                conexion.Open();
                return comando.ExecuteNonQuery();
            }
        }

        public int Eliminar(int idPersona)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_EliminarPersona", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@IdPersona", idPersona);

                conexion.Open();
                return comando.ExecuteNonQuery();
            }
        }
    }
}
