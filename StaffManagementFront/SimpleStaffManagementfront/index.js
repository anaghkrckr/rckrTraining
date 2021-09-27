// const base_url = "http://staffapi.dev.grcdev.com/api/staff"
const base_url = "http://staffapi.dev.grcdev.com/api/staff"
let deleteList = []
let mainStaffList = []
let pagedStaffList = []
let staffTypes = {
    staffs: {
        All: { objName: "department", name: "Department" },
        Teacher: { objName: "subject", name: "Subject" },
        Administrator: { objName: "administratorDepartment", name: "Administrator Department" },
        Support: { objName: "supportDepartment", name: "Support Department" }
    },
    selected: "All"
}


let sortOrder = { Id: 0, Name: 0, Age: 0, Type: 0, }

let icons = {
    sortUp: "<i class='bi bi-caret-down-fill'></i>",
    sortDown: "<i class='bi bi-caret-up-fill'></i>"
}

let tableHead = {
    staffId: "Id",
    staffName: "Name",
    staffAge: "Age",
    staffType: "Type",
}


let currentPage = 0;

const formStaffLists = document.getElementById("formStaffs");
formStaffLists.addEventListener("change", handleFormStaffListChange);

const form = document.querySelector('form');
form.addEventListener('submit', formSubmitHandler);

const staffOptions = document.getElementById("staffs")

main();

async function main() {
    Object.keys(staffTypes.staffs).forEach((key) => {
        var a = document.createElement("a");
        a.onclick = () => {
            fetchStaff(key);
            currentPage = 0;
        }
        a.href = "#"
        a.textContent = key;
        staffOptions.appendChild(a);
    })

    fetchStaff(staffTypes.selected)
}
async function fetchStaff(staffType) {
    staffTypes.selected = staffType;
    document.getElementById("staffSelectionBtn").textContent = staffTypes.selected;
    let fetchUrl = base_url;
    if (staffTypes.selected != "All") {
        fetchUrl = base_url + "/" + staffType;
        staffOptions.value = staffType;
    }

    let response = await fetch(fetchUrl)
    var data = await response.json();
    mainStaffList = data;
    sortOrder[staffTypes.staffs[staffTypes.selected].objName] = 0;
    renderStaffTable()
}

function sortStaffList(item) {
    sortOrder[item] = !sortOrder[item];
    if (item == "department") {
        mainStaffList.sort(function (a, b) {
            return sortOrder[item] ? (((b.subject || b.administratorDepartment || b.supportDepartment) > (a.subject || a.administratorDepartment || a.supportDepartment)) ? 1 : (((a.subject || a.administratorDepartment || a.supportDepartment) > (b.subject || b.administratorDepartment || b.supportDepartment)) ? -1 : 0)) : (((a.subject || a.administratorDepartment || a.supportDepartment) > (b.subject || b.administratorDepartment || b.supportDepartment)) ? 1 : (((b.subject || b.administratorDepartment || b.supportDepartment) > (a.subject || a.administratorDepartment || a.supportDepartment)) ? -1 : 0));
        });
    } else {
        mainStaffList.sort(function (a, b) {
            return sortOrder[item] ? ((b[item] > a[item]) ? 1 : ((a[item] > b[item]) ? -1 : 0)) : ((a[item] > b[item]) ? 1 : ((b[item] > a[item]) ? -1 : 0));
        });
    }
    renderStaffTable()
}

