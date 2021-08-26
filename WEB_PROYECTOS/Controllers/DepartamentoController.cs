using ENTIDAD;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WEB_PROYECTOS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        public ActionResult Index()
        {
            var dptos = DepartamentoCN.ListarDepartamentos();
            return View(dptos);
        }

        // GET
        public ActionResult Crear()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Departamento dpto)
        {
            try
            {
                if (dpto.NombreDepartamento == null)
                    return Json(new { ok = false, msg = "Debes ingresar un nombre de Departamento." }, JsonRequestBehavior.AllowGet);

                DepartamentoCN.Agregar(dpto);
                return Json(new { ok = true , toRedirect = Url.Action("Index") },JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false ,msg= "Ocurrio un Error al Agregar Departamento" },JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDepartamento(int id)
        {
            var dpto = DepartamentoCN.GetDepartamento(id);
            return View(dpto);
        }

        public ActionResult GetDepartamentos() {
            var dptos = DepartamentoCN.ListarDepartamentos();
            return Json(new { data = dptos},JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(int id)
        {
            var dpto = DepartamentoCN.GetDepartamento(id);
            return View(dpto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Departamento dpto)
        {
            try
            {
                if (dpto.NombreDepartamento == null)
                    return Json(new { ok = false, msg = "Debes ingresar un nombre de Departamento." }, JsonRequestBehavior.AllowGet);
                DepartamentoCN.Editar(dpto);
                return Json(new { ok = true ,toRedirect = Url.Action("Index")},JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "Ocurrió un error al Editar el Departamento" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dpto = DepartamentoCN.GetDepartamento(id.Value);
            return View(dpto);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                DepartamentoCN.ELiminar(id);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "Ocurrio un problema al eliminar" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}