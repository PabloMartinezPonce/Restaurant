/*!
        SCRIPT INCRUSTADO EN VISTA DE PRODUCTO
 */

/* CONFIGRACIONES  GLOBALES  */
var idProducto = 0;
var jsonProducto = null;
var editarProducto = false;

function AlertExpiration(data) {
    //if (ValidarSesion(data)) {
    //    swal({ title: titleSesion, text: txtSesion, icon: "info" }).then((value) => { window.location = location.origin; });
    //}
}

function ConsultarProductoModal() {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-search"></i> Consultar datos del producto: ' + jsonProducto.nombre;
    idProducto = 0;
    BloquearProducto(true);
    LimpiarModalProducto();
    LlenarFormulario();
    $('#modal-producto').modal({ backdrop: 'static', keyboard: false });
}

function EditarProductoModal(_jsonProducto, _idProducto) {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-edit"></i> Editar datos del producto: ' + _jsonProducto.nombre;
    editarProducto = true;
    jsonProducto = _jsonProducto;
    idProducto = _idProducto;
    BloquearProducto(false);
    LimpiarModalProducto();
    LlenarFormulario();
    $('#modal-producto').modal({ backdrop: 'static', keyboard: false });
}

function CrearProductoModal() {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-user-plus"></i> Crear nueva producto';
    editarProducto = false;
    idProducto = 0;
    BloquearProducto(false);
    LimpiarModalProducto();
    $('#modal-producto').modal({ backdrop: 'static', keyboard: false });
}

function LlenarFormulario() {
    document.getElementById("id").value = jsonProducto.id;
    document.getElementById("nombre").value = jsonProducto.nombre;
    document.getElementById("codigo").value = jsonProducto.codigo;
    document.getElementById("stock").value = jsonProducto.stock;
    document.getElementById("precioCosto").value = jsonProducto.precioCosto;
    document.getElementById("precioVenta").value = jsonProducto.precioVenta;
    document.getElementById("activo").value = jsonProducto.activo;
    document.getElementById("descuento").value = jsonProducto.descuento;
    document.getElementById("descripcion").value = jsonProducto.descripcion;
    document.getElementById("idCategoria").value = jsonProducto.idCategoria;
    document.getElementById("idProveedor").value = jsonProducto.idProveedor;
}

function LimpiarModalProducto() {
    $('#formProducto').parsley().reset();
    let clearFieldTxt = "";
    let clearFieldInt = 0;
    let clearFieldBool = false;

    document.getElementById("id").value = clearFieldInt;
    document.getElementById("nombre").value = clearFieldTxt
    document.getElementById("codigo").value = clearFieldTxt;
    document.getElementById("stock").value = clearFieldInt;
    document.getElementById("precioCosto").value = clearFieldInt;
    document.getElementById("precioVenta").value = clearFieldInt;
    document.getElementById("activo").value = clearFieldBool;
    document.getElementById("descuento").value = clearFieldInt
    document.getElementById("descripcion").value = clearFieldTxt;
    document.getElementById("idCategoria").value = clearFieldInt;
    document.getElementById("idProveedor").value = clearFieldInt;

    $('#idCategoria').val(null).trigger('change');
    $('#idProveedor').val(null).trigger('change');
}

function BloquearProducto(bloquear) {
    if (!bloquear) {
        document.getElementById('idBtnProducto').style.display = 'block';
        $('#fsProducto').removeAttr("disabled");
    } else {
        document.getElementById('idBtnProducto').style.display = 'none';
        $('#fsProducto').attr('disabled', 'disabled');
    }
}

$('#dtProductos').DataTable({
    responsive: true
});

//Envia a guardar formulario 
$("#idBtnProducto").click(function (event) {
    event.stopPropagation();
    event.preventDefault();

    let $form = $('#formProducto');
    let valid = $form.parsley().validate();
    let url = editarProducto ? "EditProducto" : "CreateProducto";

    if (!valid) {
        return false;
    }
    var formData = $form.serialize();

    $.ajax({
        url: url,
        data: formData,
        type: 'POST',
        success: function (data) {
            if (data.objectResponse) {
                swal({ title: "¡Guardado!", text: data.message, icon: "success", confirmButtonColor: "#470a68" }).then((value) => { location.reload(true); })
            } else {
                swal({ title: "Error.", text: data.message, icon: "error" });
            }
        }, error: function (err) {
            swal({ title: "Error Inesperado", text: err.responseText, icon: "error" }).then((value) => { /*window.location = location.origin;*/ })
        }
    }).done(function (data) {
        AlertExpiration(data);
    });
});

//Eliminar elemento
function EliminarProducto(id) {
    swal({
        title: '¿Esta seguro de eliminarlo?',
        text: 'El registro se eliminará permanentemente.',
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: 'DeleteProducto',
                    type: "POST",
                    data: { 'id': id },
                    success: function (data) {
                        if (ValidarSesion(data)) {
                            swal({ title: titleSesion, text: txtSesion, icon: "info" }).then((value) => { window.location = location.origin; });
                        }
                        else if (data.objectResponse) {
                            swal({ title: "Éxito", text: data.message, icon: "success" }).then((value) => { location.reload(true); })
                        } else {
                            swal({ title: "Error", text: data.message, icon: "error" });
                        }
                    },
                    error: function (err) {
                        swal({ title: "Error Inesperado", text: err.responseText, icon: "error" }).then((value) => {/* window.location = location.origin;*/ })
                    }
                }).done(function (data) {
                    AlertExpiration(data);
                });
            }
        });
}

//Modificar estatus de producto
function ModificarEstatus(producto, estatus, nombre) {
    var title = !estatus ? "¿Esta seguro de reactivar a " + nombre + "?" : "¿Esta seguro de bloquear a " + nombre + "?";
    var text = !estatus ? nombre + " volverá a ingresar al sistema" : nombre + " podrá ingresar al sistema";
    swal({
        title: title,
        text: text,
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '../../Producto/ModifyStatus',
                    type: "POST",
                    data: { 'producto': producto, 'esCorreo': false, 'enable': !estatus },
                    success: function (data) {
                        if (data.responseCode == 200) {
                            swal({ title: "Éxito", text: data.message, icon: "success" }).then((value) => { location.reload(true); })
                        } else {
                            swal({ title: "Error", text: data.message, icon: "error" });
                        }
                    },
                    error: function (err) {
                        swal({ title: "Error Inesperado", text: "", icon: "error" }).then((value) => { window.location = location.origin; })
                    }
                }).done(function (data) {
                    AlertExpiration(data);
                });
            }
        });
}