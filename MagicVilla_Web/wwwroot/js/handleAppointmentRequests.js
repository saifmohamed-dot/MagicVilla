function createInQueueAppointment(date) {
    var inqAppt = document.createElement('div');
    inqAppt.className = "col-10 btn text-light mb-2 fs-4";
    inqAppt.style.backgroundColor = "#3498db";
    var stateSpan = document.createElement('span');
    stateSpan.innerText = "In Queue";
    inqAppt.innerText = date + " ";
    inqAppt.appendChild(stateSpan);
    return inqAppt;
}
function createAppointmentsRequestsForm(requests) {
    const form = new FormData();
    for (let index = 0; index < requests.length; index++) {
        form.append(`Requests[${index}].AppointmentId`, requests[index].Id);
        form.append(`Requests[${index}].ClientId`, window.appData.ClientId);
        form.append(`Requests[${index}].IsTaken`, 0);
    }
    return form;
}
async function ajaxRequest(url, method, data) {
    var response = await fetch(url, {
        method: method,
        body: data
    });
    return response;
}
async function submitRequestsForAppointments(data , onSuccess , onFail) {
    var response = await ajaxRequest('/RequestedAppointment/RequestForAppointments', 'POST', data);
    var json = await response.json();
    if (json.success == true) {
        onSuccess();
    }
    else {
        onFail([...js.errors]);
    }
}
function handleAskingRequestsAction(requests) {
    form = createAppointmentsRequestsForm(requests);
    submitRequestsForAppointments(form, () => {
        selectedAppointed.forEach(appt => {
            var newAppointmentNode = createInQueueAppointment(appt.Date);
            document.getElementById('requested-container').appendChild(newAppointmentNode);
        });
        selectedAppointed = [];
        
    }, (message) => {
        alert(message);
        location.reload(false);
    });
}
var appBtns = document.querySelectorAll('.appt-btn');
var selectedAppointed = new Array();
var CommiteRequests = document.getElementById('commit-ask');
appBtns.forEach(btn => {
    btn.addEventListener('click', function (e) {
        e.target.classList.add('disabled');
        selectedAppointed.push({ Date: e.target.getAttribute('date-value'), Id: e.target.getAttribute('date-id'), target: e.target });
        var cnt = parseInt(window.appData.AppointmentsCnt);
        cnt--;
        // enable the ask button to commit the selected appointment ->
        document.getElementById('commit-ask').disabled = false;
        if (cnt == 0 && selectedAppointed.length == 0) {
            document.getElementById('commit-ask').disabled = true;
        }
        window.appData.AppointmentsCnt = cnt.toString();
    });
});
var requestCloseBtn = document.getElementById('requests-rollback');
requestCloseBtn.addEventListener('click', function () {
    if (selectedAppointed.length != 0) {
        for (let i = 0; i < selectedAppointed.length; i++) {
            selectedAppointed[i].target.classList.remove('disabled');
        }
        selectedAppointed = []; // remove all buffered requested appointments .
    }
    document.getElementById('commit-ask').disabled = true;
});
var appointmentOpener = document.getElementById('appt-opener');
appointmentOpener.addEventListener('click', function () {
    var cnt = parseInt(window.appData.AppointmentsCnt);
    // enable the ask button to commit the selected appointment ->
    if (selectedAppointed.length == 0) {
        document.getElementById('commit-ask').disabled = true;
    }
});
CommiteRequests.addEventListener('click', function () {
    handleAskingRequestsAction(selectedAppointed);
});