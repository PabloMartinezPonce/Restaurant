/*!
        SCRIPT INCRUSTADO EN VISTA DE USUARIO
 */

/* CONFIGRACIONES  GLOBALES  */
var idUsuario = 0;
var jsonUser = null;
var editarUsuario = false;

function AlertExpiration(data) {
    //if (ValidarSesion(data)) {
    //    swal({ title: titleSesion, text: txtSesion, icon: "info" }).then((value) => { window.location = location.origin; });
    //}
}

function ConsultarUsuarioModal() {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-search"></i> Consultar datos del usuario: ' + _jsonUser.nombre + ' ' + _jsonUser.apellido;
    idUsuario = 0;
    BloquearUsuario(true);
    LimpiarModalUsuario();
    LlenarFormulario();
    $('#modal-usuario').modal({ backdrop: 'static', keyboard: false });
}

function EditarUsuarioModal(_jsonUser, _idUsuario) {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-edit"></i> Editar datos del usuario: ' + _jsonUser.nombre + ' ' + _jsonUser.apellido;
    editarUsuario = true;
    jsonUser = _jsonUser;
    idUsuario = _idUsuario;
    BloquearUsuario(false);
    LimpiarModalUsuario();
    LlenarFormulario();
    $('#modal-usuario').modal({ backdrop: 'static', keyboard: false });
}

function CrearUsuarioModal() {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-user-plus"></i> Crear nuevo usuario';
    editarUsuario = false;
    idUsuario = 0;
    BloquearUsuario(false);
    LimpiarModalUsuario();
    $('#modal-usuario').modal({ backdrop: 'static', keyboard: false });
}

function LlenarFormulario() {
    document.getElementById("id").value = jsonUser.id;
    document.getElementById("nombre").value = jsonUser.nombre;
    document.getElementById("nombreUsuario").value = jsonUser.nombreUsuario;
    document.getElementById("apellido").value = jsonUser.apellido;
    document.getElementById("telefono").value = jsonUser.telefono;
    document.getElementById("correoElectronico").value = jsonUser.correoElectronico;
    //document.getElementById("contrasena").value = jsonUser.contrasena;
    document.getElementById("direccion").value = jsonUser.direccion;
    document.getElementById("descripcion").value = jsonUser.descripcion;
    document.getElementById("idRol").value = jsonUser.idRol;
}

function LimpiarModalUsuario() {
    $('#formUsuario').parsley().reset();
    let clearFieldTxt = "";
    let clearFieldInt = 0;
    let clearFieldBool = false;

    document.getElementById("id").value = clearFieldInt;
    document.getElementById("nombre").value = clearFieldTxt;
    document.getElementById("apellido").value = clearFieldTxt;
    document.getElementById("correoElectronico").value = clearFieldTxt;
    document.getElementById("contrasena").value = clearFieldTxt;
    document.getElementById("nombreUsuario").value = clearFieldTxt;
    document.getElementById("direccion").value = clearFieldTxt;
    document.getElementById("descripcion").value = clearFieldTxt;
    document.getElementById("idRol").value = clearFieldInt;

    $('#idRol').val(null).trigger('change');
}

function BloquearUsuario(bloquear) {
    if (!bloquear) {
        document.getElementById('idBtnUsuario').style.display = 'block';
        $('#fsUsuario').removeAttr("disabled");
    } else {
        document.getElementById('idBtnUsuario').style.display = 'none';
        $('#fsUsuario').attr('disabled', 'disabled');
    }
}

$('#dtUsuarios').DataTable();

//Envia a guardar formulario 
$("#idBtnUsuario").click(function (event) {
    event.stopPropagation();
    event.preventDefault();

    let $form = $('#formUsuario');
    let valid = $form.parsley().validate();
    let url = editarUsuario ? "EditUser" : "CreateUser";

    if (!valid) {
        return false;
    }
    var formData = $form.serialize();
    console.log(formData);
    $('#formUsuario input[type=checkbox]').each(function () {
        var addCheck = "&" + this.id + "=" + this.checked;
        formData += addCheck;
    });

    $.ajax({
        url: url,
        data: formData,
        type: 'POST',
        success: function (data) {
            if (data.objectResponse) {
                swal({ title: "¡Guardado!", text: data.message, icon: "success" }).then((value) => { });
                location.reload(true);
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
function EliminarUsuario(id) {
    swal({
        title: '¿Esta seguro de eliminarlo?',
        text: 'El registro se eliminará permanentemente.',
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: 'DeleteUser',
                type: "POST",
                data: { 'id': id },
                success: function (data) {
                    if (data.responseCode == 200) {
                        if (ValidarSesion(data)) {
                            swal("Éxito", data.message, "success");
                            location.reload(true);
                        }
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

        } else {
            swal("Cancelado", "No se efectuó ningún cambio.", "error");
        }
    });

}

//Modificar estatus de usuario
function ModificarEstatus(usuario, estatus, nombre) {
    var title = !estatus ? "¿Esta seguro de reactivar a " + nombre + "?" : "¿Esta seguro de bloquear a " + nombre + "?";
    var text = !estatus ? nombre + " volverá a ingresar al sistema" : nombre + " podrá ingresar al sistema";

    swal({
        title: title,
        text: text,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    url: '../../Usuario/ModifyStatus',
                    type: "POST",
                    data: { 'usuario': usuario, 'esCorreo': false, 'enable': !estatus },
                    success: function (data) {
                        if (data.responseCode == 200) {
                            swal("Éxito", data.message, "success");
                            location.reload(true); 
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
        
        } else {
            swal("Cancelado", "No se efectuó ningún cambio.", "error");
        }
    });
}