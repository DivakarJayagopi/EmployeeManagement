$(document).on('click', '.AddNewsFromSubmit', function () {
    var Title = $(".Title").val();
    var Description = $(".Description").val();
    var img = images;
    var EmployeeIds = $("#EmployeesList").select2("val");
    $(".AddNewsCustomErrorMessage").text("");
    if (Title == "" || Description == "") {

        if (Title == "") $(".Title").addClass("form-error");
        else $(".Title").removeClass("form-error");

        if (Description == "") $(".Description").addClass("form-error");
        else $(".Description").removeClass("form-error");
    } else {
        $("input[type='text']").removeClass("form-error");
        $("input[type='file']").removeClass("form-error");
        $(".Description").removeClass("form-error");
        $("textarea").removeClass("form-error");

        $(".AddNewsFromSubmit").attr("disabled", "disabed");
        $(".AddNewsFromSubmit").text("loading.....");

        var data = "{Title:'" + Title + "', Description:'" + Description + "', image:'" + img + "', EmployeeIds:" + JSON.stringify(EmployeeIds) + "}";
        handleAjaxRequest(null, true, "/Method/AddNews", data, "CallBackAddNews");
    }
});

function CallBackAddNews(responseData) {
    if (responseData.message.status == "success") {
        $("input[type='text']").val("");
        $("input[type='file']").val("");
        $("textarea.Description").val("");
        $("#EmployeesList").val('').change();
        swal({
            title: "Success",
            text: "Nes Added SuccessFully",
            icon: "success",
        });
    }
    else {
        $(".AddNewsCustomErrorMessage").text("Error on Adding News, Try agin in few min");
    }
    $(".AddNewsFromSubmit").removeAttr("disabled", "disabed");
    $(".AddNewsFromSubmit").text("Submit");
}

var images = "";
function LoadImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onloadend = function () {
            var Source = reader.result;
            images = Source;
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$(document).on('click', '#DeleteNews', function () {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this News",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var Id = $(this).attr("data-id");
                var data = "{Id:'" + Id + "'}";
                handleAjaxRequest(null, true, "/Method/DeleteNewsById", data, "CallBackDeleteNewsById",$(this));
            }
        });
});

function CallBackDeleteNewsById(responseData, $target) {
    if (responseData.message.status == "success") {
        $target.parent().parent().remove();
    }
    else {
        swal("Error on Deleting News !", {
            icon: "danger",
        });
    }
}