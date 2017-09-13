using NLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ScheduledTasksLibrary.BackupAndRestore
{
    public class PostgreSQLBackup
    {
        StringBuilder sbPG_dumpPath = new StringBuilder("cd /D C:\\PostgreSQLcd pg95cd bin");
        String strPG_dumpPath = "cd /D C:\\PostgreSQL\r\n\r\ncd pg95\r\n\r\ncd bin\r\n\r\n";
        public bool PgDump(string serverName, string port, string databaseName, string userName, string password, string path)
        {
            NlogDefinition.logger.Info("PostgreSQL Backup Alma İşlemi Başlatıldı. ");
            string strConnection = "Server=" + serverName + ";Port=" + port + ";Database=" + databaseName + ";Userid=" + userName + ";Password=" + password + ";";
            int start = strConnection.IndexOf("Server");
            start = start + ("Server").Length + 1;
            int end = strConnection.IndexOf(";", start);
            end = end - start;
            serverName = strConnection.Substring(start, end);
            start = strConnection.IndexOf("Port");
            start = start + ("Port").Length + 1;
            end = strConnection.IndexOf(";", start);
            end = end - start;
            port = strConnection.Substring(start, end);
            port = "5432";
            strConnection = "Server=" + serverName + ";Port=" + port + ";Database=" + databaseName + ";Userid=" + userName + ";Password=" + password + ";";
            StreamWriter sw = new StreamWriter("DBBackup.bat");
            String date = String.Format("{0:d}", DateTime.Now);
            string backupFileName = databaseName + " " + date + ".backup";
            String yol = path + backupFileName;
            StringBuilder strSB = new StringBuilder(strPG_dumpPath);

            strSB.Append("pg_dump.exe --host " + serverName + " --port " + port + " --username postgres --format custom --blobs --verbose --file ");
            strSB.Append("\"" + yol + "\"");
            strSB.Append(" \"" + databaseName + "\r\n\r\n");
            sw.WriteLine(strSB);
            sw.Dispose();
            sw.Close();
            try
            {
                Process processDB = Process.Start("DBBackup.bat");
                while (!processDB.WaitForExit(2000)) ;
                NlogDefinition.logger.Info("Process exit code: {0}", processDB.ExitCode);
                NlogDefinition.logger.Info("PostgreSQL Backup Alma İşlemi Tamamlandı. ");
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
