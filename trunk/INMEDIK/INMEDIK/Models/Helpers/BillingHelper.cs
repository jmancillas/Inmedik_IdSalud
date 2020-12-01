using System;
using INMEDIK.Models.Entity;
using INMEDIK.Common;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace INMEDIK.Models.Helpers
{
    public class FacturaConcepto
    {
        public ComprobanteConcepto Concepto { get; set; }
        public decimal SubTotal { get; set; }
        public decimal IvaCalculado { get; set; }
        public double PorcentajeIva { get; set; }
        public FacturaConcepto()
        {
            this.Concepto = new ComprobanteConcepto();
        }
    }

    public class FacturaConceptoResult : Result
    {
        public FacturaConcepto data { get; set; }
        public List<FacturaConcepto> data_list { get; set; }
        public NumericResult total { get; set; }

        public FacturaConceptoResult()
        {
            data = new FacturaConcepto();
            data_list = new List<FacturaConcepto>();
            total = new NumericResult();
        }
    }

    public class ConsultaCFDI
    {
        public string CodigoEstatus { get; set; }
        public string EsCancelable { get; set; }
        public string Estado { get; set; }
        public string EstatusCancelacion { get; set; }

        public void Llenar(string stringResponse)
        {
            CodigoEstatus = BillingHelper.Between(stringResponse, "<CodigoEstatus>", "</CodigoEstatus>");
            EsCancelable = BillingHelper.Between(stringResponse, "<EsCancelable>", "</EsCancelable>");
            Estado = BillingHelper.Between(stringResponse, "<Estado>", "</Estado>");
            EstatusCancelacion = BillingHelper.Between(stringResponse, "<EstatusCancelacion>", "</EstatusCancelacion>");
        }
    }

    public class LogFacturacion
    {
        public int IdLogFacturacion { get; set; }
        public string Mensaje { get; set; }
        public string Fecha { get; set; }
    }
    public class BillingHelper
    {
        public static FacturaConceptoResult LlenarConceptos(List<pagoFamiliaNuevo> pagos)
        {
            FacturaConceptoResult result = new FacturaConceptoResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    foreach (var pago in pagos)
                    {
                        FacturaConcepto concepto = new FacturaConcepto();
                        //ClaveProductoServicio es el catálogo del sat string
                        //NoIdentificacion es el id interno (opcional)
                        concepto.SubTotal = pago.Monto / (1 + ((decimal)pago.cuota.PorcentajeIVADeMonto / 100));
                        concepto.IvaCalculado = pago.Monto - concepto.SubTotal;
                        concepto.PorcentajeIva = pago.cuota.PorcentajeIVADeMonto;
                        concepto.Concepto.Cantidad = 1; //Cantidad
                        concepto.Concepto.ClaveUnidad = "E48"; //Clave unidad es catalogo del sat
                        concepto.Concepto.Descripcion = pago.cuota.conceptoDeCuota.Concepto; //Descripcion nombre de concepto
                        concepto.Concepto.ValorUnitario = pago.Monto; //Valor unitario = Importe /Cantidad
                        concepto.Concepto.Importe = pago.Monto; //Importe es el total decimal
                        concepto.Concepto.ClaveProdServ = pago.cuota.IdProductoServicioSAT; //Clave producto/servicio SAT
                        result.Instancia.Add(concepto);
                    }
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }

            }
            return result;
        }
        public static Resultado<GenericModel> FacturarTicket(int idTicket, int usuarioQueEnvia)
        {
            Resultado<GenericModel> result = new Resultado<GenericModel>();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    ticket facturar = db.ticket.Where(t => t.IdTicket == idTicket).FirstOrDefault();
                    //facturar.pagoFamiliaNuevo.First().Monto = 34000;
                    var conceptosRes = BillingHelper.LlenarConceptos(facturar.pagoFamiliaNuevo.Where(p => p.cuota.EsFacturable).ToList());
                    if (conceptosRes.Exito)
                    {
                        string pathCer = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["Facturacion.PathCer"].ToString());
                        string pathKey = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["Facturacion.PathKey"].ToString());
                        string pathxsl = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["Facturacion.PathXls"].ToString());
                        string clavePrivada = WebConfigurationManager.AppSettings["Facturacion.ClavePrivada"].ToString();
                        string numeroCertificado, inicio, final, serie;

                        DigitalSeal.leerCER(pathCer, out inicio, out final, out serie, out numeroCertificado);


                        Comprobante objCompXSD = new Comprobante();
                        objCompXSD.Version = "3.3";
                        objCompXSD.Folio = facturar.Folio;
                        objCompXSD.Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                        objCompXSD.FormaPago = facturar.pagoFamiliaNuevo.Where(pf => pf.cuota.EsFacturable).Select(pf => pf.tipoPagoFamilia.ClaveSAT).FirstOrDefault();
                        objCompXSD.Moneda = "MXN";
                        objCompXSD.TipoDeComprobante = "I";
                        objCompXSD.MetodoPago = "PUE"; //Pago en una sola exhibición
                        objCompXSD.LugarExpedicion = WebConfigurationManager.AppSettings["Facturacion.LugarExpedicion"].ToString(); //Código Postal
                        //objCompXSD.Sello = "Noexistente";
                        objCompXSD.NoCertificado = numeroCertificado;
                        //objCompXSD.Certificado = "";
                        objCompXSD.SubTotal = conceptosRes.Instancia.Sum(i => i.SubTotal);
                        objCompXSD.Total = conceptosRes.Instancia.Sum(i => i.Concepto.Importe);

                        ComprobanteEmisor emisor = new ComprobanteEmisor();
                        emisor.RegimenFiscal = WebConfigurationManager.AppSettings["Facturacion.Emisor.RegimenFiscal"].ToString();
                        emisor.Nombre = WebConfigurationManager.AppSettings["Facturacion.Emisor.Nombre"].ToString();
                        emisor.Rfc = WebConfigurationManager.AppSettings["Facturacion.Emisor.Rfc"].ToString();

                        ComprobanteReceptor receptor = new ComprobanteReceptor();
                        if (facturar.familia.factTipoPersona == "MORAL")
                        {
                            receptor.Nombre = facturar.familia.factRazonSocial;
                        }
                        else
                        {
                            receptor.Nombre = facturar.familia.factNombre + " " + facturar.familia.factApellidoPaterno + " " + facturar.familia.factApellidoMaterno;
                        }
                        receptor.Rfc = facturar.familia.factRFC;
                        receptor.UsoCFDI = "D10";

                        objCompXSD.Emisor = emisor;
                        objCompXSD.Receptor = receptor;

                        objCompXSD.Conceptos = conceptosRes.Instancia.Select(i => i.Concepto).ToArray();

                        string xmlString = GenerarXML(objCompXSD);
                        XmlReader xmlr = XmlReader.Create(new StringReader(xmlString));

                        #region cadena original
                        string cadenaOriginal = "";
                        XslCompiledTransform transformador = new XslCompiledTransform(true);
                        transformador.Load(pathxsl);

                        using (StringWriter sw = new StringWriter())
                        {
                            using (XmlWriter xmo = XmlWriter.Create(sw, transformador.OutputSettings))
                            {
                                transformador.Transform(xmlr, xmo);
                                cadenaOriginal = sw.ToString();
                            }
                        }
                        #endregion

                        DigitalSeal oSelloDigital = new DigitalSeal();
                        objCompXSD.Certificado = oSelloDigital.Certificado(pathCer);
                        objCompXSD.Sello = oSelloDigital.Sellar(cadenaOriginal, pathKey, clavePrivada);

                        //Generar el xml
                        xmlString = GenerarXML(objCompXSD);
                        //Guardar el xml enviado en BD
                        Resultado<GenericModel> resGuardarFact = GuardarXMLEnviado(xmlString, cadenaOriginal, idTicket, usuarioQueEnvia);
                        int idFactura = resGuardarFact.Instancia.integer_value;
                        //Mandar a timbrar
                        result = TimbrarXML(xmlString, idFactura, idTicket);
                    }
                    else
                    {
                        result.Exito = false;
                        result.Mensaje = conceptosRes.Mensaje;
                    }
                }
                catch (Exception e)
                {
                    GuardarLogError(idTicket, result.MensajeError);
                    result.Exito = false;
                    result.Excepcion = e;
                    result.Mensaje = "Ocurrió un error inesperado. " + result.MensajeError;
                }
            }
            return result;
        }

        public static string GenerarXML(Object objCompXSD)
        {
            StreamWriter stWriter = null;
            XmlSerializer xmlSerializer;
            string buffer;

            try
            {
                xmlSerializer = new XmlSerializer(objCompXSD.GetType());
                MemoryStream memStream = new MemoryStream();
                stWriter = new StreamWriter(memStream);

                XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();
                xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
                xmlNameSpace.Add("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");
                xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                xmlSerializer.Serialize(stWriter, objCompXSD, xmlNameSpace);

                //buffer = Encoding.UTF8.GetString(memStream.GetBuffer());
                buffer = Encoding.UTF8.GetString(memStream.ToArray());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (stWriter != null) stWriter.Close();
            }
            return buffer;
        }

        private static Resultado<GenericModel> IniciarSesion()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Resultado<GenericModel> resultado = new Resultado<GenericModel>();
            string token = String.Empty;
            string errorCode = String.Empty;
            string errorMsg = String.Empty;
            string usuario = WebConfigurationManager.AppSettings["Facturacion.Usuario"].ToString();
            string password = WebConfigurationManager.AppSettings["Facturacion.Password"].ToString();

            string oRequest = "";
            oRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:wspac.wsdl\">";
            oRequest = oRequest + "<soapenv:Header/><soapenv:Body><urn:openSession>";
            oRequest = oRequest + "<username>" + usuario + "</username><password>" + password + "</password></urn:openSession></soapenv:Body></soapenv:Envelope>";

            string s = InvoqueWebService(oRequest, "openSesion", "PRODUCCION");
            if (Between(s, "<ok>", "</ok>").ToUpper() == "TRUE")
            {
                token = Between(s, "<token>", "</token>");
                errorCode = Between(s, "<errorCode>", "</errorCode>");
                resultado.Exito = true;
            }
            else
            {
                resultado.Exito = false;
                errorCode = Between(s, "<errorCode>", "</errorCode>");
                errorMsg = ErrorCodeToString(Convert.ToInt32(Between(s, "<errorCode>", "</errorCode>")));
            }

            resultado.Instancia.string_value = token;
            return resultado;
        }

        private static Resultado<GenericModel> TimbrarXML(string xmlCFD, int idFactura, int idTicket)
        {
            Resultado<GenericModel> resultado = new Resultado<GenericModel>();

            try
            {
                Resultado<GenericModel> resultadoSesion = IniciarSesion();

                if (resultadoSesion.Exito)
                {
                    xmlCFD = xmlCFD.Replace("<", "&lt;");
                    xmlCFD = xmlCFD.Replace(">", "&gt;");

                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    string oRequest = "";
                    oRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:wspac.wsdl\">";
                    oRequest = oRequest + "<soapenv:Header/><soapenv:Body><urn:createCFDI><cfd><token>" + resultadoSesion.Instancia.string_value + "</token><xml>" + xmlCFD + "</xml></cfd></urn:createCFDI>";
                    oRequest = oRequest + "</soapenv:Body></soapenv:Envelope>";
                    string s = InvoqueWebService(oRequest, "createCFDI", "PRODUCCION");

                    if (Between(s, "<ok>", "</ok>").ToUpper() == "TRUE")
                    {
                        /*string errorCode = Between(s, "<errorCode>", "</errorCode>");
                        string UUID = Between(s, "<uuid>", "</uuid>");
                        string selloSAT = Between(s, "<satStamp>", "</satStamp>");
                        string selloCFD = Between(s, "<cfdStamp>", "</cfdStamp>");*/
                        string xmlTimbrado = Between(s, "<xml>", "</xml>");

                        xmlTimbrado = xmlTimbrado.Replace("&lt;", "<");
                        xmlTimbrado = xmlTimbrado.Replace("&gt;", ">");

                        GuardarXMLTimbrado(idFactura, xmlTimbrado);

                        GuardarLogError(idTicket, "Ticket facturado.");

                        resultado.Exito = true;
                        resultado.Mensaje = "Ticket facturado.";
                    }
                    else
                    {
                        string mensaje = ErrorCodeToString(Convert.ToInt32(Between(s, "<errorCode>", "</errorCode>")));
                        resultado.Exito = false;
                        resultado.Mensaje = mensaje;
                        GuardarLogError(idTicket, mensaje);
                    }
                }
                else
                {
                    resultado.Mensaje = resultadoSesion.Mensaje;
                    resultado.Exito = resultadoSesion.Exito;
                }
            }
            catch (Exception e)
            {
                resultado.Exito = false;
                resultado.Excepcion = e;
                GuardarLogError(idTicket, e.Message);
            }
            return resultado;
        }

        private static Resultado<GenericModel> GuardarXMLEnviado(string xmlEnviado, string cadenaOriginal, int idTicket, int usuarioQueEnvia)
        {
            Resultado<GenericModel> resultado = new Resultado<GenericModel>();
            try
            {
                using (PortalEscolarEntities dbContext = new PortalEscolarEntities())
                {
                    factura fact = dbContext.factura.Where(f => f.IdTicket == idTicket).FirstOrDefault();
                    if (fact == null)
                    {
                        factura nuevaFact = new factura();
                        nuevaFact.IdEstatusFactura = 1;
                        nuevaFact.IdTicket = idTicket;
                        nuevaFact.XMLEnviado = xmlEnviado;
                        nuevaFact.CadenaOriginal = cadenaOriginal;
                        nuevaFact.FechaEnviado = DateTime.Now;
                        nuevaFact.EnviadoPor = usuarioQueEnvia;

                        dbContext.factura.Add(nuevaFact);
                        dbContext.SaveChanges();

                        resultado.Instancia.integer_value = nuevaFact.IdFactura;
                    }
                    else
                    {
                        fact.XMLEnviado = xmlEnviado;
                        fact.CadenaOriginal = cadenaOriginal;
                        fact.FechaEnviado = DateTime.Now;
                        fact.EnviadoPor = usuarioQueEnvia;
                        resultado.Instancia.integer_value = fact.IdFactura;
                        dbContext.SaveChanges();
                    }

                    resultado.Exito = true;
                }
            }
            catch (Exception e)
            {
                resultado.Exito = false;
                resultado.Excepcion = e;
            }
            return resultado;
        }

        private static Resultado<GenericModel> GuardarXMLTimbrado(int idFactura, string xmlTimbrado)
        {
            Resultado<GenericModel> resultado = new Resultado<GenericModel>();
            try
            {
                using (PortalEscolarEntities dbContext = new PortalEscolarEntities())
                {
                    factura fact = dbContext.factura.Where(f => f.IdFactura == idFactura).First();
                    fact.XMLTimbrado = xmlTimbrado;
                    fact.IdEstatusFactura = 2;

                    dbContext.SaveChanges();
                    resultado.Exito = true;
                }
            }
            catch (Exception e)
            {
                resultado.Exito = false;
                resultado.Excepcion = e;
            }
            return resultado;
        }

        public static Resultado<GenericModel> GuardarLogError(int idTicket, string mensaje)
        {
            Resultado<GenericModel> resultado = new Resultado<GenericModel>();
            try
            {
                using (PortalEscolarEntities dbContext = new PortalEscolarEntities())
                {
                    logFacturacion log = new logFacturacion();
                    log.IdTicket = idTicket;
                    log.Mensaje = mensaje;
                    log.Fecha = DateTime.Now;

                    dbContext.logFacturacion.Add(log);
                    dbContext.SaveChanges();

                    resultado.Exito = true;
                }
            }
            catch (Exception e)
            {
                resultado.Exito = false;
                resultado.Excepcion = e;
            }
            return resultado;
        }

        public static Resultado<List<LogFacturacion>> ObtenerLogFacturacion(int idTicket)
        {
            Resultado<List<LogFacturacion>> result = new Resultado<List<LogFacturacion>>();
            using (PortalEscolarEntities db = new PortalEscolarEntities())
            {
                try
                {
                    var logs = db.logFacturacion.Where(l => l.IdTicket == idTicket).OrderByDescending(l => l.Fecha).ToList();
                    foreach (var logDb in logs)
                    {
                        LogFacturacion logf = new LogFacturacion();
                        logf.IdLogFacturacion = logDb.IdLogFacturacion;
                        logf.Mensaje = logDb.Mensaje;
                        logf.Fecha = logDb.Fecha.ToString("dd/MM/yyyy hh:mm:ss");

                        result.Instancia.Add(logf);
                    }
                    result.Exito = true;
                }
                catch (Exception e)
                {
                    result.Exito = false;
                    result.Excepcion = e;
                    result.Mensaje = "Ocurrió un error inesperado. " + result.MensajeError;
                }
            }
            return result;
        }

        /// <summary>
        /// Metodo consulta CFDI de pruebas, en produccion usar "SATConsultaCFDI(int idFactura)"
        /// </summary>
        /// <param name="idFactura"></param>
        /// <returns></returns>
        public static Resultado<ConsultaCFDI> SATConsultaCFDI_Pruebas(int idFactura)
        {
            Resultado<ConsultaCFDI> resultado = new Resultado<ConsultaCFDI>();
            try
            {
                #region  Obtener datos de la factura timbrada para consultarla
                string xmlTimbradoString = String.Empty;
                using (dbINMEDIK db = new dbINMEDIK())
                {
                    xmlTimbradoString = db.factura.First(f => f.IdFactura == idFactura).XMLTimbrado;
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlTimbradoString);

                var nodoComprobante = xmlDoc.GetElementsByTagName("cfdi:Comprobante")[0];
                var nodoEmisor = xmlDoc.GetElementsByTagName("cfdi:Emisor")[0];
                var nodoReceptor = xmlDoc.GetElementsByTagName("cfdi:Receptor")[0];
                var nodoTimbre = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital")[0];
                #endregion

                string expresionImpresa = "?id=" + nodoTimbre.Attributes["UUID"].Value +
                   "&amp;re=" + nodoEmisor.Attributes["Rfc"].Value +
                   "&amp;rr=" + nodoReceptor.Attributes["Rfc"].Value +
                   "&amp;tt=" + nodoComprobante.Attributes["Total"].Value;
                ;

                //Llamar WS de Consulta CFDI de prueba
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string oRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:wspac.wsdl\">" +
                                      "<soapenv:Header/>" +
                                       "<soapenv:Body>" +
                                           "<urn:Consulta>" +
                                                "<expresionImpresa>" + expresionImpresa + "</expresionImpresa>" +
                                           "</urn:Consulta>" +
                                       "</soapenv:Body>" + 0
                                   "</soapenv:Envelope>";
                string s = InvoqueWebService(oRequest, "Consulta", "PRUEBAS");

                resultado.Instancia.Llenar(s);
                resultado.Exito = true;
            }
            catch (Exception e)
            {
                resultado.Exito = false;
                resultado.Excepcion = e;
                resultado.Mensaje = "Ocurrió un error inesperado. " + resultado.MensajeError;
            }

            return resultado;
        }

        public static Resultado<GenericModel> EnviarParaCancelacionNS(int idFactura)
        {
            Resultado<GenericModel> resultado = new Resultado<GenericModel>();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    string pathCer = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["Facturacion.PathCer"].ToString());
                    string pathKey = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["Facturacion.PathKey"].ToString());

                    string usuario = WebConfigurationManager.AppSettings["Facturacion.Usuario"].ToString();
                    string password = WebConfigurationManager.AppSettings["Facturacion.Password"].ToString();
                    string certHex = PathContentToHexString(pathCer);
                    string keyHex = PathContentToHexString(pathKey);
                    string clavePrivada = WebConfigurationManager.AppSettings["Facturacion.ClavePrivada"].ToString();

                    //Obtener UUID y demas datos de la factura
                    var facturaDB = db.factura.First(f => f.IdFactura == idFactura);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(facturaDB.XMLTimbrado);
                    string uuid = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital")[0].Attributes["UUID"].Value;

                    //Llamar WS de Cancelacion
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string oRequest = "";
                    oRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:wspac.wsdl\">" +
                    "<soapenv:Header/><soapenv:Body>" +
                        "<urn:cancelCFDIckpRequest_NS>" +
                            "<cancelReq>" +
                                "<username>" + usuario + "</username>" +
                                "<password>" + password + "</password>" +
                                "<uuid>" + uuid + "</uuid>" +
                                "<cert_hex>" + certHex + "</cert_hex>" +
                                "<key_hex>" + keyHex + "</key_hex>" +
                                "<key_password>" + clavePrivada + "</key_password>" +
                            "</cancelReq>" +
                        "</urn:cancelCFDIckpRequest_NS>" +
                    "</soapenv:Body></soapenv:Envelope>";

                    string s = InvoqueWebService(oRequest, "cancelCFDIckpRequest_NS", "PRODUCCION");

                    if (Between(s, "<ok>", "</ok>").ToUpper() == "TRUE")
                    {
                        facturaDB.IdEstatusFactura = 3;
                        db.SaveChanges();

                        GuardarLogError(facturaDB.IdTicket, "Factura enviada para cancelación.");

                        resultado.Exito = true;
                        resultado.Mensaje = "Factura enviada para cancelación. Para verificar el estado de la factura de clic dentro de los próximos 5 minutos en el botón \"Ver estado cancelación\".";
                    }
                    else
                    {
                        string mensaje = ErrorCodeToString(Convert.ToInt32(Between(s, "<errorCode>", "</errorCode>")));
                        GuardarLogError(facturaDB.IdTicket, mensaje);

                        resultado.Exito = false;
                        resultado.Mensaje = mensaje;
                    }
                }
                catch (Exception e)
                {
                    resultado.Exito = false;
                    resultado.Excepcion = e;
                    resultado.Mensaje = "Ocurrió un error inesperado. " + resultado.MensajeError;
                }
            }

            return resultado;
        }

        public static Resultado<GenericModel> EstadoDeCancelacion(int idFactura, string token = null)
        {
            Resultado<GenericModel> resultado = new Resultado<GenericModel>();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (String.IsNullOrEmpty(token))
                    {
                        //Iniciar sesión
                        Resultado<GenericModel> resultIniciarSesion = IniciarSesion();
                        token = resultIniciarSesion.Instancia.string_value;
                    }

                    //Obtener UUID y demas datos de la factura
                    var facturaDB = db.factura.First(f => f.IdFactura == idFactura);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(facturaDB.XMLTimbrado);
                    string uuid = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital")[0].Attributes["UUID"].Value;

                    //Llamar WS para verificar estado de cancelacion
                    string oRequest = "";
                    oRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:wspac.wsdl\">";
                    oRequest = oRequest + "<soapenv:Header/><soapenv:Body><urn:cancelCFDIStatus><cancelCfdiStt><token>" + token + "</token><uuid>" + uuid + "</uuid></cancelCfdiStt></urn:cancelCFDIStatus>";
                    oRequest = oRequest + "</soapenv:Body></soapenv:Envelope>";
                    string s = InvoqueWebService(oRequest, "cancelCFDIStatus", "PRODUCCION");

                    if (Between(s, "<ok>", "</ok>").ToUpper() == "TRUE")
                    {
                        facturaDB.IdEstatusFactura = 4;
                        facturaDB.XMLAcuseCancelacion = Between(s, "<ack>", "</ack>")
                                                        .Replace("<", "&lt;")
                                                        .Replace(">", "&gt;");
                        db.SaveChanges();

                        GuardarLogError(facturaDB.IdTicket, "Factura cancelada.");

                        resultado.Exito = true;
                        resultado.Mensaje = "Factura cancelada.";
                        resultado.Instancia.double_value = Decimal.ToDouble(db.vwTicketFamilia.First(t => t.IdFactura == idFactura).Monto);
                    }
                    else
                    {
                        string mensaje = ErrorCodeToString(Convert.ToInt32(Between(s, "<errorCode>", "</errorCode>")));
                        GuardarLogError(facturaDB.IdTicket, mensaje);

                        resultado.Exito = false;
                        resultado.Mensaje = mensaje;
                    }
                }
                catch (Exception e)
                {
                    resultado.Exito = false;
                    resultado.Excepcion = e;
                    resultado.Mensaje = "Ocurrió un error inesperado. " + resultado.MensajeError;
                }
            }

            return resultado;
        }

        #region Factura  Impresa
        public static Resultado<GenericModel> GenerarFacturaImpresa(int idFactura)
        {
            Resultado<GenericModel> result = new Resultado<GenericModel>();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var facturaDB = db.factura.Where(f => f.IdFactura == idFactura).FirstOrDefault();

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(facturaDB.XMLTimbrado);

                    var nodoComprobante = xmlDoc.GetElementsByTagName("cfdi:Comprobante")[0];
                    var nodoEmisor = xmlDoc.GetElementsByTagName("cfdi:Emisor")[0];
                    var nodoReceptor = xmlDoc.GetElementsByTagName("cfdi:Receptor")[0];
                    var nodoTimbre = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital")[0];

                    List<KeyValuePair<string, string>> ListaClaveValor = new List<KeyValuePair<string, string>>();
                    ListaClaveValor.Add(new KeyValuePair<string, string>("nombre_emisor", nodoEmisor.Attributes["Nombre"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("rfc_emisor", nodoEmisor.Attributes["Rfc"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("regimen_fiscal", nodoEmisor.Attributes["RegimenFiscal"].Value));

                    ListaClaveValor.Add(new KeyValuePair<string, string>("folio_comprobante", nodoComprobante.Attributes["Folio"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("fecha_comprobante", nodoComprobante.Attributes["Fecha"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("lugar_expedicion", nodoComprobante.Attributes["LugarExpedicion"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("tipo_comprobante", nodoComprobante.Attributes["TipoDeComprobante"].Value));

                    ListaClaveValor.Add(new KeyValuePair<string, string>("rfc_receptor", nodoReceptor.Attributes["Rfc"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("nombre_receptor", nodoReceptor.Attributes["Nombre"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("uso_cfdi", nodoReceptor.Attributes["UsoCFDI"].Value));

                    ListaClaveValor.Add(new KeyValuePair<string, string>("forma_pago", nodoComprobante.Attributes["FormaPago"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("metodo_pago", nodoComprobante.Attributes["MetodoPago"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("moneda_comprobante", nodoComprobante.Attributes["Moneda"].Value));

                    ListaClaveValor.Add(new KeyValuePair<string, string>("desglose_conceptos", ConstruirTablaConceptos(xmlDoc)));

                    ListaClaveValor.Add(new KeyValuePair<string, string>("subtotal_comprobante", nodoComprobante.Attributes["SubTotal"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("total_comprobante", nodoComprobante.Attributes["Total"].Value));

                    double totalComprobante = double.Parse(nodoComprobante.Attributes["Total"].Value);
                    string totalEnLetra = new NumeroCardinal()
                    {
                        MascaraSalidaDecimal = "00/100 " + nodoComprobante.Attributes["Moneda"].Value,
                        SeparadorDecimalSalida = String.Empty,
                        ApocoparUnoParteEntera = true,
                        LetraCapital = true
                    }.ToCustomCardinal(totalComprobante);
                    ListaClaveValor.Add(new KeyValuePair<string, string>("importe_con_letra", totalEnLetra));

                    ListaClaveValor.Add(new KeyValuePair<string, string>("cadena_original", facturaDB.CadenaOriginal));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("sello_digital", nodoComprobante.Attributes["Sello"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("sello_sat", nodoTimbre.Attributes["SelloSAT"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("fecha_timbre", nodoTimbre.Attributes["FechaTimbrado"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("certificado_sat", nodoTimbre.Attributes["NoCertificadoSAT"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("version_timbre", nodoTimbre.Attributes["Version"].Value));
                    ListaClaveValor.Add(new KeyValuePair<string, string>("folio_fiscal_UUID", nodoTimbre.Attributes["UUID"].Value));

                    result.Instancia.string_value = PlantillaService.GenerarPlantilla("FACTURA", ListaClaveValor);

                    //Si esta cancelada agregar leyenda "Factura Cancelada"
                    if (facturaDB.IdEstatusFactura == 4)
                    {
                        result.Instancia.string_value = "<div style='bottom:50%; color:red; font-size:120px; left:10%; position:fixed; right:10%; text-align:center; top:25%; z-index:99'>Factura Cancelada</div>"
                                                        + result.Instancia.string_value;
                    }

                    result.Exito = true;
                }
                catch (Exception ex)
                {
                    result.Exito = false;
                    result.Excepcion = ex;
                    result.Mensaje = "Error inesperado" + result.MensajeError;
                }
            }
            return result;
        }

        public static string ConstruirTablaConceptos(XmlDocument xmlDoc)
        {
            var nodosConcepto = xmlDoc.GetElementsByTagName("cfdi:Concepto");

            string renglones = "";
            foreach (XmlNode item in nodosConcepto)
            {
                renglones += "" +
                    "<tr>" +
                        "<td>" + item.Attributes["ClaveProdServ"].Value + "</td>" +
                        "<td>" + item.Attributes["Cantidad"].Value + "</td>" +
                        "<td>" + item.Attributes["ClaveUnidad"].Value + "</td>" +
                        "<td>" + item.Attributes["Descripcion"].Value + "</td>" +
                        "<td>" + item.Attributes["ValorUnitario"].Value + "</td>" +
                        "<td>" + item.Attributes["Importe"].Value + "</td>" +
                    "</tr>";
            }

            string htmlString = "" +
            "<table border='1' cellpadding='1' cellspacing='0' style='border-collapse:collapse; border:1px solid black; font-size:12px; width:100%'>" +
                "<thead>" +
                    "<th>Clave Producto/Servicio</th>" +
                    "<th>Cantidad</th>" +
                    "<th>Clave Unidad</th>" +
                    "<th>Descripción</th>" +
                    "<th>Valor Unitario</th>" +
                    "<th>Importe</th>" +
                "</thead>" +
                "<tbody>" +
                    renglones +
                "</tbody>" +
            "</table>";

            return htmlString;
        }

        #endregion

        #region Metodos llamado WS
        public static string Between(string src, string findfrom, string findto)
        {
            int start = src.IndexOf(findfrom);
            int to = src.IndexOf(findto, start + findfrom.Length);
            if (start < 0 || to < 0) return "";
            string s = src.Substring(
                           start + findfrom.Length,
                           to - start - findfrom.Length);
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSoap"></param>
        /// <param name="strSOAPAction"></param>
        /// <param name="environment">Ambiente del webservice.. PRODUCCION, PRUEBAS</param>
        /// <returns></returns>
        private static string InvoqueWebService(string strSoap, string strSOAPAction, string environment)
        {
            string environmentURL = String.Empty;
            switch (environment)
            {
                case "PRODUCCION":
                    environmentURL = "https://development.4gfactor.com:8008/wspacService";
                    break;
                case "PRUEBAS":
                    environmentURL = "https://development.4gfactor.com:8098/wspacService";
                    break;
            }

            string xmlResponse = "";
            //Builds the connection to the WebService.
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(environmentURL);
            req.Headers.Add(strSOAPAction, environmentURL);
            req.ContentType = "text/xml; charset=\"utf-8\"";
            req.Accept = "text/xml";
            req.Method = "POST";
            //Passes the SoapRequest String to the WebService
            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(strSoap);
                }
            }
            //Gets the response
            WebResponse response = req.GetResponse();
            //Writes the Response
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream);
            xmlResponse = sr.ReadToEnd();

            return xmlResponse;
        }

        private static string ErrorCodeToString(int Errorcode)
        {
            string result = "";
            switch (Errorcode)
            {
                case 5100: result = "Caracteres inválidos en campo username (fuera del rango ASCII)"; break;
                case 5101: result = "Caracteres inválidos en campo password (fuera del rango ASCII)"; break;
                case 5102: result = "Caracteres inválidos en campo token (fuera del rango ASCII)"; break;
                case 5103: result = "Caracteres inválidos en campo xml (fuera del rango ASCII)"; break;
                case 5104: result = "Caracteres inválidos en campo uuid (fuera del rango ASCII)"; break;
                case 5105: result = "ID de paquete de timbres inválido"; break;
                case 5200: result = "Error de autenticación, la combinación username y password son inválidas"; break;
                case 5201: result = "Token de sesión inválido"; break;
                case 5202: result = "Sesión previamente cerrada"; break;
                case 5203: result = "Sesión expirada"; break;
                case 5300: result = "No más timbres disponibles"; break;
                case 200: result = "UUID en proceso de cancelación"; break;
                case 202: result = "UUID previamente cancelado"; break;
                case 203: result = "UUID consultado no pertenece a contribuyente"; break;
                case 205: result = "UUID consultado desconocido"; break;
                case 206: result = "UUID no solicitado para cancelación"; break;
                case 301: result = "Error en la estructura del XML con respecto al ANEXO 20 de la Resolución Miscelánea Fiscal 2010"; break;
                case 302: result = "Sello mal formado o inválido"; break;
                case 303: result = "Sello de firma no corresponde a CSD del emisor"; break;
                case 304: result = "CSD del contribuyente vencido o inválido"; break;
                case 305: result = "La fecha de emisión no esta dentro de la vigencia del CSD del Emisor"; break;
                case 306: result = "El certificado no es de tipo CSD"; break;
                case 307: result = "El CFDI contiene un timbre previo"; break;
                case 308: result = "Certificado no expedido por el SAT"; break;
                case 401: result = "CFD fuera de fecha (emitido hace más de 72 horas)"; break;
                case 402: result = "RFC del emisor no se encuentra en el régimen de contribuyentes"; break;
                case 403: result = "La fecha de emisión no es posterior al 01 de enero 2012"; break;
                case 602: result = "UUID desconocido"; break;
                case 603: result = "UUID no pertenece a contribuyente"; break;
                case 604: result = "UUID cancelado"; break;
                default: result = "Error desconocido"; break;
            }
            return result;
        }

        /// <summary>
        /// Convierte el contenido de un archivo a su expresión hexadecimal
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string PathContentToHexString(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            string str = BitConverter.ToString(bytes).Replace("-", "");
            return str;
        }

        #endregion
    }
}