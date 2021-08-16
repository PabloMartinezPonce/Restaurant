/*!
        SCRIPT INCRUSTADO EN VISTA DE PROVEEDOR
 */

/* CONFIGRACIONES  GLOBALES  */
var idProveedor = 0;
var jsonProveedor = null;
var editarProveedor = false;

function AlertExpiration(data) {
    //if (ValidarSesion(data)) {
    //    swal({ title: titleSesion, text: txtSesion, icon: "info" }).then((value) => { window.location = location.origin; });
    //}
}

function ConsultarProveedorModal() {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-search"></i> Consultar datos del proveedor: ' + jsonProveedor.nombre;
    idProveedor = 0;
    BloquearProveedor(true);
    LimpiarModalProveedor();
    LlenarFormulario();
    $('#modal-proveedor').modal({ backdrop: 'static', keyboard: false });
}

function EditarProveedorModal(_jsonProveedor, _idProveedor) {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-edit"></i> Editar datos del proveedor: ' + _jsonProveedor.nombre;
    editarProveedor = true;
    jsonProveedor = _jsonProveedor;
    idProveedor = _idProveedor;
    BloquearProveedor(false);
    LimpiarModalProveedor();
    LlenarFormulario();
    $('#modal-proveedor').modal({ backdrop: 'static', keyboard: false });
}

function CrearProveedorModal() {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-user-plus"></i> Crear nuevo proveedor';
    editarProveedor = false;
    idProveedor = 0;
    BloquearProveedor(false);
    LimpiarModalProveedor();
    $('#modal-proveedor').modal({ backdrop: 'static', keyboard: false });
}

function LlenarFormulario() {
    document.getElementById("id").value = jsonProveedor.id;
    document.getElementById("nombreContacto").value = jsonProveedor.nombreContacto;
    document.getElementById("compania").value = jsonProveedor.compania;
    document.getElementById("telefono").value = jsonProveedor.telefono;
    document.getElementById("correo").value = jsonProveedor.correo;
    document.getElementById("direccion").value = jsonProveedor.direccion;
}

function LimpiarModalProveedor() {
    $('#formProveedor').parsley().reset();
    let clearFieldTxt = "";
    let clearFieldInt = 0;
    let clearFieldBool = false;

    document.getElementById("id").value = clearFieldInt;
    document.getElementById("nombreContacto").value = clearFieldTxt;
    document.getElementById("compania").value = clearFieldTxt;
    document.getElementById("telefono").value = clearFieldTxt;
    document.getElementById("correo").value = clearFieldTxt;
    document.getElementById("direccion").value = clearFieldTxt;
}

function BloquearProveedor(bloquear) {
    if (!bloquear) {
        document.getElementById('idBtnProveedor').style.display = 'block';
        $('#fsProveedor').removeAttr("disabled");
    } else {
        document.getElementById('idBtnProveedor').style.display = 'none';
        $('#fsProveedor').attr('disabled', 'disabled');
    }
}

$('#dtProveedor').DataTable({
    responsive: true
});

//Envia a guardar formulario 
$("#idBtnProveedor").click(function (event) {
    event.stopPropagation();
    event.preventDefault();

    let $form = $('#formProveedor');
    let valid = $form.parsley().validate();
    let url = editarProveedor ? "EditProveedor" : "CreateProveedor";

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
function EliminarProveedor(id) {
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
                    url: 'DeleteProveedor',
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

//Modificar estatus de proveedor
function ModificarEstatus(proveedor, estatus, nombre) {
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
                    url: '../../Proveedor/ModifyStatus',
                    type: "POST",
                    data: { 'proveedor': proveedor, 'esCorreo': false, 'enable': !estatus },
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