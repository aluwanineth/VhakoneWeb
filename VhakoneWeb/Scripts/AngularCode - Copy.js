var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
    debugger;
    $scope.CreatePersonalDetail = function () {

        $scope.personalDetail = {};
        $scope.personalDetail.Title = $scope.Title;
        $scope.personalDetail.FirstName = $scope.FirstName;
        $scope.personalDetail.LastName = $scope.LastName;
        $scope.personalDetail.Gender = $scope.Gender;
        $scope.personalDetail.DateOfBirth = $scope.DateOfBirth;
        $scope.personalDetail.IdNumber = $scope.IdNumber;
        $scope.personalDetail.Disable = $scope.Disable;
        $http({
            method: "post",
            url: "http://vhakoneweb20170512080521.azurewebsites.net/Applicants/CreatePersonalDetail",
            datatype: "json",
            data: JSON.stringify($scope.personalDetail)
        }).then(function (response) {
            alert(response.data);
            $scope.GetAllData();
            $scope.Title = "";
            $scope.FirstName = "";
            $scope.LastName = "";
            $scope.Gender = "";
            $scope.DateOfBirth = "";
            $scope.IdNumber = "";
            $scope.Disable = "";
        })
    }
    $scope.EditPersonalDetail = function () {
        $scope.personalDetail = {};
        $scope.personalDetail = {};
        $scope.personalDetail.Title = $scope.Title;
        $scope.personalDetail.FirstName = $scope.FirstName;
        $scope.personalDetail.LastName = $scope.LastName;
        $scope.personalDetail.Gender = $scope.Gender;
        $scope.personalDetail.DateOfBirth = $scope.DateOfBirth;
        $scope.personalDetail.IdNumber = $scope.IdNumber;
        $http({
                method: "post",
                url: "http://vhakoneweb20170512080521.azurewebsites.net/Applicants/EditPersonalDetail",
                datatype: "json",
                data: JSON.stringify($scope.personalDetail)
        }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.Title = "";
                $scope.FirstName = "";
                $scope.LastName = "";
                $scope.Gender = "";
                $scope.DateOfBirth = "";
                $scope.IdNumber = "";
                document.getElementById("btnSave").setAttribute("value", "Submit");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add New Employee";
            })  
    }
    
    $scope.GetAllData = function () {
        $http({
            method: "get",
            url: "http://vhakoneweb20170512080521.azurewebsites.net/Applicants/GetApplicant"
        }).then(function (response) {
            $scope.personalDetails = response.data;
        }, function () {
            alert("Error Occur");
        })
    };
    
    $scope.UpdateEmp = function (Emp) {
        document.getElementById("EmpID_").value = Emp.Emp_Id;
        $scope.EmpName = Emp.Emp_Name;
        $scope.EmpCity = Emp.Emp_City;
        $scope.EmpAge = Emp.Emp_Age;
        document.getElementById("btnSave").setAttribute("value", "Update");
        document.getElementById("btnSave").style.backgroundColor = "Yellow";
        document.getElementById("spn").innerHTML = "Update Employee Information";
    }
})  