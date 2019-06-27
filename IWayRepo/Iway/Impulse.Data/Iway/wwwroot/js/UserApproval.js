(function ($) {
    $("#btnApprove").on("click", function () {
        $('#loading-image').show();
        var options = {};
        options.url = "ApproveVendor?id=" + $(this).siblings('input').val();
        options.type = "POST";
        options.dataType = "json";      
        options.contentType = "application/json";
        options.success = function (msg) {
            $('#loading-image').hide();
            location.reload();
        };
        $.ajax(options);


    });
})(jQuery);


