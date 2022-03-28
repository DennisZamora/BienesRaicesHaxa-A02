function CalcularFechaFinal() {
    var fechaInicio = $.trim($("#fechaInicio").val());
    var hora = $.trim($("#hora").val());
    $.ajax({
        type: 'GET',
        url: '/Citas/ConsultaFechaFinal',
        data: {
            fechaInicio: fechaInicio,
            hora: hora,
        },
        dataType: 'json',
        success: function (respuesta) {
            $("#fechaFinal").val(respuesta);
        },
        error: function (respuesta) {
            alert(respuesta);
        }
    });
}

function validarDatos() {
    var fechaInicio = $.trim($("#fechaInicio").val());
    var fechaFinal = $.trim($("#fechaFinal").val());

    if (fechaInicio.length == "" || fechaFinal == "") {
        Swal.fire({
            title: "Error al agregar cita",
            text: "Por favor intente de nuevo",
            icon: 'error',
            width: '40%',
            padding: '2%',
            backdrop: 'true',
            timerProgressBar: false,
            allowOutsideClick: true,
            allowEscapeKey: false,
            allowEnterKey: false,
            stopKeydownPropagation: false
        });
    } else {
        Swal.fire({
            title: "Cita agregada",
            icon: 'success',
            width: '40%',
            padding: '2%',
            backdrop: 'true',
            timerProgressBar: false,
            allowOutsideClick: true,
            allowEscapeKey: false,
            allowEnterKey: false,
            stopKeydownPropagation: false
        });
    }
}

var Popup;
function PopupForm1(url) {
    var formDiv = $('<div/>');
    $.get(url)
        .done(function (response) {
            formDiv.html(response);
            Popup = formDiv.dialog({
                autoOpen: true,
                resizable: false,
                title: 'Agregue su cita',
                height: 350,
                width: 350,
                close: function () {
                    Popup.dialog('destroy').remove();
                }
            });
        });
}

function SubmitForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: "POST",
            url: form.action,
            data: $(form).serialize(),
            success: function (data) {
                if (data.success) {
                    Popup.dialog('close');
                    dataTable.ajax.reload();

                    $.notify(data.message, {
                        globalPosition: "top center",
                        className: "success"
                    })
                }
            }
        });
    }
    return false;
}