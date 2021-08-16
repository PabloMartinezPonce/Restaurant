/*
Template Name: Color Admin - Responsive Admin Dashboard Template build with Twitter Bootstrap 3 & 4
Version: 4.0.0
Author: Sean Ngu
Website: http://www.seantheme.com/color-admin-v4.0/admin/
*/

function AlertExpiration(data) {
    if (ValidarSesion(data)) {
        swal({ title: titleSesion, text: txtSesion, icon: "info" }).then((value) => { window.location = location.origin; });
    }
}

var handleAjaxConsoleLog = function (settings, response) {
    var s = [], str;
    s.push(settings.type.toUpperCase() + ' url = "' + settings.url + '"');
    for (var a in settings.data) {
        if (settings.data[a] && typeof settings.data[a] === 'object') {
            str = [];
            for (var j in settings.data[a]) {
                str.push(j + ': "' + settings.data[a][j] + '"');
            }
            str = '{ ' + str.join(', ') + ' }';
        } else {
            str = '"' + settings.data[a] + '"';
        }
        s.push(a + ' = ' + str);
    }
    s.push('RESPONSE: status = ' + response.status);

    if (response.responseText) {
        if ($.isArray(response.responseText)) {
            s.push('[');
            $.each(response.responseText, function (i, v) {
                s.push('{value: ' + v.value + ', text: "' + v.text + '"}');
            });
            s.push(']');
        } else {
            s.push($.trim(response.responseText));
        }
    }
    s.push('--------------------------------------\n');
    $('#console').val(s.join('\n') + $('#console').val());
};

var handleEditableFormAjaxCall = function () {

    // mockjax used to create a fake ajax call without a host
    $.mockjaxSettings.responseTime = 500;

    $.mockjax({
        url: '/post',
        response: function (settings) {
            handleAjaxConsoleLog(settings, this);
        }
    });

    // for groups field respond
    $.mockjax({
        url: '/groups',
        response: function (settings) {
            this.responseText = [
                { value: 0, text: 'Guest' },
                { value: 1, text: 'Service' },
                { value: 2, text: 'Customer' },
                { value: 3, text: 'Operator' },
                { value: 4, text: 'Support' },
                { value: 5, text: 'Admin' }
            ];
            handleAjaxConsoleLog(settings, this);
        }
    });

    $.mockjax({
        url: '/status',
        status: 500,
        response: function (settings) {
            this.responseText = 'Internal Server Error';
            handleAjaxConsoleLog(settings, this);
        }
    });
};

var handleEditableFieldConstruct = function () {
    $.fn.editable.defaults.mode = 'inline';
    $.fn.editable.defaults.inputclass = 'form-control input-sm';
    $.fn.editable.defaults.url = '../Configuraciones/UpdateAyuda';
    var requerido = " Este campo es requerido";
    $('#1').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#2').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#3').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#4').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#5').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#6').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#7').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#8').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#9').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#10').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#11').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#12').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#13').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#14').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#15').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#16').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#17').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#18').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#19').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#20').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#21').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    $('#22').editable({
        validate: function (value) {
            if ($.trim(value) === '') {
                return requerido;
            }
        },
        url: function (params) {
            return SendPost(params);
        }
    });
    function SendPost(params) {
        $.ajax({
            type: 'POST',
            url: '../Configuraciones/UpdateSistema',
            data: JSON.stringify(params),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: true,
            cache: false,
            success: function (data) {
                if (data.objectResponse) {
                    swal({ title: "¡Guardado!", text: data.message, icon: "success" }).then((value) => { location.reload(true); });
                } else {
                    swal({ title: "Error.", text: data.message, icon: "error" }).then((value) => { location.reload(true); });
                }
            }, error: function (err) {
                swal({ title: "Error Inesperado", text: err.responseText, icon: "error" }).then((value) => { location.reload(true); })
            }
        }).done(function (data) {
            AlertExpiration(data);
        });
    }
};

var FormEditable = function () {
    "use strict";
    return {
        //main function
        init: function () {
            handleEditableFieldConstruct();
            handleEditableFormAjaxCall();
        }
    };
}();