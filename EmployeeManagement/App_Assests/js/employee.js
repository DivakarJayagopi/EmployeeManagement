
function LoadExistingEmplyeeInfo() {
    handleAjaxRequest(null, true, "/Method/GetExistingEmployeeInfo", null, "CallBackGetExistingEmployeeInfo");
}

var ExistingEmailList = [];
var ExistingMoblieNumberList = [];
function CallBackGetExistingEmployeeInfo(responseData) {
    if (responseData.message.status == "success") {
        ExistingEmailList = responseData.message.EmailList;
        ExistingMoblieNumberList = responseData.message.MoblieNumberList;
    }
    else {
        $(".customErrorMessageAddEmployee").text("Error on Adding employee, Try agin in few min");
    }
}

$(document).on('click', '.AddEmployeeFromSubmit', function () {   
    var Name = $(".Name").val().trim();
    var Email = $(".Email").val().trim();
    var Number = $(".Number").val().trim();
    var dob = $(".dob").val().trim();
    var doj = $(".doj").val().trim();
    var Address = $(".Address").val().trim();
    var Team = $(".Team").val().trim();
    var Role = $(".Role").val().trim();
    var IsAdmin = $(".IsAdmin").val().trim();

    if (Name == "" || Email == "" || Number == "" || dob == "" || doj == "" || Address == "" || Team == "" || Role == "") {

        $(".customErrorMessageAddEmployee").text("All fields are Mandatory");

        if (Name == "") $(".Name").addClass("form-error");
        else $(".Name").removeClass("form-error");

        if (Email == "") $(".Email").addClass("form-error");
        else $(".Email").removeClass("form-error");

        if (Number == "") $(".Number").addClass("form-error");
        else $(".Number").removeClass("form-error");

        if (dob == "") $(".dob").addClass("form-error");
        else $(".dob").removeClass("form-error");

        if (doj == "") $(".doj").addClass("form-error");
        else $(".doj").removeClass("form-error");

        if (Address == "") $(".Address").addClass("form-error");
        else $(".Address").removeClass("form-error");

        if (Team == "") $(".Team").addClass("form-error");
        else $(".Team").removeClass("form-error");

        if (Role == "") $(".Role").addClass("form-error");
        else $(".Role").removeClass("form-error");
    } else {
        $("input[type='text']").removeClass("form-error");
        $("input[type='date']").removeClass("form-error");
        $("input[type='number']").removeClass("form-error");

        $(".customErrorMessageAddEmployee").text("");

        var IsEmailValid = false;

        var EmailValidation = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;

        if (!EmailValidation.test(Email)) {
            $(".Email").addClass("form-error");
            $(".customErrorMessageAddEmployee").text("please enter valid email");
            return;
        } else {
            IsEmailValid = true;
            $(".Email").removeClass("form-error");
            $(".customErrorMessageAddEmployee").text("");

            if (ExistingEmailList.includes(Email)) {
                $(".Email").addClass("form-error");
                $(".customErrorMessageAddEmployee").text("Email Id already exist");
                return;
            } else if (ExistingMoblieNumberList.includes(Number)) {
                $(".Number").addClass("form-error");
                $(".customErrorMessageAddEmployee").text("Mobile Number already exist");
                return;
            }

            if (!IsEmployeeSalaryAdded) {
                $(".customErrorMessageAddEmployee").text("Add employee Salary Details");
                return;
            }
        }

        if (IsEmailValid) {
            $(".AddEmployeeFromSubmit").attr("disabled", "disabed");
            $(".AddEmployeeFromSubmit").text("loading...");
            var data = '{Name:"' + Name + '", Email:"' + Email + '", Number:' + Number + ',Password :"Welcome@123", dob:"' + dob + '", doj:"' + doj + '", Address:"' + Address + '", Team:"' + Team + '", Role:"' + Role + '", IsAdmin:' + IsAdmin + ', employeeSalaryInfo:' + JSON.stringify(employeeSalaryInfo) + '}';
            handleAjaxRequest(null, true, "/Method/AddEmployee", data, "CallBackAddEmployee", Name);
        }
    }

});

var employeeSalaryInfo;
var IsEmployeeSalaryAdded = false;

function CallBackAddEmployee(responseData, Name) {
    if (responseData.message.status == "success") {
        $("input[type='text']").val("");
        $("input[type='date']").val("");
        $("input[type='number']").val("");
        IsEmployeeSalaryAdded = false;
        $("#AddSalaryBtn").removeClass("btn-success");
        $("#AddSalaryBtn").addClass("btn-danger");
        employeeSalaryInfo = {};
        swal({
            title: "Success",
            text: "Employee " + Name + " Added SuccessFully",
            icon: "success",
        });
    }
    else {
        $(".customErrorMessageAddEmployee").text("Error on Adding employee, Try agin in few min");
    }
    $(".AddEmployeeFromSubmit").removeAttr("disabled");
    $(".AddEmployeeFromSubmit").text("Submit");
}

