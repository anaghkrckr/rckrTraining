// const base_url = "http://staffapi.dev.grcdev.com/api/staff"
const base_url = "https://localhost:44332/api/staff"
let deleteList = []
let mainStaffList = []
let pagedStaffList = []
let staffTypes = {
    Teacher: "Subject",
    Administrator: "Administrator Department",
    Support: "Support Department"
}
let tableHead = { staffId: "Id", staffName: "Name", staffAge: "Age", staffType: "Type", department: "Department" }

let currentPage = 0;

const formStaffLists = document.getElementById("formStaffs");
formStaffLists.addEventListener("change", handleFormStaffListChange);

const form = document.querySelector('form');
form.addEventListener('submit', formSubmitHandler);

const staffOptions = document.getElementById("staffs")
staffOptions.addEventListener("change", () => handleMainStaffListChange(staffOptions))

main();


async function main() {
    Object.keys(staffTypes).forEach((key) => {
        var option = document.createElement("OPTION");
        option.textContent = key;
        option.value = key;
        staffOptions.appendChild(option);
    })

    let staffType = staffOptions.value;
    fetchStaff(base_url, staffType)
}


async function fetchStaff(url, staffType) {
    let fetchUrl = url;
    if (staffOptions.value != "All") {
        fetchUrl = base_url + "/" + staffType;
        staffOptions.value = staffType;
    }

    let response = await fetch(fetchUrl)
    var data = await response.json();
    mainStaffList = data;
    rednderTableHelper(data)
}

function rednderTableHelper(data) {
    pagedStaffList = paginate(data)
    if (currentPage >= pagedStaffList.length && pagedStaffList.length > 0) {
        currentPage = pagedStaffList.length - 1;
    }
    renderStaffTable(pagedStaffList, currentPage);
}


function sortStaffList(staffList, item) {
    staffList.sort(function(a, b) { return (a[item] > b[item]) ? 1 : ((b[item] > a[item]) ? -1 : 0); });
    rednderTableHelper(staffList);
}

function renderStaffTable(data, pageNumber) {
    var headItems = tableHead;
    let table = document.getElementById("staffTable");
    table.innerHTML = ""
    let departmentName = staffTypes[staffOptions.value] ? staffTypes[staffOptions.value] : "Department";
    var header = table.createTHead();
    var row = header.insertRow(0);
    headItems.department = departmentName;
    Object.keys(headItems).forEach((key) => {
        var cell = row.insertCell(-1)
        cell.innerHTML = `<b>` + headItems[key];
        cell.onclick = () =>
            sortStaffList(mainStaffList, key)
    })
    row.insertCell(-1).innerHTML = `<b>Delete`;
    const staffPages = document.getElementById("staffPAgesList")
    staffPages.innerHTML = ""
    if (data[pageNumber] == null) {
        return;
    }
    data[pageNumber].forEach(staff => {
        var row = table.insertRow(-1);
        row.onclick = function() {
            updateForm(staff)
        }
        Object.keys(staff).forEach(function(key, i) {
            row.insertCell(i).innerHTML = staff[key]
        })
        var but = document.createElement("button");
        but.className = "btn btn-danger";
        but.textContent = "DELETE";
        but.onclick = function() {
            delStaffHelper(staff)
        };
        row.insertCell(-1).appendChild(but);
        var check = document.createElement("input")
        check.type = "checkbox";
        check.className = "form-check-input";
        check.id = staff.staffId;
        check.onclick = () => {
            event.stopImmediatePropagation()
            deleteList.push(staff);
        }
        row.insertCell(-1).appendChild(check);
    })


    pagedStaffList.forEach((item, i) => {
        var li = document.createElement("li");
        li.className = "page-link";
        var id = "page-" + (i + 1)

        li.id = id;
        li.onclick = () => {
            currentPage = i
            renderStaffTable(pagedStaffList, currentPage);
            document.getElementById(id).style.backgroundColor = "red"
        }
        li.textContent = i + 1
        staffPages.appendChild(li)

    })
    var pageId = "page-" + (pageNumber + 1)
    document.getElementById(pageId).style.backgroundColor = "red"
}