function renderStaffTable() {
    pagedStaffList = paginate(mainStaffList)
    var headItems = { ...tableHead };
    let table = document.getElementById("staffTable");
    table.getElementsByTagName('tbody')[0].innerHTML = ""
    table.getElementsByTagName('thead')[0].innerHTML = ""
    let department = staffTypes.staffs[staffTypes.selected];
    var header = table.createTHead();
    var row = header.insertRow(0);
    headItems[department.objName] = department.name;
    Object.keys(headItems).forEach((key) => {
        var cell = row.insertCell(-1)
        cell.innerHTML = (sortOrder[key] ? icons.sortUp : icons.sortDown) + `<b>` + headItems[key];
        cell.onclick = () =>
            sortStaffList(key)
        cell.style.cursor = "pointer"
    })


    row.insertCell(-1).innerHTML = `<b>Delete`;
    const staffPages = document.getElementById("staffPAgesList")
    staffPages.innerHTML = ""
    if (pagedStaffList.length == 0) {
        return;
    }

    pagedStaffList[currentPage].forEach(staff => {
        var row = table.getElementsByTagName('tbody')[0].insertRow(-1);
        row.onclick = function () {
            updateStaffForm(staff)
        }
        row.style.cursor = "pointer"
        Object.keys(staff).forEach(function (key, i) {
            var cell = row.insertCell(i)
            cell.innerHTML = staff[key]
            cell.className = "cell"
            cell.style.width = "300px"
        })
        var deleteStaffButton = document.createElement("button");
        deleteStaffButton.className = "btn btn-danger";
        deleteStaffButton.textContent = "DELETE";
        deleteStaffButton.onclick = function () {
            delStaffHelper(staff)
        };
        row.insertCell(-1).appendChild(deleteStaffButton);
        var deleteStaffCheckBox = document.createElement("input")
        deleteStaffCheckBox.type = "checkbox";
        deleteStaffCheckBox.className = "form-check-input";
        deleteStaffCheckBox.id = staff.staffId;
        deleteStaffCheckBox.onclick = () => {
            event.stopImmediatePropagation()
            deleteList.push(staff);
        }
        row.insertCell(-1).appendChild(deleteStaffCheckBox);
    })


    pagedStaffList.forEach((item, i) => {
        var li = document.createElement("li");
        li.className = "page-link";
        var id = "page-" + (i + 1)
        li.id = id;
        li.style.cursor = "pointer"
        li.onclick = () => {
            currentPage = i
            renderStaffTable(pagedStaffList, currentPage);
        }
        li.textContent = i + 1
        staffPages.appendChild(li)
    })

    var pageId = "page-" + (currentPage + 1)
    document.getElementById(pageId).style.backgroundColor = "#00a8ff"
}


function formSubmitHandler() {
    let method = "POST"
    event.preventDefault();
    let staff = {}
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
    let department = staffTypes.staffs[staff.staffType].objName
    staff[department] = data.get('staffDepartment')
    fetch(url, {
        method: method,
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(staff),
    })
        .then(response => response.json())
        .then(data => {
            fetchStaff(staffTypes.selected);
            statusPopup(data.status, "#27ae60")
            closeForm();
        })
        .catch((error) => {
            statusPopup(error, "#red")
        });
}

function addForm() {
    document.getElementById("staffFormType").innerHTML = "Add Form"
    openForm(staffTypes.selected);
}

function updateStaffForm(staff) {
    openForm(staff.staffType);
    document.getElementById("staffFormType").innerHTML = "Update Form"
    if (staff != null) {
        document.getElementById("staffId").value = staff.staffId;
        document.getElementById("staffName").value = staff.staffName;
        document.getElementById("staffAge").value = staff.staffAge;
        formStaffLists.value = staff.staffType;
        document.getElementById("staffDepartment").value = staff.subject || staff.administratorDepartment || staff.supportDepartment
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
    Object.keys(staffTypes.staffs).slice(1).forEach((staffType, i) => {
        var option = document.createElement("OPTION");
        option.textContent = staffType;
        option.value = staffType;
        formStaffLists.appendChild(option);
    });
    formStaffLists.value = staffType;
    document.getElementById("staffForm").style.display = "block"
    var department = document.getElementById("labelstaffDepartment");
    department.textContent = staffTypes.staffs[formStaffLists.value].name;

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

            fetchStaff(staffTypes.selected);
        })
        .catch((error) => { })
}

function handleFormStaffListChange() {
    var department = document.getElementById("labelstaffDepartment");
    if (formStaffLists.value != null) {
        department.textContent = staffTypes.staffs[formStaffLists.value].name.toLowerCase()
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