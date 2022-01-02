using Riddhasoft.Setup.Entity.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class StudyMateriallViewModel
    {
    }


    public class VideoMaterialViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string link { get; set; }
    }

    public class IncludedVideosViewModel
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public List<VideoMaterialViewModel> Videos { get; set; }
    }
    public class AudioMaterialViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string link { get; set; }
    }

    public class IncludedAudioViewModel
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public List<AudioMaterialViewModel> AudioMaterials{ get; set; }
    }

    public class PdfMaterialViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string link { get; set; }
    }

    public class IncludedPdfsVm
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public List<PdfMaterialViewModel> Pdfs { get; set; }
    }

    public class IncludedTestViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public PackageTestType PackageTestType { get; set; }
        public bool IsTaken { get; set; }
    }

    public class IncludedTestListViewModel
    {

        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public List<IncludedTestViewModel> IncludedTests { get; set; }
    }
}