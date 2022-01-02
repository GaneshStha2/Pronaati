using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riddhasoft.User.Entity;
using Riddhasoft.Entity.User;
using System.Data.SqlClient;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Entity.Writing;
using Riddhasoft.Setup.Entity.Reading;
using Riddhasoft.Setup.Entity.Listening;
using Riddhasoft.Setup.Entity.Speaking;
using Riddhasoft.Setup.Entity.Course;
using Riddhasoft.Student.Entity;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Entity.Package;
using Riddhasoft.Setup.Entity.QuestionPackage;
using Riddhasoft.Setup.Entity.Practice;
using Riddhasoft.MockTest.Entity;

namespace Riddhasoft.DB
{
    public class RiddhaDBContext : DbContext
    {
        public RiddhaDBContext()
            : base("name=RTechDemoContext")
        {

        }
        #region UserManagement
        public DbSet<EUser> User { get; set; }
        public DbSet<EUserRole> UserRole { get; set; }
        public DbSet<EContext> Context { get; set; }
        public DbSet<ESessionDetail> SessionDetail { get; set; }
        public DbSet<EAuditTrial> AuditTrial { get; set; }

        #endregion
        #region Menu
        public DbSet<EMenu> Menu { get; set; }
        public DbSet<EUserGroupDataVisibility> UserGroupDataVisibility { get; set; }
        public DbSet<EMenuAction> MenuAction { get; set; }
        public DbSet<EUserGroupMenuRight> UserGroupMenuRight { get; set; }
        public DbSet<EUserGroupActionRight> UserGroupActionRight { get; set; }
        #endregion
        #region setup
        public DbSet<ECompany> Company { get; set; }
        public DbSet<EBranch> Branch { get; set; }
        public DbSet<ECompanyLicense> CompanyLicense { get; set; }
        public DbSet<ECompanyLicenseLog> CompanyLicenseLog { get; set; }
        public DbSet<EDateTable> DateTable { get; set; }
        public DbSet<EWritingTypeOne> WritingTypeOne { get; set; }
        public DbSet<EWritingTypeTwo> WritingTypeTwo { get; set; }
        public DbSet<EReadingTypeOne> ReadingTypeOne { get; set; }
        public DbSet<EReadingTypeTwo> ReadingTypeTwo { get; set; }
        public DbSet<EReadingTypeThree> ReadingTypeThree { get; set; }
        public DbSet<EReadingTypeFour> ReadingTypeFour { get; set; }
        public DbSet<EReadingTypeFive> ReadingTypeFive { get; set; }
        public DbSet<EReadingTypeFiveDropdown> ReadingTypeFiveDropdown { get; set; }
        public DbSet<EReadingTypeFiveOptions> ReadingTypeFiveOptions { get; set; }
        public DbSet<EListeningTypeOne> ListeningTypeOne { get; set; }
        public DbSet<EListeningTypeTwo> ListeningTypeTwo { get; set; }
        public DbSet<EListeningTypeThree> ListeningTypeThree { get; set; }
        public DbSet<EListeningTypeFour> ListeningTypeFour { get; set; }
        public DbSet<EListeningTypeFive> ListeningTypeFive { get; set; }
        public DbSet<EListeningTypeSix> ListeningTypeSix { get; set; }
        public DbSet<EListeningTypeSeven> ListeningTypeSeven { get; set; }
        public DbSet<EListeningTypeEight> ListeningTypeEight { get; set; }
        public DbSet<ESpeakingTypeOne> SpeakingTypeOne { get; set; }
        public DbSet<ESpeakingTypeTwo> SpeakingTypeTwo { get; set; }
        public DbSet<ESpeakingTypeThree> SpeakingTypeThree { get; set; }
        public DbSet<ESpeakingTypeFour> SpeakingTypeFour { get; set; }

        public DbSet<ESpeakingTypeFive> SpeakingTypeFive { get; set; }
        public DbSet<ESpeakingTypeSix> SpeakingTypeSix { get; set; }
        public DbSet<ESpeakingTypeSeven> SpeakingTypeSeven { get; set; }

        public DbSet<EOnlineClassRoomCourse> OnlineClassRoomCourse { get; set; }
        public DbSet<EOnlineClassRoomCourseDetails> OnlineClassRoomCourseDetails { get; set; }
        public DbSet<EOnlineTraining> OnlineTraining { get; set; }
        public DbSet<EOnlineTrainingDetails> OnlineTrainingDetails { get; set; }
        public DbSet<ELanguageType> CourseType { get; set; }
        public DbSet<ESegment> Segment { get; set; }
        public DbSet<ENaatiScore> NaatiScore { get; set; }
        public DbSet<ENaatiScoreDetail> NaatiScoreDetail { get; set; }


