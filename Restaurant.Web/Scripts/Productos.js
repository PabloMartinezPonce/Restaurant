/*!
        SCRIPT INCRUSTADO EN VISTA DE PRODUCTO
 */

/* CONFIGRACIONES  GLOBALES  */

//Envia a guardar formulario 
$("#idBtnProducto").click(function (event) {
    event.stopPropagation();
    event.preventDefault();
    let id = document.getElementById("id");
    let $form = $('#formProducto');
    let valid = $form.parsley().validate();
    let url = id != 0 ? "EditProducto" : "CreateProducto";

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