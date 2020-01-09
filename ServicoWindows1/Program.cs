using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServicoWindows1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Service1 service = new Service1();
                service.OnStartDebuger(null);
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);// isso faz ele ficar repetindo e não finaliza o projeto. trava a thread
            }
            //Thiado.DataDll.ThiagoTesteData referenciaThiagoData = new Thiado.DataDll.ThiagoTesteData();// pra que isso?
            //var nome = referenciaThiagoData.Nome();// pra que isso?
            else
            {

                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
