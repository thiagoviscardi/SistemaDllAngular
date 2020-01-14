using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            Thiado.DataDll.ThiagoTesteData referenciaThiagoData = new Thiado.DataDll.ThiagoTesteData();
            var nome = referenciaThiagoData.Nome();

            Console.WriteLine(nome);

            var usuario = new Thiado.DataDll.Entidades.UsuarioEntidade();
            var correia = new Thiado.DataDll.Entidades.CorreiaEntidade();
            Thiado.DataDll.Services.CorreiaService CorreiaSalvar = new Thiado.DataDll.Services.CorreiaService();
            Thiado.DataDll.Services.UsuarioService UsuarioSalvar = new Thiado.DataDll.Services.UsuarioService();

            CorreiaSalvar.SalvarCorreia(correia);
            //UsuarioSalvar.Salvar(usuario);


            Console.ReadLine();// faz com que o console permaneça aberto!
        }
    }
}
