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
            if(_db.Correias.Count(n => n.IdResponsavel == id) > 0)
            {
                // existe um idResponsavel com este mesmo id do usuario     return true
            } else
            {
                //NÃO existe um idResponsavel com este meSMo id do usuario     return false
            }

            return _db.Correias.Count(n => n.IdResponsavel == id) >0;
            // conta  aquantidade de idResponsavel da correia é igual ao id da tabela usuario
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

        public bool Deletar(int id)
        {
            var correiaBD = (from n in _db.Correias where n.Id == id select n).SingleOrDefault();
            if (correiaBD != null)
            {
                _db.Correias.Remove(correiaBD);
                _db.SaveChanges();
                return true;
            }
            return false;

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

        public List<Entidades.CorreiaEntidade> ListarCorreiasPorNome(string nome)
        {
            List<Entidades.CorreiaEntidade> lista = new List<Entidades.CorreiaEntidade>();
            Entidades.CorreiaEntidade correia;
            if(nome!=null && nome!="")
            {
                foreach (var item in from n in _db.Correias where n.Nome.Contains(nome) select n)
                {
                    correia = new Entidades.CorreiaEntidade();
                    correia.Id = item.Id;
                    correia.Nome = item.Nome;
                    correia.IdResponsavel = item.IdResponsavel;
                    correia.Preco = item.Preco;
                   

                    lista.Add(correia);
                }
            }

            return lista;
        }

        //public Entidades.CorreiaEntidade EditarCorreia(int id)
        //{

        //}
    }
}
