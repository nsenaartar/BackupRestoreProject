using NLog;
using ScheduledTasksLibrary.BackupAndRestore;
using System;
using System.Net;
using System.Net.Mail;

namespace ScheduledTasks.BackupAndRestore
{
    public class SendMail
    {
        //OrnekKullanım: SendMailBackupInformation("info@mekansalbulut.com", "MekansalBulut61","info@mekansalbulut.com", "mail.mekansalbulut.com" );
        public bool SendMailBackupInformation(string senderAddress, string senderAddressPassword, string sentAddress, string hostAddress, string subject, string body)
        {
            NlogDefinition.logger.Info("Mail atma işlemi başlatıldı. ");
            var fromAddress = new MailAddress(senderAddress);
            var toAddress = new MailAddress(sentAddress);
            using (var smtp = new SmtpClient
            {
                Host = hostAddress,
                Port = 587,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, senderAddressPassword)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    try
                    {
                        smtp.Send(message);
                        NlogDefinition.logger.Info("Mail atma işlemi tamamlandı. ");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        NlogDefinition.logger.Error("Hata! :" + ex);
                        return false;
                    }
                }
            }
        }
    }
}


