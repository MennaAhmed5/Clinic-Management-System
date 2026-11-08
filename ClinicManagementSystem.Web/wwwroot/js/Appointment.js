$(document).ready(function () {

    // Initialize datepicker when page loads after validation
    var doctorId = $("#doctorsDropdown").val();

    if (doctorId) {
        loadDoctorSchedules(doctorId);
    }

    // Load schedules when doctor changes
    $("#doctorsDropdown").change(function () {
        loadDoctorSchedules($(this).val());
    });

});



// $('#doctorsDropdown').change(function () {

//     var selectedDoctorId = $(this).val();
//     loadDoctorSchedules(selectedDoctorId);
   

// });


function loadDoctorSchedules(selectedDoctorId) {
    $.ajax({
        url: "/Schedule/GetDoctorSchedules?doctorId=" + selectedDoctorId,
        type: "GET",
        dataType: "json",
        success: function (response) {
            var allowedDays = [];
            var data = response.data;
            data.forEach((currentElement, index) => {
                //Array contains available days for  the selected doctor
                allowedDays.push(currentElement.dayOfTheWeek);
            });
            $("#appointmentDate").datepicker("destroy");

            $("#appointmentDate").datepicker({
                dateFormat: "yy-mm-dd",
                minDate: 0,
                beforeShowDay: function (date) {
                    var day = date.getDay();
                    // Check if the current day index is in the allowedDays array
                    if ($.inArray(day, allowedDays) !== -1) {
                        // Return true to enable the date
                        return [true, '', ''];
                    } else {
                        // Return false to disable the date
                        return [false, '', ''];
                    }
                }
            });

        }

    });
}


//generate time slots based on appointment date 

$("#appointmentDate").change(function () {
    var selectedDoctorId = $("#doctorsDropdown").val();
    var date = $("#appointmentDate").val();
    $.ajax({
        url: "/Appointment/GetAvailableTimeSlots?doctorId=" + selectedDoctorId + "&date=" + date,
        type: "GET",
        dataType: "json",
        success: function (response) {
            var data = response.data;
            $("#timeSlots").empty().append(`<option value="">-- Select Time --</option>`);  
            // Check if selected date is today
            var today = new Date();
            var selectedDate = new Date(date + "T00:00:00");

            var isToday =
                selectedDate.getFullYear() === today.getFullYear() &&
                selectedDate.getMonth() === today.getMonth() &&
                selectedDate.getDate() === today.getDate();

                data.forEach(function (currentElement) {

                    if (isToday) {

                        // Convert TimeOnly string to hours and minutes
                        var [hours, minutes] = currentElement.split(":");

                        var slotTime = new Date();
                        slotTime.setHours(hours, minutes, 0, 0);

                        // Show only future slots
                        if (slotTime <= today)
                            return;
                    }
                //Array contains available days for  the selected doctor
                $("#timeSlots").append(`<option value="${currentElement}"> ${currentElement} </option >`)
            });

        }
    });
});

