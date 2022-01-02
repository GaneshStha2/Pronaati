using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.TutorialSetup
{
    public class TutorialMasterVm
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? LastModifiedDateTime { get; set; }

        public int? LastModifiedBy { get; set; }
    }


    public class TutorialDetailVm {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int TutorialID { get; set; }
        public string FileURL { get; set; }
    }
}