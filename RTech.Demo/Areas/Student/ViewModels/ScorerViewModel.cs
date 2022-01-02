using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class ScorerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public decimal Score { get; set; }
    }

    public class HomeViewModel
    {
        public List<ScorerViewModel> Scorers { get; set; }
        public List<BlogViewModel> Blogs { get; set; }
        public List<NaatiMocktestPackageVm> Packages { get; set; }

        public List<TestimonialViewModel> Testimonials { get; set; }
    }
}