(function ($) {

    loadGrid($("#hdnUserId").val());

})(jQuery);

function loadGrid(Id) {
    $('#overlay img').show();
    var ajaxCallURL = '/Dashboard/GetCorporateEventList?Uid=' + Id;
    $.ajax({
        type: "GET",
        url: ajaxCallURL,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (data) {
            console.log(data);

            $("#griddiv").html(data);
            $("#contentBtn").hide();
            $('#overlay img').hide();
            $("#topBtn").show();
        },
        error: function () {
            $("#topBtn").hide();
            $("#contentBtn").show(); $('#overlay img').hide();
         
        }
    });
}
function EventClick(id) {
    window.location.href = '/Event/CoEventDetails?id=' + id;
}
function NewEventForm(id) {
    $('#user_content').load('@Url.Action("UserDetails","User")');
    var ajaxCallURL = '/Event/_AddEvent?corporateId=' + id;
    $.ajax({
        type: "GET",
        url: ajaxCallURL,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (data) {
            $("#indexDiv").hide();
            $("#NewRequestDiv").show();
            $("#NewRequestDiv").html(data);
        },
        error: function () {
            alert("Content load failed.");
        }
    });

}

