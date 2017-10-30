var udApp = angular.module("udApp", []);



udApp.controller("PageController", function ($scope, $timeout) {

    $scope.Users=[];
    $scope.Departments=[];
    $scope.User;
    $scope.Department;
    $scope.ViewMode = "Users";
    $scope.editMode = false;

    $scope.GetAllUsers = function () {

    
        $.ajax({
            url: '/api/values/GetAllUsers',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $scope.Users = JSON.parse(data);                
                $timeout(function () { }, 100)
                input.val('')
             
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            },
            complete: function () {
                
            }
        });
    }


    $scope.GetAllDepartments = function () {


        $.ajax({
            url: '/api/values/GetAllDepartments',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $scope.Departments = JSON.parse(data);
                $scope.$apply();
                input.val('')

            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            },
            complete: function () {
          
            }
        });
    }



    $scope.CreateUser = function(userName) {

        $.ajax({
            url: '/api/values/CreateUser/',
            type: 'POST',
            data: JSON.stringify(userName),
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $scope.Users.push(JSON.parse(data))
                $scope.$apply();
                input.val("");
                input.focus();
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            },
            complete: function () {
            }
        });
    }

    $scope.CreateDepartment = function (departmentName) {

        if (isBusy == true) { return false; }
        // if (input.val() == "" || input.val() == null) { label.text("Сначала нужно ввести строку"); return false; }

        isBusy = true;
        $.ajax({
            url: '/api/values/CreateDepartment/',
            type: 'POST',
            data: JSON.stringify(departmentName),
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $scope.Departments.push(JSON.parse(data))
                $scope.$apply();
                //label.text("Добавлена строка " + '"' + input.val() + '"')
                input.val("");
                input.focus();
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            },
            complete: function () {
                isBusy = false;
                // alert("success")
            }
        });
    }



    $scope.EditUser = function(user) {
        $.ajax({
            url: '/api/values/EditUser/',
            type: 'PUT',
            data: JSON.stringify(user),
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                var modifiedUser = JSON.parse(data);

                $scope.Users.forEach(function (item, i, arr) {
                    if (modifiedUser.Id == item.Id) { item.Department = modifiedUser.Department; }

                })
                $scope.$apply();
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            },
            complete: function () {
            }
        });
    }




    $scope.EditDepartment = function (department) {
        $.ajax({
            url: '/api/values/EditDepartment/',
            type: 'PUT',
            data: JSON.stringify(department),
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                var modifiedDepartment = JSON.parse(data);

                $scope.Departments.forEach(function (item, i, arr) {
                    if (modifiedDepartment.Id == item.Id) { item.Title = modifiedDepartment.Title; }

                })
                $scope.$apply();
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            },
            complete: function () {
            }
        });
    }



    $scope.RemoveDepartment = function (departmentId) {
     
        $.ajax({
            url: '/api/values/RemoveDepartment/' + departmentId,
            type: 'DELETE',

            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var deletedIndex = 0;
                $scope.Departments.forEach(function (item, i, arr) {
                    if (item.Id == departmentId) { deletedIndex = i; }

                })
                $scope.Departments.splice(deletedIndex, 1);

                $scope.$apply();



            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            },
            complete: function () {
             

            }
        });



    }



    $scope.RemoveUser = function(userId) {
     

        $.ajax({
            url: '/api/values/RemoveUser/'+userId,
            type: 'DELETE',

            contentType: "application/json;charset=utf-8",
            success: function (data) {
                

                var deletedIndex = 0;
                $scope.Users.forEach(function (item, i, arr) {
                    if (item.Id == userId) { deletedIndex = i; }
                })
                $scope.Users.splice(deletedIndex,1);
                
                $scope.$apply();

                

            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            },
            complete: function () {
            

            }
        });



    }


})



