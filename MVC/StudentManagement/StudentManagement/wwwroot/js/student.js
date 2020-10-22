var student = student || {};

student.delete = function (id) {
    bootbox.confirm({
        title: "Cảnh báo",
        message: "Bạn có muốn xóa nhân viên này không?",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Không'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Có'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: `/Home/Delete/${id}`,
                    method: "GET",
                    contentType: 'json',
                    success: function (data) {
                        if (data.deleteResult > 0) {
                            window.location.href = "/";
                        }
                    }
                });
            }
        }
    });
}

$(document).ready(function () {
    $('#tbStudent').dataTable({
        "columnDefs": [
            {
                "targets": 0,
                "searchable": false,
            },
            {
                "targets": 3,
                "orderable": false
            },
            {
                "targets": 4,
                "orderable": false
            },
            {
                "targets": 5,
                "searchable": false,
                "orderable": false
            }
        ]
        }
    );
});