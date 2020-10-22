var cake = cake || {};

cake.delete = (id) => {
    bootbox.confirm({
        title: "Cảnh báo!!!",
        message: "<p class=\"text-danger\">Bạn có muốn xóa sách này không?</p>",
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
                        if (data.delCake > 0) {
                            window.location.href = "/";
                        }
                    }
                });
            }
        }
    });
}

$(document).ready(function () {
    $('#tbBook').dataTable({
        "columnDefs": [
            {
                "targets": 0,
                "searchable": false,
            },
            {
                "targets": 8,
                "orderable": false,
                "searchable": false
            },
        ]
    }
    );
});