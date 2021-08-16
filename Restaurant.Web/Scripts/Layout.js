/*!
        SCRIPT INCRUSTADO EN VISTA DE LAYOUT
 */

/* CONFIGRACIONES  GLOBALES  */
var titleSesion = "Sesión Expirada";
var txtSesion = "La sesión ha expirado. Por favor, inicie sesión.";
var msjAcceso = "DENEGADO";
var msjToken = "expirado";

function ValidarSesion(respuesta) {
    if (respuesta != "undefined" && respuesta != undefined) {
        if (respuesta.toString().includes(msjAcceso) || respuesta.message.includes(msjToken)) {
            return true;
        } else {
            return false;
        }
    }
}

//Convierte DateTime tipo /Date(120123123123) to dd:mm:yyyy hh:mm:ss am/pm
function ParseDateTime(input) {
    var completedDate = new Date(parseInt(input.replace("/Date(", "").replace(")/")));
    var date = completedDate.toLocaleString([], { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit', second: '2-digit', hour12: true });
    return date;
}

//Convierte DateTime tipo /Date(120123123123) to dd:mm:yyyy
function ParseDate(input) {
    var completedDate = new Date(parseInt(input.replace("/Date(", "").replace(")/")));
    completedDate.setMinutes(completedDate.getMinutes() + completedDate.getTimezoneOffset());
    var date = completedDate.toLocaleString([], { day: '2-digit', month: '2-digit', year: 'numeric' });
    return date;
}

$(document).ready(function () {
    $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
    $("img").on("click", function (event) {
        var x = event.pageX - this.offsetLeft;
        var y = event.pageY - this.offsetTop;
        console.log("X Coordinate: " + x + " Y Coordinate: " + y);
    });
    //md.initDashboardPageCharts();
});


function FullScreen() {
    if (document.fullscreenElement) {
        document.exitFullscreen()
    } else {
        document.documentElement.requestFullscreen()
    } 
}

//addEventListener("click", function () {
//    var el = document.documentElement, rfs = el.requestFullScreen || el.webkitRequestFullScreen || el.mozRequestFullScreen;
//    rfs.call(el);
//});