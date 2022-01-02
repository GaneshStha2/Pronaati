using Riddhasoft.Setup.Entity.Course;
using Riddhasoft.Setup.Entity.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.QuestionPackage
{
    public class ENaatiMockTestMaster
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
        public virtual ELanguageType LanguageType { get; set; }
    }

    public class ENaatiMockTestDetail
    {
        public int Id { get; set; }
        public int NaatiMockTestMasterId { get; set; }

        /// <summary>
        /// Dialogue Id
        /// </summary>
        public int QuestionSetId { get; set; }
        public int SegmentId { get; set; }
    }
}