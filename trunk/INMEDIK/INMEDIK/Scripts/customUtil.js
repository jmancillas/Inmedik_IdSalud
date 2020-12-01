function mostrarPantallaCarga() {
    quitarPantallaCarga();
    var pantallaCarga =
    '<div id="loading-window" class="loading-window background-gray">' +
        '<!-- Tipo Modal -->' +
        '<div class="loading-content">' +
            '<!-- Info -->' +
            '<div class="loading-info-container text-center">' +
                '<p>Cargando...</p>' +
            '</div>' +
            '<!-- Icono de carga -->' +
            '<div class="loading-icon-container text-center">' +
                '<span class="glyphicon glyphicon-refresh gly-spin"></span>' +
            '</div>' +
        '</div>' +
    '</div>';
    $('body').append(pantallaCarga);
}

function quitarPantallaCarga() {
    $('.loading-window').remove();
}

function Ut_numberWithCommas(n) {
    var text = "";
    if (n || n == 0) {
        var x = n.toString();
        text = "$" + x.split(".")[0].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + (x.split(".")[1] != undefined ? "." + x.split(".")[1] : "");
    }
    return text;
}

function Ut_getRegularExpretions() {
    var r = {};

    r.onlyNumbers =
	    {exp : /^[0-9]+$/,
	    msg: "Solo se permiten números."
	    };
    r.onlyNumbersAndAPoint =
	    { exp : /^[0-9]+(\.[0-9]+)?$/};
    r.RFC =
	    {exp : /^((([A-Z]|[a-z]| )?)(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3})))?$/,
	    msg : "El formato permitido es: AAAA111111--- ó AAA111111--- donde los guiones pueden ser letras o números."};
    r.CURP =
	    {exp : /^(([A-Z]|[a-z]){4}[0-9]{6}([A-Z]|[a-z]){6}[0-9]{2})?$/,
	    msg : "El formato permitido es: AAAA111111AAAAAA11"};
			
    r.NumAlphaHypSpace =
	    {exp : /^([A-Z]|[a-z]|[0-9]| |-)*$/};
    r.NumHypSpace =
	    {exp : /^([0-9]| |-)*$/};
    r.phone =
	    {exp : /^(| |\*|-|[0-9]|\(|\))*$/,
	    msg : "Solo se permiten paréntesis, números, guiones, asteriscos y espacios."};

    r.email =
	    {exp : /^((([^<>()\[\]\.,;:\s@"]+(\.[^<>()\[\]\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,})))?$/,
	    msg : "No tiene un formato correcto."};

    r.NSS =
	    {exp : /^([0-9]{11})?$/,
	    msg : "El formato permitido es: 11111111111."};

    r.Latitud =
	    {exp : /^([-+]?([1-8]?\d(\.\d+)?|90(\.0+)?))?$/,
	    msg : "Solo se permiten valores entre -90 y +90 (Ej.: -75.23345456)."};

    r.Longitud =
	    {exp : /^([-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?))?$/,
	    msg : "Solo se permiten valores entre -180 y +180 (Ej.: +168.98876765)."}

    r.NewEntity =
	    {exp : /^[^,.\-;:_{}\[\]´+¨*!"#$%&/()?=¡'¿|°\@¬~`^]+$/,
	    msg : "Los siguientes caracteres no estan permitidos: ,.-;:_{}[]´+¨*!&quot#$%&amp;/()?=¡'¿|°\&#64;¬~`^ "};



    return r;
}
/**
 * detect IE
 * returns version of IE or false, if browser is not Internet Explorer
 */
function detectIE() {
    var ua = window.navigator.userAgent;

    // Test values; Uncomment to check result …

    // IE 10
    // ua = 'Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)';

    // IE 11
    // ua = 'Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko';

    // Edge 12 (Spartan)
    // ua = 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 Safari/537.36 Edge/12.0';

    // Edge 13
    // ua = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Safari/537.36 Edge/13.10586';

    var msie = ua.indexOf('MSIE ');
    if (msie > 0) {
        // IE 10 or older => return version number
        return parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10);
    }

    var trident = ua.indexOf('Trident/');
    if (trident > 0) {
        // IE 11 => return version number
        var rv = ua.indexOf('rv:');
        return parseInt(ua.substring(rv + 3, ua.indexOf('.', rv)), 10);
    }

    var edge = ua.indexOf('Edge/');
    if (edge > 0) {
        // Edge (IE 12+) => return version number
        return parseInt(ua.substring(edge + 5, ua.indexOf('.', edge)), 10);
    }

    // other browser
    return false;
}
