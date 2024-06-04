var calendarEl = document.getElementById('calendar');

var calendar = new FullCalendar.Calendar(calendarEl, {
    initialView: 'dayGridMonth',
    selectable: true,
    select: function (info) {
        console.log('Selected range: ' + info.start + ' - ' + info.end);
        // Tutaj możesz dodać kod, który będzie wykonywany po wybraniu zakresu dat
    }
});