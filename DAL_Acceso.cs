using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Acceso
    {
        SqlConnection conexion = new SqlConnection(@"Data Source=DANISAJOKAN7D0F\SQLEXPRESS; Initial Catalog=PARCIALCARDACCI; Integrated Security=SSPI;");
        SqlTransaction Tx;

        public DataTable Leer(string consulta, SqlParameter[] parametros)
        {
            conexion.Open();
            SqlDataAdapter Adaptador = new SqlDataAdapter();
            DataTable tabla = new DataTable();
            Adaptador.SelectCommand = new SqlCommand(consulta, conexion);
            if (parametros != null)
                Adaptador.SelectCommand.Parameters.AddRange(parametros);
            Adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
            Adaptador.Fill(tabla);
            Adaptador = null;
            conexion.Close();
            return tabla;
        }

        public int Escribir(string consulta, SqlParameter[] parametros)
        {
            conexion.Open();
            Tx = conexion.BeginTransaction();
            SqlCommand cmd = new SqlCommand(consulta, conexion);
            int filas = 0;
            try
            {
                if (parametros != null)
                    cmd.Parameters.AddRange(parametros);
                cmd.Transaction = Tx;
                cmd.CommandType = CommandType.StoredProcedure;
                filas = cmd.ExecuteNonQuery();
                cmd = null;
                Tx.Commit();
            }
            catch (SqlException ex)
            {
                Tx.Rollback();
            }
            conexion.Close();
            return filas;
        }
    }
}
