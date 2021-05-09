var uri = 'https://localhost:44311/api/Query2And3'

//this loads the two drop downs: One in Employees and one in Stores
PageLoad();

//A function that loads the list of cities in dropdown
function PageLoad() {
    GetEmployeeNames();
    GetCityList();
}

//an AJAX Get for employees (first and last name) that populates in dropdown
function GetEmployeeNames() {
    $.get(uri + "/EmployeeNames", function (data, status) {

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

    $.get(uri + "/EmployeeSales/" + empID, function (data, status) {
        var success = "This employee sold $" + data + " of CDs for the year";
        $("#salesByEmployee").text(success);
    }).fail(function (err) {
        alert('Data failed to load');
    });
};

//An AJAX Get for stores (city) that populates in dropdown
function GetCityList() {
    $.get(uri + "/StoreCity", function (data, status) {

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

    $.get(uri + "/StoreSales/" + storeId, function (data, status) {
        var success = "This store sold  $" + data + " of CDs for the year.";
        $("#salesByStore").text(success);
    }).fail(function (err) {
        alert('Data failed to load');
    });
};