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
    public class DDepartamento
    {
        public DataTable Listar()
        {
            OracleDataReader Resultado;
            DataTable Tabla = new DataTable();
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_departamento.sp_departamento_listar", OraCon);
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
        public DataTable CargarRolDepto()
        {
            OracleDataReader Resultado;
            DataTable Tabla = new DataTable();
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_otros.sp_rol_depto_listar", OraCon);
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
        public DataTable Buscar(string Valor)
        {
            OracleDataReader Resultado;
            DataTable Tabla = new DataTable();
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_departamento.sp_departamento_buscar", OraCon);
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

        public string Existe(string Nombre)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_departamento.sp_departamento_existe", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_nombre_depto", OracleType.VarChar).Value = Nombre;
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
        public string Insertar(Departamento Obj)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_departamento.sp_departamento_insertar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_nombre_depto", OracleType.VarChar).Value = Obj.nombre_depto;
                Comando.Parameters.Add("p_rol_depto", OracleType.Int32).Value = Obj.id_rol;
                Comando.Parameters.Add("p_id_contrato", OracleType.Int32).Value = Obj.id_contrato;
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
        public string Actualizar(Departamento Obj)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_departamento.sp_departamento_actualizar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_id", OracleType.Int32).Value = Obj.id_depto;
                Comando.Parameters.Add("p_nombre_depto", OracleType.VarChar).Value = Obj.nombre_depto;
                Comando.Parameters.Add("p_rol_depto", OracleType.Int32).Value = Obj.id_rol;
                Comando.Parameters.Add("p_id_contrato", OracleType.Int32).Value = Obj.id_contrato;
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
        public string Eliminar(int Id)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_departamento.sp_departamento_eliminar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_id", OracleType.Int32).Value = Id;
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
    }
}
