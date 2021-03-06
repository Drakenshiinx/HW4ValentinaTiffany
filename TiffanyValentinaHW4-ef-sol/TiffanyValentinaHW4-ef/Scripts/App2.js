var uri = '/api/Query'

//this loads the two drop downs: One in Employees and one in Stores
DropDownLoad();

//A function that loads the list of cities in dropdown
function DropDownLoad() {
    GetEmployeeNames();
    GetCityList();
}

//this function will load the cities
$(document).ready(function () {
    $("#storescount").empty();
    GetShowData();

});

//query 1 AJAX function
function GetShowData() {
    // Send an AJAX request
    $.getJSON(uri + "/StoresNames", function (data) {
        // On success, 'data' contains a list of City and Count.
        $('<li>', { text: "City:" + data + ",Count: " + data }).appendTo($('#storescount'));
        $.each(data, function (key, item) {
            // Add a list item for City and Count.
            $('<li>', { text: formatItem(item) }).appendTo($('#storescount'));
        });
    });
}

//an AJAX Get for employees (first and last name) that populates in dropdown
function GetEmployeeNames() {
    $.get(uri + "/EmployeeNames", function (data) {

        for (var i = 0; i < data.length; i++) {
            var x = document.createElement("option");
            x.textContent = data[i].EmployeeFirstLastName;
            x.value = data[i].SalesPersonID;
            document.getElementById("namedropdown").appendChild(x);
        }

    });
}

//An AJAX Get employee sales for the year
function GetEmployeeSales() {
    var x = document.getElementById("namedropdown");
    var empID = x.value;

    $.get(uri + "/EmployeeSales/" + empID, function (data) {
        var success = "This employee sold $" + data + " of CDs for the year";
        $("#salesByEmployee").text(success);
    }).fail(function (err) {
        alert('Data failed to load');
    });
};

//An AJAX Get for stores (city) that populates in dropdown
function GetCityList() {
    $.get(uri + "/StoreCity", function (data) {

        for (var i = 0; i < data.length; i++) {
            var x = document.createElement("option");
            x.textContent = data[i].City;
            x.value = data[i].StoreID;
            document.getElementById("citydropdown").appendChild(x);
        }

    });
}

//AJAX Get store sales for the year
function GetTotalStoreSales() {
    var x = document.getElementById("citydropdown");
    var storeId = x.value;

    $.get(uri + "/StoreSales/" + storeId, function (data) {
        var success = "This store sold  $" + data + " of CDs for the year.";
        $("#salesByStore").text(success);
    }).fail(function (error) {
        alert('Data failed to load');
    });
};