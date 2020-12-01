var col1 = '8.333333333333334%';
var col2 = '16.666666666666668%';
var col3 = '25%';
var col4 = '33.333333333333336%';
var col5 = '41.66666666666667%';
var col6 = '50%';
var col7 = '58.333333333333336%';
var col8 = '66.66666666666667%';
var col9 = '75%';
var col10 = '83.33333333333334%';
var col11 = '91.66666666666667%';
var col12 = '100%';
var generalMargin = [0, 0, 0, 5];

var lineas = "===============================================";
var lineaContinua = "_________________________________________________________";
var WithColumns = "40%";

globalStyles = {
    header: {
        fontSize: 22,
        margin: generalMargin,
        bold: true
    },
    PsemiHeader: {
        fontSize: 14,
        margin: [0, 10, 0, 0],
        bold: true
    },
    PsemiHeaderNoBold: {
        fontSize: 10
    },

    PsemiHeaderNoBoldCenter: {
        fontSize: 10,
        alignment: 'center'
    },
    label: {
        bold: true,
        fontSize: 10,
        margin: generalMargin,
        width: col2
    },
    labelSinBold: {
        //bold: true,
        fontSize: 10,
        margin: generalMargin,
        width: col2
    },
    labelImporte: {
        bold: true,
        fontSize: 10,
        // margin:[0,0,0,1],
        width: col2
    },
    normalText: {
        fontSize: 8,
        margin: generalMargin,
        italic: true,
    },
    normalTextCenter: {
        fontSize: 8,
        margin: generalMargin,
        alignment: 'center'
    },
    normalTextRight: {
        fontSize: 8,
        margin: generalMargin,
        alignment: 'right'
    }
};

function PrintTicketCierre(pClinica, pUsuario, pRetiro) {

    var content = [];
    var WithColumns = "30%";
    var lineasTicket = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -";
    //content.push({ columns: [{ width: WithColumns, text: "INMEDIK", style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: "ID Salud", style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: "R.F.C. " + pClinica.rfc, style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: ( pClinica.addressAux.fullAddress + " C.P. " + pClinica.addressAux.postalCode ).toUpperCase(), style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: lineasTicket, style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, columns: [{ text: "FOLIO", style: 'normalText' }, { text: pRetiro.number, style: 'normalTextRight' }] }] });
    content.push({ columns: [{ width: WithColumns, columns: [{ text: "CAJERO", style: 'normalText' }, { text: pUsuario, style: 'normalTextRight' }] }] });
    content.push({ columns: [{ width: WithColumns, columns: [{ text: "FECHA", style: 'normalText' }, { text: pRetiro.sCreated, style: 'normalTextRight' }] }] });
    content.push({ columns: [{ width: WithColumns, text: lineasTicket, style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, columns: [{ text: "DENOMINACION", style: 'normalText' }, { text: "CANTIDAD", style: 'normalTextRight' }] }] });
    //Cursor para identificar la denominacion y cantidad retirada
    for (var i = 0; i < pRetiro.denominationWithrawalAux.length; i++) {
        if (pRetiro.denominationWithrawalAux[i].Quantity != null) {
            if (pRetiro.denominationWithrawalAux[i].Quantity > 0) {
                content.push({ columns: [{ width: WithColumns, columns: [{ text: pRetiro.denominationWithrawalAux[i].denominationAux.Name, style: 'normalText' }, { text: pRetiro.denominationWithrawalAux[i].Quantity, style: 'normalTextRight' }] }] });
            }
        };
    };
    
    content.push({ columns: [{ width: WithColumns, columns: [{ text: "TOTAL RETIRADO:", style: 'normalText' }, { text: pRetiro.total, style: 'normalTextRight' }] }] });

    var docDefinition = {
        styles: globalStyles,
        content: content
    };

    pdfMake.createPdf(docDefinition).open();
}