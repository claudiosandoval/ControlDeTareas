using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea.Entidades
{
    public class Funcion
    {
        public int id_funcion { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_termino { get; set; }
        public bool estado_finalizacion { get; set; }
        public int id_depto { get; set; }              
    }
}
