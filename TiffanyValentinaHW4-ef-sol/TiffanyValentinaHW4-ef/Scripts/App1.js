
var uri = 'api/Query1';

//this function will load the cities
$(document).ready(function () {
    $("#storescount").empty();
    GetShowData();

});

function GetShowData() {
    // Send an AJAX request
    $.getJSON(uri + "/StoresNames", function (data) {
            // On success, 'data' contains a list of City and Count.
            $('<li>', { text: "City:" + data + ",Count: " + data  }).appendTo($('#storescount'));
            $.each(data, function (key, item) {
                // Add a list item for City and Count.
                $('<li>', { text: formatItem(item) }).appendTo($('#storescount'));
            });
        });
}

