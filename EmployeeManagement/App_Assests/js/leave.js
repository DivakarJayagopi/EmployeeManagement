$(document).on('click', '#AddLeaveRequestFromSubmit', function () {
    var LeaveType = $(".LeaveType").val().trim();
    var FromDate = $(".FromDate").val().trim();
    var ToDate = $(".ToDate").val().trim();
    var Title = $(".Title").val().trim();
    var Description = $(".Description").val().trim();

    if (LeaveType == "" || FromDate == "" || ToDate == "" || Title == "" || Description == "") {

        $(".customErrorMessageAddEmployee").text("All fields are Mandatory");

        if (LeaveType == "") $(".LeaveType").addClass("form-error");
        else $(".LeaveType").removeClass("form-error");

        if (FromDate == "") $(".FromDate").addClass("form-error");
        else $(".FromDate").removeClass("form-error");

        if (ToDate == "") $(".ToDate").addClass("form-error");
        else $(".ToDate").removeClass("form-error");

        if (Title == "") $(".Title").addClass("form-error");
        else $(".Title").removeClass("form-error");

        if (Description == "") $(".Description").addClass("form-error");
        else $(".Description").removeClass("form-error");
    } else {
        $("input[type='text']").removeClass("form-error");
        $("input[type='date']").removeClass("form-error");
        $("input[type='date']").removeClass("form-error");
        $("textarea").removeClass("form-error");

        $(".customErrorMessageAddLeaveRequest").text("");
        var data = '{Type:"' + LeaveType + '", Title:"' + Title + '", FromDate:"' + FromDate + '", ToDate:"' + ToDate + '", Reason:"' + Description + '", Status:"pending"}';
        handleAjaxRequest(null, true, "/Method/AddLeaveRequest", data, "CallBackAddLeaveRequest");
    }
});

$(document).on('change', '.FromDate', function () {
    var FromDateValue = $(this).val();
    $(".ToDate").attr("min", FromDateValue);
    $(".ToDate").attr("value", "");
    $(".ToDate").val("");
});

function CallBackAddLeaveRequest(responseData) {
    if (responseData.message.status == "success") {
        $("input[type='text']").val("");
        $("input[type='date']").val("");
        $("input[type='number']").val("");
        $("textarea").val("");
        swal({
            title: "Success",
            text: "Your Leave Request Added SuccessFully",
            icon: "success",
        });
    }
    else {
        $(".customErrorMessageAddLeaveRequest").text("Error on Adding leave request, Try agin in few min");
    }
}

$(document).on('click', '#rejectLeaveRequest', function () {
    $("#leaveRejectReason").modal("show");
});

$(document).on('click', '#RejectLeaveRequestButton', function () {
    var data = '{Id:"' + Id + '", Status:"Rejected", EmployeeId:' + EmployeeId + ', Reason:' + Reason + '}';
    handleAjaxRequest(null, true, "/Method/UpdateLeaveRequest", data, "CallBackAddLeaveRequest");
    $("#leaveRejectReason").modal("show");
});

$(document).on('click', '#acceptLeaveRequest', function () {
    swal({
        title: "Are you sure?",
        text: "Do you want Accept this Request",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var data = '{Id:"' + Id + '", Status:"Accepted", EmployeeId:' + EmployeeId + ', Reason:""}';
                handleAjaxRequest(null, true, "/Method/UpdateLeaveRequest", data, "CallBackAddLeaveRequest");
            }
        });
});