using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteDeSolucao.Helper
{
    public class JsonRetorno
    {
        public JsonRetorno()
        {
            Mensagem = string.Empty;
            Criticas = new List<Criticas>();
           
        }

        public object Data { get; set; }
        public string Mensagem { get; set; }



        public List<Criticas> Criticas { get; set; }

    }

    public class Criticas
    {
        public Criticas()
        {
            FieldId = string.Empty;
            FieldClass = string.Empty;
        }
        public string Mensagem { get; set; }
        public string CampoId { get; set; } // o que isso pega msm?
        public string FieldId { get; set; }
        public string FieldClass { get; set; }
    }

}