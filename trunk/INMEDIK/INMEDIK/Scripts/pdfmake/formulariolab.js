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

globalStyles = {
    header: {
        fontSize: 22,
        margin: generalMargin,
        bold: true,
        alignment: 'center'
    },
    PsemiHeader: {
        fontSize: 12,
        margin: [0, 10, 0, 0],
        bold: true
    },
    PsemiHeaderNoBold: {
        fontSize: 10
    },

    Title: {
        fontSize: 16,
        alignment: 'left',
        margin: [106, 0, 0, 0]
    },
    label: {
        bold: true,
        fontSize: 10,
        margin: generalMargin,
        width: col2,
        border: '1px'
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
        width: col2,

    },
    normalText: {
        fontSize: 10,
        margin: generalMargin,
        italic: true,
    },
    normalTextCenter: {
        fontSize: 10,
        margin: generalMargin,
        alignment: 'center'
    },
    normalTextRight: {
        fontSize: 10,
        margin: generalMargin,
        alignment: 'right'
    },
    normalTextLeft: {
        fontSize: 10,
        margin: generalMargin,
        alignment: 'left'
    }
};


function PrintFormjs(formObj) {
    var content = [];
    var form = formObj.elementList;
    //content.push({ columns: [{ text: "INMEDIK", style: 'Title' }] });
    content.push({ columns: [{ text: "ID Salud", style: 'Title' }] });
    content.push({ columns: [{ text: " ", margin: [0, 20, 0, 0], style: 'normalText' }] });
    content.push({ columns: [{ text: formObj.name, style: 'normalText' }] });
    content.push({ columns: [{ text: formObj.description ==  null ? '' : formObj.description, style: 'normalText' }] });
    content.push({ columns: [{ text: " ", style: 'normalText' }] });
    //content.push({ columns: [{ width: '20%', text: "Campo", style: 'normalText' }, { width: '65%', text: "Valor", style: 'normalTextCenter' }, { width: '15%', text: "Referencia", style: 'normalTextLeft' }] });
    for (var i = 0; i < formObj.elementList.length; i++) {
        var field = formObj.elementList[i].field;
        var module = formObj.elementList[i].module;
        if (field != null) {
            if (i == 0)
            {
                content.push({ columns: [{ width: '20%', text: "Campo", style: 'normalText' }, { width: '65%', text: "Valor", style: 'normalTextCenter' }, { width: '15%', text: "Referencia", style: 'normalTextLeft' }] });
            }
            if (field.fieldTypeAux.code == 'Option') {
                content.push({
                    columns: [
                        { width: '20%', text: field.tag, style: 'label' },
                        { width: '65%', text: field.value == undefined ? '' : field.value.value + ' ' + (field.unit == null ? '' : field.unit), style: 'normalTextCenter' }
                    ]
                });
            }
            else {
                var columns = [
                        { width: '20%', text: field.tag, style: 'label' },
                        { width: '65%', text: field.value == undefined ? '' : field.value + ' ' + (field.unit == null ? '' : field.unit), style: 'normalTextCenter' }
                ];
                if (field.reference != null) {
                    columns.push({ width: '15%', text: field.reference, style: 'normalText' });

                }
                else if (field.lowerLimit != null || field.upperLimit != null) {
                    columns.push({ width: '15%', text: (field.lowerLimit == null ? '' : field.lowerLimit) + ' - ' + (field.upperLimit == null ? '' : field.upperLimit), style: 'normalText' });
                }
                content.push({ columns: columns });
            }
        }

        else if (module != null) {
           
                content.push({ columns: [{ text: module.name, style: 'PsemiHeader' }] });
                content.push({ columns: [{ text: module.description == null ? '' : module.description, style: 'normalText' }] });
                content.push({ columns: [{ text: " ", style: 'normalText' }] });
                content.push({ columns: [{ width: '20%', text: "Campo", style: 'normalText' }, { width: '65%', text: "Valor", style: 'normalTextCenter' }, { width: '15%', text: "Referencia", style: 'normalTextLeft' }] });
            
            for (var e = 0; e < module.fieldList.length; e++) {
                if (module.fieldList[e].fieldTypeAux.code == 'Option') {
                    content.push({
                        columns: [
                        {
                            width: '20%',
                            text: module.fieldList[e].tag,
                            style: 'label'
                        },
                        {
                            width: '65%',
                            text: module.fieldList[e].value == undefined ? '' : module.fieldList[e].value.value + ' ' + (module.fieldList[e].unit == null ? '' : module.fieldList[e].unit),
                            style: 'normalTextCenter'
                        }
                        ]
                    })
                }
                else {
                    var columns = [
                        { width: '20%', text: module.fieldList[e].tag, style: 'label' },
                        { width: '65%', text: module.fieldList[e].value == undefined ? '' : module.fieldList[e].value + ' ' + (module.fieldList[e].unit == null ? '' : module.fieldList[e].unit), style: 'normalTextCenter' }
                    ];
                    if (module.fieldList[e].reference != null) {
                        columns.push({ width: '15%', text: module.fieldList[e].reference, style: 'normalText' });
                    }
                    else if (module.fieldList[e].lowerLimit != null || module.fieldList[e].upperLimit != null) {
                        columns.push({ width: '15%', text: (module.fieldList[e].lowerLimit == null ? '' : module.fieldList[e].lowerLimit) + ' - ' + (module.fieldList[e].upperLimit == null ? '' : module.fieldList[e].upperLimit), style: 'normalText' })
                    }
                    content.push({ columns: columns });
                }
            }
        }
    }
    var docDefinition = {
        styles: globalStyles,
        content: content,
        pageMargins: [160, 40, 60, 40]
    };
    pdfMake.createPdf(docDefinition).open();

}
