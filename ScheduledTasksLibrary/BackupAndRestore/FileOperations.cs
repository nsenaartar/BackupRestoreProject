using NLog;
using System;
using System.IO;

namespace ScheduledTasksLibrary.BackupAndRestore
{
    public class FileOperations
    {
        public bool FolderZip(string path)
        {
            NlogDefinition.logger.Info("Klasor sıkıstırma işlemi başladı");
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                try
                {
                    zip.AddDirectory(path);
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
                    zip.Save(string.Format("{0}{1}.zip", di.Parent.FullName, di.Name));
                    NlogDefinition.logger.Info("Klasor sıkıstırma işlemi tamamlandı.");
                    return true;
                }
                catch (Exception ex)
                {
                    NlogDefinition.logger.Error("Hata! : " + ex);
                    return false;
                }
            }
        }
        public bool FileZip(string path,string databaseName)
        {
            NlogDefinition.logger.Info("Dosya sıkıstırma işlemi başladı");
            string path1 = path + @"\" + databaseName +" "+ String.Format("{0:d}", DateTime.Now) + ".backup";
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                FileInfo fi = new FileInfo(path1);
                zip.AddFile(path1);
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path1);
                try
                {
                    zip.Save(string.Format("{0}/{1}.zip", di.Parent.FullName, databaseName+" " + String.Format("{0:d}", DateTime.Now)));
                    NlogDefinition.logger.Info("Dosya sıkıstırma işlemi tamamlandı.");
                    return true;
                }
                catch (Exception ex)
                {
                    NlogDefinition.logger.Error("Hata! : " + ex);
                    return false;
                }
            }
        }
        public bool Copy(string fileName, string fileLocation, string copyLocation)
        {
            NlogDefinition.logger.Info("Dosya kopyalama işlemi başladı");
            if (!File.Exists(fileLocation + "\\" + fileName))
            {
                NlogDefinition.logger.Warn("Kopyalanacak dosya belirtilen konumda bulunamadı.");
            }
            else if (File.Exists(copyLocation + "\\" + fileName))
            {
                NlogDefinition.logger.Warn("Belirtilen klasörde " + fileName + "isimli dosya zaten mevcut");
            }
            try
            {
                File.Copy(fileLocation + "\\" + fileName, copyLocation + "\\" + fileName);
                NlogDefinition.logger.Info("Dosya kopyalama işlemi tamamlandı.");
                return true;
            }
            catch (Exception ex)
            {
                NlogDefinition.logger.Error("Hata! : " + ex);
                return false;
            }
        }
        public bool Move(string fileNameforCopy,string fileRename, string fileLocation, string movingLocation)
        {
            NlogDefinition.logger.Info("Dosya taşıma işlemi başladı");
            if (!File.Exists(fileLocation + "\\" + fileNameforCopy))
            {
                NlogDefinition.logger.Warn("Tasınacak dosya belirtilen konumda bulunamadı.");
            }
            else if (File.Exists(movingLocation + "\\" + fileNameforCopy))
            {
                NlogDefinition.logger.Warn("Belirtilen klasörde " + fileNameforCopy + "isimli dosya zaten mevcut");
            }
            try
            {
                File.Move(fileLocation + "\\" + fileNameforCopy, movingLocation + "\\" + fileRename);
                NlogDefinition.logger.Info("Dosya taşıma işlemi tamamlandı.");
                return true;
            }
            catch (Exception ex)
            {
                NlogDefinition.logger.Error("Hata! : " + ex);
                return false;
            }
        }
        public bool Delete(string fileName, string fileLocation)
        {
            NlogDefinition.logger.Info("Dosya silme işlemi başladı.");
            if (!File.Exists(fileLocation + "\\" + fileName))
            {
                NlogDefinition.logger.Warn("Silinecek dosya belirtilen konumda bulunamadı.");
            }
            try
            {
                File.Delete(fileLocation + "\\" + fileName);
                NlogDefinition.logger.Info("Dosya silme işlemi tamamlandı.");
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
