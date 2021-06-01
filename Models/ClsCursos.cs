using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EXPRAU2_CONDORI.Models
{
    [Serializable]
    [XmlRoot("planstudio"), XmlType("planstudio")]
    public class Plan
    {
        public string codigo { get; set; }
        public string asignatura { get; set; }
        public string ht { get; set; }
        public string hp { get; set; }
        public string th { get; set; }
        public string creditos { get; set; }
        public string pre_requisito { get; set; }


        XmlDocument doc;
        string rutaXml;
        public List<Plan> ListarPlan()
        {
            //Definicion
            XDocument xmlData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/planstudio.xml"));

            var objEstudiante = new List<Plan>();
            objEstudiante = (from col in xmlData.Descendants("plan")

                             select new Plan
                             {
                                 codigo = col.Element("codigo").Value,
                                 asignatura = col.Element("asignatura").Value,
                                 ht = col.Element("ht").Value,
                                 hp = col.Element("hp").Value,
                                 th = col.Element("th").Value,
                                 creditos = col.Element("creditos").Value,
                                 pre_requisito = col.Element("pre_requisito").Value,

                             }).ToList();
            return objEstudiante;
        }

        public void _Add(string codigo, string asignatura, string ht, string hp, string th, string creditos, string pre_requisito)
        {
            doc = new XmlDocument();
            rutaXml = HttpContext.Current.Server.MapPath("~/App_Data/planstudio.xml");
            doc.Load(rutaXml);

            XmlNode empleado = _Crear_plan(codigo, asignatura, ht, hp, th, creditos, pre_requisito);

            XmlNode nodoRaiz = doc.DocumentElement;

            nodoRaiz.InsertAfter(empleado, nodoRaiz.LastChild);

            doc.Save(rutaXml);


        }
        private XmlNode _Crear_plan(string codigo, string asignatura, string ht, string hp, string th, string creditos, string pre_requisito)
        {

            XmlNode plan = doc.CreateElement("plan");

            XmlElement codigo1 = doc.CreateElement("codigo");
            codigo1.InnerText = codigo;
            plan.AppendChild(codigo1);

            XmlElement asignatura1 = doc.CreateElement("asignatura");
            asignatura1.InnerText = asignatura;
            plan.AppendChild(asignatura1);

            XmlElement ht1 = doc.CreateElement("ht");
            ht1.InnerText = ht;
            plan.AppendChild(ht1);

            XmlElement hp1 = doc.CreateElement("hp");
            hp1.InnerText = hp;
            plan.AppendChild(hp1);

            XmlElement th1 = doc.CreateElement("th");
            th1.InnerText = th;
            plan.AppendChild(th1);

            XmlElement creditos1 = doc.CreateElement("creditos");
            creditos1.InnerText = creditos;
            plan.AppendChild(creditos1);

            XmlElement pre_requisito1 = doc.CreateElement("pre_requisito");
            pre_requisito1.InnerText = pre_requisito;
            plan.AppendChild(pre_requisito1);

            return plan;
        }
        public void _DeleteNodo(string id_borrar)
        {

            doc = new XmlDocument();
            rutaXml = HttpContext.Current.Server.MapPath("~/App_Data/planstudio.xml");
            doc.Load(rutaXml);

            XmlNode empleados = doc.DocumentElement;
            XmlNodeList listaEmpleados = doc.SelectNodes("planstudio/plan");

            foreach (XmlNode item in listaEmpleados)
            {
                if (item.SelectSingleNode("codigo").InnerText == id_borrar)
                {
                    XmlNode nodoOld = item;
                    empleados.RemoveChild(nodoOld);
                }
            }
            doc.Save(rutaXml);
        }

        public List<Plan> Buscar(string criterio, string nombre)
        {
            XDocument xmlData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/planstudio.xml"));

            var objEstudiante = new List<Plan>();
            objEstudiante = (from col in xmlData.Descendants("plan")
                             where col.Element(criterio).Value.ToString() == (nombre)
                             select new Plan
                             {
                                 codigo = col.Element("codigo").Value,
                                 asignatura = col.Element("asignatura").Value,
                                 ht = col.Element("ht").Value,
                                 hp = col.Element("hp").Value,
                                 th = col.Element("th").Value,
                                 creditos = col.Element("creditos").Value,
                                 pre_requisito = col.Element("pre_requisito").Value,
                             }).ToList();
            return objEstudiante;
        }

        public Plan DAtos(string codigo)
        {


            XDocument xmlData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/planstudio.xml"));

            var objEstudiante = new Plan();
            objEstudiante = (from col in xmlData.Descendants("plan")
                             where col.Element("codigo").Value.ToString() == (codigo)
                             select new Plan
                             {
                                 codigo = col.Element("codigo").Value,
                                 asignatura = col.Element("asignatura").Value,
                                 ht = col.Element("ht").Value,
                                 hp = col.Element("hp").Value,
                                 th = col.Element("th").Value,
                                 creditos = col.Element("creditos").Value,
                                 pre_requisito = col.Element("pre_requisito").Value,
                             }).FirstOrDefault();
            return objEstudiante;
        }

        public void _UpdateXml(string codigo, string asignatura, string ht, string hp, string th, string creditos, string pre_requisito)
        {
            doc = new XmlDocument();
            rutaXml = HttpContext.Current.Server.MapPath("~/App_Data/planstudio.xml");

            XmlElement empleados = doc.DocumentElement;

            XmlNodeList listaEmpleados = doc.SelectNodes("planstudio/plan");

            XmlNode nuevo_empleado = _Crear_plan(codigo, asignatura, ht, hp, th, creditos, pre_requisito);

            foreach (XmlNode item in listaEmpleados)
            {

                if (item.FirstChild.InnerText == codigo)
                {
                    XmlNode nodoOld = item;
                    empleados.ReplaceChild(nuevo_empleado, nodoOld);

                }
            }

            doc.Save(rutaXml);
        }

    }

}