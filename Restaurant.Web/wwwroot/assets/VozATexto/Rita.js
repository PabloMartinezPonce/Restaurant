/*!
        SCRIPT INCRUSTADO EN SUB VISTA RITA
 */

var rec;
var frase;
var fraseUsuario;
//var nombre = usuario.nombre;
var formatoIncorrecto = "Comando inválido. por favor usa el formato correcto, por ejemplo. 5 coronas para la mesa 6, intentalo de nuevo";
rec = new webkitSpeechRecognition();
rec.lang = "es-MX";
rec.continuous = true;
rec.interim = true;
rec.addEventListener("result", Escuchar);

function AlertExpiration(data) {
    if (ValidarSesion(data)) {
        swal({ title: titleSesion, text: txtSesion, icon: "info" }).then((value) => { window.location = location.origin; });
    }
}

function Hablame() {
    MicroOn();
    //$('#modal-cuentas').modal({ backdrop: 'static', keyboard: false });
    $('#modal-cuentas').modal('show');
    rec.start();
}

function Escuchar(event) {
    for (let i = event.resultIndex; i < event.results.length; i++) {
        Hablar(event.results[i][0].transcript);
        rec.abort();
        MicroOff();
    }
}

function Hablar(texto) {
    fraseUsuario = texto;
    if (texto.includes("inicio") || texto.includes("estadistica") || texto.includes("dashboard")) {
        frase = "Redirigiendo a dashboard";
        window.location.href = location.origin + "/Login/Dashboard";
    } else if (texto.includes("producto") && (texto.includes("crear") || texto.includes("creo") || texto.includes("nuevo"))) {
        frase = "Selecciona el botón nuevo, llena los campos y guarda.";
        window.location.href = location.origin + "/Producto/Productos";
    } else if (texto.includes("producto") && (texto.includes("ver") || texto.includes("cuantos") || texto.includes("donde"))) {
        frase = "Aquí tienes todos los productos registrados.";
        window.location.href = location.origin + "/Producto/Productos";
    } else if (texto.includes("complemento") && (texto.includes("crear") || texto.includes("creo") || texto.includes("nuevo"))) {
        frase = "Selecciona el botón nuevo, llena los campos y guarda.";
        window.location.href = location.origin + "/Complemento/Complementos";
    } else if (texto.includes("complemento") && (texto.includes("ver") || texto.includes("cuantos") || texto.includes("donde"))) {
        frase = "Aquí tienes todos los complementos registrados.";
        window.location.href = location.origin + "/Complemento/Complementos";
    } else if (texto.includes("categoria") && (texto.includes("crear") || texto.includes("creo") || texto.includes("nuevo"))) {
        frase = "Selecciona el botón nuevo, llena los campos y guarda.";
        window.location.href = location.origin + "/Categoria/Categorias";
    } else if (texto.includes("categoria") && (texto.includes("ver") || texto.includes("cuantos") || texto.includes("donde"))) {
        frase = "Aquí tienes todas las categorías registradas.";
        window.location.href = location.origin + "/Categoria/Categorias";
    } else if (texto.includes("usuario") && (texto.includes("crear") || texto.includes("creo") || texto.includes("nuevo"))) {
        frase = "Selecciona el botón nuevo, llena los campos y guarda.";
        window.location.href = location.origin + "/Usuario/Usuarios";
    } else if ((texto.includes("usuario") || (texto.includes("staf") || (texto.includes("empleados")) && (texto.includes("ver") || texto.includes("cuantos") || texto.includes("donde"))))) {
        frase = "Aquí tienes todos los usuarios registrados.";
        window.location.href = location.origin + "/Usuario/Usuarios";
    } else if (texto.includes("perfil")) {
        frase = "Redirigiendo a tu perfil";
        window.location.href = location.origin + "/Usuario/MiPerfil";
    } else if (texto.includes("sesión")) {
        frase = "Cerrando sesion";
        window.location.href = location.origin;
    } else if (texto.includes("nuevo pedido")) {
        frase = "Selecciona una mesa";
        $('#modalPedidoManual').modal('show');
        //$('#modalPedidoManual').modal({ backdrop: 'static', keyboard: false });
    } else if (texto.toLowerCase().includes("abrir cuenta") || texto.toLowerCase().includes("abre cuenta") || texto.toLowerCase().includes("nueva cuenta")) {
        AbrirCuenta();
        return false;
    } else if (texto.includes("agregar") && texto.includes("cuenta")) {
        AgregarProducto();
        return false;
    }
    else if (texto.includes("creador") || texto.includes("creó")) {
        frase = "El ingeniero Pablo Martínez, es un papucho, su cara parece tallada por los mismos ángeles.";
    } else if (texto.toLowerCase().includes("siri") || texto.toLowerCase().includes("cortana")) {
        frase = "No sea mamón " + nombre + ", me llamo Rita";
    } else if (texto.includes("quién eres") || texto.includes("te llamas")) {
        frase = "Hola " + nombre + ", mi nombre es Rita Miller y soy la asistente personal del bar las paseras.";
    } else if (texto.includes("eres real")) {
        frase = "Obvio microbio, saca las frías o que?.";
    } else if (texto.includes("Saca las cheves")) {
        frase = "Ya me volví niño ueno uei.";
    } else if (texto.includes("Hola")) {
        frase = "Hola " + nombre + ", como va todo?.";
    } else if (texto.includes("ventas")) {
        frase = "2 3, ahí van más o menillos, pero velo por ti mismo. Te redirecciono a tus ventas.";
    } else if (texto.includes("canta")) {
        frase = "taaamaaaarindooooo, 1 2 y 3 tamarindo 1 2 y 3 tamarindo.";
    } else if (texto.includes("Chema o Cristian") || texto.includes("Cristian o Chema") || texto.includes("Chema y Cristian") || texto.includes("Cristian y Chema")) {
        frase = "Christian picha cervezas a cualquier morra, es un power ranger, y chema siempre se clava el cambio, valen madre los dos.";
    } else if (texto.includes("Buenos días")) {
        frase = "Buenos días " + nombre + ", uachen el paisaje jomis.";
    } else if (texto.includes("te gusta el campo")) {
        frase = "Me gusta el campo esseee, me voy a hacer un chanque de poca madre, con albercas cercas de estacas blancas y todo. jeeey " + nombre + " está de aquellas tu mamacita, mua.";
    } else if (texto.includes("mejor bar")) {
        frase = "Obviamente el bar las paseras, somos los nuevos dueños del rancho, los vatos locos ya marcharon.";
    } else if (texto.includes("te gusta tu trabajo") || texto.includes("que se siente ser asistente")) {
        frase = "Pues ahí más o menillos. Sabes por que me gusta ser asistente personal, pa darle una feria a mi jefe y una feria pa mi viejo, y pal comprar vicio claro, pa comprar un vino, un toquecito de mota, el chemo y unas chevecitas. y el foquín.";
    } else if (texto.includes("cuántos años tienes")) {
        frase = "Ya tengo muchos años de asistente, no toda mi vida pero ya tengo años. O sea que no tengo toda mi vida pero ya tengo años, yo";
    } else if (texto.includes("nueva tecnología")) {
        frase = "¿Como de la nueva?, es que no entiendo.";
    } else if (texto.includes("manda saludos")) {
        frase = "jeeei. whaaats up jomis";
    } else if (texto.includes("Qué día es hoy")) {
        frase = GetDayPhrase();
    } else if (texto.includes("Carlos")) {
        frase = "¿Quien será?, ¡Oh! Carlos la vaca"
    } else {
        frase = "No entendí " + nombre + ". por favor dilo de nuevo";
    }
    console.log("Indentifique: " + texto);
    $("#btnSpeak").click();
}

