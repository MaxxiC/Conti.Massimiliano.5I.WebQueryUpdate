using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml.Linq;
using Model5I_Conti;
using System.IO;

namespace Conti.Massimiliano._5I.WebQueryUpdate
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// WebQuery
        /// </summary>

        string LastQuery = GetLastQuery();

        private string percorsoDB = HostingEnvironment.MapPath(@"~/App_Data/Database1.accdb");
        private string nomeDB = "Database1.accdb";

        public ActionResult WebQuery()
        {
            return View();
        }

        public static string GetLastQuery()
        {
            StreamReader st = new StreamReader(HostingEnvironment.MapPath(@"~/App_Data/ultimaQuery.txt"));
            string ret = st.ReadLine();
            st.Close();
            return ret;
        }

        public ActionResult WebQueryRet(string MyQuery)
        {
            StreamWriter f = new StreamWriter(HostingEnvironment.MapPath(@"~/App_Data/ultimaQuery.txt"));
            f.WriteLine(MyQuery);
            f.Close();

            DAL dl = new DAL(nomeDB);
            DataTable dt = dl.Getdata(MyQuery);
            MyOBJ Mobj = new MyOBJ(nomeDB, dt, MyQuery);

            MyOBJ tryy = new MyOBJ(
                nomeDB,
                new DAL(nomeDB).Getdata(MyQuery),
                MyQuery);

            return View(Mobj);
        }

        public ActionResult WebQueryEdit(string id, string nomeDB, string query)
        {
            DAL dl = new DAL(nomeDB);
            DataTable dt = dl.Getdata(LastQuery);
            MyOBJ mm = new MyOBJ(nomeDB, dt, LastQuery);

            List<string> mout = new List<string>();

            foreach (List<string> f in mm.li)
            {
                if (f[0] == id)
                {
                    mout = f;
                }
            }

            return View(mout);
        }

        public ActionResult UpdateQuery(List<string> lst)
        {
            int a = 0;
            MyOBJ tryy = new MyOBJ(
                nomeDB,
                new DAL(nomeDB).Getdata(LastQuery),
                LastQuery);

            int indice = 0;
            int i = 0;

            foreach (List<string> f in tryy.li)
            {
                
                if (f[0] == lst[0])
                {
                    indice = i;
                }
                i++;
            }

            tryy.li[indice] = lst;


            return View("WebQueryRet-List", tryy.li);
        }




        /// <summary>
        /// /// PersoneXML
        /// </summary>

        private string nomeFile = HostingEnvironment.MapPath(@"~/App_Data/Persone.xml");

        public ActionResult PersoneXMLWeb()
        {
            XElement data = XElement.Load(nomeFile);
            var persone = (from l in data.Elements("Persona") select new Persona(l)).ToList();
            return View(persone);
        }

        public ActionResult XMLReadWrite()
        {
            var p = new Persone(nomeFile);
            return View("XMLReadWrite", p);
        }

        /// <summary>
        /// - XMLReadWrite2
        /// </summary>
        /// 

        public ActionResult XMLReadWrite2()
        {
            var p = new Persone(nomeFile);
            return View(p);
        }

        public ActionResult AddPredefinito()
        {
            var p = new Persone(nomeFile);
            p.AggiungiPredefinito();

            return View("XMLReadWrite2", p);
        }

        public ActionResult DelSelected(int IdToDel)
        {
            var p = new Persone(nomeFile);
            p.RemoveAll(x => x.IdPersona == IdToDel);
            p.Save();

            return View("XMLReadWrite2", p);
        }

        public ActionResult XML_AddContact()
        {
            return View();
        }

        public ActionResult RetContatto(Persona ctn)
        {
            var p = new Persone(nomeFile);
            p.MyAdd(ctn);
            p.Save();

            return View("XMLReadWrite2", p);
        }

        public ActionResult XML_ViewContact(int ctn)
        {
            var p = new Persone(nomeFile);
            Persona temp = p.FirstOrDefault(x => x.IdPersona == ctn);
            return View(temp);
        }

        public ActionResult XML_EditContact(int ctn)
        {
            var p = new Persone(nomeFile);
            Persona temp = p.FirstOrDefault(x => x.IdPersona == ctn);
            return View("XML_AddContact", temp);
        }

    }
}