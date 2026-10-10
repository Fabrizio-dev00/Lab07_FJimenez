using System.Data.SqlClient;

namespace CapaDatos
{
    public static class Conexion
    {
        private static readonly string cadenaConexion =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=BDSeccionA;Integrated Security=True";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}