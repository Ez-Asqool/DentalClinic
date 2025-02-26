$(document).ready(function () {
    $('#patient').DataTable({
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admin/Patient/AllData",
            "type": "POST",
            "datatype": "JSON"
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ],
        "columns": [
            { "data": "id", "name": "Id", "autowidth": true },
            { "data": "patient.name", "name": "Name", "autowidth": true },
            { "data": "doctor.name", "name": "Name", "autowidth": true },
            { "data": "date", "name": "Date", "autowidth": true },
            { "data": "timefrom", "name": "TimeFrom", "autowidth": true },
            { "data": "timeto", "name": "TimeTo", "autowidth": true },
            { "data": "visit", "name": "Visit", "autowidth": true },
            
        ],
        "language": {
            "search": "🔍 بحث:",
            "lengthMenu": "عرض _MENU_ سجل",
            "info": "عرض _START_ إلى _END_ من _TOTAL_ سجل",
            "infoEmpty": "عرض 0 إلى 0 من 0 سجل",
            "infoFiltered": "(تم تصفيتها من _MAX_ سجل كليًا)",
            "paginate": {
                "next": "التالي",
                "previous": "السابق"
            }
        },
        "createdRow": function (row, data, index) {
            $('td', row).css('text-align', 'center'); // Center align all table data
        }
    });
});
