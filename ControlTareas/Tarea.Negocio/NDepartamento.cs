using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Tareas.Datos;
using Tarea.Entidades;

namespace Tarea.Negocio
{
    public class NDepartamento
    {
        public static DataTable Listar()
        {
            DDepartamento Datos = new DDepartamento();
            return Datos.Listar();
        }
        public static DataTable CargarRolDepto()
        {
            DDepartamento Datos = new DDepartamento();
            return Datos.CargarRolDepto();
        }
        public static DataTable Buscar(string Valor)
        {
            DDepartamento Datos = new DDepartamento();
            return Datos.Buscar(Valor);
        }
        public static string Insertar(string nombre_depto, int id_rol, int id_contrato)
        {
            DDepartamento Datos = new DDepartamento();
            Departamento Obj = new Departamento();
            string Existe = Datos.Existe(nombre_depto);
            if (Existe.Equals("1"))
            {
                return "El departamento ya existe";
            }
            else
            {
                Obj.nombre_depto = nombre_depto;
                Obj.id_rol = id_rol;
                Obj.id_contrato = id_contrato;
                return Datos.Insertar(Obj);
            }

        }
        public static string Actualizar(int id, string NomAnterior, string nombre_depto, int rol_depto, int id_contrato)
        {
            DDepartamento Datos = new DDepartamento();
            Departamento Obj = new Departamento();
            if (NomAnterior.Equals(nombre_depto))
            {
                Obj.id_depto = id;
                Obj.nombre_depto = nombre_depto;
                Obj.id_rol = rol_depto;
                Obj.id_contrato = id_contrato;
                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(nombre_depto);
                if (Existe.Equals("1"))
                {
                    return "El departamento ya existe";
                }
                else
                {
                    Obj.id_depto = id;
                    Obj.nombre_depto = nombre_depto;
                    Obj.id_rol = rol_depto;
                    Obj.id_contrato = id_contrato;
                    return Datos.Actualizar(Obj);
                }
            }


        }
        public static string Eliminar(int id)
        {
            DDepartamento Datos = new DDepartamento();
            return Datos.Eliminar(id);
        }
    }
}
