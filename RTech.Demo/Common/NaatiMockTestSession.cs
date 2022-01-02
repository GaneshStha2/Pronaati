using Riddhasoft.MockTest.Entity;
using RTech.Demo.Areas.MockTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Common
{
    public class NaatiMockTestSession
    {

        public static List<LanguageTestViewModel> MocktestConversations
        {
            get
            {
                return GetSession("MocktestConversations") as List<LanguageTestViewModel>;
            }
            set
            {
                SetSession("MocktestConversations", value);
            }
        }


        public static List<LanguageTestViewModel> MocktestConversationsTwo
        {
            get
            {
                return GetSession("MocktestConversations") as List<LanguageTestViewModel>;
            }
            set
            {
                SetSession("MocktestConversations", value);
            }
        }


        public static int ScreenCount
        {
            get
            {
                return (int)GetSession("ScreenCount");
            }
            set
            {
                SetSession("ScreenCount", value);
            }
        }
        public static int TotalQuestionsCount
        {
            get
            {
                return (int)GetSession("TotalQuestionsCount");
            }
            set
            {
                SetSession("TotalQuestionsCount", value);
            }
        }

        public static int PackageId
        {
            get
            {
                return (int)GetSession("PackageId");
            }
            set
            {
                SetSession("PackageId", value);
            }
        }


        public static int MockTestId
        {
            get
            {
                return (int)GetSession("MockTestId");
            }
            set
            {
                SetSession("MockTestId", value);
            }
        }
        public static TestSource TestSource
        {
            get
            {
                return (TestSource)GetSession("TestSource");
            }
            set
            {
                SetSession("TestSource", value);
            }
        }


        public static int QuestionSetId
        {
            get
            {
                return (int)GetSession("QuestionSet");
            }
            set
            {
                SetSession("QuestionSet", value);
            }
        }
        public static int MockTestQuestionMasterId
        {
            get
            {
                return (int)GetSession("MockTestQuestionMasterId");
            }
            set
            {
                SetSession("MockTestQuestionMasterId", value);
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
}