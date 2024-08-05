using BL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using reservation_app_server.Models;
using System.Dynamic;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Xml.Linq;

namespace BL.Services
{


    public class ActivityDayListService : IActivityDayListService
    {
        private IActivityDayListRepository _activityDayListRepository;
        private IHelperService<ActivityDayListItem> _helperService;
        private string EntityName = "ActivityDay";
        public ActivityDayListService(IActivityDayListRepository activityDayListRepository, IHelperService<ActivityDayListItem> helperService)
        {
            _activityDayListRepository = activityDayListRepository;
            _helperService = helperService;
        }
        public ResultItem<ActivityDayListItem> GetActivityDayList(FilterProperties filterProperties, bool flag)
        {
            List<ActivityDayListItem> activityDayListItems = _activityDayListRepository.GetActivityDayList();
            activityDayListItems = _helperService.CasesHelper(activityDayListItems, filterProperties, EntityName);

            if (filterProperties.FilterList.Count() > 0 && filterProperties.FilterList.Last()?.Type == "Any")
            {
                activityDayListItems = _helperService.FilterAnyFieldActivityDay(activityDayListItems, filterProperties.FilterList.Last());
            }
            ResultItem<ActivityDayListItem> result = new();
            result.DataList = (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + filterProperties.NumberOfRowsInPage <= activityDayListItems.Count ? activityDayListItems.GetRange((filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage, (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + filterProperties.NumberOfRowsInPage - (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage) :
                (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + 1 <= activityDayListItems.Count ? activityDayListItems.GetRange((filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage, activityDayListItems.Count - (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage) :
                activityDayListItems;
            result.NumberOfRows = activityDayListItems.Count;
            return result;
        }

        public async void SendEmail(MailInfo item)
        {
            string fromAddress = "100mailmanagement@gmail.com";
            string toAddress = item.To;
            string subject = item.Subject;
            string body = item.body;
            string attachmentPath = item.FilePath; 



            using (MailMessage mail = new MailMessage(fromAddress, toAddress))
            {
                mail.Subject = subject;
                mail.Body = body;



                if (item.Cc != null && item.Cc.Count > 0)
                {
                    foreach (var cc in item.Cc)
                    {
                        mail.CC.Add(cc.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(attachmentPath))
                {
                    try
                    {
                        byte[] fileBytes = Convert.FromBase64String(attachmentPath);

                        string fileExtension = GetFileExtensionFromBase64(attachmentPath);
                        if (string.IsNullOrEmpty(fileExtension))
                        {
                            throw new InvalidOperationException("Unsupported file type");
                        }

                        string tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{fileExtension}");
                        await File.WriteAllBytesAsync(tempPath, fileBytes);
                        Attachment attachment = new Attachment(tempPath, MediaTypeNames.Application.Octet)
                        {
                            Name = item.FileName 
                        };

                        mail.Attachments.Add(attachment);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error decoding or attaching file: {ex.Message}");
                    }
                }





                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("100mailmanagement@gmail.com", "ysfd pnlq aoxe lrsr");
                    client.EnableSsl = true;

                    client.Send(mail);
                }
            }
        }


        private string GetFileExtensionFromBase64(string base64String)
        {
            var data = base64String.Substring(0, 5);
            switch (data.ToUpper())
            {
                case "JVBER": return ".pdf";
                case "/9J/4": return ".jpg";
                case "iVBOR": return ".png";

                default: return string.Empty;
            }
        }
    }


}

