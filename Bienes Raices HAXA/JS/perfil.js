function editarPerfil() {
    var idUsuario = $.trim($("#idUsuario").val());
    var idRol = $.trim($("#idRol").val());
    var cedula_identificacion = $.trim($("#cedula_identificacion").val());
    var nombre = $.trim($("#nombre").val());
    var primerApellido = $.trim($("#apellido1").val());
    var segundoApellido = $.trim($("#apellido2").val());
    var telefono = $.trim($("#telefono").val());
    var contrasena = $.trim($("#password").val());
    var correo = $.trim($("#email").val());

    var espacios = false;
    var cont = 0;
    var validarCorreo = false;
    while (!espacios && (cont < contrasena.length)) {
        if (contrasena.charAt(cont) == " ") {
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
        telefono == "" || contrasena == "" || correo == "") {
        Swal.fire({
            title: "Error al editar el perfil",
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
    } else if (contrasena.length < 5 || espacios) {
        Swal.fire({
            title: "Contraseña muy debil",
            text: "Ingrese una contraseña correcta: Contenga un minimo de 6 caracteres, ningun espacio en blanco",
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
            title: "Correo electronico incorrecto",
            text: "Ingrese un correo electronico correcto",
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
    }else {
        $.ajax({
            type: 'POST',
            url: '/Perfil/EditarPerfil',
            data: {
                idUsuario: idUsuario,
                idRol: idRol,
                cedula_identificacion: cedula_identificacion,
                nombre: nombre,
                primerApellido: primerApellido,
                segundoApellido: segundoApellido,
                telefono: telefono,
                contrasena: contrasena,
                correo: correo,
            },
            dataType: 'json',
            success: function (respuesta) {
                Swal.fire({
                    title: "Perfil editado con exito",
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
                    title: "Perfil editado con exito",
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