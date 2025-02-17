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
            {
                "render": function (data, type, row) {
                    return `<a href="#" class="btn-details text-dark text-hover-primary "  
                             data-bs-toggle="modal" data-bs-target="#kt_modal_details" 
                             title="عرض التفاصيل" data-id="${row.id}">
                                ${row.name}
                            </a>`;
                },
                "name": "Name"
            },
            { "data": "age", "name": "Age", "autowidth": true },
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
