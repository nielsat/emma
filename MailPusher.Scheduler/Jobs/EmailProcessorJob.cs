using HtmlAgilityPack;
using MailPusher.Common.Helpers;
using MailPusher.Repository.Repositories;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MailPusher.Scheduler.Jobs
{
    public class EmailProcessorJob : IJob
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                logger.Debug("email message processing");
                string emailBaseFolder = AppSettingsHelper.GetValueFromAppSettings(Common.Enums.AppSettingsKey.hmailServerEmailBaseFolder);
                List<string> emailFiles = Directory.GetFiles(emailBaseFolder, "*.eml", SearchOption.AllDirectories).ToList();
                ImageCopySettings settings = new ImageCopySettings();
                EmailRepo repo = new EmailRepo();
                PublisherRepo publisherRepo = new PublisherRepo();
                foreach (var email in emailFiles)
                {
                    try
                    {
                        CDO.Message message = ReadMessage(email);
                        
                        int publisherId = GetPublisherIdFromEmail(message);
                        string emailHTML = ChangeImgSrcToLocal(message.HTMLBody, settings, publisherId);
                        repo.AddEmail(new Common.Models.Email()
                        {
                            EmailHeaders = GetEmailHeaders(message),
                            HTML = emailHTML,
                            HTMLText = emailHTML,
                            ReceivedGMT = message.ReceivedTime.ToUniversalTime(),
                            SenderAddress = message.From,
                            SenderName = message.Sender,
                            SubjectLine = message.Subject,
                            Text = message.TextBody,
                            PublisherID = publisherId,
                            Copy = message.CC
                        });
                        File.Delete(email);
                        publisherRepo.UpdateLastReceivedEmailDate(publisherId);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            
        }

        public List<Common.Models.EmailHeader> GetEmailHeaders(CDO.Message message) {
            var result = new List<Common.Models.EmailHeader>();
            var fields = message.Fields;
            foreach (dynamic item in message.Fields)
            {
                result.Add(new Common.Models.EmailHeader() {
                    HeaderName = item.Name==null?"":item.Name,
                    HeaderValue = item.Value==null?"":item.Value.ToString()
                });
            }
            return result;
        }

        protected int GetPublisherIdFromEmail(CDO.Message message)
        {
            string errorMsg = string.Empty;
            int result = GetPublisherIdFromEmailString(message.To, out errorMsg);
            if (result == 0)
            {
                result = GetPublisherIdFromLogs(message);
            }

            if (result == 0)
            {
                throw new Exception(errorMsg);
            }

            return result;
        }

        protected int GetPublisherIdFromEmailString(string email, out string errorMsg)
        {
            errorMsg = string.Empty;

            Regex regex = new Regex(@"\+\d+\@");
            MatchCollection matches = regex.Matches(email);
            int result = 0;
            if (matches.Count >= 0)
            {
                string strResult = matches[matches.Count-1].Value.Substring(1, matches[matches.Count-1].Value.Length - 2);
                if (!int.TryParse(strResult, out result))
                {
                    errorMsg = string.Format("Wrong PublisherId - {0} . Expected int value", strResult);
                }
            }
            else
            {
                errorMsg = "Wrong email To format. Expected - '[from]+[publisherId]@[domain]', received - " + email;
            }
            
            return result;
        }

        protected int GetPublisherIdFromLogs(CDO.Message message)
        {
            var emailHeaders = GetEmailHeaders(message);
            var receivedHeader = emailHeaders.Find(x => x.HeaderName == "urn:schemas:mailheader:received");
            Regex regex = new Regex(@"\[\d+\.\d+\.\d+\.\d+\]");
            MatchCollection matches = regex.Matches(receivedHeader.HeaderValue);

            if (matches.Count != 1)
            {
                return 0;
            }

            string dateString = string.Format("{0:yyy-MM-dd}", message.ReceivedTime);
            string receivedFropIp = matches[0].Value.Substring(1, matches[0].Value.Length - 2); 
            string plusAddressingTemplate = string.Format("\"{0}\"	\"RECEIVED: RCPT TO:<", receivedFropIp);
            string logFilePath = AppSettingsHelper.GetValueFromAppSettings(Common.Enums.AppSettingsKey.hMailServerLogsFolder);
            string lofFileName = string.Format("hmailserver_{0}.log", dateString);
            
            int result = 0;
            using (StreamReader file = new StreamReader(Path.Combine(logFilePath, lofFileName)))
            {
                string line = string.Empty;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.IndexOf(plusAddressingTemplate) != -1)
                    {
                        int lineDateStartIndex = line.IndexOf(dateString);
                        if (lineDateStartIndex == -1) { continue; }

                        string dateTimeString = line.Substring(lineDateStartIndex, line.Length - lineDateStartIndex);
                        dateTimeString = dateTimeString.Substring(0, dateTimeString.IndexOf('"'));
                        DateTime fileReceivedTime = Convert.ToDateTime(dateTimeString);

                        long diff = message.ReceivedTime.Ticks - fileReceivedTime.Ticks;
                        diff = diff < 0 ? -diff : diff;

                        if (diff < (10000000 * 5))
                        {
                            string errorMsg = string.Empty;
                            result = GetPublisherIdFromEmailString(line, out errorMsg);
                        }
                    }
                }
            }
            return result;
        }

        protected CDO.Message ReadMessage(String emlFileName)
        {
            CDO.Message msg = new CDO.MessageClass();
            ADODB.Stream stream = new ADODB.StreamClass();
            stream.Open(Type.Missing,
                           ADODB.ConnectModeEnum.adModeUnknown,
                           ADODB.StreamOpenOptionsEnum.adOpenStreamUnspecified,
                           String.Empty,
                           String.Empty);
            stream.LoadFromFile(emlFileName);
            stream.Flush();
            msg.DataSource.OpenObject(stream, "_Stream");
            msg.DataSource.Save();
            return msg;
        }

        protected string ChangeImgSrcToLocal(string html, ImageCopySettings settings, int publisherId) {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            foreach (HtmlNode img in doc.DocumentNode.Descendants("img"))
            {
                try
                {
                    string newFileName = GenerateNewImageName();
                    DownloadRemoteImageFile(img.Attributes["src"].Value,
                        GenerateFilePath(newFileName, publisherId, settings));
                    img.Attributes["src"].Value = GenerateUrl(newFileName, publisherId, settings);
                }
                catch (Exception ex)
                {
                    logger.Error("Cannot change img src to local", ex);
                }
            }

            string result = string.Empty;
            using (StringWriter writer = new StringWriter())
            {
                doc.Save(writer);
                result = writer.ToString();
            }
            return result;
        }

        protected string GenerateFilePath(string fileName, int publisherId, ImageCopySettings settings)
        {
            return Path.Combine(settings.EmailStorageBaseAddress, publisherId.ToString(), fileName);
        }

        protected string GenerateNewImageName()
        {
            return string.Format("{0}.jpg", Guid.NewGuid());
        }

        protected string GenerateUrl(string fileName, int publisherId, ImageCopySettings settings)
        {
            return Path.Combine(settings.EmailStorageBaseWebAddress,publisherId.ToString(), fileName).Replace("\\", "/");
        }

        protected void DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Check that the remote file was found. The ContentType
            // check is performed since a request for a non-existent
            // image file might be redirected to a 404-page, which would
            // yield the StatusCode "OK", even though the image was not
            // found.
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {
                CreateDirectoryIfNotExists(fileName);
                // if the remote file was found, download oit
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }
            }
        }

        protected bool CreateDirectoryIfNotExists(string fileName) {
            string directoryPath = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return true;
        }
    }

    public class ImageCopySettings {
        public string EmailStorageBaseAddress { get; set; }
        public string EmailStorageBaseWebAddress { get; set; }
        public ImageCopySettings() {
            EmailStorageBaseAddress = AppSettingsHelper.GetValueFromAppSettings(Common.Enums.AppSettingsKey.emailStorageBaseAddress);
            EmailStorageBaseWebAddress = AppSettingsHelper.GetValueFromAppSettings(Common.Enums.AppSettingsKey.emailStorageBaseWebAddress);
        }
    }

    public class CDOFieldItem {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
