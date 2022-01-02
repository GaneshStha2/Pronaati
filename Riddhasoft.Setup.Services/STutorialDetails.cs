using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services
{
    public class STutorialDetails : Riddhasoft.Services.Common.IBaseService<ETutorialDetails>
    {
        Riddhasoft.DB.RiddhaDBContext db = null;
        public STutorialDetails()
        {
            db = new DB.RiddhaDBContext();

        }
        public ServiceResult<ETutorialDetails> Add(ETutorialDetails model)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<IQueryable<ETutorialDetails>> List()
        {

            return new ServiceResult<IQueryable<ETutorialDetails>>()
            {
                Data = db.ETutorialDetails,
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(ETutorialDetails model)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<ETutorialDetails> Update(ETutorialDetails model)
        {
            throw new NotImplementedException();
        }


        public ServiceResult<List<ETutorialDetails>> AddTutorialDetails(List<ETutorialDetails> TutorialDetails)
        {

            db.ETutorialDetails.AddRange(TutorialDetails);
            db.SaveChanges();
            return new ServiceResult<List<ETutorialDetails>>()
            {

                Data = TutorialDetails,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> RemoveTutorialDetails(List<ETutorialDetails> TutorialDetails)
        {

            db.ETutorialDetails.RemoveRange(TutorialDetails);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed successfully",
                Status = ResultStatus.Ok
            };

        }
    }
}
