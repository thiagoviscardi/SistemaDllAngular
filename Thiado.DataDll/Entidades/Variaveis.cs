using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thiado.DataDll.Entidades
{
    public class Variaveis : TMSAMongo.Entities.BaseMongoEntity
    {
        public Variaveis()
        {
            this.Valor = 0;
        }
        public string Descricao { get; set; }
        public string UnidadeMedida { get; set; }
        public string VariavelLetra { get; set; }
        public string VariavelWDL { get; set; }
        public int Valor { get; set; }
        public string Tipo { get; set; }
        public List<OpcoesVariavelDeControle> Opcoes { get; set; }
    }
    public class OpcoesVariavelDeControle
    {
        public string Valor { get; set; }
        public string Descricao { get; set; }
    }
}