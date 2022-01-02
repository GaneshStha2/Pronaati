using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.EmailTemplete.ViewModels
{
    public class EmailTemplateViewModel
    {
        public int Id { get; set; }
        public string EmailSubject { get; set; }
        public string ReceivingEmailAddress { get; set; }
        public string Title { get; set; }
        public string GenderDesignation { get; set; }
        public string ReceiverName { get; set; }
        public string SenderPhone { get; set; }
        public string SenderEmail { get; set; }

        [AllowHtml]
        public string MailBody { get; set; }


        public string link { get; set; }
    }
}