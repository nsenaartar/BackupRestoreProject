using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledTasksLibrary.BackupAndRestore
{
    public class NlogDefinition
    {
      public static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
