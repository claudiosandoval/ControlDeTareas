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
    public class DFuncion
    {
        public DataTable Listar()
        {
            OracleDataReader Resultado;
            DataTable Tabla = new DataTable();
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_funcion.sp_funcion_listar", OraCon);
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
        public string Insertar(Funcion Obj)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_funcion.sp_funcion_insertar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_descripcion", OracleType.VarChar).Value = Obj.descripcion;
                Comando.Parameters.Add("p_fecha_termino", OracleType.DateTime).Value = Obj.fecha_termino;
                Comando.Parameters.Add("p_id_depto", OracleType.Int32).Value = Obj.id_depto;
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
        public string Existe(string desc)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_funcion.sp_funcion_existe", OraCon);
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
        public string Eliminar(int Id)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_funcion.sp_funcion_eliminar", OraCon);
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
        public string Actualizar(Funcion Obj)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_funcion.sp_funcion_actualizar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_id", OracleType.Int32).Value = Obj.id_funcion;
                Comando.Parameters.Add("p_descripcion", OracleType.VarChar).Value = Obj.descripcion;
                Comando.Parameters.Add("p_fecha_termino", OracleType.DateTime).Value = Obj.fecha_termino;
                Comando.Parameters.Add("p_id_depto", OracleType.Int32).Value = Obj.id_depto;
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
        public DataTable Buscar(int Valor)
        {
            OracleDataReader Resultado;
            DataTable Tabla = new DataTable();
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_funcion.sp_funcion_buscar", OraCon);
                Comando.CommandType = System.Data.CommandType.StoredProcedure;
                OraCon.Open();
                Comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
                Comando.Parameters.Add("valor", OracleType.Int32).Value = Valor;
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
