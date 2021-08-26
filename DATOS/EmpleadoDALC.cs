using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class EmpleadoDALC
    {
        public void Agregar(Empleado empleado)
        {
            using (var db = new ProyectosContext())
            {
                db.Empleado.Add(empleado);
                db.SaveChanges();
            }
        }
        public List<EmpleadoCE> ListarEmpleados()
        {
            string sql = @"select e.EmpleadoID, e.Nombres, e.Apellidos, e.Email, e.Direccion, e.Celular, e.DepartamentoID, d.NombreDepartamento
                           from Empleado e
                           inner join Departamento d on e.DepartamentoID=d.DepartamentoID";
            using (var db = new ProyectosContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Database.SqlQuery<EmpleadoCE>(sql).ToList();
            }
        }

        public EmpleadoCE GetEmpleado(int id)
        {
            string sql = @"select e.EmpleadoID, e.Nombres, e.Apellidos, e.Email, e.Direccion, e.Celular, e.DepartamentoID, d.NombreDepartamento
                           from Empleado e
                            inner join Departamento d on e.DepartamentoID=d.DepartamentoID
                            where EmpleadoID = @CodEmpleado";
            using (var db = new ProyectosContext())
            {
                //return db.Departamento.Find(id);
                //return db.Empleado.Where(d => d.EmpleadoID == id).FirstOrDefault();
                return db.Database.SqlQuery<EmpleadoCE>(sql,
                    new SqlParameter("@CodEmpleado", id)).FirstOrDefault();
            }
        }
        public void Editar(Empleado empleado)
        {
            using (var db = new ProyectosContext())
            {
                var e = db.Empleado.Find(empleado.EmpleadoID);
                e.Nombres = empleado.Nombres;
                e.Apellidos=empleado.Apellidos;
                e.Email = empleado.Email;
                e.Direccion = empleado.Direccion;
                e.Celular = empleado.Celular;
                e.DepartamentoID = empleado.DepartamentoID;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using (var db = new ProyectosContext())
            {
                var empleado = db.Empleado.Find(id);
                db.Empleado.Remove(empleado);
                db.SaveChanges();
            }
        }
    }
}
