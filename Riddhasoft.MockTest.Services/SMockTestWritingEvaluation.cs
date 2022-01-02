using Riddhasoft.DB;
using Riddhasoft.MockTest.Entity;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.MockTest.Services
{
    public class SMockTestWritingEvaluation : IBaseService<EMockTestWritingEvaluation>
    {
        RiddhaDBContext db = null;
        public SMockTestWritingEvaluation()
        {
            db = new RiddhaDBContext();
        }
        public ServiceResult<EMockTestWritingEvaluation> Add(EMockTestWritingEvaluation model)
        {
            db.MockTestWritingEvaluation.Add(model);
            db.SaveChanges();
            return new ServiceResult<EMockTestWritingEvaluation>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<List<EMockTestWritingEvaluation>> AddRange(List<EMockTestWritingEvaluation> model)
        {
            db.MockTestWritingEvaluation.AddRange(model);
            db.SaveChanges();
            return new ServiceResult<List<EMockTestWritingEvaluation>>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<IQueryable<EMockTestWritingEvaluation>> List()
        {
            return new ServiceResult<IQueryable<EMockTestWritingEvaluation>>()
            {
                Data = db.MockTestWritingEvaluation,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Remove(EMockTestWritingEvaluation model)
        {
            db.MockTestWritingEvaluation.Remove(model);
            db.SaveChanges();
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "Removed Successfully !",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<EMockTestWritingEvaluation> Update(EMockTestWritingEvaluation model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new ServiceResult<EMockTestWritingEvaluation>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public string getQuestionText(int questionId, QuestionType questionType)
        {
            if (questionType == QuestionType.WritingTypeOne)
            {
                var data = db.WritingTypeOne.Where(x => x.Id == questionId).FirstOrDefault();
                if (data != null)
                {
                    string questionText = data.Question;
                    return questionText;
                }
            }
            else if (questionType == QuestionType.WritingTypeTwo)
            {
                var data = db.WritingTypeTwo.Where(x => x.Id == questionId).FirstOrDefault();
                if (data != null)
                {
                    string questionText = data.Question;
                    return questionText;
                }
            }
            return "";
        }

        public void saveWritingObtainedMarks(int mockTestReportId)
        {
            if (mockTestReportId != 0)
            {
                var studentMockTestData = db.MockTestReport.Where(x => x.Id == mockTestReportId).FirstOrDefault();
                int speakingMarks = studentMockTestData.SpeakingMarks;
                int readingMarks = studentMockTestData.ReadingMarks;
                int listeningMarks = studentMockTestData.ListeningMarks;

                var writingData = db.MockTestWritingEvaluation.Where(x => x.MockTestReportId == mockTestReportId).ToList();
                int totalQuestions = writingData.Count();
                int totalMarks = writingData.Sum(x => x.ObtainedMarks);
                int obtainedTotalWritingMarks = totalMarks / totalQuestions;

                int totalOverallMarks = (speakingMarks + readingMarks + listeningMarks + obtainedTotalWritingMarks) / 4;

                //Updating data into MockTestReport Table
                studentMockTestData.WritingMarks = obtainedTotalWritingMarks;
                studentMockTestData.OverallScore = totalOverallMarks;
                studentMockTestData.IsReportReadyToView = true;
                studentMockTestData.ReportIssueDate = DateTime.Now.Date;
                db.Entry(studentMockTestData).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                //deleting data from MockTestWritingEvaluation table
                db.MockTestWritingEvaluation.RemoveRange(writingData);
                db.SaveChanges();

                //return true;

            }
            //return false;
        }

        public bool isWritingMarkingFinished(int mockTestReportId)
        {
            if (mockTestReportId != 0)
            {
                var data = db.MockTestWritingEvaluation.Where(x => x.MockTestReportId == mockTestReportId && x.ObtainedMarks == 0).FirstOrDefault();
                if (data == null)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
