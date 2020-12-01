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

function PrintNewOrder(OrderObj) {


    var content = [];
    var WithColumns = "30%";
    var lineasTicket = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -";
    //content.push({ columns: [{ width: WithColumns, text: "INMEDIK", style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: "ID Salud", style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: "R.F.C. " + OrderObj.clinicAux.rfc, style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: (OrderObj.clinicAux.addressAux.fullAddress + "C.P. " + OrderObj.clinicAux.addressAux.postalCode).toUpperCase(), style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: lineasTicket, style: 'normalTextCenter' }] });

    content.push({ columns: [{ width: WithColumns, columns: [{ text: "TICKET", style: 'normalText' }, { text: OrderObj.Ticket, style: 'normalTextRight' }] }] });
    content.push({ columns: [{ width: WithColumns, columns: [{ text: "CAJERO", style: 'normalText' }, { text: OrderObj.employeeAux.personAux.fullName, style: 'normalTextRight' }] }] });
    content.push({ columns: [{ width: WithColumns, columns: [{ text: "CLIENTE", style: 'normalText' }, { text: OrderObj.patientAux.personAux.fullName, style: 'normalTextRight' }] }] });
    content.push({ columns: [{ width: WithColumns, columns: [{ text: "FOLIO", style: 'normalText' }, { text: OrderObj.patientAux.id, style: 'normalTextRight' }] }] });
    content.push({ columns: [{ width: WithColumns, columns: [{ text: "FECHA", style: 'normalText' }, { text: OrderObj.sCreated.toUpperCase(), style: 'normalTextRight' }] }] });
    content.push({ columns: [{ width: WithColumns, text: lineasTicket, style: 'normalTextCenter' }] });
    content.push({
        columns: [{
            width: WithColumns, columns: [
                     { width: '65%', text: "CONCEPTO", style: 'normalText' },
					 { width: '35%', text: "TOTAL", style: 'normalTextRight' }
            ]
        }]
    });
    for (var i = 0; i < OrderObj.orderPackageAuxList.length; i++) {
        content.push({
            columns: [{
                width: WithColumns, columns: [
                     { width: '65%', text: OrderObj.orderPackageAuxList[i].packageAux.name, style: 'normalText' },
                     { width: '35%', text: Ut_numberWithCommas(OrderObj.orderPackageAuxList[i].packageAux.price), style: 'normalTextRight' }
                ]
            }]
        });
    }
    content.push({ columns: [{ width: WithColumns, text: lineasTicket, style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: "PAGOS APLICADOS", style: 'normalTextCenter' }] });
    content.push({
        columns: [{
            width: WithColumns, columns: [
                     { text: "CONCEPTO", style: 'normalText' },
                     { text: "TOTAL", style: 'normalTextRight' }
            ]
        }]
    });


    for (var i = 0; i < OrderObj.paymentAux.length; i++) {

        var comis = " ";
        if (OrderObj.paymentAux[i].PaymentTypeAux.Name == "Tarjeta") {
            comis = Ut_numberWithCommas(OrderObj.paymentAux[i].Commission);
        }
        else {
            comis = "-";
        }
        content.push({
            columns: [{
                width: WithColumns, columns: [
                     { text: OrderObj.paymentAux[i].PaymentTypeAux.Name, style: 'normalText' },
                     { text: comis, style: 'normalTextCenter' },
                     { text: Ut_numberWithCommas(OrderObj.paymentAux[i].Amount), style: 'normalTextRight' }
                ]
            }]
        });
    }

    if (OrderObj.Total < OrderObj.cancelledPackageAuxList[0].refund)
    {
        content.push({ columns: [{ width: WithColumns, text: lineasTicket, style: 'normalTextCenter' }] });
        content.push({ columns: [{ width: WithColumns, text: "DEVOLUCIÓN.", style: 'normalTextCenter' }] });


        content.push({
            columns: [{
                width: WithColumns, columns: [
                         { text: "CONCEPTO", style: 'normalText' },
                         { text: "TOTAL", style: 'normalTextRight' }
                ]
            }]
        });

        content.push({
            columns: [{
                width: WithColumns, columns: [
                    { text: "Efectivo", style: 'normalText' },
                    { text: Ut_numberWithCommas(OrderObj.cancelledPackageAuxList[0].refund - OrderObj.Total), style: 'normalTextRight' }
                ]
            }]
        })
    }

    content.push({ columns: [{ width: WithColumns, text: lineasTicket, style: 'normalTextCenter' }] });
    content.push({ columns: [{ width: WithColumns, text: "GRACIAS POR SU COMPRA.", style: 'normalTextCenter' }] });



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