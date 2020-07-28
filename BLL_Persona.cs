using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Persona
    {
        DAL.Persona mapperPersona = new DAL.Persona();

        public List<BE.Persona> ListarTodos() { return mapperPersona.ListarTodos(); }

        public bool Alta(BE.Persona persona)
        {
            int Resultado = mapperPersona.Alta(persona);

            if(Resultado == 0 || Resultado == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Baja(BE.Persona persona)
        {
            int Resultado = mapperPersona.Baja(persona);

            if (Resultado == 0 || Resultado == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public bool Modificar(BE.Persona persona)
        {
            int Resultado = mapperPersona.Modificar(persona);

            return Resultado != 0 && Resultado != -1;
        }

       // public BE.Persona ListarPersona(BE.Persona persona)
       // {
       //     return mapperPersona.ListarPersona(persona);
       // }
    }
}
