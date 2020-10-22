var create = create || {};
var provinceId = 0;
create.changeProvince = function (id) {
    create.drawDistrict(id);
}

create.drawDistrict = function (provinceId) {
    $.ajax({
        url: `/Home/Districts/${provinceId}`,
        method: "GET",
        contentType: "json",
        success: function (data) {
            $("#DistrictId").empty();
            $.each(data.districts, function (i, v) {
                $("#DistrictId").append(`
                    <option value=${v.id}>${v.districtName}</option>
                `);
            });
        }
    });
    $("#WardId").empty();
}

create.changeDistrict = function (id) {
    provinceId = $("#ProvinceId").val();
    create.drawWard(id, provinceId);
}

create.drawWard = function (districtId, provinceId) {
    $.ajax({
        url: `/Home/Wards/${districtId}/${provinceId}`,
        method: "GET",
        contentType: "json",
        success: function (data) {
            $("#WardId").empty();
            $.each(data.wards, function (i, v) {
                $("#WardId").append(`
                    <option value=${v.id}>${v.wardName}</option>
                `);
            });
        }
    });
}