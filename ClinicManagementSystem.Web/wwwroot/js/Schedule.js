//create
//submit create modal
$(document).on('submit', '#scheduleModalContent form', function (e) {
    e.preventDefault();

    var form = $(this);

    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: form.serialize(),

        success: function (response) {

            // Validation failed: display partial view
            if (typeof response === 'string') {
                $('#scheduleModalContent').html(response);
            }
            // Validation passed: response is JSON
            else if (response.success) {

                var modalElement =
                    document.getElementById('createScheduleModal');

                var modalInstance = bootstrap.Modal.getInstance(modalElement);

                if (modalInstance) {
                    modalInstance.hide();
                } else {
                    new bootstrap.Modal(modalElement).hide();
                }

                toastr.success(response.message);

                location.reload();
            }
        },

        error: function (xhr) {
            console.log(xhr.responseText);
        }
    });
});


//update
// Open Edit Modal
$(document).on('click', '.edit-schedule', function () {

    var doctorId = $(this).data('doctor-id');
    var day = $(this).data('day');

    $.get('/Schedule/Update', {
        doctorId: doctorId,
        dayOfTheWeek: day
    }, function (response) {

        $('#editScheduleModalContent').html(response);

        var modalElement =
            document.getElementById('editScheduleModal');

        new bootstrap.Modal(modalElement).show();
    });
});

// Open Edit Modal
$(document).on('click', '.edit-schedule', function () {

    var doctorId = $(this).data('doctor-id');
    var day = $(this).data('day');

    $.get('/Schedule/Update', {
        doctorId: doctorId,
        dayOfTheWeek: day
    }, function (response) {

        $('#editScheduleModalContent').html(response);

        var modalElement =
            document.getElementById('editScheduleModal');

        new bootstrap.Modal(modalElement).show();
    });
});


// Submit Edit Form
$(document).on('submit', '#editScheduleModalContent form', function (e) {

    e.preventDefault();

    var form = $(this);

    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: form.serialize(),

        success: function (response) {

            if (typeof response === 'string') {
                $('#editScheduleModalContent').html(response);
            }
            else if (response.success) {

                var modalElement =
                    document.getElementById('editScheduleModal');

                bootstrap.Modal.getInstance(modalElement)?.hide();

                toastr.success(response.message);

                location.reload();
            }
        },

        error: function (xhr) {
            console.log(xhr.responseText);
        }
    });
});


//delete
//open modal
$(document).on('click', '.delete-schedule', function () {

    var doctorId = $(this).data('doctor-id');
    var day = $(this).data('day');

    $.get('/Schedule/Delete', {
        doctorId: doctorId,
        dayOfTheWeek: day
    }, function (response) {

        $('#deleteScheduleModalContent').html(response);

        var modalElement =
            document.getElementById('deleteScheduleModal');

        new bootstrap.Modal(modalElement).show();
    });
});


// Submit Edit Form
$(document).on('submit', '#deleteScheduleModalContent form', function (e) {

    e.preventDefault();

    var form = $(this);

    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: form.serialize(),

        success: function (response) {

            if (typeof response === 'string') {
                $('#deleteScheduleModalContent').html(response);
            }
            else if (response.success) {

                var modalElement =
                    document.getElementById('deleteScheduleModal');

                bootstrap.Modal.getInstance(modalElement)?.hide();

                toastr.success(response.message);

                location.reload();
            }
        },

        error: function (xhr) {
            console.log(xhr.responseText);
        }
    });
});