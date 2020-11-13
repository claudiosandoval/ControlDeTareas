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
    public class DUsuario
    {
        public DataTable Listar()
        {
            OracleDataReader Resultado;
            DataTable Tabla = new DataTable();
            OracleConnection OraCon = new OracleConnection();   
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_funcionario.sp_funcionario_listar", OraCon);
                Comando.CommandType = System.Data.CommandType.StoredProcedure;
                Comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
                OraCon.Open();
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
        public DataTable Login(string Email,string Clave)
        {
            OracleDataReader Resultado;
            DataTable Tabla = new DataTable();
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_usuario.sp_usuario_login", OraCon);
                Comando.CommandType = System.Data.CommandType.StoredProcedure;
                Comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
                Comando.Parameters.Add("p_email", OracleType.VarChar).Value = Email;
                Comando.Parameters.Add("p_password", OracleType.VarChar).Value = Clave;
                OraCon.Open();
                Resultado = Comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                return null;
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
                OracleCommand Comando = new OracleCommand("pkg_funcionario.sp_funcionario_buscar", OraCon);
                Comando.CommandType = System.Data.CommandType.StoredProcedure;
                OraCon.Open();
                Comando.Parameters.Add("registros", OracleType.Cursor).Direction = ParameterDirection.Output;
                Comando.Parameters.Add("p_valor", OracleType.VarChar).Value = Valor;
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
        public string Existe(string Email)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_funcionario.sp_funcionario_existe", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_email", OracleType.VarChar).Value = Email;
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
        public string ExisteRut(string rut)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_funcionario.sp_funcionario_existe_rut", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_rut", OracleType.VarChar).Value = rut;
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
        public string Insertar(Usuario Obj)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_funcionario.sp_funcionario_insertar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_run", OracleType.VarChar).Value = Obj.run;
                Comando.Parameters.Add("p_nombres", OracleType.VarChar).Value = Obj.nombres;
                Comando.Parameters.Add("p_ap_paterno", OracleType.VarChar).Value = Obj.ap_paterno;
                Comando.Parameters.Add("p_ap_materno", OracleType.VarChar).Value = Obj.ap_materno;
                Comando.Parameters.Add("p_direccion", OracleType.VarChar).Value = Obj.direccion;
                Comando.Parameters.Add("p_email", OracleType.VarChar).Value = Obj.email;
                Comando.Parameters.Add("p_fono_movil", OracleType.Int32).Value = Obj.telefono_movil;
                Comando.Parameters.Add("p_fono_fijo", OracleType.Int32).Value = Obj.telefono_fijo;
                Comando.Parameters.Add("p_comuna", OracleType.Int32).Value = Obj.id_comuna;
                Comando.Parameters.Add("p_rol", OracleType.Int32).Value = Obj.id_rol;
                Comando.Parameters.Add("p_empresa", OracleType.Int32).Value = Obj.id_empresa;
                Comando.Parameters.Add("p_password", OracleType.VarChar).Value = Obj.password;
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
        public string Actualizar(Usuario Obj)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("pkg_funcionario.sp_funcionario_actualizar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_id", OracleType.Int32).Value = Obj.id_funcionario;
                Comando.Parameters.Add("p_run", OracleType.VarChar).Value = Obj.run;
                Comando.Parameters.Add("p_nombres", OracleType.VarChar).Value = Obj.nombres;
                Comando.Parameters.Add("p_ap_paterno", OracleType.VarChar).Value = Obj.ap_paterno;
                Comando.Parameters.Add("p_ap_materno", OracleType.VarChar).Value = Obj.ap_materno;
                Comando.Parameters.Add("p_direccion", OracleType.VarChar).Value = Obj.direccion;
                Comando.Parameters.Add("p_email", OracleType.VarChar).Value = Obj.email;
                Comando.Parameters.Add("p_fono_movil", OracleType.Int32).Value = Obj.telefono_movil;
                Comando.Parameters.Add("p_fono_fijo", OracleType.Int32).Value = Obj.telefono_fijo;
                Comando.Parameters.Add("p_comuna", OracleType.Int32).Value = Obj.id_comuna;
                Comando.Parameters.Add("p_rol", OracleType.Int32).Value = Obj.id_rol;
                Comando.Parameters.Add("p_empresa", OracleType.Int32).Value = Obj.id_empresa;
                Comando.Parameters.Add("p_password", OracleType.VarChar).Value = Obj.password;
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
                OracleCommand Comando = new OracleCommand("pkg_funcionario.sp_funcionario_eliminar", OraCon);
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

        /*public string Activar(int Id)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("sp_usuario_activar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("numDocumento", OracleType.VarChar).Value = Id;
                OraCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo activar el registro";
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

        }*/
        /*public string Desactivar(int Id)
        {
            string Rpta = "";
            OracleConnection OraCon = new OracleConnection();
            try
            {
                OraCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("sp_usuario_desactivar", OraCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("numDocumento", OracleType.VarChar).Value = Id;
                OraCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo desactivar el registro";
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
        }*/

    }
}