// TEST (No está en función)
function playSentence(text) {
    var msg = new SpeechSynthesisUtterance();
    msg.text = frase;
    window.speechSynthesis.speak(msg);
}

$("#btnSpeak").click(function () {
    console.log("Va a decir: " + frase);
    speechSynthesis.speak(new SpeechSynthesisUtterance(frase));
});

$("#btnSpeakOFF").click(function () {
    MicroOn();
    rec.start();
});

$("#btnSpeakON").click(function () {
    rec.abort();
    MicroOff();
});

function GetData() {
    let items = fraseUsuario.split(" ");
    let cantidad = GetNumber(items[0]);
    let producto = GetProduct(items);
    let index = items.indexOf('mesa') + 1;
    let idMesa = GetNumber(items[index]);
}

function AbrirCuenta() {
    let mesa = "mesa";
    let textMesa = fraseUsuario.slice(fraseUsuario.indexOf(mesa) + mesa.length);
    let IdMesa = GetNumber(textMesa.trim());

    if (IdMesa != undefined) {
        event.stopPropagation();
        event.preventDefault();
        $.ajax({
            url: '../../Cuenta/CreateCuentaAssistent',
            type: "POST",
            data: { 'IdMesa': parseInt(IdMesa), 'IdEmpleado': parseInt(idUsuarioLogged) },
            success: function (data) {
                if (data.responseCode == 200) {
                    frase = data.message;
                    window.location.href = "/Cuenta/FormAgregarProducto?id=" + data.objectResponse;
                } else {
                    frase = data.message;
                }
            },
            error: function (err) {
                frase = formatoIncorrecto;
            }
        }).done(function (data) {
            //AlertExpiration(data);
            console.log("Indentifiqué: " + fraseUsuario);
            $("#btnSpeak").click();
        });
    }
    else {
        frase = formatoIncorrecto;
    }
}

