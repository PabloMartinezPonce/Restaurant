
// ....::::FUNCIONES REPORTES ERRORES::::....

function AlertExpiration(data) {
    if (ValidarSesion(data)) {
        swal({ title: titleSesion, text: txtSesion, icon: "info" }).then((value) => { window.location = location.origin; });
    }
}

var editarReporteMovimiento = false;
var fechaInicio;
var fechaFin;
var nowDate = new Date();
var day = nowDate.getDate();
var month = nowDate.getMonth() + 1;

if (day < 10) day = "0" + day;
if (month < 10) month = "0" + month;

var fechaActual = nowDate.getFullYear() + "/" + month + "/" + day;
var fechaTemp = day + "/" + month + "/" + nowDate.getFullYear();

$('#fechaFiltradoM').daterangepicker({
    "timePicker24Hour": true,
    format: 'DD-MM-YYYY',
    ranges: {
        'Hoy': [moment(), moment()],
        'Ayer': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
        'Útlimos 7 días': [moment().subtract(6, 'days'), moment()],
        'Útlimos 30 días': [moment().subtract(29, 'days'), moment()],
        'Este Mes': [moment().startOf('month'), moment().endOf('month')],
        'Último Mes': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
    },
    //"startDate": "03/28/2020",
    //"endDate": "04/03/2020"
}, function (start, end, label) {
    fechaInicio = start.format('YYYY/MM/DD');
    fechaFin = end.format('YYYY/MM/DD');
    console.log('New date range selected: ' + fechaInicio + ' to ' + fechaFin + ' (predefined range: ' + label + ')');
});

function EliminarReporteMovimiento(id) {
    swal({
        title: '¿Esta seguro de eliminar?',
        text: 'El registro se eliminará permanentemente.',
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: 'EliminarReporteMovimiento',
                    type: "POST",
                    data: { 'id': id },
                    success: function (data) {
                        if (!ValidarSesion(data)) { return false; }

                        if (data.success) {
                            swal({ title: "Éxito", text: "¡Registro Eliminado!", icon: "success" }).then((value) => { location.reload(true); })
                        } else {
                            swal({ title: "Movimiento", text: data.msj, icon: "error" });
                        }
                    },
                    error: function (data) {
                        if (!ValidarSesion(data.responseText)) { return false; }
                    }
                }).done(function (data) { if (!ValidarSesion(data.responseText)) { return false; } });
            }
        });
}

function LimpiarModalReporteMovimiento() {
    editarReporteMovimiento = false;
    document.getElementById('leyendModal').innerHTML = "<i class='fa fa-truck'></i>&nbsp; Agregar Nuevo ReporteMovimiento";
    $('#formReporteMovimiento').parsley().reset();
    document.getElementById("nombre").value = "";
    document.getElementById("telefono").value = "";
    document.getElementById("direccion").value = "";
    document.getElementById("RFC").value = "";
    document.getElementById("empresa").value = "";
}

function VerRegistros(valorA, valorN, modulo, accion, afectado) {
    if (valorA !== "N/A" && valorN !== "N/A") {
        document.getElementById('divValorA').style.display = 'inline-block';
        document.getElementById('divValorN').style.display = 'inline-block';
        document.getElementById('divValorAS').style.display = 'none';
        document.getElementById('divValorNS').style.display = 'none';

        var val1 = valorA.replace(/\:/g, ": </span>");
        var val2 = val1.replace(/\|/g, "<span style='font-weight: bold;'>");
        var val3 = val2.replace(/\Ç/g, "<br />");
        document.getElementById("txtValorA").innerHTML = val3;
        document.getElementById("txtValorAS").innerHTML = val3;

        var val1 = valorN.replace(/\:/g, ": </span>");
        var val2 = val1.replace(/\|/g, "<span style='font-weight: bold;'>");
        var val3 = val2.replace(/\Ç/g, "<br />");
        document.getElementById("txtValorN").innerHTML = val3;
        document.getElementById("txtValorNS").innerHTML = val3;
    } else {
        document.getElementById('divValorA').style.display = 'none';
        document.getElementById('divValorN').style.display = 'none';
        if (valorA !== "N/A") {
            document.getElementById('divValorAS').style.display = 'inline-block';
            var val1 = valorA.replace(/\:/g, ": </span>");
            var val2 = val1.replace(/\|/g, "<span style='font-weight: bold;'>");
            var val3 = val2.replace(/\Ç/g, "<br />");
            document.getElementById("txtValorA").innerHTML = val3;
            document.getElementById("txtValorAS").innerHTML = val3;
        } else {
            document.getElementById('divValorAS').style.display = 'none';
        }
        if (valorN !== "N/A") {
            document.getElementById('divValorNS').style.display = 'inline-block';
            var val1 = valorN.replace(/\:/g, ": </span>");
            var val2 = val1.replace(/\|/g, "<span style='font-weight: bold;'>");
            var val3 = val2.replace(/\Ç/g, "<br />");
            document.getElementById("txtValorN").innerHTML = val3;
            document.getElementById("txtValorNS").innerHTML = val3;
        } else {
            document.getElementById('divValorNS').style.display = 'none';
        }
    }

    document.getElementById("leyendModal").innerHTML = "";
    document.getElementById("leyendModal").innerHTML = "<i class='fa fa-edit fa-x2'></i> <span style='font-weight: bold;'>Movimiento: </span>" + accion + " en módulo " + modulo + " a " + afectado;
    $('#modalReporteM').modal('show');
}

