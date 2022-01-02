using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity
{
    public class ETutorials
    {

        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? LastModifiedDateTime { get; set; }

        public int? LastModifiedBy { get; set; }

    }

    public class ETutorialDetails
    {

        public int Id { get; set; }
        public string FileName { get; set; }
        public int TutorialID { get; set; }
        public string FileURL { get; set; }
    }
}
