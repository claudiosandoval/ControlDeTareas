using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OracleClient;
using Tarea.Entidades;

namespace Tareas.Datos
{
    public class DTarea
    {
        public DataTable ListarTarea()
        {
            OracleDataReader Resultado;
            DataTable Tabla = new DataTable();
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_tarea.sp_tarea_listar", OraCon);
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
        public string Insertar(ETarea Obj)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_tarea.sp_tarea_insertar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_descripcion", OracleType.VarChar).Value = Obj.descripcion;
                Comando.Parameters.Add("p_fecha_inicio", OracleType.DateTime).Value = Obj.fecha_inicio;
                Comando.Parameters.Add("p_duracion_dias", OracleType.Int32).Value = Obj.duracion_dias;
                Comando.Parameters.Add("p_id_funcion", OracleType.Int32).Value = Obj.id_funcion;
                OraCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo ingresar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (OraCon.State == ConnectionState.Open) OraCon.Close();
            }

            return Rpta;
        }
        public DataTable ListarEstadoTarea()
        {
            OracleDataReader Resultado;
            DataTable Tabla = new DataTable();
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_otros.sp_estado_tarea_listar", OraCon);
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
        public string Eliminar(int Id)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_tarea.sp_tarea_eliminar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_id", OracleType.VarChar).Value = Id;
                OraCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (OraCon.State == ConnectionState.Open) OraCon.Close();
            }
            return Rpta;
        }
        public DataTable Buscar(string Valor)
        {
            OracleDataReader Resultado;
            DataTable Tabla = new DataTable();
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_tarea.sp_tarea_buscar", OraCon);
                Comando.CommandType = System.Data.CommandType.StoredProcedure;
                OraCon.Open();
                Comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
                Comando.Parameters.Add("valor", OracleType.VarChar).Value = Valor;
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
        public string Actualizar(ETarea Obj)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_tarea.sp_tarea_actualizar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_id", OracleType.Int32).Value = Obj.id_tarea;
                Comando.Parameters.Add("p_descripcion", OracleType.VarChar).Value = Obj.descripcion;
                Comando.Parameters.Add("p_fecha_inicio", OracleType.DateTime).Value = Obj.fecha_inicio;
                Comando.Parameters.Add("p_duracion_dias", OracleType.Int32).Value = Obj.duracion_dias;
                Comando.Parameters.Add("p_id_funcion", OracleType.Int32).Value = Obj.id_funcion;
                OraCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (OraCon.State == ConnectionState.Open) OraCon.Close();
            }

            return Rpta;
        }
        public string Existe(string desc)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_tarea.sp_tarea_existe", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_descripcion", OracleType.VarChar).Value = desc;
                OracleParameter ParExiste = new OracleParameter();
                ParExiste.ParameterName = "existe";
                ParExiste.OracleType = OracleType.Int32;
                ParExiste.Direction = ParameterDirection.Output;
                Comando.Parameters.Add(ParExiste);
                OraCon.Open();
                Comando.ExecuteNonQuery();
                Rpta = Convert.ToString(ParExiste.Value);
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (OraCon.State == ConnectionState.Open) OraCon.Close();
            }

            return Rpta;
        }
    }
}