$(document).on('click', '#AddEmployeeSalaryInfoBtn', function () {
    var Basic = $(".Basic").val().trim();
    var DA = $(".DA").val().trim();
    var HRA = $(".HRA").val().trim();
    var MedicalAllowances = $(".MedicalAllowances").val().trim();
    var ConveyanceCharges = $(".ConveyanceCharges").val().trim();
    var SpecialAllowances = $(".SpecialAllowances").val().trim();

    if (Basic == "" || DA == "" || HRA == "" || MedicalAllowances == "" || ConveyanceCharges == "" || SpecialAllowances == "") {

        $(".customErrorMessageAddSalaryInfo").text("All fields are Mandatory");

        if (Basic == "") $(".Basic").addClass("form-error");
        else $(".Basic").removeClass("form-error");

        if (DA == "") $(".DA").addClass("form-error");
        else $(".DA").removeClass("form-error");

        if (HRA == "") $(".HRA").addClass("form-error");
        else $(".HRA").removeClass("form-error");

        if (MedicalAllowances == "") $(".MedicalAllowances").addClass("form-error");
        else $(".MedicalAllowances").removeClass("form-error");

        if (ConveyanceCharges == "") $(".ConveyanceCharges").addClass("form-error");
        else $(".ConveyanceCharges").removeClass("form-error");

        if (SpecialAllowances == "") $(".SpecialAllowances").addClass("form-error");
        else $(".SpecialAllowances").removeClass("form-error");
    } else {
        $("input[type='number']").removeClass("form-error");

        employeeSalaryInfo = {
            "Basic": Basic,
            "DA": DA,
            "HRA": HRA,
            "MedicalAllowances": MedicalAllowances,
            "ConveyanceCharges": ConveyanceCharges,
            "SpecialAllowances": SpecialAllowances
        }
        IsEmployeeSalaryAdded = true;
        $("#AddSalaryBtn").removeClass("btn-danger");
        $("#AddSalaryBtn").addClass("btn-success");
        $("#AddEmployeeSalaryInfo").modal("hide");
    }

});


$(document).on('click', '#EditEmployeeInfo', function () {
    var Id = $(this).attr("data-id");
    var data = '{Id:"' + Id + '"}';
    handleAjaxRequest(null, true, "/Method/GetEmployeeById", data, "CallBackGetEmployeeById");
    
});

var _tempExistingEmailList = [];
var _tempExistingMoblieNumberList = [];
function CallBackGetEmployeeById(responseData) {
    if (responseData.message.status == "success") {
        var EmployeeInfo = responseData.message.EmployeeInfo;

        if (EmployeeInfo != "undefined" && EmployeeInfo != null && EmployeeInfo != "") {
            $(".EmployeeId").val(EmployeeInfo.Id);
            $(".Name").val(EmployeeInfo.Name);
            $(".Email").val(EmployeeInfo.Email);
            $(".Number").val(EmployeeInfo.MobileNumber);
            $(".dob").val(responseData.message.DateObject_dob);
            $(".doj").val(responseData.message.DateObject_doj);
            $(".Address").val(EmployeeInfo.Address);
            $(".Team").val(EmployeeInfo.Team);
            $(".Role").val(EmployeeInfo.Role);
            $(".IsAdmin").val(EmployeeInfo.IsAdmin);
            $("#ViewEmployeeInfo").modal("show");

            _tempExistingEmailList = ExistingEmailList;
            _tempExistingMoblieNumberList = ExistingMoblieNumberList;

            const EmailIndex = _tempExistingEmailList.indexOf(EmployeeInfo.Email);
            if (EmailIndex > -1) {
                _tempExistingEmailList.splice(EmailIndex, 1); // 2nd parameter means remove one item only
            }

            const MobileNumberIndex = _tempExistingMoblieNumberList.indexOf(EmployeeInfo.MobileNumber.toString());
            if (MobileNumberIndex > -1) {
                _tempExistingMoblieNumberList.splice(MobileNumberIndex, 1); // 2nd parameter means remove one item only
            }


            $(".customErrorMessageUpdateEmployee").text("");
        }
    }
    else {

    }
}

