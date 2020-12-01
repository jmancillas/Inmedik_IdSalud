using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    #region vwAlmBranchRequest

    public class vwAlmBranchRequestAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return (folio).ToString("D7"); } }
        public string clinic { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public DateTime? responded { get; set; }
        public string sResponded { get { return responded.HasValue ? DateTimeHelper.GetDateTimeFromDateTimeUTC(responded.Value).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")) : ""; } }

        public vwAlmBranchRequestAux()
        {

        }
    }

    public class vwBranchRequestResult : Result
    {
        public vwAlmBranchRequestAux data { get; set; }
        public List<vwAlmBranchRequestAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwBranchRequestResult()
        {
            data = new vwAlmBranchRequestAux();
            data_list = new List<vwAlmBranchRequestAux>();
            total = new NumericResult();
        }
    }

    #endregion

    #region vwAlmBranchRequestResponse

    public class vwAlmBranchRequestResponseAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return (folio).ToString("D7"); } }
        public int requestId { get; set; }
        public int requestFolio { get; set; }
        public string srequestFolio { get { return (requestFolio).ToString("D7"); } }
        public string clinic { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }

        public vwAlmBranchRequestResponseAux()
        {

        }
    }

    public class vwBranchRequestResponseResult : Result
    {
        public vwAlmBranchRequestResponseAux data { get; set; }
        public List<vwAlmBranchRequestResponseAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwBranchRequestResponseResult()
        {
            data = new vwAlmBranchRequestResponseAux();
            data_list = new List<vwAlmBranchRequestResponseAux>();
            total = new NumericResult();
        }
    }

    #endregion

    #region vwAlmBranchRequestConfirmed

    public class vwAlmBranchRequestConfirmedAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return (folio).ToString("D7"); } }
        public int requestId { get; set; }
        public int requestFolio { get; set; }
        public string srequestFolio { get { return (requestFolio).ToString("D7"); } }
        public string clinic { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }

        public vwAlmBranchRequestConfirmedAux()
        {

        }
    }

    public class vwBranchRequestConfirmedResult : Result
    {
        public vwAlmBranchRequestConfirmedAux data { get; set; }
        public List<vwAlmBranchRequestConfirmedAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwBranchRequestConfirmedResult()
        {
            data = new vwAlmBranchRequestConfirmedAux();
            data_list = new List<vwAlmBranchRequestConfirmedAux>();
            total = new NumericResult();
        }
    }

    #endregion

    #region vwAlmBranchRequestRejected

    public class vwAlmBranchRequestRejectedAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return (folio).ToString("D7"); } }
        public int requestId { get; set; }
        public int requestFolio { get; set; }
        public string srequestFolio { get { return (requestFolio).ToString("D7"); } }
        public string clinic { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }

        public vwAlmBranchRequestRejectedAux()
        {

        }
    }

    public class vwBranchRequestRejectedResult : Result
    {
        public vwAlmBranchRequestRejectedAux data { get; set; }
        public List<vwAlmBranchRequestRejectedAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwBranchRequestRejectedResult()
        {
            data = new vwAlmBranchRequestRejectedAux();
            data_list = new List<vwAlmBranchRequestRejectedAux>();
            total = new NumericResult();
        }
    }

    #endregion

    #region vwAlmBranchRequestToDelivery

    public class vwAlmBranchRequestToDeliveryAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return (folio).ToString("D7"); } }
        public int requestId { get; set; }
        public int requestFolio { get; set; }
        public string srequestFolio { get { return (requestFolio).ToString("D7"); } }
        public int confirmedId { get; set; }
        public int confirmedFolio { get; set; }
        public string sconfirmedFolio { get { return (confirmedFolio).ToString("D7"); } }
        public string clinic { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }

        public vwAlmBranchRequestToDeliveryAux()
        {

        }
    }

    public class vwAlmBranchRequestToDeliveryResult : Result
    {
        public vwAlmBranchRequestToDeliveryAux data { get; set; }
        public List<vwAlmBranchRequestToDeliveryAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwAlmBranchRequestToDeliveryResult()
        {
            data = new vwAlmBranchRequestToDeliveryAux();
            data_list = new List<vwAlmBranchRequestToDeliveryAux>();
            total = new NumericResult();
        }
    }

    #endregion    

    #region vwAlmBranchRequestDelivered

    public class vwAlmBranchRequestDeliveredAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return (folio).ToString("D7"); } }
        public int requestId { get; set; }
        public int requestFolio { get; set; }
        public string srequestFolio { get { return (requestFolio).ToString("D7"); } }
        public int clinicId { get; set; }
        public string clinic { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }

        public vwAlmBranchRequestDeliveredAux()
        {

        }
    }

    public class vwAlmBranchRequestDeliveredResult : Result
    {
        public vwAlmBranchRequestDeliveredAux data { get; set; }
        public List<vwAlmBranchRequestDeliveredAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public vwAlmBranchRequestDeliveredResult()
        {
            data = new vwAlmBranchRequestDeliveredAux();
            data_list = new List<vwAlmBranchRequestDeliveredAux>();
            total = new NumericResult();
        }
    }

    #endregion    

    #region vwAlmBranchRequestResponseTransaction

    public class vwAlmBranchRequestResponseTransactionAux
    {
        public int almBranchRequestTransactionId { get; set; }
        public int almBranchRequestResponseTransactionId { get; set; }
        public int almBranchRequestResponseId { get; set; }
        public int conceptId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public decimal requested { get; set; }
        public decimal available { get; set; }
        public decimal price { get; set; }
        public bool iva { get; set; }
        public decimal amountIva { get { return iva ? price * available * 0.16m : 0; } }
        public decimal total { get { return (price * available) + amountIva; } }
        public bool confirmed { get; set; }

        public vwAlmBranchRequestResponseTransactionAux()
        {

        }
    }

    #endregion    

    #region vwAlmBranchRequestConfirmedTransaction

    public class vwAlmBranchRequestConfirmedTransactionAux
    {
        public int almBranchRequestTransactionId { get; set; }
        public int almBranchRequestConfirmedTransactionId { get; set; }
        public int almBranchRequestConfirmedId { get; set; }
        public int conceptId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public decimal requested { get; set; }
        public decimal sended { get; set; }
        public decimal confirmed { get; set; }
        public decimal price { get; set; }
        public bool iva { get; set; }
        public decimal amountIva { get { return iva ? price * confirmed * 0.16m : 0; } }
        public decimal total { get { return (price * confirmed) + amountIva; } }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }

        public vwAlmBranchRequestConfirmedTransactionAux()
        {

        }
    }

    #endregion    

    #region vwAlmBranchRequestRejectedTransaction

    public class vwAlmBranchRequestRejectedTransactionAux
    {
        public int almBranchRequestTransactionId { get; set; }
        public int almBranchRequestConfirmedTransactionId { get; set; }
        public int almBranchRequestRejectedId { get; set; }
        public int conceptId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public decimal requested { get; set; }
        public decimal rejected { get; set; }
        public decimal price { get; set; }
        public bool iva { get; set; }
        public decimal amountIva { get { return iva ? price * rejected * 0.16m : 0; } }
        public decimal total { get { return (price * rejected) + amountIva; } }

        public vwAlmBranchRequestRejectedTransactionAux()
        {

        }
    }

    #endregion    

    #region vwAlmBranchRequestToSendTransaction

    public class vwAlmBranchRequestToSendTransactionAux
    {
        public int almBranchRequestTransactionId { get; set; }
        public int almBranchRequestToSendId { get; set; }
        public int conceptId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public decimal requested { get; set; }
        public decimal toSend { get; set; }
        public decimal price { get; set; }
        public bool iva { get; set; }
        public decimal amountIva { get { return iva ? price * toSend * 0.16m : 0; } }
        public decimal total { get { return (price * toSend) + amountIva; } }

        public vwAlmBranchRequestToSendTransactionAux()
        {

        }
    }

    #endregion        

    #region vwAlmBranchRequestToDeliveryTransaction

    public class vwAlmBranchRequestToDeliveryTransactionAux
    {
        public int almBranchRequestToDeliveryTransactionId { get; set; }
        public int almBranchRequestConfirmedTransactionId { get; set; }
        public int almBranchRequestToDeliveryId { get; set; }
        public int almBranchRequestConfirmedId { get; set; }
        public int conceptId { get; set; }
        public string concept { get; set; }
        public string code { get; set; }
        public decimal toSend { get; set; }
        public decimal sended { get; set; }
        public decimal toReturn { get; set; }
        public bool delivered { get; set; }
        public bool iva { get; set; }
        public decimal price { get; set; }
        public decimal minPrice { get; set; }
        public decimal maxPrice { get; set; }
        public decimal amountIva { get { return iva ? price * toSend * 0.16m : 0; } }
        public decimal total { get { return (price * toSend) + amountIva; } }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }

        public int almStockId { get; set; }
        public DateTime? expiredDate { get; set; }
        public string batch { get; set; }

        public vwAlmBranchRequestToDeliveryTransactionAux()
        {

        }
    }

    #endregion            

    #region vwAlmBranchRequestDeliveredTransaction

    public class vwAlmBranchRequestDeliveredTransactionAux
    {
        public int almBranchRequestDeliveredTransactionId { get; set; }
        public int AlmBranchRequestDeliveredId { get; set; }
        public int AlmBranchRequestConfirmedId { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return folio.ToString("D7"); } }
        public int conceptId { get; set; }
        public string conceptName { get; set; }
        public string code { get; set; }
        public decimal toSend { get; set; }
        public decimal sended { get; set; }
        public decimal returned { get; set; }
        public bool iva { get; set; }
        public decimal price { get; set; }
        public decimal minPrice { get; set; }
        public decimal maxPrice { get; set; }
        public string batch { get; set; }
        public decimal amountIva { get { return iva ? price * toSend * 0.16m : 0; } }
        public decimal total { get { return (price * toSend) + amountIva; } }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime expiredDate { get; set; }
        public string sExpiredDate { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(expiredDate).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }

        public vwAlmBranchRequestDeliveredTransactionAux()
        {

        }
    }

    #endregion            

    #region AlmBranchRequest

    public class AlmBranchRequestAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return folio.ToString("D7"); } }
        public List<AlmBranchRequestTransactionAux> almBranchRequestTransactionAux { get; set; }
        public ClinicAux clinicAux { get; set; }
        public AlmStatusAux almStatusAux { get; set; }
        public string warehouseComments { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }

        public AlmBranchRequestAux()
        {
            almBranchRequestTransactionAux = new List<AlmBranchRequestTransactionAux>();
            clinicAux = new ClinicAux();
            almStatusAux = new AlmStatusAux();
        }

    }

    public class AlmBranchRequestResult : Result
    {
        public AlmBranchRequestAux data { get; set; }
        public List<AlmBranchRequestAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public AlmBranchRequestResult()
        {
            data = new AlmBranchRequestAux();
            data_list = new List<AlmBranchRequestAux>();
            total = new NumericResult();
        }
    }

    #endregion

    #region AlmBranchRequestTransaction

    public class AlmBranchRequestTransactionAux
    {
        public int id { get; set; }
        public ConceptAux conceptAux { get; set; }
        public decimal quantity { get; set; }
        public bool active { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }

        public AlmBranchRequestTransactionAux()
        {
            conceptAux = new ConceptAux();
        }
    }

    #endregion

    #region AlmBranchRequestResponse

    public class AlmBranchRequestResponseAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return folio.ToString("D7"); } }
        public AlmBranchRequestAux almBranchRequestAux { get; set; }
        public List<AlmBranchRequestResponseTransactionAux> almBranchRequestResponseTransactionAux { get; set; }
        public List<vwAlmBranchRequestResponseTransactionAux> vwAlmBranchRequestResponseTransactionAuxList { get; set; }
        public AlmStatusAux almStatusAux { get; set; }
        public string branchComments { get; set; }
        public string warehouseComments { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }

        public List<vwAlmBranchRequestResponseTransactionAux> RequestResponseTransactionsConfirmed { get; set; }
        public List<vwAlmBranchRequestResponseTransactionAux> RequestResponseTransactionsRejected { get; set; }

        public AlmBranchRequestResponseAux()
        {
            almBranchRequestResponseTransactionAux = new List<AlmBranchRequestResponseTransactionAux>();
            vwAlmBranchRequestResponseTransactionAuxList = new List<vwAlmBranchRequestResponseTransactionAux>();
            RequestResponseTransactionsConfirmed = new List<vwAlmBranchRequestResponseTransactionAux>();
            RequestResponseTransactionsRejected = new List<vwAlmBranchRequestResponseTransactionAux>();
            almBranchRequestAux = new AlmBranchRequestAux();
            almStatusAux = new AlmStatusAux();
        }

        public void SeparateAcceptedAndRejectedRequest()
        {
            foreach (var vw in vwAlmBranchRequestResponseTransactionAuxList)
            {
                if (vw.confirmed)
                {
                    RequestResponseTransactionsConfirmed.Add(vw);
                }
                else
                {
                    RequestResponseTransactionsRejected.Add(vw);
                }
            }
        }
    }

    public class AlmBranchRequestResponseResult : Result
    {
        public AlmBranchRequestResponseAux data { get; set; }
        public List<AlmBranchRequestResponseAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public AlmBranchRequestResponseResult()
        {
            data = new AlmBranchRequestResponseAux();
            data_list = new List<AlmBranchRequestResponseAux>();
            total = new NumericResult();
        }
    }

    #endregion    

    #region AlmBranchRequestResponseTransaction

    public class AlmBranchRequestResponseTransactionAux
    {
        public int id { get; set; }
        public ConceptAux conceptAux { get; set; }
        public decimal quantity { get; set; }
        public decimal price { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }

        public AlmBranchRequestResponseTransactionAux()
        {
            conceptAux = new ConceptAux();
        }
    }

    #endregion

    #region AlmBranchRequestConfirmed

    public class AlmBranchRequestConfirmedAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return folio.ToString("D7"); } }
        public AlmBranchRequestResponseAux requestResponseAux { get; set; }
        public AlmStatusAux almStatusAux { get; set; }
        public List<AlmBranchRequestConfirmedTransactionAux> requestConfirmedTransactionsAux { get; set; }
        public bool seen { get; set; }
        public string comments { get; set; }
        public string branchComments { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }

        public List<vwAlmBranchRequestConfirmedTransactionAux> vwAlmBranchRequestConfirmedTransactionAuxList { get; set; }

        public AlmBranchRequestConfirmedAux()
        {
            requestResponseAux = new AlmBranchRequestResponseAux();
            almStatusAux = new AlmStatusAux();
            requestConfirmedTransactionsAux = new List<AlmBranchRequestConfirmedTransactionAux>();
            vwAlmBranchRequestConfirmedTransactionAuxList = new List<vwAlmBranchRequestConfirmedTransactionAux>();
        }
    }

    public class AlmBranchRequestConfirmedResult : Result
    {
        public AlmBranchRequestConfirmedAux data { get; set; }
        public List<AlmBranchRequestConfirmedAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public AlmBranchRequestConfirmedResult()
        {
            data = new AlmBranchRequestConfirmedAux();
            data_list = new List<AlmBranchRequestConfirmedAux>();
            total = new NumericResult();
        }
    }

    #endregion

    #region AlmBranchRequestConfirmedTransaction

    public class AlmBranchRequestConfirmedTransactionAux
    {
        public int id { get; set; }
        public AlmBranchRequestConfirmedAux requestConfirmedAux { get; set; }
        public ConceptAux conceptAux { get; set; }
        public decimal quantity { get; set; }
        public decimal price { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }

        public AlmBranchRequestConfirmedTransactionAux()
        {
            requestConfirmedAux = new AlmBranchRequestConfirmedAux();
            conceptAux = new ConceptAux();
        }
    }

    #endregion

    #region AlmBranchRequestRejected

    public class AlmBranchRequestRejectedAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return folio.ToString("D7"); } }
        public AlmBranchRequestResponseAux requestResponseAux { get; set; }
        public AlmStatusAux almStatusAux { get; set; }
        public List<AlmBranchRequestRejectedTransactionAux> requestRejectedTransactionsAux { get; set; }
        public string comments { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }

        public List<vwAlmBranchRequestRejectedTransactionAux> vwAlmBranchRequestRejectedTransactionAuxList { get; set; }

        public AlmBranchRequestRejectedAux()
        {
            requestResponseAux = new AlmBranchRequestResponseAux();
            almStatusAux = new AlmStatusAux();
            requestRejectedTransactionsAux = new List<AlmBranchRequestRejectedTransactionAux>();
            vwAlmBranchRequestRejectedTransactionAuxList = new List<vwAlmBranchRequestRejectedTransactionAux>();
        }
    }

    #endregion

    #region AlmBranchRequestRejectedTransaction

    public class AlmBranchRequestRejectedTransactionAux
    {
        public int id { get; set; }
        public AlmBranchRequestRejectedAux requestRejectedAux { get; set; }
        public ConceptAux conceptAux { get; set; }
        public decimal quantity { get; set; }
        public decimal price { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }

        public AlmBranchRequestRejectedTransactionAux()
        {
            requestRejectedAux = new AlmBranchRequestRejectedAux();
            conceptAux = new ConceptAux();
        }
    }

    public class AlmBranchRequestRejectedResult : Result
    {
        public AlmBranchRequestRejectedAux data { get; set; }
        public List<AlmBranchRequestRejectedAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public AlmBranchRequestRejectedResult()
        {
            data = new AlmBranchRequestRejectedAux();
            data_list = new List<AlmBranchRequestRejectedAux>();
            total = new NumericResult();
        }
    }

    #endregion

    #region AlmBranchRequestToDelivery

    public class AlmBranchRequestToDeliveryAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return folio.ToString("D7"); } }
        public AlmStatusAux almStatusAux { get; set; }
        public List<AlmBranchRequestToDeliveryTransactionAux> requestToDeliveryTransactionsAux { get; set; }
        public List<vwAlmBranchRequestToDeliveryTransactionAux> vwrequestToDeliveryTransactionsAux { get; set; }
        public AlmBranchRequestConfirmedAux requestConfirmedAux { get; set; }

        public string comments { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }

        public AlmBranchRequestToDeliveryAux()
        {
            almStatusAux = new AlmStatusAux();
            requestToDeliveryTransactionsAux = new List<AlmBranchRequestToDeliveryTransactionAux>();
            requestConfirmedAux = new AlmBranchRequestConfirmedAux();
            vwrequestToDeliveryTransactionsAux = new List<vwAlmBranchRequestToDeliveryTransactionAux>();
        }

        public bool IsValidRequest()
        {
            bool result = false;

            using (dbINMEDIK db = new dbINMEDIK())
            {
                foreach (var transaction in vwrequestToDeliveryTransactionsAux)
                {
                    result = db.Concept.Any(concept => concept.id == transaction.conceptId);

                    if (!result)
                    {
                        break;
                    }

                    //result = transaction.price <= transaction.minPrice;

                    //if (!result)
                    //{
                    //    break;
                    //}
                }
            }

            return result;
        }
    }

    #endregion

    #region AlmBranchRequestToDeliveryTransaction

    public class AlmBranchRequestToDeliveryTransactionAux
    {
        public int id { get; set; }
        public AlmBranchRequestToDeliveryAux requestToDeliveryAux { get; set; }
        public ConceptAux conceptAux { get; set; }
        public decimal ToSend { get; set; }
        public DateTime created { get; set; }
        public bool delivered { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public int almStockId { get; set; }
        public AlmBranchRequestToDeliveryTransactionAux()
        {
            requestToDeliveryAux = new AlmBranchRequestToDeliveryAux();
            conceptAux = new ConceptAux();
        }
    }

    public class AlmBranchRequestToDeliveryResult : Result
    {
        public AlmBranchRequestToDeliveryAux data { get; set; }
        public List<AlmBranchRequestToDeliveryAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public AlmBranchRequestToDeliveryResult()
        {
            data = new AlmBranchRequestToDeliveryAux();
            data_list = new List<AlmBranchRequestToDeliveryAux>();
            total = new NumericResult();
        }
    }

    #endregion

    #region AlmBranchRequestDelivered

    public class AlmBranchRequestDeliveredAux
    {
        public int id { get; set; }
        public int folio { get; set; }
        public string sFolio { get { return folio.ToString("D7"); } }
        public AlmStatusAux almStatusAux { get; set; }
        public List<AlmBranchRequestDeliveredTransactionAux> requestDeliveredTransactionsAux { get; set; }
        public List<vwAlmBranchRequestDeliveredTransactionAux> vwrequestDeliveredTransactionsAux { get; set; }
        public AlmBranchRequestToDeliveryAux requestToDeliveryAux { get; set; }

        public string comments { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }

        public AlmBranchRequestDeliveredAux()
        {
            almStatusAux = new AlmStatusAux();
            requestDeliveredTransactionsAux = new List<AlmBranchRequestDeliveredTransactionAux>();
            requestToDeliveryAux = new AlmBranchRequestToDeliveryAux();
            vwrequestDeliveredTransactionsAux = new List<vwAlmBranchRequestDeliveredTransactionAux>();
        }
    }

    #endregion

    #region AlmBranchRequestDeliveredTransaction

    public class AlmBranchRequestDeliveredTransactionAux
    {
        public int id { get; set; }
        public AlmBranchRequestDeliveredAux requestDeliveredAux { get; set; }
        public ConceptAux conceptAux { get; set; }
        public decimal ToSend { get; set; }
        public decimal price { get; set; }
        public DateTime created { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(created).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public DateTime updated { get; set; }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(updated).ToString("dddd, dd/MM/yyyy", new CultureInfo("es-MX")); } }
        public AlmBranchRequestDeliveredTransactionAux()
        {
            requestDeliveredAux = new AlmBranchRequestDeliveredAux();
            conceptAux = new ConceptAux();
        }
    }

    public class AlmBranchRequestDeliveredResult : Result
    {
        public AlmBranchRequestDeliveredAux data { get; set; }
        public List<AlmBranchRequestDeliveredAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public AlmBranchRequestDeliveredResult()
        {
            data = new AlmBranchRequestDeliveredAux();
            data_list = new List<AlmBranchRequestDeliveredAux>();
            total = new NumericResult();
        }
    }

    #endregion

    #region WarehouseRequest

    public class WarehouseRequestAux
    {
        public int RequestSentFromWarehouseCounter { get; set; }
        public int RequestReceivedFromWarehouseCounter { get; set; }
        public int RequestConfirmedFromWarehouseCounter { get; set; }
        public int RequestInDeliveryFromWarehouseCounter { get; set; }
    }

    public class WarehouseRequestResult : Result
    {
        public WarehouseRequestAux data { get; set; }
        public List<WarehouseRequestAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public WarehouseRequestResult()
        {

        }
    }

    #endregion

    #region PredictedProductRequest
    public class PredictedProductRequestAux
    {
        public string code { get; set; }
        /// <summary>
        /// conceptId
        /// </summary>
        public int id { get; set; }
        public decimal sales { get; set; }
        public int quantity { get; set; }
        public int stock { get; set; }
        public string name { get; set; }
    }
    public class PredictedProductRequestResult : Result
    {
        public PredictedProductRequestAux data { get; set; }
        public List<PredictedProductRequestAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public PredictedProductRequestResult()
        {
            data = new PredictedProductRequestAux();
            data_list = new List<PredictedProductRequestAux>();
            total = new NumericResult();
        }
    }
    #endregion

    public class WarehouseRequestHelper
    {
        #region vwBranchRequest

        public static vwBranchRequestResult GetBranchRequestSendDT(DTParameterModel model, int clinicId)
        {
            vwBranchRequestResult result = new vwBranchRequestResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwAlmBranchRequest> query = db.vwAlmBranchRequest.Where(vw => vw.AlmStatusId == AlmStatusAux.Enviado && vw.ClinicId == clinicId);
                    foreach (DTColumn column in model.Columns)
                    {
                        bool columnHasValue = !string.IsNullOrEmpty(column.Search.Value);
                        switch (column.Data)
                        {
                            case "sFolio":
                                query = columnHasValue ? query.Where(br => br.Folio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "clinic":
                                query = columnHasValue ? query.Where(br => br.Clinic.Contains(column.Search.Value)) : query;
                                break;
                            default:
                                break;
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Order.First().Dir) && !string.IsNullOrEmpty(model.Order.First().Data))
                    {
                        string order = model.Order.First().Dir;
                        string orderColumn = model.Order.First().Data;

                        switch (orderColumn)
                        {
                            case "sFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Folio) : query.OrderByDescending(br => br.Folio);
                                break;
                            case "clinic":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Clinic) : query.OrderByDescending(br => br.Clinic);
                                break;
                            case "sCreated":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Created) : query.OrderByDescending(br => br.Created);
                                break;
                            case "sResponded":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Responded) : query.OrderByDescending(br => br.Responded);
                                break;
                            default:
                                break;
                        }
                    }

                    result.total.value = query.Count();
                    query = query.Skip(model.Start).Take(model.Length);
                    foreach (vwAlmBranchRequest objDb in query)
                    {
                        vwAlmBranchRequestAux aux = new vwAlmBranchRequestAux();
                        DataHelper.fill(aux, objDb);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        #endregion

        #region AlmBranchRequest

        public static AlmBranchRequestResult GetAlmBranchRequestDetail(int wareHouseRequestId)
        {
            AlmBranchRequestResult result = new AlmBranchRequestResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    AlmBranchRequest abrInDb = db.AlmBranchRequest.Where(ab => ab.Id == wareHouseRequestId).FirstOrDefault();
                    if (abrInDb == null)
                    {
                        throw new Exception("Solicitud no válida");
                    }

                    DataHelper.fill(result.data, abrInDb);
                    DataHelper.fill(result.data.clinicAux, abrInDb.Clinic);
                    DataHelper.fill(result.data.almStatusAux, abrInDb.AlmStatus);
                    foreach (var almBranchRequestTransaction in abrInDb.AlmBranchRequestTransaction)
                    {
                        AlmBranchRequestTransactionAux abrtAux = new AlmBranchRequestTransactionAux();
                        DataHelper.fill(abrtAux, almBranchRequestTransaction);
                        DataHelper.fill(abrtAux.conceptAux, almBranchRequestTransaction.Concept);
                        DataHelper.fill(abrtAux.conceptAux.productAux, almBranchRequestTransaction.Concept.Product.First());
                        result.data.almBranchRequestTransactionAux.Add(abrtAux);
                    }

                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        #endregion

        #region AlmBranchRequestResponse

        public static vwBranchRequestResponseResult GetBranchRequestOnHoldDT(DTParameterModel model, int clinicId)
        {
            vwBranchRequestResponseResult result = new vwBranchRequestResponseResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwAlmBranchRequestResponse> query = db.vwAlmBranchRequestResponse.Where(vw => vw.AlmStatusId == AlmStatusAux.Enviado && vw.ClinicId == clinicId);
                    foreach (DTColumn column in model.Columns)
                    {
                        bool columnHasValue = !string.IsNullOrEmpty(column.Search.Value);
                        switch (column.Data)
                        {
                            case "sFolio":
                                query = columnHasValue ? query.Where(br => br.Folio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "srequestFolio":
                                query = columnHasValue ? query.Where(br => br.RequestFolio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "clinic":
                                query = columnHasValue ? query.Where(br => br.Clinic.Contains(column.Search.Value)) : query;
                                break;
                            default:
                                break;
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Order.First().Dir) && !string.IsNullOrEmpty(model.Order.First().Data))
                    {
                        string order = model.Order.First().Dir;
                        string orderColumn = model.Order.First().Data;

                        switch (orderColumn)
                        {
                            case "sFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Folio) : query.OrderByDescending(br => br.Folio);
                                break;
                            case "srequestFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.RequestFolio) : query.OrderByDescending(br => br.RequestFolio);
                                break;
                            case "status":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Clinic) : query.OrderByDescending(br => br.Clinic);
                                break;
                            case "created":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Created) : query.OrderByDescending(br => br.Created);
                                break;
                            default:
                                break;
                        }
                    }

                    result.total.value = query.Count();
                    query = query.Skip(model.Start).Take(model.Length);
                    foreach (vwAlmBranchRequestResponse objDb in query)
                    {
                        vwAlmBranchRequestResponseAux aux = new vwAlmBranchRequestResponseAux();
                        DataHelper.fill(aux, objDb);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        public static AlmBranchRequestResponseResult GetAlmBranchRequestResponseDetail(int wareHouseRequestResponseId)
        {
            AlmBranchRequestResponseResult result = new AlmBranchRequestResponseResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    AlmBranchRequestResponse abrrInDb = db.AlmBranchRequestResponse.Where(ab => ab.Id == wareHouseRequestResponseId).FirstOrDefault();

                    if (abrrInDb == null)
                        throw new Exception("Solicitud no válida");

                    DataHelper.fill(result.data, abrrInDb);
                    DataHelper.fill(result.data.almStatusAux, abrrInDb.AlmStatus);
                    List<vwAlmBranchRequestResponseTransaction> vwList = db.vwAlmBranchRequestResponseTransaction.Where(v => v.AlmBranchRequestResponseId == wareHouseRequestResponseId).ToList();
                    foreach (var vw in vwList)
                    {
                        vwAlmBranchRequestResponseTransactionAux aux = new vwAlmBranchRequestResponseTransactionAux();
                        DataHelper.fill(aux, vw);
                        result.data.vwAlmBranchRequestResponseTransactionAuxList.Add(aux);
                    }

                    DataHelper.fill(result.data.almBranchRequestAux, abrrInDb.AlmBranchRequest);
                    DataHelper.fill(result.data.almBranchRequestAux.clinicAux, abrrInDb.AlmBranchRequest.Clinic);
                    DataHelper.fill(result.data.almBranchRequestAux.almStatusAux, abrrInDb.AlmBranchRequest.AlmStatus);

                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        public static AlmBranchRequestResponseResult SaveAlmBranchRequestResponse(AlmBranchRequestResponseAux almBranchRequestResponseAux)
        {
            AlmBranchRequestResponseResult result = new AlmBranchRequestResponseResult();

            almBranchRequestResponseAux.SeparateAcceptedAndRejectedRequest();
            bool RequestRejected = false;

            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (almBranchRequestResponseAux.RequestResponseTransactionsConfirmed.Count > 0)
                        {
                            AlmBranchRequestConfirmed requestConfirmedInDb = db.AlmBranchRequestConfirmed.Create();
                            AlmFolio almFolioInDb = db.AlmFolio.First();

                            requestConfirmedInDb.Folio = almFolioInDb.BranchRequestConfirmedFolio;
                            requestConfirmedInDb.AlmBranchRequestResponseId = almBranchRequestResponseAux.id;
                            requestConfirmedInDb.AlmStatusId = AlmStatusAux.Enviado;
                            requestConfirmedInDb.BranchComments = almBranchRequestResponseAux.branchComments;
                            requestConfirmedInDb.Seen = false;
                            requestConfirmedInDb.Created = DateTime.UtcNow;
                            requestConfirmedInDb.Updated = DateTime.UtcNow;

                            db.AlmBranchRequestConfirmed.Add(requestConfirmedInDb);
                            db.SaveChanges();

                            almFolioInDb.BranchRequestConfirmedFolio++;
                            db.SaveChanges();

                            foreach (var requestConfirmedTransaction in almBranchRequestResponseAux.RequestResponseTransactionsConfirmed)
                            {
                                AlmBranchRequestConfirmedTransaction requestConfirmedTransactionInDb = db.AlmBranchRequestConfirmedTransaction.Create();
                                requestConfirmedTransactionInDb.AlmBranchRequestConfirmedId = requestConfirmedInDb.Id;
                                requestConfirmedTransactionInDb.AlmBranchRequestResponseTransactionId = requestConfirmedTransaction.almBranchRequestResponseTransactionId;
                                requestConfirmedTransactionInDb.ConceptId = requestConfirmedTransaction.conceptId;
                                requestConfirmedTransactionInDb.Quantity = requestConfirmedTransaction.requested;
                                requestConfirmedTransactionInDb.Price = requestConfirmedTransaction.price;
                                requestConfirmedTransactionInDb.Created = DateTime.UtcNow;
                                requestConfirmedTransactionInDb.Updated = DateTime.UtcNow;

                                db.AlmBranchRequestConfirmedTransaction.Add(requestConfirmedTransactionInDb);
                            }
                            db.SaveChanges();
                        }

                        if (almBranchRequestResponseAux.RequestResponseTransactionsRejected.Count > 0)
                        {
                            AlmBranchRequestRejected requestRejectedInDb = db.AlmBranchRequestRejected.Create();
                            AlmFolio almFolioInDb = db.AlmFolio.First();

                            requestRejectedInDb.Folio = almFolioInDb.BranchRequestRejectedFolio;
                            requestRejectedInDb.AlmBranchRequestResponseId = almBranchRequestResponseAux.id;
                            //requestRejectedInDb.Comments = almBranchRequestResponseAux.comments;
                            requestRejectedInDb.AlmStatusId = AlmStatusAux.Rechazado;
                            requestRejectedInDb.Created = DateTime.UtcNow;
                            requestRejectedInDb.Updated = DateTime.UtcNow;

                            db.AlmBranchRequestRejected.Add(requestRejectedInDb);
                            db.SaveChanges();

                            almFolioInDb.BranchRequestRejectedFolio++;
                            db.SaveChanges();

                            foreach (var requestRejectedTransaction in almBranchRequestResponseAux.RequestResponseTransactionsRejected)
                            {
                                AlmBranchRequestRejectedTransaction requestRejectedTransactionInDb = db.AlmBranchRequestRejectedTransaction.Create();
                                requestRejectedTransactionInDb.AlmBranchRequestRejectedId = requestRejectedInDb.Id;
                                requestRejectedTransactionInDb.ConceptId = requestRejectedTransaction.conceptId;
                                requestRejectedTransactionInDb.Quantity = requestRejectedTransaction.requested;
                                requestRejectedTransactionInDb.Price = requestRejectedTransaction.price;
                                requestRejectedTransactionInDb.Created = DateTime.UtcNow;
                                requestRejectedTransactionInDb.Updated = DateTime.UtcNow;

                                db.AlmBranchRequestRejectedTransaction.Add(requestRejectedTransactionInDb);
                            }
                            db.SaveChanges();
                        }

                        RequestRejected = almBranchRequestResponseAux.RequestResponseTransactionsRejected.Count == almBranchRequestResponseAux.vwAlmBranchRequestResponseTransactionAuxList.Count;

                        AlmBranchRequest branchRequestInDb = db.AlmBranchRequest.Where(abr => abr.Id == almBranchRequestResponseAux.almBranchRequestAux.id).FirstOrDefault();
                        branchRequestInDb.AlmStatusId = RequestRejected ? AlmStatusAux.Rechazado : AlmStatusAux.Confirmado;

                        AlmBranchRequestResponse branchRequestResponseInDb = db.AlmBranchRequestResponse.Where(abrr => abrr.Id == almBranchRequestResponseAux.id).FirstOrDefault();
                        branchRequestResponseInDb.AlmStatusId = RequestRejected ? AlmStatusAux.Rechazado : AlmStatusAux.Confirmado;
                        db.SaveChanges();

                        dbTransaction.Commit();
                        result.success = true;
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        result.success = false;
                        result.exception = e;
                        result.message = $"Ocurrió un error inesperado {result.exception_message}";
                    }
                }
            }
            return result;
        }

        #endregion

        #region AlmBranchRequestConfirmed

        public static vwBranchRequestConfirmedResult GetBranchRequestConfirmedDT(DTParameterModel model, int clinicId)
        {
            vwBranchRequestConfirmedResult result = new vwBranchRequestConfirmedResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwAlmBranchRequestConfirmed> query = db.vwAlmBranchRequestConfirmed.Where(vw => vw.AlmStatusId == AlmStatusAux.Enviado && vw.ClinicId == clinicId);
                    foreach (DTColumn column in model.Columns)
                    {
                        bool columnHasValue = !string.IsNullOrEmpty(column.Search.Value);
                        switch (column.Data)
                        {
                            case "sFolio":
                                query = columnHasValue ? query.Where(br => br.Folio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "srequestFolio":
                                query = columnHasValue ? query.Where(br => br.RequestFolio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "clinic":
                                query = columnHasValue ? query.Where(br => br.Clinic.Contains(column.Search.Value)) : query;
                                break;
                            default:
                                break;
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Order.First().Dir) && !string.IsNullOrEmpty(model.Order.First().Data))
                    {
                        string order = model.Order.First().Dir;
                        string orderColumn = model.Order.First().Data;

                        switch (orderColumn)
                        {
                            case "sFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Folio) : query.OrderByDescending(br => br.Folio);
                                break;
                            case "srequestFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.RequestFolio) : query.OrderByDescending(br => br.RequestFolio);
                                break;
                            case "clinic":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Clinic) : query.OrderByDescending(br => br.Clinic);
                                break;
                            case "sCreated":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Created) : query.OrderByDescending(br => br.Created);
                                break;
                            default:
                                break;
                        }
                    }

                    result.total.value = query.Count();
                    query = query.Skip(model.Start).Take(model.Length);
                    foreach (vwAlmBranchRequestConfirmed objDb in query)
                    {
                        vwAlmBranchRequestConfirmedAux aux = new vwAlmBranchRequestConfirmedAux();
                        DataHelper.fill(aux, objDb);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        public static AlmBranchRequestConfirmedResult GetAlmBranchRequestConfirmedDetail(int requestConfirmedId)
        {
            AlmBranchRequestConfirmedResult result = new AlmBranchRequestConfirmedResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    AlmBranchRequestConfirmed abrcInDb = db.AlmBranchRequestConfirmed.Where(ab => ab.Id == requestConfirmedId).FirstOrDefault();

                    if (abrcInDb == null)
                        throw new Exception("Solicitud no válida");

                    DataHelper.fill(result.data, abrcInDb);
                    DataHelper.fill(result.data.almStatusAux, abrcInDb.AlmStatus);
                    List<vwAlmBranchRequestConfirmedTransaction> vwList = db.vwAlmBranchRequestConfirmedTransaction.Where(v => v.AlmBranchRequestConfirmedId == requestConfirmedId).ToList();
                    foreach (var vw in vwList)
                    {
                        vwAlmBranchRequestConfirmedTransactionAux aux = new vwAlmBranchRequestConfirmedTransactionAux();
                        DataHelper.fill(aux, vw);
                        result.data.vwAlmBranchRequestConfirmedTransactionAuxList.Add(aux);
                    }

                    DataHelper.fill(result.data.requestResponseAux, abrcInDb.AlmBranchRequestResponse);
                    DataHelper.fill(result.data.requestResponseAux.almBranchRequestAux, abrcInDb.AlmBranchRequestResponse.AlmBranchRequest);
                    DataHelper.fill(result.data.requestResponseAux.almBranchRequestAux.clinicAux, abrcInDb.AlmBranchRequestResponse.AlmBranchRequest.Clinic);
                    DataHelper.fill(result.data.requestResponseAux.almBranchRequestAux.almStatusAux, abrcInDb.AlmBranchRequestResponse.AlmBranchRequest.AlmStatus);

                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        #endregion

        #region AlmBranchRequestRejected

        public static vwBranchRequestRejectedResult GetBranchRequestRejectedDT(DTParameterModel model, int clinicId)
        {
            vwBranchRequestRejectedResult result = new vwBranchRequestRejectedResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwAlmBranchRequestRejected> query = db.vwAlmBranchRequestRejected.Where(vw => vw.AlmStatusId == AlmStatusAux.Rechazado && vw.ClinicId == clinicId);
                    foreach (DTColumn column in model.Columns)
                    {
                        bool columnHasValue = !string.IsNullOrEmpty(column.Search.Value);
                        switch (column.Data)
                        {
                            case "sFolio":
                                query = columnHasValue ? query.Where(br => br.Folio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "srequestFolio":
                                query = columnHasValue ? query.Where(br => br.RequestFolio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "clinic":
                                query = columnHasValue ? query.Where(br => br.Clinic.Contains(column.Search.Value)) : query;
                                break;
                            default:
                                break;
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Order.First().Dir) && !string.IsNullOrEmpty(model.Order.First().Data))
                    {
                        string order = model.Order.First().Dir;
                        string orderColumn = model.Order.First().Data;

                        switch (orderColumn)
                        {
                            case "sFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Folio) : query.OrderByDescending(br => br.Folio);
                                break;
                            case "srequestFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.RequestFolio) : query.OrderByDescending(br => br.RequestFolio);
                                break;
                            case "clinic":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Clinic) : query.OrderByDescending(br => br.Clinic);
                                break;
                            case "sCreated":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Created) : query.OrderByDescending(br => br.Created);
                                break;
                            default:
                                break;
                        }
                    }

                    result.total.value = query.Count();
                    query = query.Skip(model.Start).Take(model.Length);
                    foreach (vwAlmBranchRequestRejected objDb in query)
                    {
                        vwAlmBranchRequestRejectedAux aux = new vwAlmBranchRequestRejectedAux();
                        DataHelper.fill(aux, objDb);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        public static AlmBranchRequestRejectedResult GetAlmBranchRequestRejectedDetail(int requestRejectedId)
        {
            AlmBranchRequestRejectedResult result = new AlmBranchRequestRejectedResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    AlmBranchRequestRejected abrrInDb = db.AlmBranchRequestRejected.Where(ab => ab.Id == requestRejectedId).FirstOrDefault();

                    if (abrrInDb == null)
                        throw new Exception("Solicitud no válida");

                    DataHelper.fill(result.data, abrrInDb);
                    DataHelper.fill(result.data.almStatusAux, abrrInDb.AlmStatus);
                    List<vwAlmBranchRequestRejectedTransaction> vwList = db.vwAlmBranchRequestRejectedTransaction.Where(v => v.AlmBranchRequestRejectedId == requestRejectedId).ToList();
                    foreach (var vw in vwList)
                    {
                        vwAlmBranchRequestRejectedTransactionAux aux = new vwAlmBranchRequestRejectedTransactionAux();
                        DataHelper.fill(aux, vw);
                        result.data.vwAlmBranchRequestRejectedTransactionAuxList.Add(aux);
                    }

                    DataHelper.fill(result.data.requestResponseAux, abrrInDb.AlmBranchRequestResponse);
                    DataHelper.fill(result.data.requestResponseAux.almBranchRequestAux, abrrInDb.AlmBranchRequestResponse.AlmBranchRequest);
                    DataHelper.fill(result.data.requestResponseAux.almBranchRequestAux.clinicAux, abrrInDb.AlmBranchRequestResponse.AlmBranchRequest.Clinic);

                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        #endregion

        #region AlmBranchRequestToDelivery        

        public static AlmBranchRequestToDeliveryResult GetWarehouseRequestToDeliveryDetail(int requestToDeliveryId)
        {
            AlmBranchRequestToDeliveryResult result = new AlmBranchRequestToDeliveryResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    AlmBranchRequestToDelivery requestToDeliveryInDb = db.AlmBranchRequestToDelivery.Where(request => request.Id == requestToDeliveryId).FirstOrDefault();

                    if (requestToDeliveryInDb == null)
                    {
                        throw new Exception("Solicitud no válida");
                    }

                    DataHelper.fill(result.data, requestToDeliveryInDb);
                    DataHelper.fill(result.data.almStatusAux, requestToDeliveryInDb.AlmStatus);
                    DataHelper.fill(result.data.requestConfirmedAux.requestResponseAux.almBranchRequestAux.clinicAux, requestToDeliveryInDb.AlmBranchRequestConfirmed.AlmBranchRequestResponse.AlmBranchRequest.Clinic);

                    List<vwAlmBranchRequestToDeliveryTransaction> vwTransactions = db.vwAlmBranchRequestToDeliveryTransaction.Where(vw => vw.AlmBranchRequestConfirmedId == requestToDeliveryInDb.AlmBranchRequestConfirmedId && vw.AlmBranchRequestToDeliveryId == requestToDeliveryId).OrderBy(vw => vw.ConceptId).ToList();

                    foreach (var transaction in vwTransactions)
                    {
                        vwAlmBranchRequestToDeliveryTransactionAux vwAux = new vwAlmBranchRequestToDeliveryTransactionAux();
                        DataHelper.fill(vwAux, transaction);
                        result.data.vwrequestToDeliveryTransactionsAux.Add(vwAux);

                    }

                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        public static vwAlmBranchRequestToDeliveryResult GetBranchRequestToDeliveryDT(DTParameterModel model, int clinicId)
        {
            vwAlmBranchRequestToDeliveryResult result = new vwAlmBranchRequestToDeliveryResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwAlmBranchRequestToDelivery> query = db.vwAlmBranchRequestToDelivery.Where(vw => vw.AlmStatusId == AlmStatusAux.Enviado && vw.ClinicId == clinicId);
                    foreach (DTColumn column in model.Columns)
                    {
                        bool columnHasValue = !string.IsNullOrEmpty(column.Search.Value);
                        switch (column.Data)
                        {
                            case "sFolio":
                                query = columnHasValue ? query.Where(br => br.Folio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "srequestFolio":
                                query = columnHasValue ? query.Where(br => br.RequestFolio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "sconfirmedFolio":
                                query = columnHasValue ? query.Where(br => br.ConfirmedFolio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "clinic":
                                query = columnHasValue ? query.Where(br => br.Clinic.Contains(column.Search.Value)) : query;
                                break;
                            default:
                                break;
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Order.First().Dir) && !string.IsNullOrEmpty(model.Order.First().Data))
                    {
                        string order = model.Order.First().Dir;
                        string orderColumn = model.Order.First().Data;

                        switch (orderColumn)
                        {
                            case "sFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Folio) : query.OrderByDescending(br => br.Folio);
                                break;
                            case "srequestFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.RequestFolio) : query.OrderByDescending(br => br.RequestFolio);
                                break;
                            case "sconfirmedFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.ConfirmedFolio) : query.OrderByDescending(br => br.ConfirmedFolio);
                                break;
                            case "clinic":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Clinic) : query.OrderByDescending(br => br.Clinic);
                                break;
                            case "sCreated":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Created) : query.OrderByDescending(br => br.Created);
                                break;
                            default:
                                break;
                        }
                    }

                    result.total.value = query.Count();
                    query = query.Skip(model.Start).Take(model.Length);
                    foreach (vwAlmBranchRequestToDelivery objDb in query)
                    {
                        vwAlmBranchRequestToDeliveryAux aux = new vwAlmBranchRequestToDeliveryAux();
                        DataHelper.fill(aux, objDb);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        #endregion

        #region AlmBranchRequestDelivered        

        public static AlmBranchRequestDeliveredResult GetWarehouseRequestDeliveredDetail(int requestDeliveredId)
        {
            AlmBranchRequestDeliveredResult result = new AlmBranchRequestDeliveredResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    AlmBranchRequestDelivered requestDeliveredInDb = db.AlmBranchRequestDelivered.Where(request => request.Id == requestDeliveredId).FirstOrDefault();

                    if (requestDeliveredInDb == null)
                        throw new Exception("Solicitud no válida");

                    DataHelper.fill(result.data, requestDeliveredInDb);
                    DataHelper.fill(result.data.requestToDeliveryAux, requestDeliveredInDb.AlmBranchRequestToDelivery);
                    DataHelper.fill(result.data.requestToDeliveryAux.requestConfirmedAux, requestDeliveredInDb.AlmBranchRequestToDelivery.AlmBranchRequestConfirmed);
                    DataHelper.fill(result.data.requestToDeliveryAux.requestConfirmedAux.requestResponseAux.almBranchRequestAux.clinicAux, requestDeliveredInDb.AlmBranchRequestToDelivery.AlmBranchRequestConfirmed.AlmBranchRequestResponse.AlmBranchRequest.Clinic);


                    List<vwAlmBranchRequestDeliveredTransaction> vwTransactions =
                        db.vwAlmBranchRequestDeliveredTransaction.Where(vw => vw.AlmBranchRequestDeliveredId == requestDeliveredInDb.Id).OrderBy(vw => vw.ConceptId).ToList();

                    foreach (var transaction in vwTransactions)
                    {
                        vwAlmBranchRequestDeliveredTransactionAux vwAux = new vwAlmBranchRequestDeliveredTransactionAux();
                        DataHelper.fill(vwAux, transaction);
                        result.data.vwrequestDeliveredTransactionsAux.Add(vwAux);
                    }

                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        public static vwAlmBranchRequestDeliveredResult GetBranchRequestDeliveredDT(DTParameterModel model, int clinicId)
        {
            vwAlmBranchRequestDeliveredResult result = new vwAlmBranchRequestDeliveredResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwAlmBranchRequestDelivered> query = db.vwAlmBranchRequestDelivered.Where(vw => vw.AlmStatusId == AlmStatusAux.Enviado && vw.ClinicId == clinicId);
                    foreach (DTColumn column in model.Columns)
                    {
                        bool columnHasValue = !string.IsNullOrEmpty(column.Search.Value);
                        switch (column.Data)
                        {
                            case "sFolio":
                                query = columnHasValue ? query.Where(br => br.Folio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "srequestFolio":
                                query = columnHasValue ? query.Where(br => br.RequestFolio.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "clinic":
                                query = columnHasValue ? query.Where(br => br.Clinic.Contains(column.Search.Value)) : query;
                                break;
                            default:
                                break;
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Order.First().Dir) && !string.IsNullOrEmpty(model.Order.First().Data))
                    {
                        string order = model.Order.First().Dir;
                        string orderColumn = model.Order.First().Data;

                        switch (orderColumn)
                        {
                            case "sFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Folio) : query.OrderByDescending(br => br.Folio);
                                break;
                            case "srequestFolio":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.RequestFolio) : query.OrderByDescending(br => br.RequestFolio);
                                break;
                            case "clinic":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Clinic) : query.OrderByDescending(br => br.Clinic);
                                break;
                            case "sCreated":
                                query = order.ToUpper() == "ASC" ? query.OrderBy(br => br.Created) : query.OrderByDescending(br => br.Created);
                                break;
                            default:
                                break;
                        }
                    }

                    result.total.value = query.Count();
                    query = query.Skip(model.Start).Take(model.Length);
                    foreach (vwAlmBranchRequestDelivered objDb in query)
                    {
                        vwAlmBranchRequestDeliveredAux aux = new vwAlmBranchRequestDeliveredAux();
                        DataHelper.fill(aux, objDb);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado {result.exception_message}";
                }
            }
            return result;
        }

        #endregion        

        #region WarehouseRequest
        public static Result SaveWarehouseRequest(ConceptAux conceptAux, int currentClinic)
        {
            Result res = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbtransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        AlmFolio almFolio = db.AlmFolio.First();
                        AlmFolioAux almFolioAux = new AlmFolioAux();
                        DataHelper.fill(almFolioAux, almFolio);

                        AlmBranchRequest almBranchRequest = db.AlmBranchRequest.Create();
                        almBranchRequest.Folio = almFolioAux.BranchRequestFolio;
                        almBranchRequest.ClinicId = currentClinic;
                        almBranchRequest.AlmStatusId = AlmStatusAux.Enviado;
                        almBranchRequest.Seen = false;
                        almBranchRequest.Created = DateTime.UtcNow;
                        almBranchRequest.Updated = DateTime.UtcNow;
                        db.AlmBranchRequest.Add(almBranchRequest);
                        db.SaveChanges();

                        foreach (var concept in conceptAux.WarehouseConcept)
                        {
                            AlmBranchRequestTransaction almBranchRequestTransaction = db.AlmBranchRequestTransaction.Create();
                            almBranchRequestTransaction.AlmBranchRequestId = almBranchRequest.Id;
                            almBranchRequestTransaction.ConceptId = concept.id;
                            almBranchRequestTransaction.Quantity = concept.dStock;
                            almBranchRequestTransaction.Active = true;
                            almBranchRequestTransaction.Created = DateTime.UtcNow;
                            almBranchRequestTransaction.Updated = DateTime.UtcNow;
                            db.AlmBranchRequestTransaction.Add(almBranchRequestTransaction);
                        }

                        db.SaveChanges();

                        almFolio.BranchRequestFolio++;
                        db.SaveChanges();

                        dbtransaction.Commit();
                        res.success = true;
                    }
                    catch (Exception e)
                    {
                        dbtransaction.Rollback();
                        res.success = false;
                        res.exception = e;
                        res.message = $"Ocurrió un error inesperado {res.exception_message}";
                    }
                }

            }
            return res;
        }
        public static WarehouseRequestResult GetWarehouseRequestCounter()
        {
            WarehouseRequestResult result = new WarehouseRequestResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    WarehouseRequestAux WarehouseRequestCounter = new WarehouseRequestAux()
                    {
                        RequestSentFromWarehouseCounter = 0,
                        RequestReceivedFromWarehouseCounter = 0,
                        RequestConfirmedFromWarehouseCounter = 0,
                        RequestInDeliveryFromWarehouseCounter = 0
                    };

                    WarehouseRequestCounter.RequestSentFromWarehouseCounter =
                        db.AlmBranchRequest.Where(abr => abr.AlmStatusId == AlmStatusAux.Enviado && !abr.Seen).Count();

                    WarehouseRequestCounter.RequestReceivedFromWarehouseCounter =
                        db.AlmBranchRequest.Where(abr => abr.AlmStatusId == AlmStatusAux.Enviado && abr.Seen).Count();


                    result.data = WarehouseRequestCounter;
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado { result.exception_message }";
                }
            }
            return result;
        }

        public static AlmBranchRequestToDeliveryResult SaveAlmBranchRequestToDelivery(AlmBranchRequestToDeliveryAux requestToDelivery, int currentClinic, int currentUserId)
        {
            AlmBranchRequestToDeliveryResult result = new AlmBranchRequestToDeliveryResult();
            bool deliveryCompleted = false;

            NumericResult taxRes = ParameterHelper.GetTax();

            if (!taxRes.success)
            {
                result.success = false;
                result.message = "Ocurrió un error al obtener el Iva configurado.";
                return result;
            }

            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    if (requestToDelivery.IsValidRequest())
                    {
                        try
                        {
                            // Crear una transacción entregada
                            AlmBranchRequestDelivered requestDeliveredInDb = db.AlmBranchRequestDelivered.Create();
                            AlmFolio almFolioInDb = db.AlmFolio.First();
                            AlmBranchRequestToDelivery requestToDeliveryInDb = db.AlmBranchRequestToDelivery.Where(request => request.Id == requestToDelivery.id).FirstOrDefault();
                            AlmBranchRequestConfirmed requestConfirmedInDb = db.AlmBranchRequestConfirmed.Where(request => request.Id == requestToDeliveryInDb.AlmBranchRequestConfirmedId).FirstOrDefault();
                            AlmBranchRequestResponse requestResponseInDb = db.AlmBranchRequestResponse.Where(request => request.Id == requestConfirmedInDb.AlmBranchRequestResponseId).FirstOrDefault();
                            AlmBranchRequest requestInDb = db.AlmBranchRequest.Where(request => request.Id == requestResponseInDb.AlmBranchRequestId).FirstOrDefault();


                            requestDeliveredInDb.Folio = almFolioInDb.BranchRequestDeliveredFolio;
                            requestDeliveredInDb.AlmBranchRequestToDeliveryId = requestToDelivery.id;
                            requestDeliveredInDb.AlmStatusId = AlmStatusAux.Enviado;
                            requestDeliveredInDb.Seen = false;
                            requestDeliveredInDb.Comments = requestToDelivery.comments;
                            requestDeliveredInDb.Created = DateTime.UtcNow;
                            requestDeliveredInDb.Updated = DateTime.UtcNow;

                            db.AlmBranchRequestDelivered.Add(requestDeliveredInDb);

                            db.SaveChanges();

                            IReturn dbReturn = new IReturn();

                            if (requestToDelivery.vwrequestToDeliveryTransactionsAux.Sum(t => t.toReturn) > 0)
                            {
                                dbReturn = db.IReturn.Create();
                                dbReturn.AlmBranchRequestToDeliveryId = requestToDelivery.id;
                                dbReturn.ClinicId = currentClinic;
                                dbReturn.Confirmed = false;
                                dbReturn.Created = DateTime.UtcNow;
                                dbReturn.Reason = "Devolución de envío";
                                dbReturn.Updated = DateTime.UtcNow;
                                dbReturn.CreatedId = currentUserId;
                                dbReturn.UpdatedId = currentUserId;
                            }

                            //Crea el registro de Invoice Padre

                            Invoice dbInvoice = db.Invoice.Create();
                            dbInvoice.Code = "Almacen-" + requestDeliveredInDb.Folio.ToString("D7");
                            dbInvoice.ClinicId = currentClinic;
                            dbInvoice.Created = DateTime.UtcNow;
                            dbInvoice.CreatedBy = currentUserId;
                            dbInvoice.DeliveredId = requestDeliveredInDb.Id;
                            dbInvoice.IsCanceled = false;

                            // Crear partidas de la transacción entregada

                            foreach (var transaction in requestToDelivery.vwrequestToDeliveryTransactionsAux)
                            {
                                AlmBranchRequestDeliveredTransaction transactionInDb = db.AlmBranchRequestDeliveredTransaction.Create();
                                transactionInDb.AlmBranchRequestDeliveredId = requestDeliveredInDb.Id;
                                transactionInDb.AlmBranchRequestToDeliveryTransactionId = transaction.almBranchRequestToDeliveryTransactionId;
                                transactionInDb.ConceptId = transaction.conceptId;
                                transactionInDb.Sended = transaction.sended;
                                transactionInDb.Price = transaction.price;
                                transactionInDb.MinPrice = transaction.minPrice;
                                transactionInDb.Created = DateTime.UtcNow;
                                transactionInDb.Updated = DateTime.UtcNow;

                                db.AlmBranchRequestDeliveredTransaction.Add(transactionInDb);

                                // Actualizar costo
                                var concept = db.Concept.Where(c => c.id == transaction.conceptId).FirstOrDefault();
                                var almStock = db.AlmStock.FirstOrDefault(s => s.Id == transaction.almStockId);
                                if (concept != null && almStock != null)
                                {
                                    concept.Cost = transaction.price;
                                    concept.Price = almStock.MaxPrice;
                                }

                                if (transaction.sended > 0)
                                {
                                    // Generar un stockAux con el costo del producto
                                    Stock stockInDb = db.Stock.Create();

                                    stockInDb.ConceptId = transaction.conceptId;
                                    stockInDb.InStock = (int)transaction.sended;
                                    if (concept.Iva)
                                    {
                                        stockInDb.Cost = transaction.price * .16m;
                                    }
                                    else
                                    {
                                        stockInDb.Cost = transaction.price;
                                    }

                                    stockInDb.ClinicId = currentClinic;
                                    stockInDb.Iva = transaction.iva;
                                    stockInDb.CurrIva = 16;
                                    stockInDb.Created = DateTime.UtcNow;
                                    stockInDb.Batch = transaction.batch;
                                    stockInDb.ExpiredDate = transaction.expiredDate;
                                    stockInDb.AlmStockId = transaction.almStockId;

                                    db.Stock.Add(stockInDb);

                                    db.SaveChanges();

                                    //Se crea un registro de la transacción en facturas
                                    InvoiceMovement invoicemovDb = db.InvoiceMovement.Create();
                                    invoicemovDb.ConceptId = transaction.conceptId;
                                    invoicemovDb.Quantity = (int)transaction.sended;
                                    invoicemovDb.Cost = transaction.price;
                                    invoicemovDb.Iva = concept.Iva;
                                    invoicemovDb.Price = transaction.maxPrice;
                                    invoicemovDb.CurrentIva = taxRes.IntValue;
                                    invoicemovDb.DeliveredTransactionId = transactionInDb.Id;
                                    invoicemovDb.StockId = stockInDb.id;
                                    dbInvoice.InvoiceMovement.Add(invoicemovDb);
                                }
                                if (transaction.toReturn > 0)
                                {
                                    IReturnProducts returnProduct = db.IReturnProducts.Create();
                                    returnProduct.AlmBranchRequestToDeliveryTransactionId = transaction.almBranchRequestToDeliveryTransactionId;
                                    returnProduct.ConceptId = transaction.conceptId;
                                    returnProduct.Created = DateTime.UtcNow;
                                    returnProduct.Quantity = (int)transaction.toReturn;
                                    returnProduct.Reason = "Devolución de envío";
                                    returnProduct.AlmStockId = transaction.almStockId;
                                    dbReturn.IReturnProducts.Add(returnProduct);
                                }

                                db.SaveChanges();

                                // Marcar como entregada la transacción                                
                                AlmBranchRequestToDeliveryTransaction requestToDeliveryTransactionInDb = db.AlmBranchRequestToDeliveryTransaction.Where(vw => vw.Id == transaction.almBranchRequestToDeliveryTransactionId).FirstOrDefault();
                                requestToDeliveryTransactionInDb.Delivered = true;

                            }
                            if (dbReturn.ClinicId > 0)
                            {
                                db.IReturn.Add(dbReturn);
                            }

                            db.Invoice.Add(dbInvoice);

                            db.SaveChanges();

                            almFolioInDb.BranchRequestDeliveredFolio++;

                            requestToDeliveryInDb.Delivered = db.AlmBranchRequestToDeliveryTransaction.Where(vws => vws.AlmBranchRequestToDeliveryId == requestToDeliveryInDb.Id).All(vws => vws.Delivered);
                            deliveryCompleted = db.AlmBranchRequestConfirmedTransaction.Where(vw => vw.AlmBranchRequestConfirmedId == requestConfirmedInDb.Id).Any(vw => !vw.ToDelivery) && db.AlmBranchRequestToDeliveryTransaction.Where(vws => vws.AlmBranchRequestConfirmedId == requestConfirmedInDb.Id).Any(vws => vws.Delivered);

                            requestToDeliveryInDb.AlmStatusId = AlmStatusAux.Entregado;

                            //if (deliveryCompleted)
                            //{
                            //    requestConfirmedInDb.AlmStatusId = AlmStatusAux.Entregado;
                            //    requestResponseInDb.AlmStatusId = AlmStatusAux.Entregado;
                            //    requestInDb.AlmStatusId = AlmStatusAux.Entregado;
                            //}

                            db.SaveChanges();
                            dbTransaction.Commit();
                            result.success = true;
                        }
                        catch (Exception e)
                        {
                            dbTransaction.Rollback();
                            result.success = false;
                            result.exception = e;
                            result.message = $"Ocurrió un error inesperado { result.exception_message }";
                        }
                    }
                    else
                    {
                        throw new Exception("Transacción no válida");
                    }
                }
            }
            return result;
        }

        public static PredictedProductRequestResult GetPredictedWarehouseRequest(int clinicId)
        {
            PredictedProductRequestResult result = new PredictedProductRequestResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    //Obtiene la ultima solicitud enviada y recibida en la clínica
                    vwAlmBranchRequestDelivered dbDelivery = db.vwAlmBranchRequestDelivered.Where(d => d.ClinicId == clinicId && d.Estatus == "Entregado").OrderByDescending(d => d.Id).FirstOrDefault();
                    vwAlmBranchRequestDeliveredAux aux = new vwAlmBranchRequestDeliveredAux();
                    if (dbDelivery != null)
                    {
                        DataHelper.fill(aux, dbDelivery);
                    }
                    DateTime? fechaInicial = dbDelivery != null ? dbDelivery.Updated : DateTime.UtcNow.AddDays(-7).Date;

                    if (fechaInicial.HasValue)
                    {
                        //TODO: Si merma se agrega tambien se ocuparía agregar al storeprocedure

                        List<PredictedProductRequestAux> list = new List<PredictedProductRequestAux>();

                        using (var conn = new SqlConnection(db.Database.Connection.ConnectionString))
                        {
                            using (var command = new SqlCommand("sp_calc_warehouserequest", conn) { CommandType = CommandType.StoredProcedure })
                            {
                                conn.Open();
                                command.Parameters.Add(new SqlParameter("referenceDate",fechaInicial.Value));
                                command.Parameters.Add(new SqlParameter("clinicId", clinicId));

                                using (SqlDataReader rdr = command.ExecuteReader())
                                {
                                    while (rdr.Read())
                                    {
                                        list.Add(new PredictedProductRequestAux()
                                        {
                                            code = rdr["code"].ToString(),
                                            id = Convert.ToInt32(rdr["conceptId"]),
                                            name = rdr["name"].ToString(),
                                            sales = Convert.ToInt32(rdr["qty"]),
                                            stock = Convert.ToInt32(rdr["inStock"])
                                        });
                                    }
                                }
                            }
                        }
                        if (list.Count() > 0)
                        {
                            result.data_list.AddRange(list);
                            result.success = true;
                        }
                        else
                        {
                            result.success = false;
                            result.message = string.Format("No se encontraron movimientos que requieran una solicitud nueva desde la última solicitud entregada({0}) ni en los últimos 7 días.", aux.sUpdated);
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "No existe una solicitud anterior para calcular una nueva solicitud.";
                    }

                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurrió un error inesperado { result.exception_message }";
                }
            }
            return result;
        }

        #endregion
    }
}