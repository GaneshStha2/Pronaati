using Riddhasoft.DB;
using Riddhasoft.Setup.Entity.QuestionSet;
using RTech.Demo.Areas.Student.ViewModels;
using RTech.Demo.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.Services
{
    public class ReportServices
    {
        RiddhaDBContext dbContext = null;
        CommonServices _commonServices = null;
        public ReportServices()
        {
            dbContext = new RiddhaDBContext();
            _commonServices = new CommonServices();
        }


        public ReportsViewModel getReportLists()
        {
            var result = new ReportsViewModel();
            result.ReportGridVm = (from c in dbContext.NaatiMockTestTaken.Where(x => x.StudentId == Common.StudentSession.LoggedStudent.StudentId).ToList()
                                   select new ReportGridViewModel()
                                   {
                                       Id = c.Id,
                                       MockTestSetName = getMockTestName(c.MockTestId),
                                       Date = c.TestTakenDate.ToString(),
                                       IsReportReady = c.IsScored,
                                       MockTestId = c.MockTestId
                                   }).ToList();
            return result;
        }
        public MockTestReportViewModel getStudentMockTestReport(int mockTestId, int studentId)
        {
            MockTestReportViewModel vm = new MockTestReportViewModel();


            var mockTest = dbContext.NaatiMockTestMaster.Where(x => x.Id == mockTestId).FirstOrDefault();
            var naatiScoreData = dbContext.NaatiScore.Where(x => x.StudentId == studentId && x.MockTestId == mockTestId).ToList();

            vm = (from c in naatiScoreData
                  join d in dbContext.NaatiScoreDetail.ToList() on c.Id equals d.NaatiScoreId
                  join e in dbContext.StudentInformation.ToList() on c.StudentId equals e.Id
                  select new MockTestReportViewModel()
                  {
                      Id = c.Id,
                      StudentId = c.StudentId,
                      StudentName = _commonServices.getStudentFullNameFromId(c.StudentId),
                      CustomerCode = e.Code,
                      MockTestName = mockTest.Title,
                      StudentFirstName = e.FirstName,
                      StudentLastName = e.LastName,
                      Language = mockTest.LanguageType.Name,
                  }).FirstOrDefault();

            var mockTestDetails = dbContext.NaatiMockTestDetail.Where(x => x.NaatiMockTestMasterId == mockTestId).ToList();

            //to get Scores
            var naatiScoreMasterId = dbContext.NaatiScore.Where(x => x.MockTestId == mockTestId && x.StudentId == studentId).Select(x => x.Id).FirstOrDefault();
            var naatiScoreDetails = dbContext.NaatiScoreDetail.Where(x => x.NaatiScoreId == naatiScoreMasterId).ToList();
            vm.Scores = new List<ReportScoreDetail>();
            foreach (var item in mockTestDetails)
            {
                ReportScoreDetail scoreDetail = new ReportScoreDetail()
                {
                    DialogueName = _commonServices.getNaatiQuestionSetNameFromQuestionSetId(item.QuestionSetId),
                    DialogueScore = naatiScoreDetails.Where(x => x.QuestionSetId == item.QuestionSetId).Select(x => x.QuestionScore).Sum()
                };
                vm.OverallScore += scoreDetail.DialogueScore;
                vm.Scores.Add(scoreDetail);
            }

            //for test date
            vm.TestDate = dbContext.NaatiMockTestTaken.Where(x => x.MockTestId == mockTestId && x.StudentId == studentId).Select(x => x.TestTakenDate).FirstOrDefault().ToString();

            //For Feedback
            vm.FeedBacks = new List<string>();
            string feedBacks = naatiScoreData.Select(x => x.FeedBacks).FirstOrDefault();
            if (!string.IsNullOrEmpty(feedBacks))
            {
                string[] feedbackIdArray = feedBacks.Split(',');
                for (int i = 0; i < feedbackIdArray.Length; i++)
                {
                    int feedBackId = int.Parse(feedbackIdArray[i]);
                    var feedBackDetail = dbContext.FeedBack.Where(x => x.Id == feedBackId).Select(x => x.Feedback).FirstOrDefault();
                    vm.FeedBacks.Add(feedBackDetail);
                }
            }



            return vm;

        }


        public string getDateInString(DateTime date)
        {
            int month = date.Month;
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            string year = date.Year.ToString();
            string day = date.Day.ToString();
            string finalDate = day + " " + monthName + " " + year;
            return finalDate;

        }

        public string getQuestionSetName(int questionSetId)
        {
            string name = dbContext.QuestionSetMaster.Where(x => x.Id == questionSetId).Select(x => x.QuestionSetName).FirstOrDefault();
            return name;
        }


        public string getMockTestName(int mockTestId)
        {
            string name = dbContext.NaatiMockTestMaster.Where(x => x.Id == mockTestId).Select(x => x.Title).FirstOrDefault();
            return name;
        }


        public int getSpeakingMarks()
        {
            bool firstTimeTaker = false;
            var data = dbContext.MockTestReport.Where(x => x.StudentId == Common.StudentSession.LoggedStudent.StudentId).FirstOrDefault();
            if (data == null)
            {
                firstTimeTaker = true;
            }
            int speakingTypeTwoMarks = 0;
            int speakingTypeThreeMarks = 0;
            int speakingTypeFourMarks = 0;
            int speakingTypeFiveMarks = 0;
            int speakingTypeSixMarks = 0;
            int speakingTypeSevenMarks = 0;

            //calculating marks for speaking typeTwo
            int noOfTypeTwoQuestions = 0;
            foreach (var item in Common.MockTestSession.SpeakingTypeTwo)
            {
                noOfTypeTwoQuestions++;
                if (firstTimeTaker == true)
                {
                    speakingTypeTwoMarks += createRandomNumber(60, 65);
                }
                else
                {
                    speakingTypeTwoMarks += createRandomNumber(64, 70);
                }
            }
            if (noOfTypeTwoQuestions != 0)
            {
                speakingTypeTwoMarks = speakingTypeTwoMarks / noOfTypeTwoQuestions;
            }


            //calculating marks for Speaking Type Three
            int noOfTypeThreeQuestions = 0;
            foreach (var item in Common.MockTestSession.SpeakingTypeThree)
            {
                noOfTypeThreeQuestions++;
                if (firstTimeTaker == true)
                {
                    speakingTypeThreeMarks += createRandomNumber(60, 65);
                }
                else
                {
                    speakingTypeThreeMarks += createRandomNumber(64, 70);
                }
            }
            if (noOfTypeThreeQuestions != 0)
            {
                speakingTypeThreeMarks = speakingTypeThreeMarks / noOfTypeThreeQuestions;
            }
            //calculating marks for speaking type four
            int noOfSpeakingTypeFourQuestions = 0;
            foreach (var item in Common.MockTestSession.SpeakingTypeFour)
            {
                noOfSpeakingTypeFourQuestions++;
                if (firstTimeTaker == true)
                {
                    speakingTypeFourMarks += createRandomNumber(60, 65);
                }
                else
                {
                    speakingTypeFourMarks += createRandomNumber(64, 70);
                }
            }
            if (noOfSpeakingTypeFourQuestions != 0)
            {
                speakingTypeFourMarks = speakingTypeFourMarks / noOfSpeakingTypeFourQuestions;
            }


            //calculating marks for speaking Type Five 
            int noOfTypeFiveQuestions = 0;
            foreach (var item in Common.MockTestSession.SpeakingTypeFive)
            {
                noOfTypeFiveQuestions++;
                if (firstTimeTaker == true)
                {
                    speakingTypeFiveMarks += createRandomNumber(60, 65);
                }
                else
                {
                    speakingTypeFiveMarks += createRandomNumber(64, 70);
                }
            }
            if (noOfTypeFiveQuestions != 0)
            {
                speakingTypeFiveMarks = speakingTypeFiveMarks / noOfTypeFiveQuestions;
            }


            //calculating marks for speaking Type Six
            int noOfTypeSixQuestions = 0;
            foreach (var item in Common.MockTestSession.SpeakingTypeSix)
            {
                noOfTypeSixQuestions++;
                if (firstTimeTaker == true)
                {
                    speakingTypeSixMarks += createRandomNumber(60, 65);
                }
                else
                {
                    speakingTypeSixMarks += createRandomNumber(64, 70);
                }
            }
            if (noOfTypeSixQuestions != 0)
            {
                speakingTypeSixMarks = speakingTypeSixMarks / noOfTypeSixQuestions;
            }


            //calculating marks for speaking type seven
            int noOfTypeSevenQuestions = 0;
            foreach (var item in Common.MockTestSession.SpeakingTypeSeven)
            {
                noOfTypeSevenQuestions++;
                if (firstTimeTaker == true)
                {
                    speakingTypeSevenMarks += createRandomNumber(60, 65);
                }
                else
                {
                    speakingTypeSevenMarks += createRandomNumber(64, 70);
                }
            }
            if (noOfTypeSevenQuestions != 0)
            {
                speakingTypeSevenMarks = speakingTypeSevenMarks / noOfTypeSevenQuestions;
            }


            int overallSpeakingMarks = (speakingTypeTwoMarks + speakingTypeThreeMarks + speakingTypeFourMarks + speakingTypeFiveMarks + speakingTypeSixMarks + speakingTypeSevenMarks) / 6;
            return overallSpeakingMarks;
        }

        public int getReadingMarks()
        {
            int typeOneMarks = 0;
            int typeTwoMarks = 0;
            int typeThreeMarks = 0;
            int typeFourMarks = 0;
            int typeFiveMarks = 0;
            int overallMarks = 0;

            //calculating reading Type One marks
            int totalTypeOneMarks = 0;
            int noOfTypeOneQuestions = 0;
            foreach (var item in Common.MockTestSession.ReadingTypeOne)
            {
                noOfTypeOneQuestions++;
                if (item.ISCorrectAnswer == true)
                {
                    totalTypeOneMarks += 90;
                }
            }
            if (noOfTypeOneQuestions != 0)
            {
                typeOneMarks = totalTypeOneMarks / noOfTypeOneQuestions;
            }


            //calculating reading Type Two Marks
            int totalTypeTwoMarks = 0;
            int noOfTypeTwoQuestions = 0;
            foreach (var item in Common.MockTestSession.ReadingTypeTwo)
            {
                noOfTypeTwoQuestions++;
                int totalCorrectAnswers = getNumberOfCorrectAnswers(item.QuestionId, QuestionType.ReadingTypeTwo);
                int matchedTotalAnswers = item.NumberOfCorrectAnswers;
                int obtainedMarks = (matchedTotalAnswers / totalCorrectAnswers) * 90;
                totalTypeTwoMarks += obtainedMarks;
            }
            if (noOfTypeTwoQuestions != 0)
            {
                typeTwoMarks = totalTypeTwoMarks / noOfTypeTwoQuestions;
            }


            //calculating reading Type Three Marks
            int totalTypeThreeMarks = 0;
            int noOfTypeThreeQuestions = 0;
            foreach (var item in Common.MockTestSession.ReadingTypeThree)
            {
                noOfTypeThreeQuestions++;
                int obtainedMarks = (item.NumberOfCorrectAnswers / 5) * 90;
                totalTypeThreeMarks += obtainedMarks;
            }
            if (noOfTypeThreeQuestions != 0)
            {
                typeThreeMarks = totalTypeThreeMarks / noOfTypeThreeQuestions;
            }


            //calculating reading Type Four Marks
            int totalTypeFourMarks = 0;
            int noOfTypeFourQuestions = 0;
            foreach (var item in Common.MockTestSession.ReadingTypeFour)
            {
                noOfTypeFourQuestions++;
                int noOfAnswers = 0;
                var ansData = dbContext.ReadingTypeFour.Where(x => x.Id == item.QuestionId).FirstOrDefault().QuestionText;
                noOfAnswers = ansData.Count(x => x == '{');
                int obtainedMarks = (item.NumberOfCorrectAnswers / noOfAnswers) * 90;
                totalTypeFourMarks += obtainedMarks;
            }
            if (noOfTypeFourQuestions != 0)
            {
                typeFourMarks = totalTypeFourMarks / noOfTypeFourQuestions;
            }

            //calculating reading Type Five Marks
            int noOfTypeFiveQuestions = 0;
            int totalTypeFiveMarks = 0;
            foreach (var item in Common.MockTestSession.ReadingTypeFive)
            {
                noOfTypeFiveQuestions++;
                int noOfOptions = dbContext.ReadingTypeFiveDropdown.Where(x => x.ReadingTypeFiveId == item.QuestionId).ToList().Count();
                int obtainedMarks = (item.NumberOfCorrectAnswers / noOfOptions) * 90;
                totalTypeFiveMarks += obtainedMarks;
            }
            if (noOfTypeFiveQuestions != 0)
            {
                typeFiveMarks = totalTypeFiveMarks / noOfTypeFiveQuestions;
            }


            overallMarks = (typeOneMarks + typeTwoMarks + typeThreeMarks + typeFourMarks + typeFiveMarks) / 5;
            return overallMarks;


        }

        public int getListeningMarks()
        {
            int typeOneMarks = 0;
            int typeTwoMarks = 0;
            int typeThreeMarks = 0;
            int typeFourMarks = 0;
            int typeFiveMarks = 0;
            int typeSixMarks = 0;
            int typeSevenMarks = 0;
            int typeEightMarks = 0;
            int overallMarks = 0;

            //calculating listening type One marks
            int noOfTypeOneQuestions = 0;
            int totalTypeOneMarks = 0;
            foreach (var item in Common.MockTestSession.ListeningTypeOne)
            {
                noOfTypeOneQuestions++;
                int obtainedMarks = 0;
                char[] delimiters = new char[] { ' ', '\r', '\n' };
                int noOfWords = 0;
                if (!string.IsNullOrEmpty(item.AnswerText))
                {
                    noOfWords = item.AnswerText.Length <= 0 ? 0 : item.AnswerText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
                }

                if (noOfWords > 50 && noOfWords < 70)
                {
                    obtainedMarks = createRandomNumber(80, 85);
                }
                else
                {
                    obtainedMarks = createRandomNumber(60, 70);
                }
                totalTypeOneMarks += obtainedMarks;
            }
            if (noOfTypeOneQuestions != 0)
            {
                typeOneMarks = totalTypeOneMarks / noOfTypeOneQuestions;
            }
            //calculating listening Type Two Marks
            int totalTypeTwoMarks = 0;
            int noOfTypeTwoQuestions = 0;
            foreach (var item in Common.MockTestSession.ListeningTypeTwo)
            {
                noOfTypeTwoQuestions++;
                int totalCorrectAnswers = getNumberOfCorrectAnswers(item.QuestionId, QuestionType.ListeningTypeTwo);
                int attemptedCorrectAnswers = item.NumberOfCorrectAnswers;
                int marksObtained = (attemptedCorrectAnswers / totalCorrectAnswers) * 90;
                totalTypeTwoMarks += marksObtained;
            }
            if (noOfTypeTwoQuestions != 0)
            {
                typeTwoMarks = totalTypeTwoMarks / noOfTypeTwoQuestions;
            }
            //calculating listening Type Three Marks

            int totalTypeThreeMarks = 0;
            int noOfTypeThreeQuestions = 0;
            foreach (var item in Common.MockTestSession.ListeningTypeThree)
            {
                noOfTypeThreeQuestions++;
                int totalCorrectAnswers = getNumberOfCorrectAnswers(item.QuestionId, QuestionType.ListeningTypeThree);
                int attemptedCorrectAnswers = item.NumberOfCorrectAnswers;
                int marksObtained = (attemptedCorrectAnswers / totalCorrectAnswers) * 90;
                totalTypeThreeMarks += marksObtained;
            }
            if (noOfTypeThreeQuestions != 0)
            {
                typeThreeMarks = totalTypeThreeMarks / noOfTypeThreeQuestions;
            }


            //calculating Listening Type Four Marks
            int noOfTypeFourQuestions = 0;
            int totalTypeFourMarks = 0;
            foreach (var item in Common.MockTestSession.ListeningTypeFour)
            {
                noOfTypeFourQuestions++;
                if (item.IScorrectAnswer == true)
                {
                    totalTypeFourMarks += 90;
                }
            }
            if (noOfTypeFourQuestions != 0)
            {


                typeFourMarks = totalTypeFourMarks / noOfTypeFourQuestions;
            }
            //calculating Listening Type Five Marks
            int noOfTypeFiveQuestions = 0;
            int totalTypeFiveMarks = 0;
            foreach (var item in Common.MockTestSession.ListeningTypeFive)
            {
                noOfTypeFiveQuestions++;
                if (item.IScorrectAnswer == true)
                {
                    totalTypeFiveMarks += 90;
                }
            }
            if (noOfTypeFiveQuestions != 0)
            {
                typeFiveMarks = totalTypeFiveMarks / noOfTypeFiveQuestions;
            }
            //calculating Listening Type Six Marks
            int noOfTypeSixQuestions = 0;
            int totalTypeSixMarks = 0;
            foreach (var item in Common.MockTestSession.ListeningTypeSix)
            {
                noOfTypeSixQuestions++;
                if (item.IScorrectAnswer == true)
                {
                    totalTypeSixMarks += 90;
                }
            }
            if (noOfTypeSixQuestions != 0)
            {
                typeSixMarks = totalTypeSixMarks / noOfTypeSixQuestions;
            }


            //calculating Listening Type Seven Marks
            int noOfTypeSevenQuestions = 0;
            int totalTypeSevenMarks = 0;
            foreach (var item in Common.MockTestSession.ListeningTypeSeven)
            {
                noOfTypeSevenQuestions++;
                int totalCorrectAnswers = getNumberOfCorrectAnswers(item.QuestionId, QuestionType.ListeningTypeSeven);
                int givenCorrectAnswers = item.NumberOfCorrectAnswers;
                int marksObtained = (givenCorrectAnswers / totalCorrectAnswers) * 90;
                totalTypeSevenMarks += marksObtained;
            }
            if (noOfTypeSevenQuestions != 0)
            {
                typeSevenMarks = totalTypeSevenMarks / noOfTypeSevenQuestions;
            }
            //calculating Listening Type Eight Marks
            int noOfTypeEightQuestions = 0;
            int totalTypeEightMarks = 0;
            foreach (var item in Common.MockTestSession.ListeningTypeEight)
            {
                noOfTypeEightQuestions++;
                if (item.IScorrectAnswer == true)
                {
                    totalTypeEightMarks += 90;

                }
            }
            if (noOfTypeEightQuestions != 0)
            {
                typeEightMarks = totalTypeEightMarks / noOfTypeEightQuestions;
            }
            overallMarks = (typeOneMarks + typeTwoMarks + typeThreeMarks + typeFourMarks + typeFiveMarks + typeSixMarks + typeSevenMarks + typeEightMarks) / 8;
            return overallMarks;

        }


        public int createRandomNumber(int from, int till)
        {
            Random rnd = new Random();
            int num = rnd.Next(from, till);  // creates a number between 'from' and 'till'
            return num;
        }

        public int getNumberOfCorrectAnswers(int questionId, QuestionType questionType)
        {
            if (questionType == QuestionType.ReadingTypeTwo)
            {
                var data = dbContext.ReadingTypeTwo.Where(x => x.Id == questionId).FirstOrDefault().IsCorrectAnswer.Trim();
                string[] dataList = data.Split(',');
                int count = dataList.Count();
                return count;
            }
            else if (questionType == QuestionType.ListeningTypeTwo)
            {
                var data = dbContext.ListeningTypeTwo.Where(x => x.Id == questionId).FirstOrDefault().IsCorrectAnswer.Trim();
                string[] dataList = data.Split(',');
                int count = dataList.Count();
                return count;
            }
            else if (questionType == QuestionType.ListeningTypeThree)
            {
                var data = dbContext.ListeningTypeThree.Where(x => x.Id == questionId).FirstOrDefault().Answers.Trim();
                string[] dataList = data.Split(',');
                int count = dataList.Count();
                return count;
            }
            else if (questionType == QuestionType.ListeningTypeSeven)
            {
                var data = dbContext.ListeningTypeSeven.Where(x => x.Id == questionId).FirstOrDefault().Answers.Trim();
                string[] dataList = data.Split(',');
                int count = dataList.Count();
                return count;
            }
            return 0;

        }

    }
}