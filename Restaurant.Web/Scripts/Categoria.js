/*!
        SCRIPT INCRUSTADO EN VISTA DE CATEGORIA
 */

/* CONFIGRACIONES  GLOBALES  */
var idCategoria = 0;
var jsonCategoria = null;
var editarCategoria = false;

function AlertExpiration(data) {
    //if (ValidarSesion(data)) {
    //    swal({ title: titleSesion, text: txtSesion, icon: "info" }).then((value) => { window.location = location.origin; });
    //}
}

function ConsultarCategoriaModal() {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-search"></i> Consultar datos de la categoría: ' + jsonCategoria.nombre;
    idCategoria = 0;
    BloquearCategoria(true);
    LimpiarModalCategoria();
    LlenarFormulario();
    $('#modal-categoria').modal({ backdrop: 'static', keyboard: false });
}

function EditarCategoriaModal(_jsonCategoria, _idCategoria) {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-edit"></i> Editar datos de la categoría: ' + _jsonCategoria.nombre;
    editarCategoria = true;
    jsonCategoria = _jsonCategoria;
    idCategoria = _idCategoria;
    BloquearCategoria(false);
    LimpiarModalCategoria();
    LlenarFormulario();
    $('#modal-categoria').modal({ backdrop: 'static', keyboard: false });
}

function CrearCategoriaModal() {
    document.getElementById("modalTitle").innerHTML = '<i class="fa fa-user-plus"></i> Crear nueva categoría';
    editarCategoria = false;
    idCategoria = 0;
    BloquearCategoria(false);
    LimpiarModalCategoria();
    $('#modal-categoria').modal({ backdrop: 'static', keyboard: false });
}

function LlenarFormulario() {
    document.getElementById("id").value = jsonCategoria.id;
    document.getElementById("nombre").value = jsonCategoria.nombre;
    document.getElementById("descripcion").value = jsonCategoria.descripcion;
}

function LimpiarModalCategoria() {
    $('#formCategoria').parsley().reset();
    let clearFieldTxt = "";
    let clearFieldInt = 0;
    let clearFieldBool = false;

    document.getElementById("id").value = clearFieldInt;
    document.getElementById("nombre").value = clearFieldTxt;
    document.getElementById("descripcion").value = clearFieldTxt;
}

function BloquearCategoria(bloquear) {
    if (!bloquear) {
        document.getElementById('idBtnCategoria').style.display = 'block';
        $('#fsCategoria').removeAttr("disabled");
    } else {
        document.getElementById('idBtnCategoria').style.display = 'none';
        $('#fsCategoria').attr('disabled', 'disabled');
    }
}

$('#dtCategorias').DataTable({
    responsive: true
});

//Envia a guardar formulario 
$("#idBtnCategoria").click(function (event) {
    event.stopPropagation();
    event.preventDefault();

    let $form = $('#formCategoria');
    let valid = $form.parsley().validate();
    let url = editarCategoria ? "EditCategoria" : "CreateCategoria";

    if (!valid) {
        return false;
    }
    var formData = $form.serialize();
    $('#formCategoria input[type=checkbox]').each(function () {
        var addCheck = "&" + this.id + "=" + this.checked;
        formData += addCheck;
    });

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
function EliminarCategoria(id) {
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
                    url: 'DeleteCategoria',
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

//Modificar estatus de categoria
function ModificarEstatus(categoria, estatus, nombre) {
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
                    url: '../../Categoria/ModifyStatus',
                    type: "POST",
                    data: { 'categoria': categoria, 'esCorreo': false, 'enable': !estatus },
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