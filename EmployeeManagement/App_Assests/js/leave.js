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
        var data = '{Type:"' + LeaveType + '", Title:"' + Title + '", FromDate:"' + FromDate + '", ToDate:"' + ToDate + '", Description:"' + Description + '", Status:"pending"}';
        handleAjaxRequest(null, true, "/Method/AddLeaveRequest", data, "CallBackAddLeaveRequest");
    }
});

$(document).on('click', '.SelectMonth', function () {
    var SelectedDate = $(".LeaveRequestMonth").val();
    if (SelectedDate != "undefined" && SelectedDate != null && SelectedDate != "") {
        $(".LeaveRequestListBody").html("");
        $(".SelectMonth").attr("disabled", "disabed");
        $(".SelectMonth").text("Loading..");
        var data = '{SelectedDate:"' + SelectedDate + '"}';
        handleAjaxRequest(null, true, "/Method/GetLeaveRquestByDate", data, "CallBackGetLeaveRquestByDate");
    }
});

function CallBackGetLeaveRquestByDate(responseData) {
    if (responseData.message.status == "success") {
        var LeaveRequestList = responseData.message.LeaveRequestList;
        if (LeaveRequestList != "undefined" && LeaveRequestList != null && LeaveRequestList != "") {
            var RequestListHTML = "";
            $.each(LeaveRequestList, function (key, value) {
                var Comments = value.Comments;
                if (typeof (Comments) == "undefined" || Comments == null || Comments == null) {
                    Comments = "";
                }
                RequestListHTML += "<tr>";
                RequestListHTML += "<td>" + value.Name + "</td>";
                if (IsAdmin == "1") {
                    RequestListHTML += "<td> KT-" + value.EmployeeCode + "</td>";
                }
                RequestListHTML += "<td>" + value.Team + "</td>";
                RequestListHTML += "<td>" + value.Title + "</td>";
                RequestListHTML += "<td>" + value.FromDateString + "</td>";
                RequestListHTML += "<td>" + value.ToDateString + "</td>";
                RequestListHTML += "<td>" + value.Description + "</td>";
                RequestListHTML += "<td>" + value.Status + "</td>";
                value.Comments = (typeof (value.Comments) == "undefined" || value.Comments == null || value.Comments == "") ? "" : value.Comments;
                RequestListHTML += "<td>" + value.Comments + "</td>";
                RequestListHTML += "/<tr>";
            });
            
        } else {
            RequestListHTML += "<tr>";
            RequestListHTML += "<td colspan=8 style='text-align:center;'> No data available in table </td>";
            RequestListHTML += "/<tr>";
        }
        $(".LeaveRequestListBody").html(RequestListHTML);
    }
    else {

    }
    $(".SelectMonth").removeAttr("disabled", "disabed");
    $(".SelectMonth").text("Submit");
    $("#leaveRequestList").show("");
}

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
    var Id = $(this).attr("data-id");
    var EmployeeId = $(this).attr("data-employeeId");
    $(".SelectedId").val(Id);
    $(".SelectedEmployeeId").val(EmployeeId);
    $("#leaveRejectReason").modal("show");
});

$(document).on('click', '.RejectLeaveRequestButton', function () {
    var Id = $(".SelectedId").val();
    var EmployeeId = $(".SelectedEmployeeId").val();    
    var Reason = $(".LeaveRejectionReason").val().trim();
    if (Reason != "undefined" && Reason != null && Reason != "") {
        $(".LeaveRejectionReason").removeClass("form-error");
        $(".LeaveRejectionErrorMessage").text("");
        $("#leaveRejectReason").modal("hide");
        var data = '{Id:"' + Id + '", Status:"Rejected", EmployeeId:"' + EmployeeId + '", Reason:"' + Reason + '"}';
        handleAjaxRequest(null, true, "/Method/UpdateLeaveRequest", data, "CallBackUpdateLeaveRequest", Id);
        $("#leaveRejectReason").modal("show");
    } else {
        $(".LeaveRejectionReason").addClass("form-error");
        $(".LeaveRejectionErrorMessage").text("Enter Valid Reason for rejection");
    }
});

function CallBackUpdateLeaveRequest(responseData, Id) {
    if (responseData.message.status == "success") {
        var $target = $("a[data-id='" + Id + "']#acceptLeaveRequest").parent().parent().remove();
    }
    else {

    }
}

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
                var Id = $(this).attr("data-id");
                var EmployeeId = $(this).attr("data-employeeId");
                var data = '{Id:"' + Id + '", Status:"Accepted", EmployeeId:"' + EmployeeId + '", Reason:"Your Leave Request has been Accepcted"}';
                handleAjaxRequest(null, true, "/Method/UpdateLeaveRequest", data, "CallBackUpdateLeaveRequest", Id);
            }
        });
});