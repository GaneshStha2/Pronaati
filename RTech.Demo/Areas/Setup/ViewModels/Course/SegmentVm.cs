using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Course
{
    public class SegmentGridVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }

        public int FromLanguageId { get; set; }
        public int ToLanguageId { get; set; }
        public string ToLanguageName { get; set; }
        public string FromLanguageName { get; set; }
    }

    public class SegmentSetUpVm
    {
        public int LanguageId { get; set; }
        public List<SegmentGridVm> Segments { get; set; }
    }
}