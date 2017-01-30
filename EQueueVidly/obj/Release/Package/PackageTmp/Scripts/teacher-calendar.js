var selectedEvent;

$(document).ready(function() {


    $(function() {
        $('#datetimepicker1').datetimepicker({ format: 'DD/MMM/YYYY HH:mm' });
    });

    $('#calendar').fullCalendar({
        header: {
            left: 'delete,edit,today prev,next ',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        defaultView: 'agendaDay',
        editable: false,
        allDaySlot: false,
        selectable: true,
        slotMinutes: 15,
        timezone: 'local',
        events: '@Url.Action("GetScheduleEvents", "Calendar", new{Id = User.Identity.GetUserId()})',
        eventClick: function(calEvent, jsEvent, view) {
            selectedEvent = calEvent;
        },
        dayClick: function(date, allDay, jsEvent, view) {
            $('#eventTitle').val("");
            $('#eventDate').val(getFormattedDate(date));
            $('#newSelectedModal').modal();
        },
        customButtons: {
            delete: {
                text: 'Delete selected',
                click: function() {
                    if (selectedEvent != null) {
                        alert('delete the selected event: ' + selectedEvent.title);
                        $('#calendar').fullCalendar('removeEvents', selectedEvent.id);
                        DeleteEvent(selectedEvent, null);
                    } else alert('Please select an event first!');
                }
            },
            edit: {
                text: 'Edit selected',
                click: function() {
                    if (selectedEvent != null) {
                        $('#editSelectedModal').modal();
                        $('#editEventTitle').val(selectedEvent.title);
                        $('#editEventId').val(selectedEvent.id);
                        $('#editEventDate').val(getFormattedDate(selectedEvent.start));
                        $('#editEventDuration').val(selectedEvent.duration);
                        $('#editTimeLimit').val(selectedEvent.timeLimit);

                    } else alert('Please select an event first!');
                }
            }
        }

    });



});

$('#btnPopupCancel').click(function() {
    ClearPopupFormValues();
    $('#popupEventForm').hide();
});
$('#btnPopupSaveEdit').on('click', function(e) {
    e.preventDefault();
    var endTime = new Date($('#editEventDate').val());
    endTime.setMinutes(endTime.getMinutes() + parseInt($('#editEventDuration').val()));
    selectedEvent.title = $('#editEventTitle').val();
    selectedEvent.start = new Date($('#editEventDate').val());
    selectedEvent.end = endTime;
    selectedEvent.duration = $('#editEventDuration').val();
    selectedEvent.timeLimit = $('#editTimeLimit').val();
    var dataRow = {
        'appointment': selectedEvent
    }
    $.ajax({
        type: 'POST',
        url: '@Url.Action("EditEvent", "Manage")',
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(dataRow),
        success: function(response) {
            if (response == 'True') {
                $("#calendar").fullCalendar('renderEvent', selectedEvent, true);
                $('#calendar').fullCalendar('refetchEvents');
                alert('Event edited!');
            } else {
                alert('Error, could not save changes!');
            }
        }
    });
});

$('#btnPopupSave').click(function() {

    $('#popupEventForm').hide();

    var endTime = new Date($('#eventDate').val());
    endTime.setMinutes(endTime.getMinutes() + parseInt($('#eventDuration').val()));

    var appointment = {
        title: $('#eventTitle').val(),
        start: new Date($('#eventDate').val()),
        end: endTime,
        duration: $('#eventDuration').val(),
        timeLimit: $('#timeLimit').val()
    }
    var dataRow = {
        'appointment': appointment
    }
    ClearPopupFormValues();

    $.ajax({
        type: 'POST',
        url: '@Url.Action("SaveEvent", "Manage")',
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(dataRow),
        success: function(response) {
            if (response == 'True') {
                $('#calendar').fullCalendar('refetchEvents');
                alert('New event saved!');
            } else {
                alert('Error, could not save event!');
            }
        }
    });
    $('#calendar').fullCalendar('refresh');
    $('#calendar').fullCalendar('refetchEvents');
});

function getFormattedDate(date) {
    var today = new Date(date);
    var dd = today.getDate();
    var mm = today.getMonth(); //January is 0!
    var hh = today.getHours();
    var min = today.getMinutes();
    if (hh == 0)
        hh = '00';
    if (min == 0)
        min = '00';
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }

    var month = new Array();
    month[0] = "Jan";
    month[1] = "Feb";
    month[2] = "Mar";
    month[3] = "Apr";
    month[4] = "May";
    month[5] = "Jun";
    month[6] = "Jul";
    month[7] = "Aug";
    month[8] = "Sep";
    month[9] = "Oct";
    month[10] = "Nov";
    month[11] = "Dec";

    var today = dd + '/' + month[mm] + '/' + yyyy + ' ' + hh + ':' + min;
    return today;
}

function ClearPopupFormValues() {
    $('#eventID').val("");
    $('#eventTitle').val("");
    $('#eventDuration').val("");
    $('#timeLimit').val("");
}

function DeleteEvent(event, view) {
    var dataRow = {
        'AppointmentId': event.id
    }
    $.ajax({
        type: 'POST',
        url: '@Url.Action("DeleteEvent", "Calendar")',
        data: dataRow,
        success: function(response) {
            if (response == 'True') {
                $('#calendar').fullCalendar('refetchEvents');
                alert('Event deleted!');
            } else {
                alert('Error, could not delete event!');
            }
        }
    });
}
