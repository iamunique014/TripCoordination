// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("click", function (event) {
    var sidebar = document.getElementById("sidebarMenu");

    if (!sidebar) return;

    var isClickInsideSidebar = sidebar.contains(event.target);
    var isToggleButton = event.target.closest('[data-bs-toggle="collapse"]');

    if (!isClickInsideSidebar && !isToggleButton && sidebar.classList.contains("show")) {
        var bsCollapse = bootstrap.Collapse.getInstance(sidebar);
        bsCollapse.hide();
    }
});


//Setup DataTables
function setupDataTable(tableSelector) {
    $(document).ready(function () {
        $(tableSelector).DataTable({
            dom: '<"row mb-2"' +
                '<"col-md-6 d-flex align-items-center"l>' +
                '<"col-md-6 d-flex justify-content-md-end justify-content-start mt-2 mt-md-0"B>' +
                '>' +
                '<"row mb-2"<"col-sm-12"f>>' +
                '<"row"<"col-sm-12"tr>>' +
                '<"row mt-2"<"col-sm-5"i><"col-sm-7"p>>',
            buttons: [
                //Excell button
                {
                    extend: 'excel',
                    text: 'Export to Excel',
                    className: 'btn btn-sm btn-outline-info',
                    exportOptions:
                    {
                        columns: ':not(.noExport)'
                    }
                },

                //PDF Button
                {
                    extend: 'pdf',
                    text: 'Export to PDF',
                    className: 'btn btn-sm btn-outline-danger',
                    exportOptions:
                    {
                        columns: ':not(.noExport)'
                    }
                },

            ],
            responsive: true,
            scrollX: true,
            autoWidth: false,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
        });
    });
}