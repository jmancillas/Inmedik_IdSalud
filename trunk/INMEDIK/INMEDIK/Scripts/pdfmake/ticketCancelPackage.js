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

function PrintRefundPack(OrderObj) {
    var content = [];
    var WithColumns = "30%";
    var lineasTicket = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -";
    //content.push({ columns: [{ width: WithColumns, text: "INMEDIK", style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: "ID Salud", style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: "R.F.C. " + OrderObj.clinicAux.rfc, style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: (OrderObj.clinicAux.addressAux.fullAddress + " C.P. " + OrderObj.clinicAux.addressAux.postalCode).toUpperCase(), style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: lineasTicket, style: 'normalTextCenter' }] });

    content.push({ columns: [{ width: WithColumns, columns: [{ text: "TICKET CANC.", style: 'normalText' }, { text: OrderObj.Ticket, style: 'normalTextRight' }] }] });

    content.push({ columns: [{ width: WithColumns, columns: [{ text: "CLIENTE", style: 'normalText' }, { text: OrderObj.patientAux.personAux.fullName, style: 'normalTextRight' }] }] });
    content.push({ columns: [{ width: WithColumns, columns: [{ text: "FOLIO", style: 'normalText' }, { text: OrderObj.patientAux.id, style: 'normalTextRight' }] }] });
    content.push({ columns: [{ width: WithColumns, columns: [{ text: "FECHA", style: 'normalText' }, { text: OrderObj.sCreated.toUpperCase(), style: 'normalTextRight' }] }] });

    content.push({ columns: [{ width: WithColumns, text: lineasTicket, style: 'normalTextCenter' }] });

    content.push({ columns: [{ width: WithColumns, text: "PAQUETES CANCELADOS", style: 'normalTextCenter' }] });

    content.push({
        columns: [{
            width: WithColumns, columns: [
                     { width: '70%', text: "CONCEPTO", style: 'normalText' },
					 { width: '30%', text: "TOTAL", style: 'normalTextRight' }
            ]
        }]
    });

    for (var i = 0; i < OrderObj.orderPackageAuxList.length; i++) {
        if (OrderObj.orderPackageAuxList[i].packageSelected) {
            if (OrderObj.orderPackageAuxList[i].packageAux.iva) {
                content.push({
                    columns: [{
                        width: WithColumns, columns: [
                            { width: '70%', text: OrderObj.orderPackageAuxList[i].packageAux.name, style: 'normalText' },
                            { width: '30%', text: Ut_numberWithCommas(OrderObj.orderPackageAuxList[i].packageAux.price * 1.16), style: 'normalTextRight' }
                        ]
                    }]
                });
            }
            else {
                content.push({
                    columns: [{
                        width: WithColumns, columns: [
                            { width: '70%', text: OrderObj.orderPackageAuxList[i].packageAux.name, style: 'normalText' },
                            { width: '30%', text: Ut_numberWithCommas(OrderObj.orderPackageAuxList[i].packageAux.price), style: 'normalTextRight' }
                        ]
                    }]
                });
            }
        }
    }


    content.push({ columns: [{ width: WithColumns, text: lineasTicket, style: 'normalTextCenter' }] });

    content.push({ columns: [{ width: WithColumns, columns: [{ text: "TOTAL DE DEVOLUCIÓN", style: 'normalText' }, { text: Ut_numberWithCommas(OrderObj.paymentAmmount), style: 'normalTextRight' }] }] });



    content.push({ columns: [{ width: WithColumns, text: lineasTicket, style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: "GRACIAS POR SU VISITA.", style: 'normalTextCenter' }] });



    var docDefinition = {
        styles: globalStyles,
        content: content
    };
    var IEver = detectIE();
    if (IEver === false) {
        pdfMake.createPdf(docDefinition).open();
    }
    else {
        pdfMake.createPdf(docDefinition).download();
    }
}