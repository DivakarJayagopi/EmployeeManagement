
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
        }

        if (IsEmailValid) {
            var data = '{Name:"' + Name + '", Email:"' + Email + '", Number:' + Number + ',Password :"Welcome@123", dob:"' + dob + '", doj:"' + doj + '", Address:"' + Address + '", Team:"' + Team + '", Role:"' + Role + '", IsAdmin:' + IsAdmin + '}';
            handleAjaxRequest(null, true, "/Method/AddEmployee", data, "CallBackAddEmployee", Name);
        }
    }

});

function CallBackAddEmployee(responseData, Name) {
    if (responseData.message.status == "success") {
        $("input[type='text']").val("");
        $("input[type='date']").val("");
        $("input[type='number']").val("");
        swal({
            title: "Success",
            text: "Employee " + Name + " Added SuccessFully",
            icon: "success",
        });
    }
    else {
        $(".customErrorMessageAddEmployee").text("Error on Adding employee, Try agin in few min");
    }
}




    
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
        }
        $("#ViewEmployeeInfo").modal("hide");
    }
    else {
        $(".customErrorMessageUpdateEmployee").text("Error on Adding employee, Try agin in few min");
    }
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
        //swal("Poof! Employee has been deleted!", {
        //    icon: "success",
        //});
    }
    else {
        swal("Error on Deleting Employee !", {
            icon: "danger",
        });
    }
}