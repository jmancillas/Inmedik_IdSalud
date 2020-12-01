using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{    
    public class AlmStatusAux
    {
        public readonly static int Enviado = 1;
        public readonly static int Recibido = 2;
        public readonly static int Rechazado = 3;
        public readonly static int Confirmado = 4;
        public readonly static int EnEspera = 5;
        public readonly static int Entregado = 6;

        public int id { get; set; }
        public string name { get; set; }
    }


    public class AlmStatusHelper
    {

    }
}