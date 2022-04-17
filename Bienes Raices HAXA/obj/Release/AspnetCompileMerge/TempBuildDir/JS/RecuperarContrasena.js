function recuperarContrasena() {
    var correo = $.trim($("#email").val());

    var espacios = false;
    var cont = 0;
    var validarCorreo = false;

    while (cont < correo.length) {
        if (correo.charAt(cont) == "@" || correo.charAt(cont) == ".com"){
            validarCorreo = true;
        }
        cont++;
    }

    if (correo == "") {
        Swal.fire({
            title: "Error al enviar el correo electrónico",
            text: "Se encuentran datos en blanco",
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
    } else if (validarCorreo == false) {
        Swal.fire({
            title: "Correo electrónico incorrecto",
            text: "Ingrese un correo electrónico correcto",
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
        $.ajax({
            type: 'POST',
            url: '/LogIn/IniciarRecuperar',
            data: {
                correo: correo,
            },
            dataType: 'json',
            success: function (respuesta) {
                Swal.fire({
                    title: "Correo enviado con exito",
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
            },
            error: function (respuesta) {
                Swal.fire({
                    title: "Correo enviado con exito",
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
        });
    }
}


function recuperacion() {
    var contrasena = $.trim($("#contrasena").val());
    var contrasena2 = $.trim($("#contrasena2").val());
    var token = $.trim($("#token").val());

    var espacios = false;
    var espacios2 = false;
    var cont = 0;

    while (!espacios && (cont < contrasena.length)) {
        if (contrasena.charAt(cont) == " ") {
            espacios = true;

        }
        cont++;
    }

    while (!espacios && (cont < contrasena2.length)) {
        if (contrasena2.charAt(cont) == " ") {
            espacios2 = true;
        }
        cont++;
    }

    if (contrasena == "" || contrasena2 == "") {
        Swal.fire({
            title: "Error al cambiar la contraseña",
            text: "Se encuentran datos en blanco",
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
    } else if (contrasena == false || contrasena2 == false) {
        Swal.fire({
            title: "Error al cambiar la contraseña",
            text: "Las contraseña tiene espacios en blanco",
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
    } else if (contrasena != contrasena2) {
        Swal.fire({
            title: "Error al cambiar la contraseña",
            text: "Las contraseñas no coinciden",
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
        $.ajax({
            type: 'POST',
            url: '/LogIn/Recuperar',
            data: {
                contrasena: contrasena,
                token: token,
            },
            dataType: 'json',
            success: function (respuesta) {
                Swal.fire({
                    title: "Contraseña actualizada con éxito",
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
            },
            error: function (respuesta) {
                Swal.fire({
                    title: "Contraseña actualizada con éxito",
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
        });
    }
}


