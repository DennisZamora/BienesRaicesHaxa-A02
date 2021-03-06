function registrarDueno() {
    var cedula_identificacion = $.trim($("#cedula_identificacion").val());
    var nombre = $.trim($("#nombre").val());
    var primerApellido = $.trim($("#apellido1").val());
    var segundoApellido = $.trim($("#apellido2").val());
    var telefono = $.trim($("#telefono").val());
    var password = $.trim($("#password").val());
    var correo = $.trim($("#email").val());

    var espacios = false;
    var cont = 0;
    var validarCorreo = false;
    

    while (!espacios && (cont < password.length)) {
        if (password.charAt(cont) == " ") {
            espacios = true;
        }
        cont++;
    }

    cont = 0;
    while (cont < correo.length) {
        if (correo.charAt(cont) == "@") {
            validarCorreo = true;
        }
        cont++;
    }

    if (cedula_identificacion == "" || nombre == "" || primerApellido == "" || segundoApellido == "" ||
        telefono == "" || password == "" || correo == "") {
        Swal.fire({
            title: "Error al registrar el dueño",
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
    } else if (password.length < 6 || espacios) {
        Swal.fire({
            title: "Contraseña muy debil",
            text: "Ingrese una contraseña correcta: Contenga un mínimo de 6 caracteres, ningún espacio en blanco",
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
    } else if (telefono.length != 8) {
        Swal.fire({
            title: "Número telefónico incorrecto",
            text: "Ingrese un número telefónico correcto con una longitud de 8 caracteres",
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
            url: '/GestionDueno/Registrar',
            data: {
                cedula_identificacion: cedula_identificacion,
                nombre: nombre,
                primerApellido: primerApellido,
                segundoApellido: segundoApellido,
                telefono: telefono,
                password: password,
                correo: correo,
            },
            dataType: 'json',
            success: function (respuesta) {
                Swal.fire({
                    title: "Dueño registrado con éxito",
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
                    title: "Dueño registrado con éxito",
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