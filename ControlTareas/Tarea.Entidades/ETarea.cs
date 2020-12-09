using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea.Entidades
{
    public class ETarea
    {
        public int id_tarea { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_termino { get; set; }
        public DateTime fecha_plazo { get; set; }
        public int duracion_dias { get; set; }
        public int id_estado { get; set; }
        public int id_supertarea { get; set; }
        public int id_funcion { get; set; }
    }
}
