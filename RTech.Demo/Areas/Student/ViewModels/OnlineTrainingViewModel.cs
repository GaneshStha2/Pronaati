using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class OnlineTrainingMasterViewModel
    {
        public int OnlineTrainigId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CourseCode { get; set; }
        public string ImageUrl { get; set; }
        
        public List<OnlineTrainingDetailsViewModel> onlineTrainingFileURLList { get; set; }
    }

    public class OnlineTrainingDetailsViewModel
    {
        public int OnlineTrainingDetailsId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileExtension { get; set; }

    }


    //public class OnlineTrainingListVm {

    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public string DateTime { get; set; }

    //}
    
}