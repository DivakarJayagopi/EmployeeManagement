$(document).ready(function () {
    $('#employeeListTable').DataTable();
});


$(document).on('click', '#EditEmployeeInfo', function () {
    $("#ViewEmployeeInfo").modal("show");
})

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
                swal("Poof! Employee has been deleted!", {
                    icon: "success",
                });
            } 
        });
});