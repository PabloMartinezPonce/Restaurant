/*!
        SCRIPT INCRUSTADO EN VISTA DE MESA
 */

/* CONFIGRACIONES  GLOBALES  */
var idMesa = 0;
var jsonMesa = null;
var editarMesa = false;

function AlertExpiration(data) {
    //if (ValidarSesion(data)) {
    //    swal({ title: titleSesion, text: txtSesion, icon: "info" }).then((value) => { window.location = location.origin; });
    //}
}

function ConsultarMesaModal() {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-search"></i> Consultar datos de la mesa: ' + jsonMesa.nombre;
    idMesa = 0;
    BloquearMesa(true);
    LimpiarModalMesa();
    LlenarFormulario();
    $('#modal-mesa').modal({ backdrop: 'static', keyboard: false });
}

function EditarMesaModal(_jsonMesa, _idMesa) {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-edit"></i> Editar datos de la mesa: ' + _jsonMesa.nombre;
    editarMesa = true;
    jsonMesa = _jsonMesa;
    idMesa = _idMesa;
    BloquearMesa(false);
    LimpiarModalMesa();
    LlenarFormulario();
    $('#modal-mesa').modal({ backdrop: 'static', keyboard: false });
}

function CrearMesaModal() {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-user-plus"></i> Crear nueva mesa';
    editarMesa = false;
    idMesa = 0;
    BloquearMesa(false);
    LimpiarModalMesa();
    $('#modal-mesa').modal({ backdrop: 'static', keyboard: false });
}

function LlenarFormulario() {
    document.getElementById("id").value = jsonMesa.id;
    document.getElementById("mesa").value = jsonMesa.mesa;
    document.getElementById("descripcion").value = jsonMesa.descripcion;
}

function LimpiarModalMesa() {
    $('#formMesa').parsley().reset();
    let clearFieldTxt = "";
    let clearFieldInt = 0;
    let clearFieldBool = false;

    document.getElementById("id").value = clearFieldInt;
    document.getElementById("mesa").value = clearFieldTxt;
    document.getElementById("descripcion").value = clearFieldTxt;
}

function BloquearMesa(bloquear) {
    if (!bloquear) {
        document.getElementById('idBtnMesa').style.display = 'block';
        $('#fsMesa').removeAttr("disabled");
    } else {
        document.getElementById('idBtnMesa').style.display = 'none';
        $('#fsMesa').attr('disabled', 'disabled');
    }
}

$('#dtMesas').DataTable({
    responsive: true
});

//Envia a guardar formulario 
$("#idBtnMesa").click(function (event) {
    event.stopPropagation();
    event.preventDefault();

    let $form = $('#formMesa');
    let valid = $form.parsley().validate();
    let url = editarMesa ? "EditMesa" : "CreateMesa";

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
function EliminarMesa(id) {
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
                    url: 'DeleteMesa',
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

//Modificar estatus de mesa
function ModificarEstatus(mesa, estatus, nombre) {
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
                    url: '../../Mesa/ModifyStatus',
                    type: "POST",
                    data: { 'mesa': mesa, 'esCorreo': false, 'enable': !estatus },
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