using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BackupWindowsService
{
    static class Program
    {
        static void Main()
        {
#if (!DEBUG)
                   ServiceBase[] ServicesToRun;
                   ServicesToRun = new ServiceBase[] 
        	   { 
        	        new Service1() 
        	   };
                   ServiceBase.Run(ServicesToRun);
#else
            Service1 myServ = new Service1();
            myServ.Process();
#endif
        }
    }
}