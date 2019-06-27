$("#SaveEventForm").on("submit", function (event) {
    $('#loading-image').show();
    var $this = $(this);
    var frmValues = $(this).serializeFormJSON();
    console.log(frmValues);
    $.ajax({
        type: $this.attr('method'),
        url: $this.attr('action'),
        data: frmValues,
        dataType: "json"
    })
        .done(function (data) {
        })
        .fail(function () {
        });
    event.preventDefault();
});