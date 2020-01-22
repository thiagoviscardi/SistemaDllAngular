using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thiado.DataDll.Services
{
    public class CorreiaService
    {
        private Model.TreinamentoThiagoEntities1 _db = new Model.TreinamentoThiagoEntities1();
        // tenho que ficar usando essas conexões diferentes??

        public bool VerificaSeTemCorreia(int id)
        {
            var temCorreia = (from n in _db.Correias where n.Id == id select n).SingleOrDefault(); // fazer o select no usuario que queremos
            if (temCorreia != null)
            {
                return true;
            }

                return false;//                  CONTINUAR DAQUI!!
           // return _db.Correias.Count(n => n.Id == id) > 0;// entender isso do banco como fazer???

        }

        public Entidades.CorreiaEntidade SalvarCorreia(Entidades.CorreiaEntidade correia)
        {
            Model.Correias correiaDB = new Model.Correias();
            if (correia.Id > 0)
            {
                correiaDB = (from n in _db.Correias where n.Id == correia.Id select n).SingleOrDefault();// esse Correias depois do _db é a tabela do banco né?!
            }
            //correiaDB.Id = correia.Id;
            correiaDB.IdResponsavel = correia.IdResponsavel;
            correiaDB.Nome = correia.Nome;
            correiaDB.Preco = correia.Preco;

            if(correia.Id == 0)
            {
                _db.Correias.Add(correiaDB);
            }

            _db.SaveChanges();

            correia.Id = correiaDB.Id;

            return correia;
        }

     


        public List<Entidades.CorreiaEntidade> ListarCorreias()
        {
            Entidades.CorreiaEntidade correiaEntidade = null;
            List<Entidades.CorreiaEntidade> lista = new List<Entidades.CorreiaEntidade>();
            foreach (var item in from n in _db.Correias select n)
            {
                correiaEntidade = new Entidades.CorreiaEntidade();
                correiaEntidade.Id = item.Id;
                correiaEntidade.IdResponsavel = item.IdResponsavel;
                correiaEntidade.Nome = item.Nome;
                correiaEntidade.Preco = item.Preco;

                lista.Add(correiaEntidade);
            }
            return lista;
        }

        //public Entidades.CorreiaEntidade EditarCorreia(int id)
        //{

        //}
    }
}
