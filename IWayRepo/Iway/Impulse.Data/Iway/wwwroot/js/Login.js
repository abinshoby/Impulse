(function ($) {
    $.fn.serializeFormJSON = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };
})(jQuery);

//$("#SignUpForm").on("submit", function (event) {
//    $('#loading-image').show();
//    var $this = $(this);
//    var frmValues = $(this).serializeFormJSON();
//    console.log(frmValues);
//    $.ajax({
//        type: $this.attr('method'),
//        url: $this.attr('action'),
//        data: frmValues
//    })
//        .done(function () {
//            $('#loading-image').hide();
//           // $("#para").text("Welcome " + frmValues.Company + "!! . Thanks for signing up for iWAY! We're excited to have you onboard.");
//        })
//        .fail(function () {
//            $("#para").text("An error occured");
//        });
//    event.preventDefault();
//});
//$("#SignUpFormVendor").on("submit", function (event) {
//    $('#loading-image').show();
//    var $this = $(this);
//    var frmValues = $(this).serializeFormJSON();
//    console.log(frmValues);
//    $.ajax({
//        type: $this.attr('method'),
//        url: $this.attr('action'),
//        data: frmValues
        
//    })
//        .done(function () {
//            $('#loading-image').hide();
//            //$("#para").text("Welcome " + frmValues.Company + "!! . Thanks for signing up for iWAY! We're excited to have you onboard.");
//        })
//        .fail(function () {
//            $("#para").text("An error occured");
//        });
//    event.preventDefault();
//});
//$("#SignInForm").on("submit", function (event) {
//    $('#loading-image').show();
//    var $this = $(this);
//    var frmValues = $(this).serializeFormJSON();

//    $.ajax({
//        type: $this.attr('method'),
//        url: $this.attr('action'),
//        data: frmValues       
//    })
//        .done(function (data) {
      
//            $('#loading-image').hide();
//        })
//        .fail(function () {
//        });
//    event.preventDefault();
//});
$('#loading-image').bind('ajaxStart', function () {
    $(this).show();
}).bind('ajaxStop', function () {
    $(this).hide();
});
