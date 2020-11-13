using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Datos;
using System.Data;
using Tarea.Entidades;
using System.Net.Http;

namespace Tarea.Negocio
{
    public class NUsuario
    {
        public static DataTable Listar()
        {
            DUsuario Datos = new DUsuario();
            return Datos.Listar();
        }
        public static DataTable Login(string Email, string Clave)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Login(Email, Clave);
        }
        public static DataTable Buscar(string Valor)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Buscar(Valor);
        }
        public static string Insertar(string run, string nombres, string ap_paterno, string ap_materno, 
            string direccion, string email, int telefono_movil, int telefono_fijo, int id_comuna, int id_rol, int id_empresa, string password)
        {
            DUsuario Datos = new DUsuario();
            string Existe = Datos.Existe(email);
            if (Existe.Equals("1"))
            {
                return "Ya existe un usuario con este email";
            }
            else
            {
                Usuario Obj = new Usuario();
                Obj.run = run;
                Obj.nombres = nombres;
                Obj.ap_paterno = ap_paterno;
                Obj.ap_materno = ap_materno;
                Obj.direccion = direccion;
                Obj.email = email;
                Obj.telefono_movil = telefono_movil;
                Obj.telefono_fijo = telefono_fijo;
                Obj.id_comuna = id_comuna;
                Obj.id_rol = id_rol;
                Obj.id_empresa = id_empresa;
                Obj.password = password;
                return Datos.Insertar(Obj);
            }

        }
        public static string Actualizar(int id_funcionario, string rutAnterior, string run, string nombres, string ap_paterno, string ap_materno,
            string direccion, string emailAnterior, string email, int telefono_movil, int telefono_fijo, int id_comuna, int id_rol, int id_empresa, string password)
        {
            DUsuario Datos = new DUsuario();
            Usuario Obj = new Usuario();

            if (!emailAnterior.Equals(email))
            {
                if (Datos.Existe(email).Equals("1"))
                {
                    return "Ya existe un usuario con este email, porfavor ingrese otro.";
                }
            }

            if (!rutAnterior.Equals(run))
            {
                if (Datos.ExisteRut(run).Equals("1"))
                {
                    return "Ya existe el RUN ingresado, por favor ingrese otro.";
                }
            }

            Obj.id_funcionario = id_funcionario;
            Obj.run = run;
            Obj.nombres = nombres;
            Obj.ap_paterno = ap_paterno;
            Obj.ap_materno = ap_materno;
            Obj.direccion = direccion;
            Obj.email = email;
            Obj.telefono_movil = telefono_movil;
            Obj.telefono_fijo = telefono_fijo;
            Obj.id_comuna = id_comuna;
            Obj.id_rol = id_rol;
            Obj.id_empresa = id_empresa;
            Obj.password = password;
            return Datos.Actualizar(Obj);
        }
        public static string Eliminar(int id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Eliminar(id);
        }
        /*public static string Activar(int id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Activar(id);
        }
        public static string Desactivar(int id)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Desactivar(id);
        }*/
    }
}
