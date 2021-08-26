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
    [Authorize(Roles="Admin")]
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {
            var empleados = EmpleadoCN.ListarEmpleados();
            return View(empleados);
        }
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Empleado empleado)
        {
            try
            {
                if (empleado.Nombres == null)
                    return Json(new { ok = false, msg = "Debe ingresar el nombre del empleado" }, JsonRequestBehavior.AllowGet);

                EmpleadoCN.Agregar(empleado);
                return Json(new { ok = true, toRedirect = @Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "Ocurrio un error al agregar el empleado" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetEmpleado(int id)
        {
            var empleado = EmpleadoCN.GetEmpleado(id);
            return View(empleado);
        }
        public ActionResult Editar(int id)
        {
            var empleado = EmpleadoCN.GetEmpleado(id);
            return View(empleado);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Empleado empleado)
        {
            try
            {
                if (empleado.Nombres == null)
                    return Json(new { ok = false, msg = "Debe ingresar el nombre del empleado" }, JsonRequestBehavior.AllowGet);
                EmpleadoCN.Editar(empleado);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "Ocurrio un error al editar el empleado" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var empleado = EmpleadoCN.GetEmpleado(id.Value);
            return View(empleado);
        }

        public JsonResult ListarEmpleados() {
            try
            {
                var e = EmpleadoCN.ListarEmpleados();
                return Json(new {ok=true,empleado=e }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { ok = false, msg = "No se  pudo listar empleados" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                EmpleadoCN.Eliminar(id);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "Ocurrio un error al eliminar el Empleado" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}