$(document).on('click', '#UpdateEmployeeFromSubmit', function () {
    var Id = $(".EmployeeId").val().trim();
    var Name = $(".Name").val().trim();
    var Email = $(".Email").val().trim();
    var Number = $(".Number").val().trim();
    var dob = $(".dob").val();
    var doj = $(".doj").val();
    var Address = $(".Address").val().trim();
    var Team = $(".Team").val().trim();
    var Role = $(".Role").val().trim();
    var IsAdmin = $(".IsAdmin").val().trim();

    if (Name == "" || Email == "" || Number == "" || dob == "" || doj == "" || Address == "" || Team == "" || Role == "") {

        $(".customErrorMessageAddEmployee").text("All fields are Mandatory");

        if (Name == "") $(".Name").addClass("form-error");
        else $(".Name").removeClass("form-error");

        if (Email == "") $(".Email").addClass("form-error");
        else $(".Email").removeClass("form-error");

        if (Number == "") $(".Number").addClass("form-error");
        else $(".Number").removeClass("form-error");

        if (dob == "") $(".dob").addClass("form-error");
        else $(".dob").removeClass("form-error");

        if (doj == "") $(".doj").addClass("form-error");
        else $(".doj").removeClass("form-error");

        if (Address == "") $(".Address").addClass("form-error");
        else $(".Address").removeClass("form-error");

        if (Team == "") $(".Team").addClass("form-error");
        else $(".Team").removeClass("form-error");

        if (Role == "") $(".Role").addClass("form-error");
        else $(".Role").removeClass("form-error");
    } else {
        $("input[type='text']").removeClass("form-error");
        $("input[type='date']").removeClass("form-error");
        $("input[type='number']").removeClass("form-error");

        $(".customErrorMessageUpdateEmployee").text("");

        var IsEmailValid = false;

        var EmailValidation = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;

        if (EmailValidation.test(Email)) {
            IsEmailValid = true;
            $(".Email").removeClass("form-error");
            $(".customErrorMessageUpdateEmployee").text("");
        } else {
            $(".Email").addClass("form-error");
            $(".customErrorMessageUpdateEmployee").text("please enter valid email");
        }

        if (_tempExistingEmailList.includes(Email)) {
            $(".Email").addClass("form-error");
            $(".customErrorMessageAddEmployee").text("Email Id already exist");
            return;
        } else if (_tempExistingMoblieNumberList.includes(Number)) {
            $(".Number").addClass("form-error");
            $(".customErrorMessageAddEmployee").text("Mobile Number already exist");
            return;
        }

        if (IsEmailValid) {
            $("#UpdateEmployeeFromSubmit").attr("disabled", "disabed");
            $("#UpdateEmployeeFromSubmit").text("loading...");
            var data = '{Id:"' + Id + '",Name:"' + Name + '", Email:"' + Email + '", Number:' + Number + ', dob:"' + dob + '", doj:"' + doj + '", Address:"' + Address + '", Team:"' + Team + '", Role:"' + Role + '", IsAdmin:' + IsAdmin + '}';
            handleAjaxRequest(null, true, "/Method/UpdateEmployee", data, "CallBackUpdateEmployee", Name);
        }
    }
});

function CallBackUpdateEmployee(responseData, Name) {
    if (responseData.message.status == "success") {

        var EmployeeInfo = responseData.message.EmployeeInfo;

        if (EmployeeInfo != "undefined" && EmployeeInfo != null && EmployeeInfo != "") {
            $("td[data-id='" + EmployeeInfo.Id + "'].UpatedName").text(EmployeeInfo.Name);
            $("td[data-id='" + EmployeeInfo.Id + "'].UpatedEmail").text(EmployeeInfo.Email);
            $("td[data-id='" + EmployeeInfo.Id + "'].UpatedMobileNumber").text(EmployeeInfo.MobileNumber);
            $("td[data-id='" + EmployeeInfo.Id + "'].UpatedDOB").text(responseData.message.DateObject_dob);
            $("td[data-id='" + EmployeeInfo.Id + "'].UpatedDOJ").text(responseData.message.DateObject_doj);
            $("td[data-id='" + EmployeeInfo.Id + "'].UpatedAddress").text(EmployeeInfo.Address);
            $("td[data-id='" + EmployeeInfo.Id + "'].UpatedTeam").text(EmployeeInfo.Team);
            $("td[data-id='" + EmployeeInfo.Id + "'].UpatedRole").text(EmployeeInfo.Role);

            if (EmployeeInfo.IsAdmin == 1) {
                $("td[data-id='" + EmployeeInfo.Id + "'].PermissionType").text("Admin");
            }
            else {
                $("td[data-id='" + EmployeeInfo.Id + "'].PermissionType").text("Normal");
            }
        }
        $("#ViewEmployeeInfo").modal("hide");
    }
    else {
        $(".customErrorMessageUpdateEmployee").text("Error on Adding employee, Try agin in few min");
    }
    $("#UpdateEmployeeFromSubmit").removeAttr("disabled");
    $("#UpdateEmployeeFromSubmit").text("Update");
}

