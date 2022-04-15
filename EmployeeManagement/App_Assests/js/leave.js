$(document).ready(function () {
    $('#leaveRequestListTable').DataTable();
});

$(document).on('click', '#rejectLeaveRequest', function () {
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
                swal("Poof! Leave Request has been Accepted", {
                    icon: "success",
                });
            }
        });
});