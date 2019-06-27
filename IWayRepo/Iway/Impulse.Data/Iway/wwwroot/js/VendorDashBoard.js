(function ($) {
    loadVendorProfile($("#hdnUserId").val());
    loadGrid($("#hdnUserId").val());
})(jQuery);

function loadGrid(Id) {
    $('#overlay img').show();
    var ajaxCallURL = '/Dashboard/GetVendorEventList?Uid=' + Id;
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
           
        }
    });
}

function loadVendorProfile(Id) {
    $('#overlay img').show();
    var ajaxCallURL = '/Dashboard/GetVendorProfileCompletion?Uid=' + Id;
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