        #region Naati New Entity

        public DbSet<EMockTestQuestionMaster> MockTestQuestionMaster { get; set; }
        public DbSet<EMockTestQuestionDetail> MockTestQuestionDetail { get; set; }
        public DbSet<ENaatiPackage> NaatiPackage { get; set; }
        public DbSet<EPackageFile> PackageFile { get; set; }
        public DbSet<EPackageMockTest> PackageMockTest { get; set; }
        public DbSet<ENaatiMockTestAnswer> NaatiMockTestAnswer { get; set; }
        public DbSet<ENaatiMockTestTaken> NaatiMockTestTaken { get; set; }
        #endregion

        #endregion
        #region Student
        public DbSet<EStudentInformation> StudentInformation { get; set; }
        public DbSet<EStudentLogin> StudentLogin { get; set; }
        #endregion

        public DbSet<ECountry> Country { get; set; }
        public DbSet<EQuestionSetMaster> QuestionSetMaster { get; set; }
        public DbSet<EQuestionSetDetail> QuestionSetDetail { get; set; }
        public DbSet<EFiles> EFiles { get; set; }
        public DbSet<EFolder> EFolder { get; set; }

        public DbSet<ETutorials> ETutorials { get; set; }
        public DbSet<ETutorialDetails> ETutorialDetails { get; set; }

        public DbSet<EPackage> Package { get; set; }
        public DbSet<EPackageDetails> PackageDetails { get; set; }

        public DbSet<EQuestionPackageDetail> QuestionPackageDetail { get; set; }

        public DbSet<EQuestionPackageMaster> QuestionPackageMaster { get; set; }


        public DbSet<EPackagePaymentDetails> PackagePaymentDetails { get; set; }
        public DbSet<EMockTestReport> MockTestReport { get; set; }


        #region Practice Module

        public DbSet<EVocabularyAndPronunciationBooster> EVocabularyAndPronunciationBoosters { get; set; }

        public DbSet<ESynonymBooster> ESynonymBoosters { get; set; }
        public DbSet<ESynonymBoosterOptionDetails> ESynonymBoosterOptionDetails { get; set; }

        public DbSet<EBoosterCollocation> BoosterCollocation { get; set; }
        public DbSet<EBoosterCollocationDetails> BoosterCollocationDetails { get; set; }
        public DbSet<EMasterTopicSentenceBooster> EMasterTopicSentenceBoosters { get; set; }

        public DbSet<EMasterTopicSentenceBoosterOptionDetails> EMasterTopicSentenceBoosterOptionDetails { get; set; }
        public DbSet<ENaatiMockTestMaster> NaatiMockTestMaster { get; set; }
        public DbSet<ENaatiMockTestDetail> NaatiMockTestDetail { get; set; }
        #endregion

        public DbSet<EOnlineClassRoomCoursePracticeDetails> EOnlineClassRoomCoursePracticeDetails { get; set; }

        public DbSet<EWritingAnswer> WritingAnswer { get; set; }
        public DbSet<ESpeakingAnswer> SpeakingAnswer { get; set; }
        public DbSet<ELIsteningTypeOneAnswer> LIsteningTypeOneAnswer { get; set; }
        public DbSet<EListeningTypeTwoAnswer> ListeningTypeTwoAnswer { get; set; }
        public DbSet<EListeningTypeThreeAnswer> ListeningTypeThreeAnswer { get; set; }
        public DbSet<EListeningTypeFourAnswer> ListeningTypeFourAnswer { get; set; }
        public DbSet<EListeningTypeFiveAnswer> ListeningTypeFiveAnswer { get; set; }
        public DbSet<EListeningTypeSixAnswer> ListeningTypeSixAnswer { get; set; }
        public DbSet<ELIsteningTYpeSevenAnswer> LIsteningTYpeSevenAnswer { get; set; }
        public DbSet<ELIsteningTYpeEightAnswer> LIsteningTYpeEightAnswer { get; set; }
        public DbSet<EReadingTypeOneAnswer> ReadingTypeOneAnswer { get; set; }
        public DbSet<EReadingTypeTwoAnswer> ReadingTypeTwoAnswer { get; set; }
        public DbSet<EReadingTypeThreeAnswer> ReadingTypeThreeAnswer { get; set; }
        public DbSet<EReadingTypeFourAnswer> ReadingTypeFourAnswer { get; set; }
        public DbSet<EReadingTypeFiveAnswer> ReadingTypeFiveAnswer { get; set; }
        public DbSet<EMockTestWritingEvaluation> MockTestWritingEvaluation { get; set; }
        public DbSet<EMockTestPurchasedPackages> MockTestPurchasedPackages { get; set; }

        public DbSet<EFeedBack> FeedBack { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

}
