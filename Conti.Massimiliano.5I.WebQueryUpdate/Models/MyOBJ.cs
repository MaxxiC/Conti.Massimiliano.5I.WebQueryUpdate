using Model5I_Conti;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Conti.Massimiliano._5I.WebQueryUpdate
{
    public class MyOBJ
    {
        public DataTable dt { get; set; }
        public List<List<string>> li { get; set; }

        public string query { get; set; }
        public string DataBase { get; set; }

        public string TheId { get; set; }

        public MyOBJ(string nomeDB, DataTable dtt, string queryy)
        {
            DataBase = nomeDB;
            query = queryy;
            dt = dtt;
            //riempita la DataTable
            //riempio la lista di lista

            li = new List<List<string>>();
            List<string> sec = new List<string>();

            foreach (DataRow row in dtt.Rows)
            {
                sec = new List<string>();
                for (int i = 0; i < dtt.Columns.Count; i++)
                {
                    sec.Add(row[i].ToString());
                }
                li.Add(sec);
            }

        }
    }
}