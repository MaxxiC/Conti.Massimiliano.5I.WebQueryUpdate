using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Model5I_Conti
{
    public class Persona
    {

        public int IdPersona { get; set; }
        public string Nome_C { get; set; }
        public string Cognome_C { get; set; }
        public string Indirizzo_C { get; set; }
        public string Number_C { get; set; }

        public Persona () { }

        public Persona(XElement e)
        {
            Nome_C = e.Attribute("Nome").Value;
            Cognome_C = e.Attribute("Cognome").Value;
            Indirizzo_C = e.Attribute("Indirizzo").Value;
            Number_C = e.Attribute("Numero").Value;
        }

        public Persona(int id, string Nome, string Cogno, string Numb, string Indirizzo)
        {
            this.IdPersona = id;
            this.Nome_C = Nome;
            this.Cognome_C = Cogno;
            this.Number_C = Numb;
            this.Indirizzo_C = Indirizzo;
        }

        //////////////////////////////
        public XElement XML
        {
            get
            {
                return new XElement("Persona",
                        new XAttribute("Nome", Nome_C),
                        new XAttribute("Cognome", Cognome_C),
                        new XAttribute("Indirizzo", Indirizzo_C),
                        new XAttribute("Numero", Number_C)
                );
            }
        }
    }

    /// <divisione>
    /// ///////////////////////
    /// </divisione>
    public class Persone : List<Persona>
    {
        public string FileName { get; }

        public XElement XML
        {
            get
            {
                return new XElement(
                "Rubrica",
                    from item in this
                    select item.XML);
            }
        }

        public Persone(string fileName)
        {
            FileName = fileName;
            XElement Elements = XElement.Load(FileName);
            AddRange(
                from item in Elements.Elements("Persona")
                select new Persona(item)
            );

            int temp = 0;
            foreach (var item in this)
            {
                item.IdPersona = temp++;
            }
        }

        public void Save()
        {
            XML.Save(FileName);
        }

        public void AggiungiPredefinito()
        {
            var pp = this;
            int temp = pp[pp.Count-1].IdPersona;

            Add(
                new Persona
                {
                    IdPersona = temp + 1,
                    Nome_C = "NAS",
                    Cognome_C = "ONE",
                    Number_C = "cientodiciottooo",
                    Indirizzo_C = "via Lone 00"
                }
            );

            Save();
        }

        public void MyAdd(Persona ctn)
        {
            int temp = this[this.Count - 1].IdPersona;
            ctn.IdPersona = temp + 1;
            this.Add(ctn);
        }
    }
}