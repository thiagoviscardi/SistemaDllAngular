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

            
            TMSAMongo.Services.MongoService servico = new TMSAMongo.Services.MongoService();
            tamborzao tambor = new tamborzao();
            tambor.Nome = "Mongo Tambor";
            tambor.Preco = 200;
            tambor.Cor = "Azul";

            //servico.Save<tamborzao>(tambor);

            var pegaTambor = servico.CarregarTudo<tamborzao>();
             
        }

       
        
    }
    public class tamborzao : TMSAMongo.Entities.BaseMongoEntity
    {
        //public int Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public string Cor { get; set; }
       
    }
}
