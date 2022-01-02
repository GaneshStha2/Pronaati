using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionPackage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.QuestionPackage
{
    public class SNaatiMockTest
    {
        RiddhaDBContext db = null;
        public SNaatiMockTest()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<ENaatiMockTestMaster> Add(ENaatiMockTestMaster model)
        {

            db.NaatiMockTestMaster.Add(model);
            db.SaveChanges();
            return new ServiceResult<ENaatiMockTestMaster>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<ENaatiMockTestMaster>> List()
        {
            return new ServiceResult<IQueryable<ENaatiMockTestMaster>>()
            {
                Data = db.NaatiMockTestMaster,
                Message = "",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<int> Remove(ENaatiMockTestMaster model)
        {
            db.NaatiMockTestMaster.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<ENaatiMockTestDetail>> ListDetails()
        {

            return new ServiceResult<List<ENaatiMockTestDetail>>()
            {
                Data = db.NaatiMockTestDetail.ToList(),
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<ENaatiMockTestMaster> Update(ENaatiMockTestMaster model)
        {
            db.Entry<ENaatiMockTestMaster>(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ENaatiMockTestMaster>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }
        public ServiceResult<List<ENaatiMockTestDetail>> AddDetails(List<ENaatiMockTestDetail> detailList)
        {
            db.NaatiMockTestDetail.AddRange(detailList);
            db.SaveChanges();
            return new ServiceResult<List<ENaatiMockTestDetail>>()
            {
                Data = detailList,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<int> RemoveDetailss(List<ENaatiMockTestDetail> detailList)
        {
            db.NaatiMockTestDetail.RemoveRange(detailList);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };
        }
    }
}
