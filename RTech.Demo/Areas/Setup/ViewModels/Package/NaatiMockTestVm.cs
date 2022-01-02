using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Package
{
    public class NaatiMockTestVm
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int LanguageTypeId { get; set; }
        public string LanguageTypeName { get; set; }
    }


    public class NaatiMockTestDetailVm
    {
        public int Id { get; set; }
        public int NaatiMockTestMasterId { get; set; }
        public int QuestionSetId { get; set; }
        public string Name{ get; set; }
    }


    public class NaatiMockTestSetupVm
    {
        public NaatiMockTestVm NaatiMockTestVm { get; set; }
        public List<NaatiMockTestDetailVm> NaatiMockTestDetailVm { get; set; }
    }
    public class NaatiMocktestKendoGridVm
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string LanguageType { get; set; }
    }
}