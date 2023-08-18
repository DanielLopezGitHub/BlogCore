let dataTable

$(document).ready(() => {
    cargarDatatable()
});

cargarDatatable = () => {
    dataTable = $('#tblCategorias').DataTable({
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 de 0 de 0 Entradas",
            "infoFiltered": "(Filtrando de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": "",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%",
        "ajax": {
            "url": "/Admin/categorias/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "50%" },
            { "data": "orden", "width": "20%" },
            {
                "data": "id", "render": (data) => {
                    return `<div class="text-center">
                                        <a href="/Admin/Categorias/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                            <i class="far fa-edit"></i>
                                            Editar
                                        </a>
                                        &nbsp;
                                         <a onclick=Delete("/Admin/Categorias/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                            <i class="far fa-trash-alt"></i>
                                            Borrar
                                        </a>
                                    </div>`
                },
                "width": "30%"
            }
        ]
    })
}


/*SweetAlert y Toastr*/

Delete = (url) => {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    },
    () => {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: (data) => {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        })
    })
}

