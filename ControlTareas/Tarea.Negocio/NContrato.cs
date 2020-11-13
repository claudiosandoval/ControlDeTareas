using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Datos;
using Tarea.Entidades;

namespace Tarea.Negocio
{
    public class NContrato
    {
        public static DataTable Listar()
        {
            DContratos Datos = new DContratos();
            return Datos.Listar();
        }

        public static DataTable ListarComuna()
        {
            DContratos Datos = new DContratos();
            return Datos.ListarComuna();
        }
        public static DataTable Buscar(string Valor)
        {
            DContratos Datos = new DContratos();
            return Datos.Buscar(Valor);
        }
        public static string Insertar(string rut_empresa, string nombre_empresa, string direccion, int id_comuna)
        {
            DContratos Datos = new DContratos();
            string Existe = Datos.Existe(nombre_empresa);
            if (Existe.Equals("1"))
            {
                return "El contrato ya existe";
            }
            else
            {
                Contrato Obj = new Contrato();
                Obj.rut = rut_empresa;
                Obj.nombre = nombre_empresa;
                Obj.direccion = direccion;
                Obj.id_comuna = id_comuna;
                return Datos.Insertar(Obj);
            }
            
        }      
        public static string Actualizar(int id, string rut_empresa, string NomAnterior,string nombre_empresa, string direccion, int id_comuna)
        {
            DContratos Datos = new DContratos();
            Contrato Obj = new Contrato();
            if (NomAnterior.Equals(nombre_empresa))
            {
                Obj.id_empresa = id;
                Obj.rut = rut_empresa;
                Obj.nombre = nombre_empresa;
                Obj.direccion = direccion;
                Obj.id_comuna = id_comuna;
                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(nombre_empresa);
                if (Existe.Equals("1"))
                {
                    return "El contrato ya existe";
                }
                else
                {
                    Obj.id_empresa = id;
                    Obj.rut = rut_empresa;
                    Obj.nombre = nombre_empresa;
                    Obj.direccion = direccion;
                    Obj.id_comuna = id_comuna;
                    return Datos.Actualizar(Obj);
                }
            }
            
            
        }
        public static string Eliminar(int id)
        {
            DContratos Datos = new DContratos();
            return Datos.Eliminar(id);
        }
    }
}
