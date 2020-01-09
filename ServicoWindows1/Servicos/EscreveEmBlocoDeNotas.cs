using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServicoWindows1.Servicos
{
    class EscreveEmBlocoDeNotas
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

           
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\Users\thiago.viscardi\Desktop\ServicoThiago\DataAtual.txt", true);

            sw.WriteLine(DateTime.Now.ToString()+" Funcionou?");// gera e escreve a no arquivo. da um append
            //File.Open(@"C:\Users\thiago.viscardi\Desktop\ServicoThiago\DataAtual.txt", FileMode.Open);
            sw.Close();
         

            _timer.Start();
        }
    }
}
