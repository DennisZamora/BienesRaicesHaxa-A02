function Filtro() {
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
            url: '/Home/FiltrarPropiedad',
            data: {
                Property_idCategoria: Property_idCategoria,
                Property_provincia: Property_provincia,
                Property_canton: Property_canton,
                Property_pisos: Property_pisos,
                Property_habitacion: Property_habitacion,
                Property_garage: Property_garage,
                min_price: min_price,
                Property_ba_os: Property_ba_os,
                max_price: max_price,
            },
            dataType: 'json',
            success: function (data) {
                document.getElementById("prop").innerHTML = "";
                $('#prop').append(data);
                Swal.close();
            },
            error: function (data) {
                document.getElementById("prop").innerHTML = "";
                $('#prop').append(data.responseText);
                //Swal.fire({
                //    title: "Error al buscar",
                //    icon: 'error',
                //    width: '40%',
                //    padding: '2%',
                //    backdrop: 'true',
                //    timerProgressBar: false,
                //    allowOutsideClick: true,
                //    allowEscapeKey: false,
                //    allowEnterKey: false,
                //    stopKeydownPropagation: false
                //});
            }
        });
    }
}