function formSubmitHandler() {
    let method = "POST"
    event.preventDefault();
    staff = {}
    let url = base_url;
    const data = new FormData(event.target);
    staff.staffName = data.get('staffName');
    staff.staffAge = data.get('staffAge');
    staff.staffType = data.get('staffType').charAt(0).toUpperCase() + data.get('staffType').slice(1);
    if (!data.get('staffId') == "") {
        staff.staffId = data.get('staffId')
        method = "PUT"
        url = base_url + "/" + staff.staffType + "/" + staff.staffId
    }

    switch (staff.staffType.toLowerCase()) {
        case "teacher":
            staff.subject = data.get('staffDepartment');
            break;
        case "administrator":
            staff.administratorDepartment = data.get('staffDepartment');
            break;
        case "support":
            staff.supportDepartment = data.get('staffDepartment');
            break;
    }

    fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(staff),
        })
        .then(response => response.json())
        .then(data => {
            fetchStaff(base_url, staff.staffType);
            statusPopup(data.status, "#27ae60")
            closeForm();
        })
        .catch((error) => {
            statusPopup(error, "#red")
        });
}

function addForm() {
    document.getElementById("staffFormType").innerHTML = "Add Form"
    openForm(staffOptions.value);
}

function updateForm(staff) {
    openForm(staff.staffType);
    document.getElementById("staffFormType").innerHTML = "Update Form"
    if (staff != null) {
        document.getElementById("staffId").value = staff.staffId;
        document.getElementById("staffName").value = staff.staffName;
        document.getElementById("staffAge").value = staff.staffAge;
        formStaffLists.value = staff.staffType;
        document.getElementById("staffDepartment").value = staff.subject || staff.administratorDepartment || staff.supportDepartment;

    }

}

function closeForm() {
    var length = formStaffLists.options.length - 1;
    for (var i = length; i > 0; i--) {
        formStaffLists.remove(i);
    }
    document.getElementById("staffForm").style.display = "none"
    document.getElementById("staffId").value = null;
    document.getElementById("staffName").value = null;
    document.getElementById("staffAge").value = null;
    formStaffLists.value = null;
    document.getElementById("staffDepartment").value = null;

}

function openForm(staffType) {
    Object.keys(staffTypes).forEach((staffType, i) => {
        var option = document.createElement("OPTION");
        option.textContent = staffType;
        option.value = staffType;
        formStaffLists.appendChild(option);
    });
    if (staffType != "All") {
        formStaffLists.value = staffType;
    }
    document.getElementById("staffForm").style.display = "block"
    var department = document.getElementById("labelstaffDepartment");
    if (formStaffLists.value != null) {
        switch (formStaffLists.value.toLowerCase()) {
            case "teacher":
                department.textContent = "Subject";
                break;
            case "administrator":
                department.textContent = "Administrator Department";
                break;
            case "support":
                department.textContent = "Support Department";
                break;
        }
    }
}


function delStaffHelper(staff) {
    event.stopImmediatePropagation()
    event.preventDefault();
    if (deleteList.length == 0 && staff == null) {
        statusPopup("Please select a staff", "#e74c3c")
        return;
    }
    document.getElementById("delConfirmationYESBtn").onclick = () => {
        deleteStaff(staff);
        document.getElementById("confirmationDelete").style.display = "none"
    }
    document.getElementById("delConfirmationNOBtn").onclick = () => {
        document.getElementById("confirmationDelete").style.display = "none"
    }
    document.getElementById("confirmationDelete").style.display = "block"
}

async function deleteStaff(staff) {
    let url = base_url
    let data = deleteList;
    let staffType = staffOptions.value;
    if (staff != null) {
        data = [staff]
    }
    fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
        .then((response) => response.json())
        .then((data) => {
            deleteList = [];
            statusPopup("Delete Success", "#27ae60")

            fetchStaff(base_url, staffType);
        })
        .catch((error) => {})
}

function setConfirmation(value) {

}

function handleMainStaffListChange(staffOptions) {

    if (staffOptions.value != 1) {

        staffType = staffOptions.options[staffOptions.selectedIndex].text;
        fetchStaff(base_url, staffType);
    }
}




function handleFormStaffListChange() {
    var department = document.getElementById("labelstaffDepartment");
    if (formStaffLists.value != null) {
        switch (formStaffLists.value.toLowerCase()) {
            case "teacher":
                department.textContent = "Subject";
                break;
            case "administrator":
                department.textContent = "Administrator Department";
                break;
            case "support":
                department.textContent = "Support Department";
                break;
            default:
                department.textContent = "Department";
                break;
        }

    }
}


function statusPopup(message, color) {
    var popup = document.getElementById("statusPopup");
    popup.innerHTML = `<h5>` + message + `</h5>`;
    popup.style.display = "block";
    popup.style.backgroundColor = color
    setTimeout(() => {
        popup.style.display = "none";
        popup.innerHTML = "";

    }, 2000);
}