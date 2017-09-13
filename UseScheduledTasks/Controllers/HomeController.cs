using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using ScheduledTasksLibrary.BackupAndRestore;
using ScheduledTasks.BackupAndRestore;

namespace UseScheduledTasks.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            NlogDefinition.logger.Info("Use Scheduled Task Projesi başlatıldı.");
            return View();
        }
        public ActionResult PortgreSqlBackup()
        {
            PostgreSQLBackup postgreSqlBackup = new PostgreSQLBackup();
            postgreSqlBackup.PgDump("localhost", "5432", "dvdrentral", "postgres", "14531453", @"D:\YEDEK\");
            ViewBag.Durum = "PortgreSQL Backup Alındı.";
            return View("Index");
        }
        public ActionResult SendMail()
        {
            SendMail sendMail = new SendMail();
            sendMail.SendMailBackupInformation("info@mekansalbulut.com", "MekansalBulut61", "info@mekansalbulut.com", "mail.mekansalbulut.com", "Zamanlanmış Backup Bilgi Maili", "Otomatik yedekleme işlemi tamamlandı.");
            ViewBag.Durum = "Mail Gönderildi.";
            return View("Index");
        }
        public ActionResult FileOperations()
        {
            FileOperations deneme = new FileOperations();
            //deneme.FolderZip(@"D:YEDEK");
            //deneme.Move("dvdrentral.backup", @"D:YEDEK", @"D:");
            //deneme.Copy("dvdrentral.backup", @"D:", @"D:YEDEK");
            //deneme.Delete("dvdrentral.backup", @"D:"); 
            deneme.FileZip(@"D:\YEDEK", "dvdrentral");
            ViewBag.Durum = "Backup sıkıstırıldı.";
            return View("Index");
        }     
        public ActionResult PostgreSQLRestore()
        {
            PostgreSQLRestore deneme = new PostgreSQLRestore();
            deneme.PgRestore("localhost", "5432", "dvdrentral", "portgres", "14531453", @"D:Yedek\dvdrentral.backup");
            ViewBag.Durum = "PostgreSQLRestore İşlemi Tamamlandı";
            return View("Index");
        }
    }
}