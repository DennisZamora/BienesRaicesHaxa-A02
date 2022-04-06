function eliminarEmpleado() {
    var idUsuario = $.trim($("#idUsuario").val());
    var idRol = $.trim($("#idRol").val());
    var cedula_identificacion = $.trim($("#cedula_identificacion").val());
    var nombre = $.trim($("#nombre").val());
    var primerApellido = $.trim($("#apellido1").val());
    var segundoApellido = $.trim($("#apellido2").val());
    var telefono = $.trim($("#telefono").val());
    var password = $.trim($("#password").val());
    var correo = $.trim($("#email").val());

    Swal.fire({
        title: '¿Seguro de eliminar el empleado?',
        text: "No se podra revertir  una vez eliminado",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Eliminar'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                'Empleado eliminado!',
                'El empleado ha sido eliminado',
                'success'
            )

            $.ajax({
                type: 'POST',
                url: '/GestionEmpleados/Eliminar',
                data: {
                    idUsuario: idUsuario,
                    cedula_identificacion: cedula_identificacion,
                    nombre: nombre,
                    primerApellido: primerApellido,
                    segundoApellido: segundoApellido,
                    telefono: telefono,
                    password: password,
                    correo: correo,
                    idRol: idRol,
                },
                dataType: 'json',
            });
        }
    })
}