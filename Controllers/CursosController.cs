using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EXPRAU2_CONDORI.Models;

namespace EXPRAU2_CONDORI.Controllers
{
    public class CursosController : Controller
    {
        // GET: Cursos
        Plan p = new Plan();
        public ActionResult Index(Plan obj, string criterio, string busqueda)
        {
            
            if (busqueda == "" || busqueda == null || criterio == "" || criterio == null)
            {
                return View(obj.ListarPlan());
            }
            else
            {
                return View(obj.Buscar(criterio, busqueda));
            }
        }

        public ActionResult Crear()
        {
            return View();
        }

        public ActionResult Guardar(Plan o)
        {
            Random rd = new Random();
            p._Add(o.codigo, o.asignatura, o.ht, o.hp, rd.Next(2, 4).ToString(), o.creditos, o.pre_requisito);
            var query = p.ListarPlan();
            return View("Index", query);
        }

        public ActionResult Eliminar(Plan o)
        {
            string id = o.codigo;
            p._DeleteNodo(id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Editar(string codigo)
        {

            Plan oj = new Plan();
            var data = oj.DAtos(codigo);
            return View(data);
        }

        public ActionResult Modificar(Plan datos)
        {
            Plan oj = new Plan();
            oj._UpdateXml(datos.codigo, datos.asignatura, datos.ht, datos.hp, datos.th, datos.creditos, datos.pre_requisito);
            var query = p.ListarPlan();
            return View("Index", query);
        }


    }
}