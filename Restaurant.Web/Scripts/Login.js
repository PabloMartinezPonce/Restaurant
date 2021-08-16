/*!
        SCRIPT INCRUSTADO EN VISTA LOGIN
 */

/* CONFIGRACIONES  GLOBALES  */
var re = /^(([^<>()\[\]\\.,;:\s"]+(\.[^<>()\[\]\\.,;:\s"]+)*)|(".+"))((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
var intentos = 0;
var usuario = document.getElementById("usuario");
var urlLogin;
var urlDisable;


// INJECCTION DE VARIABLES
var Login = Login || (function () {
    var _args = {}; // private

    return {
        init: function (Args) {
            _args = Args;
            // some other initialising
        },
        helloWorld: function () {
            urlLogin = _args[0];
            urlDisable = _args[1];
        }
    };
}());


function Loading(mostrar) {
    if (mostrar) {
        $('.spinnerLogin').show();
        $('.labelSpiner').show();
    }
    else {
        $('.spinnerLogin').hide();
        $('.labelSpiner').hide();
    }
}
Loading(false);

$(document).ready(function () {
    App.init();
    LoginV2.init();

    function EsCorreo() {
        if (re.test(document.getElementById("usuario").value)) {
            document.getElementById("esCorreo").value = true;
        } else {
            document.getElementById("esCorreo").value = false;
        }
    }

    $('#loginForm').submit(function (e) {
        e.preventDefault();
        Loading(true);
        EsCorreo();
        //url = "@Url.Content("~/Login/ValidateLogin")"
        parametros = $(this).serialize();

        $.ajax({
            url: urlLogin,
            data: parametros,
            type: 'POST',
            success: function (data) {
                //$('.spinnerLogin').show();
                var obj = JSON.parse(data);
                if (obj.responseCode == 200) {
                    document.location.href = "Login/Dashboard";
                } else {
                    intentos = obj.message.includes("incorrecta") ? intentos + 1 : intentos;
                    if (intentos == 3) {
                        BloquearUsuario(parametros);
                    } else {
                        swal({ title: "Error", text: obj.message, icon: "error" });
                    }
                }
            }
        }).done(function (data) {
            Loading(false);
        });;

        function BloquearUsuario(parametros) {
            Loading(true);
            $.ajax({
                url: urlDisable,
                data: parametros,
                type: 'POST',
                success: function (data) {
                    var obj = JSON.parse(data);
                    if (obj.responseCode == 200) {
                        intentos = 0;
                        swal({ title: "¡Demasiados intentos!", text: "La cuenta ha sido bloqueada temporalmente.", icon: "warning", }).then((value) => { location.reload(true); })
                    }
                }
            }).done(function (data) {
                Loading(false);
            });;

        }
    });
});