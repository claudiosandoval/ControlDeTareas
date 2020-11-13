using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace Tareas.Datos
{
    public class Conexion
    {
        private string Base;
        private string Servidor;
        private string Usuario;
        private string Clave;
        private bool Seguridad;
        private static Conexion Con = null;

        private Conexion() //constructor
        {
            this.Base = "xe";
            this.Servidor = "DESKTOP-S2RP912\\SQLEXPRESS";
            this.Usuario = "controltareasv2";
            this.Clave = "123";
            this.Seguridad = true;
        }
        public OracleConnection CrearConexion()
        {
            OracleConnection Cadena = new OracleConnection();
            try
            {
                Cadena.ConnectionString = "DATA SOURCE =" + this.Base + "; PASSWORD =" + this.Clave + "; USER ID =" + this.Usuario + ";";
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
            }
            return Cadena;
        }

        public static Conexion getInstancia()
        {
            if (Con == null)
            {
                Con = new Conexion();
            }
            return Con;
        }
    }
}
