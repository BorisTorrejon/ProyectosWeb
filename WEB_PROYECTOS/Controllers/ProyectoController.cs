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
    public class ProyectoController : Controller
    {
        // GET: Proyecto
        public ActionResult Index()
        {
            var proyectos = ProyectoCN.ListarProyectos();
            return View(proyectos);
        }
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Proyecto proyecto)
        {
            try
            {
                if (proyecto.NombreProyecto == null)
                    return Json(new { ok = false, msg = "Debe ingresar el nombre del proyecto" }, JsonRequestBehavior.AllowGet);

                //demora 1000 milisegundos
                //System.Threading.Thread.Sleep(1000);

                ProyectoCN.Agregar(proyecto);
                return Json(new { ok = true, toRedirect = @Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "Ocurrio un error al agregar el Proyecto" }, JsonRequestBehavior.AllowGet);
                //ModelState.AddModelError("", "Ocurrio un error al agregar el Proyecto");
                //return View(proyecto);
            }
        }

        public ActionResult GetProyecto(int id)
        {
            var proyecto = ProyectoCN.GetProyecto(id);
            return View(proyecto);
        }
        public ActionResult Editar(int id)
        {
            var proyecto = ProyectoCN.GetProyecto(id);
            return View(proyecto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Proyecto proyecto)
        {
            try
            {
                if (proyecto.NombreProyecto == null)
                    return Json(new { ok = false, msg = "Debe ingresar el nombre del proyecto" }, JsonRequestBehavior.AllowGet);
                ProyectoCN.Editar(proyecto);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "Ocurrio un error al agregar el Proyecto" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var proyecto = ProyectoCN.GetProyecto(id.Value);
            return View(proyecto);
        }
        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                ProyectoCN.Eliminar(id);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "Ocurrio un error al eliminar el Proyecto" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarProyectos() {
            try
            {
                var p = ProyectoCN.ListarProyectos();
                return Json(new {ok = true,proyectos = p }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "No se pudo listar proyectos" }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult FListarProyectos()
        {
            try
            {
                var p = ProyectoCN.FListarProyectos();
                return Json(new { ok = true, proyectos = p }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "No se pudo listar proyectos" }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public ActionResult AsignarProyecto()
        {
            return View(ProyectoCN.listarProyectoEmpleado());
        }
        
        [HttpPost]
        public ActionResult AsignarProyecto(int proyectoID, int empleadoID)
        {
            try
            {
                if (ProyectoCN.ExisteRelacion(proyectoID, empleadoID))
                    return Json(new { ok = false, msg = "El empleado ya se encuentra asignado en este Proyecto" }, JsonRequestBehavior.AllowGet);
                ProyectoCN.AsignarProyecto(proyectoID, empleadoID);
                return Json(new { ok = true, toRedirect = Url.Action("AsignarProyecto") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "Ocurrio un error al asignar al Proyecto" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EliminarProyectoEmpleado(int proyectoID, int empleadoID) {
            try
            {
                ProyectoCN.EliminarProyectoEmpleado(proyectoID, empleadoID);
                return Json(new { ok = true, toRedirect = Url.Action("AsignarProyecto") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "se elimino a " + empleadoID + " de " + proyectoID }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}