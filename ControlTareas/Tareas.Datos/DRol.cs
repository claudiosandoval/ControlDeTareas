using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OracleClient;

namespace Tareas.Datos
{
    public class DRol
    {
        public DataTable Listar()
        {
            OracleDataReader Resultado;
            DataTable Tabla = new DataTable();
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_otros.sp_rol_listar", OraCon);
                Comando.CommandType = System.Data.CommandType.StoredProcedure;
                OraCon.Open();
                Comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
                Resultado = Comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (OraCon.State == ConnectionState.Open) OraCon.Close();
            }
        }
    }
}
