$(document).ready(function () {
    $('#lab').DataTable({
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Admin/Lab/AllData",
            "type": "POST",
            "datatype": "JSON"
        },
        "columns": [
            { "data": "id", "name": "Id", "autowidth": true },
            {
                "render": function (data, type, row) {
                    return `<a href="#" class="btn-details text-dark text-hover-primary "  
                             data-bs-toggle="modal" data-bs-target="#kt_modal_details" 
                             title="عرض التفاصيل" data-id="${row.id}">
                                ${row.sampleName}
                            </a>`;
                },
                "name": "SampleName"
            },
            { "data": "patientName", "name": "PatientName", "autowidth": true },
            { "data": "labName", "name": "LabName", "autowidth": true },
            {
                "data": "sampleStatus",
                "name": "SampleStatus",
                "autowidth": true,
                "render": function (data, type, row) {
                    if (data === "تم الإستلام") {
                        return '<span class="text-success fw-bold">' + data + '</span>';
                    } else if (data === "تم الإرسال") {
                        return '<span class="text-primary fw-bold">' + data + '</span>';
                    }
                    return data;
                }
            },
            {
                "render": function (data, type, row) {
                    return `<div class="table-actions">
                                <a href="#" class="btn btn-update" data-bs-toggle="modal" data-bs-target="#kt_modal_update" data-id="${row.id}">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a href="#" class="btn btn-delete" data-id="${row.id}">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>`;
                },
                "orderable": false
            }
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