function EnviarVenta() {
    swal({
        title: "Confirmar Pedido",
        text: "¿Estás segur@? el pedido será agregado a la cuenta",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $("#btnEnviarVenta").click();
        } else {
            swal("Cancelado", "No se efectuó ningún cambio.", "error");
        }
    });
}

//ValidarPedido antes de mostrarlo
function AgregarProducto() {
    event.stopPropagation();
    event.preventDefault();

    let items = fraseUsuario.split(" ");
    let cantidad = GetNumber(items[1]);
    let producto = GetProduct(items);
    let index = items.indexOf('cuenta') + 1;
    let idCuenta = GetNumber(items[index]);

    if (fraseUsuario.includes("con")) {
        //Get Cuenta
        let index2 = items.indexOf('con') + 1;
        let complemento = GetNumber(items[index2]);
        console.log(cantidad + " " + producto + " a cuenta " + idCuenta + " con cerveza " + complemento);
    } else {
        console.log(cantidad + " " + producto + " a cuenta " + idCuenta);
    }
    if (fraseUsuario.includes("de")) {
        //Get Cuenta
        let index3 = items.indexOf('de') + 1;
        let tamaño = GetNumber(items[index3]);
        console.log(cantidad + " " + producto + " a cuenta " + idCuenta + " con cerveza " + tamaño);
    } else {
        console.log(cantidad + " " + producto + " a cuenta " + idCuenta);
    }

    $.ajax({
        url: '../../Cuenta/ValidaPedido',
        type: "POST",
        data: { 'IdCuenta': parseInt(idCuenta), 'Cantidad': parseInt(cantidad), 'Nombre': producto },
        success: function (data) {
            if (data.responseCode == 200) {
                //Obtener precio de producto y calcular total
                frase = "Revisa el pedido, " + cantidad + producto + " para la mesa número " + idCuenta;
            } else {
                frase = formatoIncorrecto;
            }
        },
        error: function (err) {
            frase = formatoIncorrecto;
        }
    }).done(function (data) {
        //AlertExpiration(data);
        console.log("Indentifiqué: " + fraseUsuario);
        $("#btnSpeak").click();
    });
}

