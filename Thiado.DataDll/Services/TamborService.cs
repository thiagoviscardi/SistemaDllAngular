using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thiado.DataDll.Services
{
    public class TamborService
    {
        // instanciar a classe de conexão com o banco de dados
        private Model.TreinamentoThiagoEntities1 _db = new Model.TreinamentoThiagoEntities1();

        public Entidades.TamborEntidade SalvarTambor(Entidades.TamborEntidade tambor)
        {
            Model.Tambor tamborDB = new Model.Tambor();
            if(tambor.Id > 0)
            {
                tamborDB = (from n in _db.Tambor where n.Id == tambor.Id select n).SingleOrDefault();

                //tamborDB.Id = tambor.Id;// não precisa colocar pois já é auto increment

            }

            if (tamborDB == null)
            {
                tambor.Id = -1;
                // gostaria de adicionar a classe helper aqui para colocar uma critica. pois se nao fizer aqui teria que chamar o banco na controller
                return tambor; //  retorna esse usuario nulo para controller
            }


            tamborDB.IdResponsavel = tambor.IdResponsavel;
            tamborDB.Nome = tambor.Nome;
            tamborDB.Preco = tambor.Preco;

            if (tambor.Id == 0)
            {
                _db.Tambor.Add(tamborDB);
            }

           
            _db.SaveChanges();
            tambor.Id = tamborDB.Id;
            return tambor;
        }

        public List<Entidades.TamborEntidade> Buscar ()

        {
            List<Entidades.TamborEntidade> lista = new List<Entidades.TamborEntidade>();
            Entidades.TamborEntidade tambor = null;
            foreach (var item in from n in _db.Tambor select n)
            {
                tambor = new Entidades.TamborEntidade();
                tambor.Id = item.Id;
                tambor.Nome = item.Nome;
                tambor.IdResponsavel = item.IdResponsavel;
                tambor.Preco = item.Preco;
               

                lista.Add(tambor);
            }
            return lista;
        }


        //{
        //    Model.TreinamentoThiagoEntities1 _db = new Model.TreinamentoThiagoEntities1();

        //    List<Entidades.TamborEntidade> lista = new List<Entidades.TamborEntidade>();

        //    Entidades.TamborEntidade tamborDB = null;
        //    var buscaUsuario = (from n in _db.Tambor select n);

        //    foreach (var item in buscaUsuario)
        //    {
        //        tamborDB = new Entidades.TamborEntidade();
        //        tamborDB.Id = item.Id;
        //        tamborDB.IdResponsavel = item.IdResponsavel;
        //        tamborDB.Nome = item.Nome;
        //        tamborDB.Preco = item.Preco;

        //        lista.Add(tamborDB);
        //    }

        //    return lista;
        //}
    }
}
