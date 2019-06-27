(function ($) {
    loadVendorProfile($("#hdnUserId").val());
    loadGrid($("#hdnUserId").val());
})(jQuery);



function loadVendorProfile(Id) {
    $('#overlay img').show();
    var ajaxCallURL = '/Dashboard/_addMoreVendorInterest?Uid=' + Id;
    $.ajax({
        type: "GET",
        url: ajaxCallURL,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (data) {
            if (data != null) {
                $("#ProfileCompletionDiv").show();
                $("#ProfileCompletionDiv").html(data);
                $('#overlay img').hide();
            }
            else {
                $("#ProfileCompletionDiv").hide();
            }

        },
        error: function () {

        }
    });
}
function loadGrid(Id) {
   
    $('#overlayProfile img').show();
    var ajaxCallURL = '/Dashboard/GetVendorInterest?Uid=' + Id;
    $.ajax({
        type: "GET",
        url: ajaxCallURL,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (data) {
            $("#griddiv").html(data);
            $('#overlayProfile img').hide();
        },
        error: function () {

        }
    });
}