//Envia a guardar venta 
$("#btnEnviarVenta").click(function (event) {
    event.stopPropagation();
    event.preventDefault();

    let $form = $('#formVenta');
    var formData = $form.serialize();
    console.log(formData);

    $.ajax({
        url: "../../Venta/CreateVenta",
        data: formData,
        type: 'POST',
        success: function (data) {
            if (data.responseCode == 200) {
                $('#modalPedidoAuto').modal('toggle');
                //$('#modalCuenta').modal({ backdrop: 'static', keyboard: false });
                $('#modalCuenta').modal('show');
                PopulateSales(data.objectResponse);
                swal({ title: "¡Guardado!", text: "Se agregó el pedido exitosamente.", icon: "success" });
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

//$('#dtCuenta').DataTable({
//    responsive: true,
//    paging: false,
//    searching: false,
//    ordering: false,
//    info: false,
//});

//function PopulateSales(data) {
//    $('#dtCuenta').DataTable().clear().draw();
//    for (var i = 0; i < data.length; i++) {
//        var rowImg = '<img src="~/Content/images/productos/Corona.png" width="70" alt=pro />';
//        var rowPrecio = '<span class="text-right" style="font-size:25px; color:white">$' + data[i].precioVenta + '</span>';
//        $('#dtCuenta').dataTable().fnAddData([
//            rowImg,
//            data[i].unidades + " " + data[i].producto,
//            rowPrecio
//        ]);
//    }
//}

function GetProduct(items) {
    var producto;
    producto = items[2];

    if (items[3] != "cuenta" && items[3] != "para" && items[3] != "la" && items[3] != "a" && items[3] != "en") {
        producto = producto + " " + items[3];
    } else {
        return producto;
    }
    if (items[4] != "cuenta" && items[4] != "para" && items[4] != "la" && items[4] != "a" && items[4] != "en") {
        producto = producto + " " + items[4];
    } else {
        return producto;
    }
    if (items[5] != "cuenta" && items[5] != "para" && items[5] != "la" && items[5] != "a" && items[5] != "en") {
        producto = producto + " " + items[5];
    } else {
        return producto;
    }

    return producto;
}

function MicroOn() {
    document.getElementById("txtIdicacion").innerHTML = '¡Escuchando!';
    document.getElementById('btnSpeakON').style.display = 'block';
    document.getElementById('btnSpeakOFF').style.display = 'none';
}

function MicroOff() {
    document.getElementById("txtIdicacion").innerHTML = '¡Da click y di lo tuyo!';
    document.getElementById('btnSpeakON').style.display = 'none';
    document.getElementById('btnSpeakOFF').style.display = 'block';
}

function GetDayPhrase() {
    var Xmas95 = new Date();
    var weekday = Xmas95.getDay();
    switch (weekday) {
        case 0:
            return "Hoy es domingazo de cruz";
        case 1:
            return "lune, odio lo lune";
        case 2:
            return "marte, también odio lo marte";
        case 3:
            return "hoy es miércoles de ceniza";
        case 4:
            return "hoy es jueveves";
        case 5:
            return "hoy es viernes de ahorcar rucas";
        case 6:
            return "hoy es sabadrink";
        default:
            return "No sé ni en que día vivo joms";
    }
}

function GetNumber(txtNumber) {
    switch (txtNumber.toLowerCase()) {
        case 'un':
        case 'uno':
        case 'una':
        case 'unos':
        case 'unas':
            return 1;
        case 'dos':
            return 2;
        case 'tres':
            return 3;
        case 'cuatro':
            return 4;
        case 'cinco':
            return 5;
        case 'seis':
            return 6;
        case 'siete':
            return 7;
        case 'ocho':
            return 8;
        case 'nueve':
            return 9;
        default:
            return txtNumber;
    }
}