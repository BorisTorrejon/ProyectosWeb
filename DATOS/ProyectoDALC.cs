using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class ProyectoDALC
    {
        public void Agregar(Proyecto proyecto)
        {
            using (var db = new ProyectosContext())
            {
                db.Proyecto.Add(proyecto);
                db.SaveChanges();
            }
        }
        public List<Proyecto> ListarProyetos()
        {
            using (var db = new ProyectosContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Proyecto.ToList();
            }
        }

        public List<Proyecto> FListarProyetos()
        {
            using (var db = new ProyectosContext())
            {
                var hoy=DateTime.Now.Date;
                db.Configuration.LazyLoadingEnabled = false;
                return db.Proyecto.Where(p => p.FechaFin > hoy).ToList();
            }
        }

        public Proyecto GetProyecto(int id)
        {
            using (var db = new ProyectosContext())
            {
                //return db.Departamento.Find(id);
                return db.Proyecto.Where(d => d.ProyectoID == id).FirstOrDefault();
            }
        }
        public void Editar(Proyecto proyecto)
        {
            using (var db = new ProyectosContext())
            {
                var p = db.Proyecto.Find(proyecto.ProyectoID);
                p.NombreProyecto = proyecto.NombreProyecto;
                p.FechaInicio = proyecto.FechaInicio;
                p.FechaFin = proyecto.FechaFin;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using (var db = new ProyectosContext())
            {
                var proyecto = db.Proyecto.Find(id);
                db.Proyecto.Remove(proyecto);
                db.SaveChanges();
            }
        }

        public bool ExisteRelacion(int proyectoID, int empleadoID)
        {
            using (var db = new ProyectosContext())
            {
                var existeRelacion = db.ProyectoEmpleado
                    .Any(pe => pe.ProyectoID == proyectoID && pe.EmpleadoID == empleadoID);
                return existeRelacion;
            }
        }

        public void AsignarProyecto(int proyectoID, int empleadoID)
        {
            var proyectoempleado = new ProyectoEmpleado
            {
                ProyectoID = proyectoID,
                EmpleadoID = empleadoID,
                FechaAlta = DateTime.Now,
            };

            using (var db = new ProyectosContext())
            {
                db.ProyectoEmpleado.Add(proyectoempleado);
                db.SaveChanges();
            }
        }

        public List<ProyectoEmpleadoCE> listarProyectoEmpleado()
        {
            string sql = @"select pe.ProyectoID,p.NombreProyecto,pe.EmpleadoID,e.Apellidos,e.Nombres,pe.FechaAlta
                            from ProyectoEmpleado pe
                            inner join Proyecto p on pe.ProyectoID = p.ProyectoID
                            inner join Empleado e on pe.EmpleadoID = e.EmpleadoID;";
            using (var db = new ProyectosContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Database.SqlQuery<ProyectoEmpleadoCE>(sql).ToList();
            }
        }

        public void EliminarProyectoEmpleado(int proyectoID, int empleadoID)
        {
            using (var db = new ProyectosContext())
            {
                var proyectoEmpleado = db.ProyectoEmpleado.Where(e => e.ProyectoID == proyectoID && e.EmpleadoID == empleadoID).FirstOrDefault();
                db.ProyectoEmpleado.Remove(proyectoEmpleado);
                db.SaveChanges();
            }
        }
    }
}
