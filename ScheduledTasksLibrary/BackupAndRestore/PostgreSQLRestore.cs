using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledTasksLibrary.BackupAndRestore
{
    public class PostgreSQLRestore
    {
        String strPG_dumpPath = "cd /D C:\\PostgreSQL\r\n\r\ncd pg95\r\n\r\ncd bin\r\n\r\n";
        public bool PgRestore(string serverName, string port, string databaseName, string userName, string password, string path)
        {
            NlogDefinition.logger.Info("Postgresql Restore İşlemi Başlatıldı.");
            StreamWriter sw = new StreamWriter("DBRestore.bat");
            StringBuilder strSB = new StringBuilder(strPG_dumpPath);
            strSB.Append("pg_restore.exe --host " + serverName + " --port " + port + " --username postgres --dbname");
            strSB.Append(" \"" + databaseName + "\"");
            strSB.Append(" --verbose ");
            strSB.Append("\"" + path + "\"");
            sw.WriteLine(strSB);
            sw.Dispose();
            sw.Close();
            try
            {
                Process processDB = Process.Start("DBRestore.bat");
                while (!processDB.WaitForExit(2000)) ;
                NlogDefinition.logger.Info("Process exit code: {0}", processDB.ExitCode);
                NlogDefinition.logger.Info("Postgresql Restore İşlemi Tamamlandı.");
                return true;
            }
            catch (Exception ex)
            {
                NlogDefinition.logger.Error("Hata! : " + ex);
                return false;
            }
        }
    }
}