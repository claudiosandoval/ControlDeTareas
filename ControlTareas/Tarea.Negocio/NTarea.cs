using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea.Entidades;
using Tareas.Datos;
using System.Data;

namespace Tarea.Negocio
{
    public class NTarea
    {
        public static DataTable Listar()
        {
            DTarea Datos = new DTarea();
            return Datos.ListarTarea();
        }
        public static string Insertar(string descripcion, DateTime fecha_inicio, int duracion_dias, int id_funcion)
        {
            DTarea Datos = new DTarea();
            ETarea Obj = new ETarea();
            Obj.descripcion = descripcion;
            Obj.fecha_inicio = fecha_inicio;
            Obj.duracion_dias = duracion_dias;
            Obj.id_funcion = id_funcion;
            return Datos.Insertar(Obj);           
        }

        public static DataTable ListarEstadoTarea()
        {
            DTarea Datos = new DTarea();
            return Datos.ListarEstadoTarea();
        }
        public static string Eliminar(int Id)
        {
            DTarea Datos = new DTarea();
            return Datos.Eliminar(Id);
        }

        public static DataTable Buscar(string Valor)
        {
            DTarea Datos = new DTarea();
            return Datos.Buscar(Valor);
        }
        public static string Actualizar(int id, string DescAnterior, string descripcion, DateTime fecha_inicio, int duracion_dias,int id_funcion)
        {
            DTarea Datos = new DTarea();

            if (!DescAnterior.Equals(descripcion))
            {
                if (Datos.Existe(descripcion).Equals("1"))
                {
                    return "Ya existe una tarea con esta descripcion, porfavor ingrese otra";
                }
            }
            ETarea Obj = new ETarea();
            Obj.id_tarea = id;
            Obj.descripcion = descripcion;
            Obj.fecha_inicio = fecha_inicio;
            Obj.duracion_dias = duracion_dias;
            Obj.id_funcion = id_funcion;
            return Datos.Actualizar(Obj);

        }
    }
}
