﻿using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DepartamentoDALC
    {
        public void Agregar(Departamento dpto)
        {
            using (var db = new ProyectosContext())
            {
                db.Departamento.Add(dpto);
                db.SaveChanges();
            }
        }
        public List<Departamento> ListarDepartamentos()
        {
            using (var db = new ProyectosContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Departamento.ToList();
            }
        }
        
        public Departamento GetDepartamento(int id)
        {
            using (var db = new ProyectosContext())
            {
                //return db.Departamento.Find(id);
                return db.Departamento.Where(d=>d.DepartamentoID==id).FirstOrDefault();
            }
        }

        public void Editar(Departamento dpto)
        {
            using (var db = new ProyectosContext())
            {
                var d = db.Departamento.Find(dpto.DepartamentoID);
                d.NombreDepartamento = dpto.NombreDepartamento;
                db.SaveChanges();
            }
        }

        public void ELiminar(int id)
        {
            using (var db = new ProyectosContext())
            {
                var dpto = db.Departamento.Find(id);
                db.Departamento.Remove(dpto);
                db.SaveChanges();
            }
        }
    }
}
