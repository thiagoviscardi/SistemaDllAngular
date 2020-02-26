using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thiado.DataDll.Entidades
{
    public class MongoUsuarioEntidade : TMSAMongo.Entities.BaseMongoEntity
    {
        //public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Sexo { get; set; }
        public bool Ativo { get; set; }

        public string Ativostr
        {
            get
            {
                return Ativo ? "Sim" : "Não";
            }
        }
    }
}
