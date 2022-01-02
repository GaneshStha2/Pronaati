using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Course
{
    public class EOnlineTrainingDetails
    {
        public int Id { get; set; }
        public int EOnlineTrainingId { get; set; }
        public string FileName { get; set; }
        public string FilerUrl { get; set; }
        public int FileId { get; set; }
        
        public FileType FileType { get; set; }

        public virtual EOnlineTraining EOnlineTraining { get; set; }
    }
}
