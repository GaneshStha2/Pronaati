using Riddhasoft.Setup.Entity.QuestionSet;
using RTech.Demo.Areas.MockTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Common
{
    public class MockTestSession
    {
        public static int QuestionPackageId
        {
            get
            {
                return (int)GetSession("QuestionPackageId");
            }
            set
            {
                SetSession("QuestionPackageId", value);
            }
        }
        public static int QuestionSetId
        {
            get
            {
                return (int)GetSession("QuestionSetId");
            }
            set
            {
                SetSession("QuestionSetId", value);
            }
        }

        

        public static List<MockQuestionTypeListVm> QuestionList
        {
            get
            {
                return GetSession("QuestionList") as List<MockQuestionTypeListVm>;
            }
            set
            {
                SetSession("QuestionList", value);
            }
        }


        public static List<ListeningTypeOneAnswerMockTestViewModel> ListeningTypeOne
        {
            get
            {
                return GetSession("ListeningTypeOne") as List<ListeningTypeOneAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ListeningTypeOne", value);
            }
        }
        public static List<ListeningTypeTwoAnswerMockTestViewModel> ListeningTypeTwo
        {
            get
            {
                return GetSession("ListeningTypeTwo") as List<ListeningTypeTwoAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ListeningTypeTwo", value);
            }
        }
        public static List<ListeningTypeThreeAnswerMockTestViewModel> ListeningTypeThree
        {
            get
            {
                return GetSession("ListeningTypeThree") as List<ListeningTypeThreeAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ListeningTypeThree", value);
            }
        }
        public static List<ListeningTypeFourAnswerMockTestViewModel> ListeningTypeFour
        {
            get
            {
                return GetSession("ListeningTypeFour") as List<ListeningTypeFourAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ListeningTypeFour", value);
            }
        }

        public static List<ListeningTypeFiveAnswerMockTestViewModel> ListeningTypeFive
        {
            get
            {
                return GetSession("ListeningTypeFive") as List<ListeningTypeFiveAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ListeningTypeFive", value);
            }
        }

        public static List<ListeningTypeSixAnswerMockTestViewModel> ListeningTypeSix
        {
            get
            {
                return GetSession("ListeningTypeSix") as List<ListeningTypeSixAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ListeningTypeSix", value);
            }
        }
        public static List<ListeningTypeSevenAnswerMockTestViewModel> ListeningTypeSeven
        {
            get
            {
                return GetSession("ListeningTypeSeven") as List<ListeningTypeSevenAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ListeningTypeSeven", value);
            }
        }

        public static List<ListeningTypeEightAnswerMockTestViewModel> ListeningTypeEight
        {
            get
            {
                return GetSession("ListeningTypeEight") as List<ListeningTypeEightAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ListeningTypeEight", value);
            }
        }
        public static List<ReadingTypeOneAnswerMockTestViewModel> ReadingTypeOne
        {
            get
            {
                return GetSession("ReadingTypeOne") as List<ReadingTypeOneAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ReadingTypeOne", value);
            }
        }
        public static List<ReadingTypeTwoAnswerMockTestViewModel> ReadingTypeTwo
        {
            get
            {
                return GetSession("ReadingTypeTwo") as List<ReadingTypeTwoAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ReadingTypeTwo", value);
            }
        }
        public static List<ReadingTypeThreeAnswerMockTestViewModel> ReadingTypeThree
        {
            get
            {
                return GetSession("ReadingTypeThree") as List<ReadingTypeThreeAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ReadingTypeThree", value);
            }
        }
        public static List<ReadingTypeFourAnswerMockTestViewModel> ReadingTypeFour
        {
            get
            {
                return GetSession("ReadingTypeFour") as List<ReadingTypeFourAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ReadingTypeFour", value);
            }
        }
        public static List<ReadingTypeFiveAnswerMockTestViewModel> ReadingTypeFive
        {
            get
            {
                return GetSession("ReadingTypeFive") as List<ReadingTypeFiveAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("ReadingTypeFive", value);
            }
        }
        public static List<SpeakingAnswerMockTestViewModel> SpeakingTypeOne
        {
            get
            {
                return GetSession("SpeakingTypeOne") as List<SpeakingAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("SpeakingTypeOne", value);
            }
        }
        public static List<SpeakingAnswerMockTestViewModel> SpeakingTypeTwo
        {
            get
            {
                return GetSession("SpeakingTypeTwo") as List<SpeakingAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("SpeakingTypeTwo", value);
            }
        }
        public static List<SpeakingAnswerMockTestViewModel> SpeakingTypeThree
        {
            get
            {
                return GetSession("SpeakingTypeThree") as List<SpeakingAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("SpeakingTypeThree", value);
            }
        }
        public static List<SpeakingAnswerMockTestViewModel> SpeakingTypeFour
        {
            get
            {
                return GetSession("SpeakingTypeFour") as List<SpeakingAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("SpeakingTypeFour", value);
            }
        }
        public static List<SpeakingAnswerMockTestViewModel> SpeakingTypeFive
        {
            get
            {
                return GetSession("SpeakingTypeFive") as List<SpeakingAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("SpeakingTypeFive", value);
            }
        }
        public static List<SpeakingAnswerMockTestViewModel> SpeakingTypeSix
        {
            get
            {
                return GetSession("SpeakingTypeSix") as List<SpeakingAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("SpeakingTypeSix", value);
            }
        }
        public static List<SpeakingAnswerMockTestViewModel> SpeakingTypeSeven
        {
            get
            {
                return GetSession("SpeakingTypeSeven") as List<SpeakingAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("SpeakingTypeSeven", value);
            }
        }
        public static List<WritingAnswerMockTestViewModel> WritingTypeOne
        {
            get
            {
                return GetSession("WritingTypeOne") as List<WritingAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("WritingTypeOne", value);
            }
        }

        public static List<WritingAnswerMockTestViewModel> WritingTypeTwo
        {
            get
            {
                return GetSession("WritingTypeTwo") as List<WritingAnswerMockTestViewModel>;
            }
            set
            {
                SetSession("WritingTypeTwo", value);
            }
        }

        public static TimeSpan SpeakingAndWritingTime
        {
            get
            {
                return (TimeSpan)GetSession("SpeakingAndWritingTime");
            }
            set
            {
                SetSession("SpeakingAndWritingTime", value);
            }
        }


        public static DateTime StartDateTime {

            get
            {
                return (DateTime)GetSession("StartDateTime");
            }
            set
            {
                SetSession("StartDateTime", value);
            }
        }

        // Time spend by user on Mock test is stored in DateTime format
        // Example  :start time is 04/05/2019 11:26 and UserOnMockTestDateTime = 04/05/2019 11:50
        public static DateTime UserOnMockTestDateTime
        {

            get
            {
                return (DateTime)GetSession("CurrentDateTime");
            }
            set
            {
                SetSession("CurrentDateTime", value);
            }
        }
        public static DateTime EndDateTime
        {

            get
            {
                return (DateTime)GetSession("EndDateTime");
            }
            set
            {
                SetSession("EndDateTime", value);
            }
        }

        public static TimeSpan ReadingTime
        {
            get
            {
                return (TimeSpan)GetSession("ReadingTime");
            }
            set
            {
                SetSession("ReadingTime", value  );
            }
        }

        public static TimeSpan ListeningTime
        {
            get
            {
                return (TimeSpan)GetSession("ListeningTime");
            }
            set
            {
                SetSession("ListeningTime", value);
            }
        }

        public static DateTime StartTime
        {
            get
            {
                return (DateTime)GetSession("StartTime");
            }
            set
            {
                SetSession("StartTime", value);
            }
        }

        public static TimeSpan SpeakingTime
        {
            get
            {
                return (TimeSpan)GetSession("SpeakingTime");
            }
            set
            {
                SetSession("SpeakingTime", value);
            }
        }

        public static TimeSpan WritingTime
        {
            get
            {
                return (TimeSpan)GetSession("WritingTime");
            }
            set
            {
                SetSession("WritingTime", value);
            }
        }

        public static TimeSpan WritingTypeOneTime
        {
            get
            {
                return (TimeSpan)GetSession("WritingTypeOneTime");
            }
            set
            {
                SetSession("WritingTypeOneTime", value);
            }
        }

        public static TimeSpan WritingTypeTwoTime
        {
            get
            {
                return (TimeSpan)GetSession("WritingTypeTwoTime");
            }
            set
            {
                SetSession("WritingTypeTwoTime", value);
            }
        }



        public static TimeSpan ListeningTypeOneTime
        {
            get
            {
                return (TimeSpan)GetSession("ListeningTypeTwoTime");
            }
            set
            {
                SetSession("ListeningTypeTwoTime", value);
            }
        }

        public static TimeSpan RemainingListeningTypeTime
        {
            get
            {
                return (TimeSpan)GetSession("ListeningTypeTwoTime");
            }
            set
            {
                SetSession("ListeningTypeTwoTime", value);
            }
        }

        public static int CurrentQuestionCount
        {
            get
            {
                return (int)(GetSession("CurrentQuestionCount") ?? 0);
            }
            set
            {
                SetSession("CurrentQuestionCount", value);
            }
        }

        public static int TotalSpeakingWritingQs
        {
            get
            {
                return (int)GetSession("TotalSpeakingWritingQs");
            }
            set
            {
                SetSession("TotalSpeakingWritingQs", value);
            }
        }

        public static int TotalReadingQs
        {
            get
            {
                return (int)GetSession("TotalReadingQs");
            }
            set
            {
                SetSession("TotalReadingQs", value);
            }
        }

        public static int TotalListeningQs
        {
            get
            {
                return (int)GetSession("TotalListeningQs");
            }
            set
            {
                SetSession("TotalListeningQs", value);
            }
        }

        private static object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }
        private static void SetSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }

    public class MockQuestionTypeListVm
    {
        public int QuestionId { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}