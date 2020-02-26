using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteMongo
{
    class Program
    {
        static void Main(string[] args)
        {

            Program program = new Program();
            //var salvaBanco = program.SalvarTamborMongo();
            var bancoMongo = program.BuscarTamborMongo();
        }

        public bool SalvarTamborMongo ()
        {
            TMSAMongo.Services.MongoService servico = new TMSAMongo.Services.MongoService();

            tamborzao tambor = new tamborzao();
            tambor.Nome = "Tambor Tambor";
            tambor.Preco = 300;
            tambor.Cor = "Rosa";

            servico.Save(tambor);

            return true;
        }

        public System.Collections.Generic.List<tamborzao> BuscarTamborMongo() {
             TMSAMongo.Services.MongoService servico = new TMSAMongo.Services.MongoService();  
            var pegaTambor = servico.CarregarTudo<tamborzao>();

            return pegaTambor;
        }

       
        
    }
    public class tamborzao : TMSAMongo.Entities.BaseMongoEntity// tem que herdar só pra pegar o id??
    {
        //public int Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public string Cor { get; set; }
       
    }
}
