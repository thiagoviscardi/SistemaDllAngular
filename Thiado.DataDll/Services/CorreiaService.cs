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
        public Entidades.CorreiaEntidade SalvarCorreia(Entidades.CorreiaEntidade correia)
        {
            Model.Correias correiaDB = new Model.Correias();
            if (correia.Id > 0)
            {
                correiaDB = (from n in _db.Correias where n.Id == correia.Id select n).SingleOrDefault();
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
    }
}
