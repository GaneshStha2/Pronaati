using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Package;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.Package
{
    public class SNaatiPackage
    {
        RiddhaDBContext db = null;
        public SNaatiPackage()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<ENaatiPackage> Add(ENaatiPackage model)
        {
            db.NaatiPackage.Add(model);
            db.SaveChanges();
            return new ServiceResult<ENaatiPackage>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<ENaatiPackage>> List()
        {
            return new ServiceResult<IQueryable<ENaatiPackage>>()
            {
                Data = db.NaatiPackage,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(ENaatiPackage model)
        {
            db.NaatiPackage.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Deleted Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<ENaatiPackage> Update(ENaatiPackage model)
        {
            db.Entry<ENaatiPackage>(model).State = EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<ENaatiPackage>()
            {
                Data = model,
                Message = "Updated Successfully",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<List<EPackageFile>> ListPackageFile()
        {

            return new ServiceResult<List<EPackageFile>>()
            {
                Data = db.PackageFile.ToList(),
                Message = "",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<List<EPackageFile>> AddPackageFileList(List<EPackageFile> list)
        {
            db.PackageFile.AddRange(list);
            db.SaveChanges();

            return new ServiceResult<List<EPackageFile>>()
            {
                Data = list,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> RemovePackageFileList(List<EPackageFile> list)
        {
            db.PackageFile.RemoveRange(list);
            db.SaveChanges();

            return new ServiceResult<int>()
            {
                Data = 1,
                Status = ResultStatus.Ok
            };
        }



        public ServiceResult<List<EPackageMockTest>> ListPackageMockTest()
        {

            return new ServiceResult<List<EPackageMockTest>>()
            {
                Data = db.PackageMockTest.ToList(),
                Message = "",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<List<EPackageMockTest>> AddPackageMockTestList(List<EPackageMockTest> list)
        {
            db.PackageMockTest.AddRange(list);
            db.SaveChanges();

            return new ServiceResult<List<EPackageMockTest>>()
            {
                Data = list,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> RemovePackageMockTestList(List<EPackageMockTest> list)
        {
            db.PackageMockTest.RemoveRange(list);
            db.SaveChanges();

            return new ServiceResult<int>()
            {
                Data = 1,
                Status = ResultStatus.Ok
            };
        }
    }
}
