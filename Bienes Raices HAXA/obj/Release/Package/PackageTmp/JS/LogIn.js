function LogIn() {
    var email = $.trim($("#email").val());
    var password = $.trim($("#password").val());

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
    while (cont < email.length) {
        if (email.charAt(cont) == "@") {
            validarCorreo = true;
        }
        cont++;
    }

    if (email == "" || password == "") {
        Swal.fire({
            title: "Error al iniciar sesión",
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
            title: "Contraseña incorrecta",
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
            url: '/LogIn/validacionLogin',
            data: {
                password: password,
                email: email,
            },
            dataType: 'json',
            success: function (respuesta) {
                //Swal.fire({
                //    title: "Usuario encontrado",
                //    icon: 'success',
                //    width: '40%',
                //    padding: '2%',
                //    backdrop: 'true',
                //    timerProgressBar: false,
                //    allowOutsideClick: true,
                //    allowEscapeKey: false,
                //    allowEnterKey: false,
                //    stopKeydownPropagation: false
                //});
                //window.location.href="~/Views/Home/Index.cshtml"
                swal({
                    title: "Usuario encontrado!",
                    text: "Login successful!",
                    type: "success"
                }).then(function () {
                    window.location.href = "~/Views/Home/Index.cshtml";
                });
            },
            error: function (respuesta) {
                //Swal.fire({
                //    title: "Usuario encontrado",
                //    icon: 'success',
                //    width: '40%',
                //    padding: '2%',
                //    backdrop: 'true',
                //    timerProgressBar: false,
                //    allowOutsideClick: true,
                //    allowEscapeKey: false,
                //    allowEnterKey: false,
                //    stopKeydownPropagation: false
                //});
                swal({
                    title: "Usuario encontrado!",
                    text: "Login successful!",
                    type: "success"
                }).then(function () {
                    window.location.href = "~/Views/Home/Index.cshtml";
                });
            }
        });
    }
}