using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Persona
    {
        Acceso ACC = new Acceso();

        public int Alta(BE.Persona persona)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@NOMBRE", persona.Nombre);
            parametros[1] = new SqlParameter("@APELLIDO", persona.Apellido);
            parametros[2] = new SqlParameter("@FECHANAC", persona.FechaNac);
            return ACC.Escribir("SP_Alta", parametros);
        }

        public int Baja(BE.Persona persona)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@ID", persona.ID);
            return ACC.Escribir("SP_Baja", parametros);
        }

        public int Modificar(BE.Persona persona)
        {
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("@ID", persona.ID);
            parametros[1] = new SqlParameter("@NOMBRE", persona.Nombre);
            parametros[2] = new SqlParameter("@APELLIDO", persona.Apellido);
            parametros[3] = new SqlParameter("@FECHANAC", persona.FechaNac);
            return ACC.Escribir("SP_Modificar", parametros);
        }

        public List<BE.Persona> ListarTodos()
        {
            DataTable tabla = ACC.Leer("SP_ListarTodos", null);
            List<BE.Persona> ls = new List<BE.Persona>();

            foreach(DataRow row in tabla.Rows)
            {
                BE.Persona persona = new BE.Persona();
                persona.ID = (int)row["ID"];
                persona.Nombre = (string)row["NOMBRE"];
                persona.Apellido = (string)row["APELLIDO"];
                persona.FechaNac = (DateTime)row["FECHA_NACIMIENTO"];
                ls.Add(persona);
            }
            return ls;
        }



       // public BE.Persona ListarPersona(BE.Persona persona)
        //{
          //  SqlParameter[] parametro = new SqlParameter[1];
            //parametro[0] = new SqlParameter("@APELLIDO", persona.Apellido);
            
           // DataTable tabla = ACC.Leer("SP_ListarPersona", parametro);
           // BE.Persona personaBD = new BE.Persona();

           // foreach (DataRow row in tabla.Rows)
           // {
            //    personaBD.ID = (int)row["ID"];
            //    personaBD.Nombre = (string)row["NOMBRE"];
            //    personaBD.Apellido = (string)row["APELLIDO"];
            //    personaBD.FechaNac = (DateTime)row["FECHA_NACIMIENTO"];
           // }

        //    return personaBD;
       // }


    }
}
