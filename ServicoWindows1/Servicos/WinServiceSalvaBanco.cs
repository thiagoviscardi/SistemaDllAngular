using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoWindows1.Servicos
{

    class WinServiceSalvaBanco
    {
        private System.Timers.Timer _timer;

        public void Executar()
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += _timer_Elapsed;
            _timer.Interval = 1000;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Stop();
            _timer.Interval = 1000 * 10;

            //System.IO.File.WriteAllText(@"C:\Users\thiago.viscardi\Desktop\ServicoThiago\DataAtual.txt", DateTime.Now.ToString()); // cria e salva/substitui uma data
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\Users\thiago.viscardi\Desktop\ServicoThiago\DataAtual.txt", true);
            //sw.WriteLine(DateTime.Now.ToString());// gera e escreve a no arquivo. da um append
            //sw.Close();
            Thiado.DataDll.Entidades.UsuarioEntidade item1 = new Thiado.DataDll.Entidades.UsuarioEntidade();

            //item1.Nome = string.Concat("Thiafo-", DateTime.Now.ToString());
            //item1.Nome = string.Format("Thiafo-{0}{1}", DateTime.Now.ToString(),"Variavel 2");
            //item1.Nome = $"Thiagos-{DateTime.Now.ToString()}";
            item1.Id = 0;
            item1.Nome = "Thiagos" + DateTime.Now.ToString();
            item1.Sexo = "M";
            item1.Idade = 34;
            item1.Ativo = true;

            Thiado.DataDll.Services.UsuarioService salvaBanco = new Thiado.DataDll.Services.UsuarioService();
            salvaBanco.Salvar(item1);

            _timer.Start();
        }

    }
}
