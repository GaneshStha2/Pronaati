namespace RTech.Demo.Migrations
{
    using Riddhasoft.Entity.User;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Riddhasoft.DB.RiddhaDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Riddhasoft.DB.RiddhaDBContext";
        }

        protected override void Seed(Riddhasoft.DB.RiddhaDBContext context)
        {


            //context.Menu.AddOrUpdate(x => x.Id,
            //           //set up
            //           new EMenu() { Code = "01", IsGroup = true, Name = "SetUp", NameNp = "सेट्प", ParentCode = String.Empty, MenuIconCss = "fa  fa-gears", MenuUrl = "#" },
            //           new EMenu() { Code = "1", IsGroup = false, Name = "Language", NameNp = "", ParentCode = "01", MenuIconCss = "fa  fa-file", MenuUrl = "/Setup/CourseType" },


            //           new EMenu() { Code = "8", IsGroup = false, Name = "Segment", NameNp = "", ParentCode = "01", MenuIconCss = "fa  fa-file", MenuUrl = "/Setup/Segment" },
            //           new EMenu() { Code = "3", IsGroup = false, Name = "Dialogue", NameNp = "", ParentCode = "01", MenuIconCss = "fa  fa-file", MenuUrl = "/Setup/QuestionSet" },
            //           new EMenu() { Code = "5", IsGroup = false, Name = "Question ", NameNp = "", ParentCode = "01", MenuIconCss = "fa  fa-file", MenuUrl = "/Setup/MockTestQuestion" },


            //           new EMenu() { Code = "6", IsGroup = false, Name = "Mock Test", NameNp = "", ParentCode = "01", MenuIconCss = "fa  fa-file", MenuUrl = "/Setup/NaatiMocktest" },
            //           new EMenu() { Code = "7", IsGroup = false, Name = "Package", NameNp = "", ParentCode = "01", MenuIconCss = "fa  fa-file", MenuUrl = "/Setup/NaatiPackage" },
            //           new EMenu() { Code = "10", IsGroup = false, Name = "Mocktest Scoring", NameNp = "", ParentCode = "01", MenuIconCss = "fa  fa-file", MenuUrl = "/Setup/AudioScoring" },

            //           //Students
            //           new EMenu() { Code = "02", IsGroup = true, Name = "Students", NameNp = "", ParentCode = String.Empty, MenuIconCss = "fa  fa-gears", MenuUrl = "#" },
            //           new EMenu() { Code = "20", IsGroup = false, Name = "Student List", NameNp = "", ParentCode = "02", MenuIconCss = "fa  fa-file", MenuUrl = "/Setup/Student" },
            //           new EMenu() { Code = "21", IsGroup = false, Name = "Student Packages", NameNp = "", ParentCode = "02", MenuIconCss = "fa  fa-file", MenuUrl = "/Setup/StudentsCoursePackages" },
            //           new EMenu() { Code = "22", IsGroup = false, Name = "Transaction History", NameNp = "", ParentCode = "02", MenuIconCss = "fa  fa-file", MenuUrl = "/Setup/Transaction" }


            //);



            //context.MenuAction.AddOrUpdate(x => x.Id,

            //              //Course Type
            //              new EMenuAction() { MenuCode = "1", ActionCode = "01", Desc = "View" },
            //              new EMenuAction() { MenuCode = "1", ActionCode = "02", Desc = "Create" },
            //              new EMenuAction() { MenuCode = "1", ActionCode = "03", Desc = "Update" },
            //              new EMenuAction() { MenuCode = "1", ActionCode = "04", Desc = "Delete" },


            //              //Question Set
            //              new EMenuAction() { MenuCode = "3", ActionCode = "10", Desc = "View" },
            //              new EMenuAction() { MenuCode = "3", ActionCode = "11", Desc = "Create" },
            //              new EMenuAction() { MenuCode = "3", ActionCode = "12", Desc = "Update" },
            //              new EMenuAction() { MenuCode = "3", ActionCode = "13", Desc = "Delete" },



            //              //MockTest Question
            //              new EMenuAction() { MenuCode = "5", ActionCode = "130", Desc = "View" },
            //              new EMenuAction() { MenuCode = "5", ActionCode = "131", Desc = "Create" },
            //              new EMenuAction() { MenuCode = "5", ActionCode = "132", Desc = "Update" },
            //              new EMenuAction() { MenuCode = "5", ActionCode = "133", Desc = "Delete" },

            //              //MockTest
            //              new EMenuAction() { MenuCode = "6", ActionCode = "134", Desc = "View" },
            //              new EMenuAction() { MenuCode = "6", ActionCode = "135", Desc = "Create" },
            //              new EMenuAction() { MenuCode = "6", ActionCode = "136", Desc = "Update" },
            //              new EMenuAction() { MenuCode = "6", ActionCode = "137", Desc = "Delete" },

            //              //naati PAckage
            //              new EMenuAction() { MenuCode = "7", ActionCode = "138", Desc = "View" },
            //              new EMenuAction() { MenuCode = "7", ActionCode = "139", Desc = "Create" },
            //              new EMenuAction() { MenuCode = "7", ActionCode = "140", Desc = "Update" },
            //              new EMenuAction() { MenuCode = "7", ActionCode = "141", Desc = "Delete" },

            //              //Student List
            //              new EMenuAction() { MenuCode = "20", ActionCode = "18", Desc = "View" },
            //              new EMenuAction() { MenuCode = "20", ActionCode = "19", Desc = "Delete" }

            //);
        }
    }
}