$('#dtReporteMovimientos').DataTable({
    responsive: true,
    "searching": true,
    "ordering": false,
    "paging": true,
    "info": true,
    "lengthMenu": true,
    "lengthChange": false,
    "pageLength": 10
});

function FiltrarReporteMovimientos(verTodo) {
    $.ajax({
        url: 'ObtenerListaMovimientos',
        data: { 'fechaInicio': fechaInicio, 'fechaFin': fechaFin },
        type: 'GET',
        success: function (data) {
            if (data.responseCode == 200) {
                var obj = JSON.parse(data.objectResponse);
                if (obj.length > 0) {
                    PopulateDataTableM(obj);
                } else {
                    $('#dtReporteMovimientos').DataTable().clear().draw();
                    swal({ title: "Sin registros.", text: "No se encontraron registros en la fecha seleccionada", icon: "info" });
                }
            } else {
                swal({ title: "Error.", text: data.msj, icon: "error" });
            }
        },
        error: function (err) {
            swal({ title: "Error Inesperado", text: err.responseText, icon: "error" }).then((value) => { /*window.location = location.origin;*/ })
        }
    }).done(function (data) {
        AlertExpiration(data);
    });
}

function PopulateDataTableM(data) {
    $('#dtReporteMovimientos').DataTable().clear().draw();
    for (var i = 0; i < data.length; i++) {
        var va = "'" + data[i].valorAnterior + "'";
        var vn = "'" + data[i].valorActual + "'";
        var md = "'" + data[i].modulo + "'";
        var tm = "'" + data[i].tipoMovimiento + "'";
        var ra = "'" + data[i].registroAfectado + "'";
        var btnVer = '<a onclick="VerRegistros(' + va + ', ' + vn + ', ' + md + ', ' + tm + ', ' + ra + ')" class="btn btn-chpurple btn-icon btn-circle" style="color:white;" title="Ver Registros"><i class="fa fa-eye"></i></a>';
        var btn = data[i].tipoMovimiento != "Exportar" ? btnVer : "";
        $('#dtReporteMovimientos').dataTable().fnAddData([
            data[i].id,
            data[i].tipoMovimiento,
            data[i].modulo,
            data[i].Usuarios.correoElectronico,
            data[i].registroAfectado,
            ParseDateTime(data[i].fecha),
            btn
        ]);
    }
}

// REPORTES

function PopulateDataTablePP(data) {

    for (var i = 0; i < data.length; i++) {
        $('#tblPacienteProtocolo').dataTable().fnAddData([
            data[i].id,
            data[i].nombre + " " + data[i].primerApellido + " " + data[i].segundoApellido,
            data[i].telefono,
            data[i].CURP,
            data[i].estadoCivil,
            data[i].Nacionalidad.pais,
            ParseDateTime(data[i].fechaNacimiento),
        ]);
    }
}

$('#tblPacienteProtocolo').DataTable({
    dom: 'Bfrtip',
    buttons: [
        { extend: 'copy', className: 'btn-sm' },
        { extend: 'csv', className: 'btn-sm' },
        { extend: 'excel', className: 'btn-sm' },
        { extend: 'pdf', className: 'btn-sm' },
        { extend: 'print', className: 'btn-sm' }
    ],
    responsive: true
});

$('#tblPacienteEnfermedad').DataTable({
    dom: 'Bfrtip',
    buttons: [
        { extend: 'copy', className: 'btn-sm' },
        { extend: 'csv', className: 'btn-sm' },
        { extend: 'excel', className: 'btn-sm' },
        { extend: 'pdf', className: 'btn-sm' },
        { extend: 'print', className: 'btn-sm' }
    ],
    responsive: true
});

$('#tblMedicoProtocolo').DataTable({
    dom: 'Bfrtip',
    buttons: [
        { extend: 'copy', className: 'btn-sm' },
        { extend: 'csv', className: 'btn-sm' },
        { extend: 'excel', className: 'btn-sm' },
        { extend: 'pdf', className: 'btn-sm' },
        { extend: 'print', className: 'btn-sm' }
    ],
    responsive: true
});

$('#txtPacienteProtocolo').select2(
    {
        width: "100%", placeholder: "Ingrese número de protocolo",
        minimumInputLength: 1,
        ajax: {
            url: '../../Reportes/ObtenerProtocolos',
            dataType: 'json',
            async: false,
            data: function (params) {
                return {
                    q: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            }
        }
    });

$('#txtPacienteProtocolo').change(function () {
    var idPProtocolo = $(this).val();
    $('#tblPacienteProtocolo').DataTable().clear().draw();
    if (idPProtocolo !== undefined && idPProtocolo !== null) {
        $.ajax({
            url: 'ObtenerPacientesProtocolo',
            data: { 'id': idPProtocolo },
            type: 'GET',
            success: function (data) {
                if (!ValidarSesion(data)) { return false; }

                if (data.exito) {
                    if (data.objeto.length > 0) {
                        PopulateDataTablePP(data.objeto);
                    } else {
                        $('#tblPacienteProtocolo').DataTable().clear().draw();
                        swal({ title: "Sin registros.", text: "No se encontraron registros en el protocolo seleccionado", icon: "info" });
                    }
                } else {
                    swal({ title: "Error.", text: "No se encontraron registros en el protocolo seleccionado", icon: "error" });
                }
            },
            error: function (data) {
                if (!ValidarSesion(data.responseText)) { return false; }
            }
        }).done(function (data) { if (!ValidarSesion(data.responseText)) { return false; } });
    }
});