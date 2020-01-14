using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thiado.DataDll.Entidades
{
    public class CorreiaEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public int IdResponsavel { get; set; }
    }
}
