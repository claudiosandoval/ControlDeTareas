using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea.Entidades;
using Tareas.Datos;

namespace Tarea.Negocio
{
    public class NFuncion
    {
        public static DataTable Listar()
        {
            DFuncion Datos = new DFuncion();
            return Datos.Listar();
        }
        public static string Insertar(string descripcion, DateTime fecha_termino, int id_depto  )
        {
            DFuncion Datos = new DFuncion();
            string existe = Datos.Existe(descripcion);
            if (existe.Equals("1"))
            {
                return "Ya existe una funcion con esta descripcion";
            }
            else
            {
                Funcion Obj = new Funcion();
                Obj.descripcion = descripcion;
                Obj.fecha_termino = fecha_termino;
                Obj.id_depto = id_depto;
                return Datos.Insertar(Obj);
            }
                

        }
        public static string Actualizar(int id, string DescAnterior,string descripcion, DateTime fecha_termino, int id_depto)
        {
            DFuncion Datos = new DFuncion();
            
            if (!DescAnterior.Equals(descripcion))
            {
                if (Datos.Existe(descripcion).Equals("1"))
                {
                    return "Ya existe una funcion con esta descripcion, porfavor ingrese otra";
                }
            }
                Funcion Obj = new Funcion();
                Obj.id_funcion = id;
                Obj.descripcion = descripcion;
                Obj.fecha_termino = fecha_termino;
                Obj.id_depto = id_depto;
                return Datos.Actualizar(Obj);
            
        }
        public static string Eliminar(int Id)
        {
            DFuncion Datos = new DFuncion();
            return Datos.Eliminar(Id);
        }
        public static DataTable Buscar(int Valor)
        {
            DFuncion Datos = new DFuncion();
            return Datos.Buscar(Valor);
        }
    }
}
