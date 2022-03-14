$(document).ready(function () {
    $('#calendar').fullCalendar({
        header:
        {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        buttonText: {
            today: 'Hoy',
            month: 'Mes',
            week: 'Semana',
            day: 'Dia'
        },
        
        events: function (start, end, timezone, callback) {
            $.ajax({
                url: '/Citas/GetCalendarData',
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    var events = [];

                    $.each(result, function (i, data) {
                        events.push(
                            {
                                title: data.titulo,
                                description: data.Desc,
                                start: moment(data.fechaInicio).format('YYYY-MM-DDTHH:mm:ss.sssZ'),
                                end: moment(data.fechaFinal).format('YYYY-MM-DDTHH:mm:ss.sssZ'),
                                backgroundColor: generarNuevoColor,
                                borderColor: generarNuevoColor
                            });
                    });
                    callback(events);
                }
            });
        },

        eventRender: function (event, element) {
            element.qtip(
                {
                    content: event.description
                });
            
        },

        editable: false
    });
});

function generarNuevoColor() {
    var simbolos, color;
    simbolos = "0123456789ABCDEF";
    color = "#";

    for (var i = 0; i < 6; i++) {
        color = color + simbolos[Math.floor(Math.random() * 16)];
    }

}