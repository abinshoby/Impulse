(function ($) {
    loadCitizenProfile($("#hdnUserId").val());
    
})(jQuery);



function loadCitizenProfile(Id) {
    $('#overlay img').show();
    var ajaxCallURL = '/Dashboard/loadCitizenProfile?Uid=' + Id;
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

