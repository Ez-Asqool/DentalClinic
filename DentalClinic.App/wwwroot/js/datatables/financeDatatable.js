$(document).ready(function () {
    // Get the current URI dynamically (for example, window.location.pathname)
    var currentUri = window.location.pathname;

    // Construct the full URL with the path parameter
    var fullUrl = `/Admin/Finance/AllData?path=${encodeURIComponent(currentUri)}`;

    $('#finance').DataTable({
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": fullUrl,
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
            { "data": "category", "name": "Category", "autowidth": true },
            { "data": "price", "name": "Price", "autowidth": true },
            { "data": "createdAt", "name": "CreatedAt", "autowidth": true },
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
