

function StudentsCoursePackagesController() {


    var self = this;

    self.KendoGridOptions = {
        title: "User",
        target: "#studentPackages",
        url: "/Api/StudentsCoursePackagesApi/GetKendoList",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'CourseCode', title: "Course Code", filterable: true },
            { field: 'CourseName', title: "Course Name", filterable: true },
            { field: 'CoursePrice', title: "Course Price", filterable: false },
            { field: 'StudentName', title: "Student Name", filterable: true },
            { field: 'StudentEmail', title: "Student Email", filterable: true },
            { field: 'StudentAddress', title: "Student Address", filterable: false },
            { field: 'StudentPhoneNumber', title: "Student PhoneNumber", filterable: false },
            { field: 'Institute', title: "Institute", filterable: false },    
            { field: 'PurchaseDate', title: "Purchase Date", filterable: false },
            { field: 'ReceiptNumber', title: "Receipt Number", filterable: true },
        ],
        SelectedItem: function (item) {
            //self.SelectedUser(new UserModel(item));
        },
        SelectedItems: function (items) {

        },
    };

}