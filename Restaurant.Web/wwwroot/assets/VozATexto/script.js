//var rec;
//var frase;
//var nombre = usuario.nombre;
//rec = new webkitSpeechRecognition();
//rec.lang = "es-MX";
//rec.continuous = true;
//rec.interim = true;
//rec.addEventListener("result", Escuchar);

//function Hablame() {
//    rec.start();
//}

//function Escuchar(event) {
//    for (let i = event.resultIndex; i < event.results.length; i++) {
//        Hablar(event.results[i][0].transcript);
//        rec.abort();
//    }
//}

//function Hablar(texto) {
//    if (texto.includes("inicio")) {
//        frase = "Redirigiendo a inicio";
//        window.location.href = location.origin + "/Login/Dashboard";
//    } else if (texto.includes("perfil")) {
//        frase = "Redirigiendo a tu perfil";
//        window.location.href = location.origin + "/Usuario/MiPerfil";
//    } else if (texto.includes("sesión")) {
//        frase = "Cerrando sesion";
//        window.location.href = location.origin;
//    } else if (texto.includes("pedido")) {
//        frase = "Selecciona una mesa";
//        $('#modal-cuentas').modal({ backdrop: 'static', keyboard: false });
//    } else if (texto.includes("creador") || texto.includes("creó")) {
//        frase = "El ingeniero Pablo Martínez, es un papucho, su cara parece tallada por los mismos ángeles.";
//    } else if (texto.toLowerCase().includes("siri") || texto.toLowerCase().includes("cortana")) {
//        frase = "No sea mamón " + nombre + ", me llamo Rita";
//    } else if (texto.includes("quién eres") || texto.includes("te llamas")) {
//        frase = "Hola " + nombre + ", mi nombre es Rita Miler y soy la asistente personal del bar las paseras.";
//    } else if (texto.includes("eres real")) {
//        frase = "Obvio microbio, saca las frías o que?.";
//    } else if (texto.includes("Saca las cheves")) {
//        frase = "Ya me volví niño ueno uei.";
//    } else if (texto.includes("Hola")) {
//        frase = "Hola " + nombre + ", como va todo?.";
//    } else if (texto.includes("ventas")) {
//        frase = "2 3, ahí van más o menillos, pero velo por ti mismo. Te redirecciono a tus ventas.";
//    } else if (texto.includes("canta")) {
//        frase = "taaamaaaarindooooo, 1 2 y 3 tamarindo 1 2 y 3 tamarindo.";
//    } else if (texto.includes("Chema o Cristian") || texto.includes("Cristian o Chema") || texto.includes("Chema y Cristian") || texto.includes("Cristian y Chema")) {
//        frase = "Christian picha cervezas a cualquier morra, es un power ranger, y chema siempre se clava el cambio, valen madre los dos.";
//    } else if (texto.includes("Buenos días")) {
//        frase = "Buenos días " + nombre + ", uachen el paisaje jomis.";
//    } else if (texto.includes("te gusta el campo")) {
//        frase = "Me gusta el campo esseee, me voy a hacer un chanque de poca madre, con albercas cercas de estacas blancas y todo. jeeey chema está de aquellas tu mamacita, mua.";
//    } else if (texto.includes("mejor bar")) {
//        frase = "Obviamente el bar las paseras, somos los nuevos dueños del rancho, los vatos locos ya marcharon.";
//    } else if (texto.includes("te gusta tu trabajo") || texto.includes("que se siente ser asistente")) {
//        frase = "Pues ahí más o menillos. Sabes por que me gusta ser asistente personal, pa darle una feria a mi jefe y una feria pa mi viejo, y pal comprar vicio claro, pa comprar un vino, un toquecito de mota, el chemo y unas chevecitas. y el foquín.";
//    } else if (texto.includes("cuántos años tienes")) {
//        frase = "Ya tengo muchos años de asistente, no toda mi vida pero ya tengo años. O sea que no tengo toda mi vida pero ya tengo años, yo";
//    } else if (texto.includes("nueva tecnología")) {
//        frase = "¿Como de la nueva?, es que no entiendo.";
//    } else if (texto.includes("manda saludos")) {
//        frase = "jeeei. whaaats up jomis";
//    } else if (texto.includes("Qué día es hoy")) {
//        frase = GetDayPhrase();
//    } else if (texto.includes("Carlos")) {
//        frase = "¿Quien será?, ¡Oh! Carlos la vaca"
//    } else {
//        frase = "No entendí " + nombre + ". por favor dilo de nuevo";
//    }
//    console.log("Indentifique: " + texto);
//    $("#btnSpeak").click();
//}

//// TEST (No está en función)
//function playSentence(text) {
//    var msg = new SpeechSynthesisUtterance();
//    msg.text = frase;
//    window.speechSynthesis.speak(msg);
//}

//$("#btnSpeak").click(function () {
//    console.log("Va a decir: " + frase);
//    speechSynthesis.speak(new SpeechSynthesisUtterance(frase));
//});

//function AgregarProducto() {
//    let items = texto.split(" ");
//    if (items.length == 5) {
//        frase = "¡Hecho!. " + items[0] + items[1] + items[2] + "para la mesa numero " + items[4]
//        let cantidad = GetNumber(items[0]);
//        let producto = items[2];
//        let mesa = items[4];
//    }
//    else {
//        frase = "No entendi. por favor usa el formato de cinco silabas, por ejemplo. 5 cervezas corona mesa 6. intentalo de nuevo"
//    }
//}

//function AgregarMesa() {

//}

//function AgregarProducto() {

//}

//function GetDayPhrase() {
//    var Xmas95 = new Date();
//    var weekday = Xmas95.getDay();
//    switch (weekday) {
//        case 0:
//            return "Hoy es domingazo de cruz";
//        case 1:
//            return "lune, odio lo lune";
//        case 2:
//            return "marte, también odio lo marte";
//        case 3:
//            return "hoy es miércoles de ceniza";
//        case 4:
//            return "hoy es jueveves";
//        case 5:
//            return "hoy es viernecin";
//        case 6:
//            return "hoy es sabadrink";
//        default:
//            return "No sé ni en que día vivo joms";
//    }
//}

//function GetNumber(txtNumber) {
//    switch (txtNumber) {
//        case 'uno':
//        case 'una':
//        case 'unos':
//        case 'unas':
//            return 1;
//        case 'dos':
//            return 2;
//        case 'tres':
//            return 3;
//        case 'cuatro':
//            return 4;
//        case 'cinco':
//            return 5;
//        case 'seis':
//            return 6;
//        case 'siete':
//            return 7;
//        case 'ocho':
//            return 8;
//        case 'nueve':
//            return 9;
//        default:
//            return null;
//    }
//}