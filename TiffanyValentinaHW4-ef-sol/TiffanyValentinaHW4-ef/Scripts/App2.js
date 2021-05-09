var uri = 'https://localhost:44311/api/Query2'

//this loads the drop down for employees
DropDownListLoad();

//this function loads the lists of employees in dropdown

function DropDownListLoad() {
    GetEmployeeNames();
}

//an AJAX Get for employees (first and last name) that populates in the drop down
function GetEmployeeNames() {
    $.get(uri + "/EmployeeNames", function (data) {

        for (var i = 0; i < data.length; i++) {
            var x = document.createElement("option");
            x.textContent = data[i].EmployerFirstLastName;
            x.value = data[i].SalesPersonID;
            document.getElementById("namedropdown").appendChild(x);
        }
    })
}