
var uri = 'api/Query1';

$(document).ready(function () {
    $('#saveResponse').text = '';
    $("#notes").empty();
    GetShowData();

});

function GetShowData() {
    // Send an AJAX request
    $.getJSON(uri)
        .done(function (data) {
            // On success, 'data' contains a list of products.
            $('<li>', { text: "Priority: Subject => Details" }).appendTo($('#notes'));
            $.each(data, function (key, item) {
                // Add a list item for the product.
                $('<li>', { text: formatItem(item) }).appendTo($('#notes'));
            });
        });
}

function formatItem(item) {
    return item.Priority + ':   ' + item.Subject + ' =>  ' + item.Details;
}

function find() {
    $('#saveResponse').text = '';
    $("#notes").empty();
    var id = $('#SearchId').val();
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            $('#note').text(formatItem(data));
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#note').text('Error: ' + err);
        });
};