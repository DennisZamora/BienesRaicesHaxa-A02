function Filtrar() {
    var Property_idCategoria = $.trim($("#Property_idCategoria").val());
    var Property_provincia = $.trim($("#Property_provincia").val());
    var Property_canton = $.trim($("#Property_canton").val());
    var Property_pisos = $.trim($("#Property_pisos").val());
    var Property_habitacion = $.trim($("#Property_habitacion").val());
    var Property_ba_os = $.trim($("#Property_ba_os").val());
    var Property_garage = $.trim($("#Property_garage").val());
    var min_price = $.trim($("#min_price").val());
    var max_price = $.trim($("#max_price").val());

    if (min_price < 0) {
        Swal.fire({
            title: "Error",
            text: "El precio minimo no debe ser menor a 0",
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
    } else if (Property_canton != "" && Property_provincia == "") {
        Swal.fire({
            title: "Error",
            text: "La provincia no puede estar en blanco",
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
    } else if (Property_idCategoria == "") {
        Swal.fire({
            title: "Error",
            text: "La categoria no puede estar en blanco",
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
            url: '/Home/Filtrar/',
            data: {
                idCategoria:Property_idCategoria,
                provincia:Property_provincia,
                canton:Property_canton,
                pisos:Property_pisos,
                habitacion:Property_habitacion,
                baños:Property_ba_os,
                garage:Property_garage,
                min_price:min_price,
                max_price:max_price
            },
            dataType: 'json',
            success: function (data) {
                Swal.fire({
                    title: "NO",
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
            },
            error: function (data) {
                Swal.fire({
                    title: "Error",
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
            }
        });
    }

}