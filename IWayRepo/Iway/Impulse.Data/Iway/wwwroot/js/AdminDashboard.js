(function ($) {
    loadGrid($("#hdnUserId").val());
})(jQuery);

function loadGrid(Id) {
    $('#overlay img').show();
    var ajaxCallURL = '/Dashboard/GetAdminEventList';
    $.ajax({
        type: "GET",
        url: ajaxCallURL,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (data) {
            $("#griddiv").html(data);
            $('#overlay img').hide();

        },
        error: function () {
            alert("Content load failed.");
        }
    });
}

