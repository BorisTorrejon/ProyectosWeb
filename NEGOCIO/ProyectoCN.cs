using ENTIDAD;
using DATOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class ProyectoCN
    {
        private static ProyectoDALC obj = new ProyectoDALC();

        public static void Agregar(Proyecto proyecto)
        {
            obj.Agregar(proyecto);
        }
        public static List<Proyecto> ListarProyectos()
        {
            return obj.ListarProyetos();
        }
        public static List<Proyecto> FListarProyectos()
        {
            return obj.FListarProyetos();
        }
        public static Proyecto GetProyecto(int id)
        {
            return obj.GetProyecto(id);
        }
        public static void Editar(Proyecto proyecto)
        {
            obj.Editar(proyecto);
        }
        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }

        public static bool ExisteRelacion(int proyectoID, int empleadoID)
        {
            return obj.ExisteRelacion(proyectoID, empleadoID);
        }
        public static void AsignarProyecto(int proyectoID, int empleadoID)
        {
            obj.AsignarProyecto(proyectoID, empleadoID);
        }
        public static List<ProyectoEmpleadoCE> listarProyectoEmpleado()
        {
            return obj.listarProyectoEmpleado();
        }
        public static void EliminarProyectoEmpleado(int proyectoID, int empleadoID)
        {
            obj.EliminarProyectoEmpleado(proyectoID, empleadoID);
        }
    }
}
