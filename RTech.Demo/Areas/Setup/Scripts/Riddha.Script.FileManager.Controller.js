/// <reference path="riddha.script.filemanager.model.js" />


function FileManagerController() {

  
    var self = this;
    var url = "/Api/FileManagerApi";
    //self.Folder = ko.observable(new FolderModel());
    //self.MasterFolders = ko.observableArray([]);

    self.FolderGroup = ko.observable(new FolderModel());
    self.FolderGroups = ko.observableArray([]);
    self.SelectedFolderGroup = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    var treeview = $("#treeview").kendoTreeView({
        loadOnDemand: false
    }).data("kendoTreeView"),

        handleTextBox = function (callback) {
            return function (e) {
                if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode) {
                    callback(e);
                }
            };
        };

    //$("#treeview").kendoTreeView({
    //    select: onSelect,
    //    dataSource: treeview
    //});

    //function onSelect(e) {
    //    test();
    //}


    $(document).on('click', '#treeview', function () {
        GetFilesByFolderId();
    });

    function GetFilesByFolderId() {
        var selectedNode = treeview.select();
        var item = treeview.dataItem(selectedNode);
        if (item != undefined) {
            Riddha.ajax.get(FileUrl + "/GetFilesByFolder?FolderId=" + item.id)
                .done(function (result) {
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), FilesModel);
                    self.Files(data);

                });

        }
    }
    //function test() {
    //    alert("Hey");
    //    Console.log("Hello");
    //}


    getTreeData();
    function getTreeData() {
        Riddha.ajax.get(url + "/GetFolderGroupTreeLst")
            .done(function (result) {
                treeview.dataSource.data(result.Data);
            });
    }

    GetFolderGroups();
    function GetFolderGroups() {
        Riddha.ajax.get(url)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), FolderModel)
                self.FolderGroups(data);
                self.FilteredGroup(data);
            });
    };

    self.CreateUpdate = function () {
        if (self.FolderGroup().Name() == "") {
            return Riddha.UI.Toast("Name is required", 0)
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.FolderGroup()))
                .done(function (result) {
                    if (result.Status == 4) {
                        var selectedNode = treeview.select();
                        var item = treeview.dataItem(selectedNode);
                        if (item) {
                            treeview.append({
                                text: self.FolderGroup().Name(),
                                id: result.Data.Id,
                                parentId: result.Data.ParentId,
                                spriteCssClass: "folder"
                            }, selectedNode);
                        }
                        else {
                            treeview.append({
                                text: self.FolderGroup().Name(),
                                id: result.Data.Id,
                                parentId: result.Data.ParentId,
                                spriteCssClass: "folder"
                            });
                        }
                        self.FolderGroups.push(new FolderModel(result.Data));
                        getTreeData();
                        self.Reset();
                        self.FolderGroupCloseModal();
                        GetFolderGroups();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                })
        }
        else if (self.ModeOfButton() == 'Update') {
            debugger
            if (self.FolderGroup().Id() == self.FolderGroup().ParentId()) {

                Riddha.UI.Toast("You can't choose the same folder as parent folder !", 0);
                return;
            }
            
            Riddha.ajax.put(url, ko.toJS(self.FolderGroup()))
                .done(function (result) {
                    if (result.Status == 4) {
                        getTreeData();
                        self.Reset();
                        self.FolderGroupCloseModal();
                        GetFolderGroups();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        };
    };

    self.Reset = function () {
        self.FolderGroup(new FolderModel({ Id: self.FolderGroup().Id() }));
        self.ModeOfButton("Create");
        //getNewSequenceForFolderGroup();
    };
    self.Refresh = function () {
        getTreeData();
    };

    self.FilteredGroup = ko.observableArray([]);
    self.SelectFolderGroup = function () {

        var selectedNode = treeview.select();
        var item = treeview.dataItem(selectedNode);
        if (item != undefined) {
            Riddha.ajax.get(url + "?id=" + item.id)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.FolderGroup(new FolderModel(result.Data));
                        var filtered = ko.utils.arrayFilter(self.FolderGroups(), function (item) {
                            return !(item.ParentId() == result.Data.Id || item.Id() == result.Data.Id);
                        });
                        self.FilteredGroup(filtered);
                        self.ModeOfButton("Update");
                        self.FolderGroupEdit();
                    }
                })


        }
        else {
            return Riddha.UI.Toast("Select a Node to Edit", 0);
        }
    };

    self.DeleteFolderGroup = function () {
        var selectedNode = treeview.select();
        var item = treeview.dataItem(selectedNode);
        if (item.hasChildren) {
            return Riddha.UI.Toast("Please delete its child first", 0);
        }
        if (item != undefined) {
            Riddha.UI.Confirm("Confirm to delete this message?", function () {
                Riddha.ajax.delete(url + "/" + item.id, null)
                    .done(function (result) {
                        if (result.Status == 4) {
                            treeview.remove(selectedNode);
                            self.Reset();
                            GetFolderGroups();
                        }
                        Riddha.UI.Toast(result.Message, result.Status);
                    });
            })



        }
        else {
            return Riddha.UI.Toast("Select a Node to Delete", 0);
        }
    };

    self.FolderGroupAdd = function () {        
        var selectedNode = treeview.select();
        var item = treeview.dataItem(selectedNode);
        if (item != undefined) {
            self.FolderGroup().ParentId(item.id);
        }
        var filtered = ko.utils.arrayFilter(self.FolderGroups(), function (item) {
            return true;
        });
        self.FilteredGroup(filtered);
        $("#FolderGroupCreationModel").modal('show');
        //getNewSequenceForFolderGroup();
    };

    self.FolderGroupEdit = function () {
        debugger;
        var selectedNode = treeview.select();
        var item = treeview.dataItem(selectedNode);
        if (item != undefined) {
            self.FolderGroup().Id(item.id);
        }
        $("#FolderGroupCreationModel").modal('show');
        //getNewSequenceForFolderGroup();
    }

    self.FolderGroupCloseModal = function () {
        $("#FolderGroupCreationModel").modal('hide');
    };




    /// Files Creation 
    self.File = ko.observable(new FilesModel());
    self.Files = ko.observableArray([]);
    self.SelectedFile = ko.observable();
    self.FolderGrps = ko.observableArray([]);
    self.FileModeOfButton = ko.observable("Create");
    var FileUrl = "/Api/FilesApi";
    GetFolderGroups();
    function GetFolderGroups() {
        Riddha.ajax.get("/Api/FilesApi/GetFolders", null)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), GlobalDropdownModel);
                self.FolderGrps(data);
            })
    };




    function GetFiles(FolderId) {
        debugger;
        Riddha.ajax.get(FileUrl + "/GetFilesByFolder?FolderId=" + FolderId)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), FilesModel);
                self.Files(data);
            });
    };


    self.FileCreateUpdate = function () {
        debugger;
        if (self.File().Name() == "") {
            return Riddha.UI.Toast("Name is required", 0)
        }
        if (self.File().FolderId() == undefined) {
            return Riddha.UI.Toast("Folder is required", 0)
        }
        if (self.File().URL() == "") {
            return Riddha.UI.Toast("Please browse a file ", 0)
        }

        if (self.FileModeOfButton() == 'Create') {
            debugger;
            Riddha.ajax.post(FileUrl, ko.toJS(self.File()))
                .done(function (result) {
                    if (result.Status == 4) {
                        GetFilesByFolderId();
                        self.FileReset();
                        self.FileCloseModal();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        }
        else if (self.FileModeOfButton() == 'Update') {
            Riddha.ajax.put(FileUrl, ko.toJS(self.File()))
                .done(function (result) {
                    if (result.Status == 4) {
                        GetFilesByFolderId();
                        self.FileReset();
                        self.FileCloseModal();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        }

        

    };

    self.FileReset = function () {
        self.File(new FilesModel({ Id: self.File().Id() }));
        self.SelectedFile(undefined);
        self.FileExample();
        self.FileModeOfButton("Create");

    };

    self.FileSelect = function (model) {
        if (self.SelectedFile() == undefined || self.SelectedFile().Id() == 0) {
            return Riddha.UI.Toast("Please select file to edit!!!", 0);
        }
        self.FileShowModal();
        self.File(new FilesModel(ko.toJS(self.SelectedFile())));
        self.FileModeOfButton('Update');
    };

    self.FileDelete = function (Item) {

        if (self.SelectedFile() == undefined || self.SelectedFile().Id() == 0) {
            return Riddha.UI.Toast("Please select file to delete!!!", 0);
        }
        Riddha.UI.Confirm("Confirm to delete this message?", function () {
            Riddha.ajax.delete(FileUrl + "/" + self.SelectedFile().Id(), null)
                .done(function (result) {
                    self.Files.remove(Item);
                    self.FileReset();
                    GetFilesByFolderId();
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })
    };

    self.FileOpen = function (item) {
        debugger;
        window.open(fileMang.SelectedFile().URL(), '_blank');
        return;
    }

    self.FileShowModal = function () {
        var selectedNode = treeview.select();
        var item = treeview.dataItem(selectedNode);
        if (item != undefined) {
            self.File().FolderId(item.id);
        }
        else {
            return Riddha.UI.Toast("Please select folder Group to create item!!!", 0);
        }
        $("#fileCreationModel").modal('show');
        //GetFiles();
    };
    self.FileEditShowModal = function () {
        $("#fileCreationModel").modal('show');
    };

    $("#fileCreationModel").on('hidden.bs.modal', function () {
        self.FileReset();
    });

    self.FileCloseModal = function () {
        $("#fileCreationModel").modal('hide');
        self.FileReset();
    };



    var id = 0;

    self.FileExample = function (item) {

        if (item != "" && item != undefined) {
            $("#" + id).css("background", "white");

            id = item.Id();
            //document.getElementById(id).css("background", "red");
            $("#" + id).css("background", "aqua");
            self.SelectedFile(new FilesModel(ko.toJS(item)));
        }
    }

    $("#save").click(function () {
        debugger;
        if (self.SelectedFile() == undefined) {

            Riddha.UI.Toast("Please select a file", 0);
            return;
        }

        
        var Id = self.SelectedFile().Id();
        $("#" + Id).css("background", "white");
        var data = self.SelectedFile();
        self.SelectedFile(undefined);

        Riddha.UI.modal.SelectClick(data);
        
    });

}