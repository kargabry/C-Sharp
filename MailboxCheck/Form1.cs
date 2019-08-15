using System;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.IO;
using S22.Imap;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;

namespace ZadaniePraktyczne
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckIniFile() == true)
            {
                string startupPath = Environment.CurrentDirectory;
                startupPath = startupPath.Replace("ZadaniePraktyczne\\ZadaniePraktyczne\\bin\\Debug", "");
                string configPath = startupPath + "config.ini";

                var MyIni = new IniFile(configPath);
                string EncryptionKey = GenerateEncryptionKey();
                CheckMailbox(Encrypt(MyIni.IniReadValue("Skrzynka", "PASS"), EncryptionKey), EncryptionKey);
                InitTimer();
            }
        }

        public string[] Contacts()
        {
            Excel.Application xlapp;
            Excel.Workbook xlWorkbook;
            Excel.Worksheet xlWorksheet;
            string startupPath = Environment.CurrentDirectory;
            startupPath = startupPath.Replace("ZadaniePraktyczne\\ZadaniePraktyczne\\bin\\Debug", "");
            string contactsPath = startupPath + "Kontakty.xlsx"; 

            xlapp = new Excel.Application();
            xlWorkbook = xlapp.Workbooks.Open(contactsPath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);

            Range Column = xlWorksheet.UsedRange.Columns[3];
            System.Array mails = (System.Array)Column.Cells.Value;
            string[] mailsArray = mails.OfType<object>().Select(o => o.ToString()).ToArray();

            foreach (var email in mailsArray)
            {
                if (ISValidEmail(email) == false)
                {
                    mailsArray = mailsArray.Where(w => w != email).ToArray();
                }
            }

            return mailsArray;
        }

        bool ISValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void CheckMailbox(string encPass, string EncryptionKey)
        {
            string startupPath = Environment.CurrentDirectory;
            startupPath = startupPath.Replace("ZadaniePraktyczne\\ZadaniePraktyczne\\bin\\Debug", "");
            string configPath = startupPath + "config.ini";

            var MyIni = new IniFile(configPath);
            var login = MyIni.IniReadValue("Skrzynka", "LOGIN");
            var imapServer = MyIni.IniReadValue("Skrzynka","IMAP_SERVER");
            var imapPort = MyIni.IniReadValue("Skrzynka", "IMAP_PORT");
            var notListed = MyIni.IniReadValue("Szablony", "NOTLISTED");
            var notAllowed = MyIni.IniReadValue("Szablony", "NOTALLOWED");

            int port = Int32.Parse(imapPort);

            var contacts = Contacts();

            using (ImapClient imap = new ImapClient(imapServer, port, login, Decrypt(encPass, EncryptionKey), AuthMethod.Login, true))
            {
                IEnumerable<uint> uids = imap.Search(SearchCondition.Unseen());
                IEnumerable<MailMessage> messages = imap.GetMessages(uids);
                richTextBox1.AppendText(Environment.NewLine + DateTime.Now + " - Connected to the mailbox.");


                if (uids.Count() > 0)
                {
                    foreach (var message in messages)
                    {
                        string from = message.From.Address.ToString().Trim();
                        string subject = message.Subject.ToString().Trim();
                        string body = message.Body.ToString().Trim();
                        string date = message.Date().ToString().Trim();
                        string fileName = from + " - " + subject + ".txt";
                        string path = startupPath + fileName;

                        richTextBox1.AppendText(Environment.NewLine + DateTime.Now + " - New email from " + from);

                        if (contacts.Contains(from))
                        {

                            AttachmentCollection attachColl = message.Attachments;
                            if (attachColl.Count > 0)
                            {
                                foreach (var item in attachColl)
                                {
                                    if (item.Name.Contains(".zip") || item.Name.Contains(".pdf") || item.Name.Contains(".docx") || item.Name.Contains(".xades"))
                                    {
                                        CopyStream(item.ContentStream, (startupPath + item.Name));
                                        richTextBox1.AppendText(Environment.NewLine + DateTime.Now + " - Attachment '" + item.Name + "' saved.");
                                    }
                                    else
                                    {
                                        MailMessage reply = new MailMessage(login, from);
                                        if (!subject.StartsWith("Re:", StringComparison.OrdinalIgnoreCase))
                                        {
                                            reply.Subject = "Re: ";
                                        }
                                        reply.Subject += subject;

                                        StringBuilder replyBody = new StringBuilder();
                                        if (message.IsBodyHtml)
                                        {
                                            replyBody.Append("<p>" + notAllowed + "</p>");
                                        }
                                        else
                                        {
                                            replyBody.AppendLine(notAllowed);
                                        }

                                        reply.Body = replyBody.ToString();
                                        reply.IsBodyHtml = message.IsBodyHtml;

                                        SendReply(reply, encPass, EncryptionKey);
                                        richTextBox1.AppendText(Environment.NewLine + "---");

                                        return;
                                    }
                                }
                            }

                            using (StreamWriter sw = File.CreateText(path))
                            {
                                sw.WriteLine("From: " + from);
                                sw.WriteLine("Date: " + date);
                                sw.WriteLine("Subject: " + subject);
                                sw.WriteLine("Body:");
                                sw.WriteLine(body);
                            }
                            richTextBox1.AppendText(Environment.NewLine + DateTime.Now + " - Email saved.");
                            richTextBox1.AppendText(Environment.NewLine + "---");
                        }
                        else
                        {
                            MailMessage reply = new MailMessage(login, from);
                            if (!subject.StartsWith("Re:", StringComparison.OrdinalIgnoreCase))
                            {
                                reply.Subject = "Re: ";
                            }
                            reply.Subject += subject;

                            StringBuilder replyBody = new StringBuilder();
                            if (message.IsBodyHtml)
                            {
                                replyBody.Append("<p>" + notListed + "</p>");
                            }
                            else
                            {
                                replyBody.AppendLine(notListed);
                            }

                            reply.Body = replyBody.ToString();
                            reply.IsBodyHtml = message.IsBodyHtml;

                            SendReply(reply, encPass, EncryptionKey);
                            richTextBox1.AppendText(Environment.NewLine + "---");
                        }
                    }
                }
                else
                {
                    richTextBox1.AppendText(Environment.NewLine + DateTime.Now + " - No new messages.");
                    richTextBox1.AppendText(Environment.NewLine + "---");
                }
            }
        }

        private void SendReply(MailMessage reply, string encPass, string EncryptionKey)
        {
            string startupPath = Environment.CurrentDirectory;
            startupPath = startupPath.Replace("ZadaniePraktyczne\\ZadaniePraktyczne\\bin\\Debug", "");
            string configPath = startupPath + "config.ini";

            var MyIni = new IniFile(configPath);
            var smtpServer = MyIni.IniReadValue("Skrzynka", "SMTP_SERVER");
            var smtpPort = MyIni.IniReadValue("Skrzynka", "SMTP_PORT");
            int port = Int32.Parse(smtpPort);
            var login = MyIni.IniReadValue("Skrzynka", "LOGIN");

            var smtp = new SmtpClient
            {
                Host = smtpServer,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(login, Decrypt(encPass, EncryptionKey))
            };
            smtp.Send(reply);

            richTextBox1.AppendText(Environment.NewLine + DateTime.Now + " - Reply sent to: " + reply.To.ToString());
        }

        public static void CopyStream(Stream stream, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }

        bool CheckIniFile()
        {
            try
            {
                string startupPath = Environment.CurrentDirectory;
                startupPath = startupPath.Replace("ZadaniePraktyczne\\ZadaniePraktyczne\\bin\\Debug", "");
                string configPath = startupPath + "config.ini";

                string[] ini = new string[30];
                var MyIni = new IniFile(configPath);
                ini[0] = MyIni.IniReadValue("Skrzynka", "SMTP_SERVER");
                ini[1] = MyIni.IniReadValue("Skrzynka", "SMTP_PORT");
                ini[2] = MyIni.IniReadValue("Skrzynka", "LOGIN");
                ini[3] = MyIni.IniReadValue("Skrzynka", "PASS");
                ini[4] = MyIni.IniReadValue("Skrzynka", "IMAP_SERVER");
                ini[5] = MyIni.IniReadValue("Skrzynka", "IMAP_PORT");
                ini[6] = MyIni.IniReadValue("Szablony", "NOTLISTED");
                ini[7] = MyIni.IniReadValue("Szablony", "NOTALLOWED");
                ini[8] = MyIni.IniReadValue("Skrzynka", "SMTP_SERVER");
                ini[9] = MyIni.IniReadValue("Skrzynka", "SMTP_PORT");

                int missingIni = 0;
                for (int i = 0; i <= 9; i++)
                {
                    if (ini[i] == "")
                    {
                        richTextBox1.AppendText(Environment.NewLine + "Missing element in config file.");
                        missingIni++;
                    }
                }
                if (missingIni == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                richTextBox1.AppendText(Environment.NewLine + "Missing element in config file.");
                return false;
            }


        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 60000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string startupPath = Environment.CurrentDirectory;
            startupPath = startupPath.Replace("ZadaniePraktyczne\\ZadaniePraktyczne\\bin\\Debug", "");
            string configPath = startupPath + "config.ini";

            var MyIni = new IniFile(configPath);
            string EncryptionKey = GenerateEncryptionKey();
            CheckMailbox(Encrypt(MyIni.IniReadValue("Skrzynka", "PASS"), EncryptionKey), EncryptionKey);
        }

        public string GenerateEncryptionKey()
        {
            string EncryptionKey = string.Empty;

            Random Robj = new Random();
            int Rnumber = Robj.Next();
            EncryptionKey = "EncPass" + Convert.ToString(Rnumber);

            return EncryptionKey;
        }

        public string Encrypt(string clearText, string EncryptionKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText, string EncryptionKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}

