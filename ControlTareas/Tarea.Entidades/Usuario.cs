using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea.Entidades
{
    public class Usuario
    {
        public int id_funcionario{ get; set; }
        public string run { get; set; }
        public string nombres { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public int telefono_movil { get; set; }
        public int telefono_fijo { get; set; }
        public int id_comuna { get; set; }
        public int id_rol{ get; set; }
        public int id_empresa { get; set; }
        public int user_id { get; set; }
        public string password { get; set; }
    }
}