$(document).on('click', '.ViewEmployeeSalaryInfo', function () {
    var EmployeeId = $(this).attr("data-id");
    var data = '{EmployeeId:"' + EmployeeId + '"}';
    handleAjaxRequest(null, true, "/Method/GetEmployeeSalaryInfoByEmployeeId", data, "CallBackGetEmployeeSalaryInfoByEmployeeId");
});

function CallBackGetEmployeeSalaryInfoByEmployeeId(responseData) {
    if (responseData.message.status == "success") {

        var SalaryInfo = responseData.message.SalaryInfo;

        if (SalaryInfo != "undefined" && SalaryInfo != null) {
            $(".EmployeeId").val(SalaryInfo.EmployeeId);
            $(".Basic").val(SalaryInfo.Basic);
            $(".DA").val(SalaryInfo.DA);
            $(".HRA").val(SalaryInfo.HRA);
            $(".MedicalAllowances").val(SalaryInfo.MedicalAllowances);
            $(".ConveyanceCharges").val(SalaryInfo.ConveyanceCharges);
            $(".SpecialAllowances").val(SalaryInfo.SpecialAllowances);
        }
        $("#ViewEmployeeSalaryInfo").modal("show");
    }
    else {
        $(".customErrorMessageUpdateEmployee").text("Error on Adding employee, Try agin in few min");
    }
}

$(document).on('click', '#UpdateEmployeeSalaryInfoBtn', function () {
    var Basic = $(".Basic").val().trim();
    var DA = $(".DA").val().trim();
    var HRA = $(".HRA").val().trim();
    var MedicalAllowances = $(".MedicalAllowances").val().trim();
    var ConveyanceCharges = $(".ConveyanceCharges").val().trim();
    var SpecialAllowances = $(".SpecialAllowances").val().trim();
    var EmployeeId = $(".EmployeeId").val();

    if (Basic == "" || DA == "" || HRA == "" || MedicalAllowances == "" || ConveyanceCharges == "" || SpecialAllowances == "") {

        $(".customErrorMessageAddSalaryInfo").text("All fields are Mandatory");

        if (Basic == "") $(".Basic").addClass("form-error");
        else $(".Basic").removeClass("form-error");

        if (DA == "") $(".DA").addClass("form-error");
        else $(".DA").removeClass("form-error");

        if (HRA == "") $(".HRA").addClass("form-error");
        else $(".HRA").removeClass("form-error");

        if (MedicalAllowances == "") $(".MedicalAllowances").addClass("form-error");
        else $(".MedicalAllowances").removeClass("form-error");

        if (ConveyanceCharges == "") $(".ConveyanceCharges").addClass("form-error");
        else $(".ConveyanceCharges").removeClass("form-error");

        if (SpecialAllowances == "") $(".SpecialAllowances").addClass("form-error");
        else $(".SpecialAllowances").removeClass("form-error");
    } else {
        $("input[type='number']").removeClass("form-error");

        employeeSalaryInfo = {
            "EmployeeId": EmployeeId,
            "Basic": Basic,
            "DA": DA,
            "HRA": HRA,
            "MedicalAllowances": MedicalAllowances,
            "ConveyanceCharges": ConveyanceCharges,
            "SpecialAllowances": SpecialAllowances
        }
        $("#UpdateEmployeeSalaryInfoBtn").attr("disabled", "disabed");
        $("#UpdateEmployeeSalaryInfoBtn").text("loading...");
        var data = '{employeeSalaryInfo:' + JSON.stringify(employeeSalaryInfo) + '}';
        handleAjaxRequest(null, true, "/Method/UpdateEmployeeSalaryInfo", data, "CallBackUpdateEmployeeSalaryInfo");
    }
});

function CallBackUpdateEmployeeSalaryInfo(responseData) {
    if (responseData.message.status == "success") {
        $("#ViewEmployeeSalaryInfo").modal("hide");
    }
    else {
        swal("Error on Deleting Employee !", {
            icon: "danger",
        });
    }
    $("#UpdateEmployeeSalaryInfoBtn").removeAttr("disabled");
    $("#UpdateEmployeeSalaryInfoBtn").text("Update");
}

$(document).on('click', '#DeleteEmployeeInfo', function () {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this Employee",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var $target = $(this);
                var Id = $(this).attr("data-id");
                var data = '{Id:"' + Id + '"}';
                handleAjaxRequest(null, true, "/Method/DeleteEmployee", data, "CallBackDeleteEmployee", $target);
            }
        });
});

function CallBackDeleteEmployee(responseData, $target) {
    if (responseData.message.status == "success") {
        $target.parent().parent().remove();
    }
    else {
        swal("Error on Deleting Employee !", {
            icon: "danger",
        });
    }
}