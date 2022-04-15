$(document).ready(function () {
    $('#newsListTable').DataTable();
});

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
                swal("Poof! News has been deleted!", {
                    icon: "success",
                });
            }
        });
});