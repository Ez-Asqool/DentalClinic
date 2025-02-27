$(document).ready(function () {
    var table = $('#appointment').DataTable({
        "processing": true,
    "serverSide": true,
    "ajax": {
        "url": "/Admin/Appointment/Alldata",
    "type": "POST",
    "dataType": "json"
        },
    "columns": [
        { "data": "id", "name": "Id" },
        { "data": "patientName", "name": "PatientName", orderable: false},
        { "data": "doctorName", "name": "DoctorName", orderable: false  },
        {"data": "date", "name": "Date" },
        { "data": "timeFrom", "name": "Time From", orderable: false },
        { "data": "timeTo", "name": "Time To", orderable: false },
        {
            "data": "type",
            "name": "Type",
            "render": function (data, type, row) {
                if (data === "New") {
                    return "جديد";
                } else if (data === "Review") {
                    return "مراجعة";
                } else {
                    return data; // If no match, return the original data
                }
            }
        },
        { "data": "visitStatus", "name": "Visit Status", orderable: false }
    ],
    "language": {
        "search": "🔍 بحث:",
    "lengthMenu": "عرض _MENU_ سجل",
    "info": "عرض _START_ إلى _END_ من _TOTAL_ سجل",
    "infoEmpty": "لا توجد بيانات",
    "infoFiltered": "(تم تصفيتها من _MAX_ سجل)",
    "paginate": {
        "next": "التالي",
    "previous": "السابق"
            }
        }
    });

    // Search functionality
    $('#searchInput').keyup(function () {
        table.search(this.value).draw();
    });
});


