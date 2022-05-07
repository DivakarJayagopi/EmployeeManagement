

$(document).on('click', '.UpdatePassword', function () {
    var EmployeeOldPassword = $(".EmployeeOldPassword").val();
    var EmployeeOldPasswordField = $(".EmployeeOldPasswordField").val();
    var NewPassword = $(".NewPassword").val();
    var ConfirmPassword = $(".ConfirmPassword").val();

    if (EmployeeOldPasswordField == "" || NewPassword == "" || ConfirmPassword == "") {

        $(".customErrorMessageAddEmployee").text("All fields are Mandatory");

        if (EmployeeOldPasswordField == "") $(".EmployeeOldPasswordField").addClass("form-error");
        else $(".EmployeeOldPasswordField").removeClass("form-error");

        if (NewPassword == "") $(".NewPassword").addClass("form-error");
        else $(".NewPassword").removeClass("form-error");

        if (ConfirmPassword == "") $(".ConfirmPassword").addClass("form-error");
        else $(".ConfirmPassword").removeClass("form-error");
    } else {
        $("input[type='text']").removeClass("form-error");
        $("input[type='password']").removeClass("form-error");
        var IsValid = false;

        if (EmployeeOldPassword != EmployeeOldPasswordField) {
            $(".EmployeeOldPasswordField").addClass("form-error");
            $(".customErrorMessageChangePassword").text("Incorrect Old password");
        } else {
            $(".EmployeeOldPasswordField").removeClass("form-error");
            IsValid = true;
        }

        if (IsValid) {
            if (NewPassword != ConfirmPassword) {
                $(".NewPassword").addClass("form-error");
                $(".ConfirmPassword").addClass("form-error");
                $(".customErrorMessageChangePassword").text("Password did not match");
            } else {
                $(".NewPassword").removeClass("form-error");
                $(".ConfirmPassword").removeClass("form-error");
                IsValid = true;
            }
        }

        if (IsValid) {
            var data = '{Password:"' + NewPassword + '"}';
            handleAjaxRequest(null, true, "/Method/UpdatePassword", data, "CallBackUpdatePassword", NewPassword);
        }
    }
});

function CallBackUpdatePassword(responseData, NewPassword) {
    if (responseData.message.status == "success") {
        $(".EmployeeOldPassword").val(NewPassword);
        $("input[type='text']").val("");
        $("input[type='password']").val("");
        swal({
            title: "Success",
            text: "Password Updated SuccessFully",
            icon: "success",
        });
    }
    else {

    }
}

$(document).on('click', '.UpdateProfileImage', function () {
    $(".EmployeeProfileImage").trigger('click');
});

$(document).on('click', '.UpdateEmployeeInfo', function () {
    var Name = $(".Name").val();
    var MobileNumber = $(".MobileNumber").val();
    var Email = $(".Email").val();
    var Address = $(".Address").val();
    if (Name == "" || MobileNumber == "" || Email == "" || Address == "") {

        $(".customErrorMessageAddEmployee").text("All fields are Mandatory");

        if (Name == "") $(".Name").addClass("form-error");
        else $(".Name").removeClass("form-error");

        if (MobileNumber == "") $(".MobileNumber").addClass("form-error");
        else $(".MobileNumber").removeClass("form-error");

        if (Email == "") $(".Email").addClass("form-error");
        else $(".Email").removeClass("form-error");

        if (Address == "") $(".Address").addClass("form-error");
        else $(".Address").removeClass("form-error");
    } else {
        $("input[type='text']").removeClass("form-error");
        $("input[type='number']").removeClass("form-error");
        $("input[type='password']").removeClass("form-error");
        var data = "{Name:'" + Name + "', Number:" + MobileNumber + ", Email:'" + Email + "', Address:'" + Address + "', ProfileImage:'" + images + "'}";
        handleAjaxRequest(null, true, "/Method/UpdateEmployeeInfo", data, "CallBackUpdateEmployeeInfo");
    }
});

function CallBackUpdateEmployeeInfo(responseData) {
    if (responseData.message.status == "success") {
        var EmployeeInfo = responseData.message.EmployeeInfo;
        if (typeof (EmployeeInfo) != undefined && EmployeeInfo != null) {
            $(".EmployeeName").text(EmployeeInfo.Name);
            $(".Profileimage").attr("src",EmployeeInfo.ProfileImage);
        }
        swal({
            title: "Success",
            text: "Updated SuccessFully",
            icon: "success",
        });
    }
    else {

    }
}

$(document).on('click', '.ForgotPassword', function () {
    $(".customSuccesMessage").text("");
    $(".customErrorMessageForgotPassword").text("");
    var Email = $(".Email").val().trim();
    if (Email == "") {
        if (Email == "") $(".Email").addClass("form-error");
        else $(".Email").removeClass("form-error");
    } else {
        $("input[type='text']").removeClass("form-error");

        var IsEmailValid = false;

        var EmailValidation = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;

        if (EmailValidation.test(Email)) {
            IsEmailValid = true;
            $(".Email").removeClass("form-error");
            $(".customErrorMessageForgotPassword").text("");
        } else {
            $(".Email").addClass("form-error");
            $(".customErrorMessageForgotPassword").text("please enter valid email");
        }

        if (IsEmailValid) {
            $(".customErrorMessageForgotPassword").text("");
            var data = '{Email:"' + Email + '"}';
            handleAjaxRequest(null, true, "/Method/ForgotPassword", data, "CallBackForgotPassword");
        }
    }
});

function CallBackForgotPassword(responseData) {
    if (responseData.message.status == "success") {
        $(".customSuccesMessage").text("Password has been send to register Email");
    }
    else {
        $(".customErrorMessageForgotPassword").text(responseData.message.errorMessage);
    }
}

var images = "";
function LoadImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onloadend = function () {
            var Source = reader.result;
            images = Source;
            $(".Profileimage").attr("src", images);
        }
        reader.readAsDataURL(input.files[0]);
    }
}