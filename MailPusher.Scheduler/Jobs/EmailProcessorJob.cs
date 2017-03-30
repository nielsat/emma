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

                        int publisherId = GetPublisherIdFromEmail(message.To);
                        string emailHTML = ChangeImgSrcToLocal(message.HTMLBody, settings, publisherId);
                        repo.AddEmail(new Common.Models.Email()
                        {
                            EmailHeaders = GetEmailHeaders(message),
                            HTML = emailHTML,
                            HTMLText = emailHTML,
                            ReceivedGMT = message.ReceivedTime,
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

        protected int GetPublisherIdFromEmail(string to)
        {
            string toName = to.Split('@')[0];
            string[] toNameParts = toName.Split('+');
            if (toNameParts.Length != 2)
            {
                throw new Exception("Wrong email To format. Expected - '[from]+[publisherId]@[domain]'");
            }
            int result = 0;
            if (!int.TryParse(toNameParts[1], out result))
            {
                throw new Exception(string.Format("Wrong PublisherId - {0} . Expected int value", toNameParts[0]));
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
                string newFileName = GenerateNewImageName();
                DownloadRemoteImageFile(img.Attributes["src"].Value, 
                    GenerateFilePath(newFileName,publisherId,settings));
                img.Attributes["src"].Value = GenerateUrl(newFileName, publisherId, settings);
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
