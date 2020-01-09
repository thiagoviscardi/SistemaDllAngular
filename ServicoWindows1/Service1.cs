using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServicoWindows1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }


        public void OnStartDebuger(string[] args)
        {
            OnStart(args);
        }
        protected override void OnStart(string[] args)
        {

            //Servicos.WinServiceSalvaBanco winServiceSalvaBanco = new Servicos.WinServiceSalvaBanco();
            //winServiceSalvaBanco.Executar();
//OUTRO SERVIÇO
            Servicos.EscreveEmBlocoDeNotas escreveEmBlocoDeNotas = new Servicos.EscreveEmBlocoDeNotas();
            escreveEmBlocoDeNotas.Executar();

        }

        protected override void OnStop()
        {
        }

        
    }
}
