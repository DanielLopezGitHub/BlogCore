var dataTable;

$(document).ready(function () {
    cargarDatatable();
})

function cargarDatatable() {
    dataTable = $("#tblCategorias").DataTable({
        "ajax": {
            "url": "/admin/categorias/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "50%" },
            { "data": "order", "width": "20%" },
            {
                "data": "id", "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Categorias/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-edit"></i>
                                    Editar
                                </a>
                                &nbsp;
                                 <a onclick="Delete(/Admin/Categorias/Delete/${data})" class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-trash-alt"></i>
                                    Borrar
                                </a>
                            </div>`;
                },
                "width": "30%"
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "",
            "infoEmpty": "",
            "infoFiltered": "",
            "infoPostFix": "",
            "thousands": "",
            "lengthMenu": "",
            "loadingRecords": "",
            "processing": "",
            "search": "",
            "zeroRecords": "",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior",
            }
        },
        "width": "100%"
    })
}