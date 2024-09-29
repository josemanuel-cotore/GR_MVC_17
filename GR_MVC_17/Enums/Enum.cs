using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GR_MVC_17.Enums
{
    public class Enum
    {
        public enum Inconveniente
        {
            Alta_Temperatura = 1,
            Baja_Temperatura = 2,
            Lluvia = 3,
            Nuieve = 4,
            Ninguno = 5

        }

        public enum Rol
        {
            Administrador = 1,
            Usuario = 2
        }

        public enum Perfil
        {
            Running = 1,
            Senderismo = 2,
            Ciclismo = 3,
            Duatlón = 4,
            Carreras = 5
        }
    }
}