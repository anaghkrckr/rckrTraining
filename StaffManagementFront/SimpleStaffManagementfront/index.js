const base_url = "https://localhost:44332/api/staff"

function handleSubmit() {
    let method = "POST"
    event.preventDefault();

    staff = {}
    let url = base_url;

    const data = new FormData(event.target);
    staff.staffName = data.get('staffName');
    staff.staffAge = data.get('staffAge');
    staff.staffType = data.get('staffType').charAt(0).toUpperCase() + data.get('staffType').slice(1);
    data.get('staffType').charAt(0).toUpperCase() + data.get('staffType').slice(1)
    if (!data.get('staffId') == "") {
        staff.staffId = data.get('staffId')
        method = "PUT"
        url = base_url + "/" + staff.staffType + "/" + staff.staffId
    }
    console.log(staff)
    console.log(url)
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
    console.log(method)
    console.log(JSON.stringify(staff))

    fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(staff),
        })
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            document.getElementById("staffForm").style.display = "none"
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

const form = document.querySelector('form');
form.addEventListener('submit', handleSubmit);

function addForm() {
    document.getElementById("staffFormType").innerHTML = "Add Form"
    document.getElementById("staffForm").style.display = "block"
}

function closeForm() {
    document.getElementById("staffForm").style.display = "none"
    document.getElementById("staffId").value = null;
    document.getElementById("staffName").value = null;
    document.getElementById("staffAge").value = null;
    document.getElementById("staffType").value = null;
    document.getElementById("staffDepartment").value = null;

}




async function fetchStaff(url) {
    let response = await fetch(url)
    var data = await response.json();
    inputData(data);
}

fetchStaff(base_url)

function inputData(data) {

    let table = document.getElementById("staffTable");
    data.forEach(staff => {
        var row = table.insertRow(-1);
        row.onclick = function() {
            updateForm(staff)
        }
        row.insertCell(0).innerHTML = staff.staffId;
        row.insertCell(1).innerHTML = staff.staffName;
        row.insertCell(2).innerHTML = staff.staffAge;
        row.insertCell(3).innerHTML = staff.staffType;
        row.insertCell(4).innerHTML = staff.subject || staff.administratorDepartment || staff.supportDepartment;

    })
}

function updateForm(staff) {
    document.getElementById("staffFormType").innerHTML = "Update Form"
    document.getElementById("staffForm").style.display = "block"
    if (staff != null) {
        document.getElementById("staffId").value = staff.staffId
        document.getElementById("staffName").value = staff.staffName;
        document.getElementById("staffAge").value = staff.staffAge;
        document.getElementById("staffType").value = staff.staffType;
        document.getElementById("staffDepartment").value = staff.subject || staff.administratorDepartment || staff.supportDepartment;

    }

}