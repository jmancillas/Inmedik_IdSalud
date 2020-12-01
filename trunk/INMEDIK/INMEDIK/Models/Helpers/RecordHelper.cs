using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using INMEDIK.Common;
using System.Data.Entity;

namespace INMEDIK.Models.Helpers
{
    public class RecordInvoiceAux
    {
        public int recordInvoiceId { get; set; }
        public int invoiceId { get; set; }
        public string code { get; set; }
        public ClinicAux clinicAux { get; set; }
        public int version { get; set; }
        public string stringVersion { get; set; }
        public DateTime created { get; set; }
        public UserAux userAux { get; set; }
        public int createdBy { get; set; }
        public string stringCreatedBy { get; set; }
        public List<RecordInvoiceTransactionAux> recordTransactionAux { get; set; }
        public string created_string
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(created.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }

        public RecordInvoiceAux()
        {
            recordTransactionAux = new List<RecordInvoiceTransactionAux>();
            clinicAux = new ClinicAux();
            userAux = new UserAux();
        }
    }

    public class RecordInvoiceResult : Result
    {
        public RecordInvoiceAux data { get; set; }
        public List<RecordInvoiceAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public RecordInvoiceResult()
        {
            this.data = new RecordInvoiceAux();
            this.data_list = new List<RecordInvoiceAux>();
            this.total = new NumericResult();
        }
    }

    public class RecordInvoiceTransactionAux
    {
        public int recordTransactionInvoiceId { get; set; }
        public int recordInvoiceId { get; set; }
        public int transactionInvoiceId { get; set; }
        public ConceptAux conceptAux { get; set; }
        public int quantity { get; set; }
        public bool iva { get; set; }
        public string stringIva { get { return iva ? "SI" : "NO"; } }
        public decimal cost { get; set; }
        public decimal price { get; set; }
        public string batch { get; set; }
        public DateTime? expiredDate { get; set; }
        public string typeOfModification { get; set; }

        public string expiredDate_string
        {
            get
            {
                string date;
                if (expiredDate == null)
                {
                    date = "Sin caducidad";
                }
                else
                {
                    date = DateTimeHelper.GetDateTimeFromDateTimeUTC((DateTime)expiredDate).ToString("dd/MM/yyyy", new CultureInfo("es-MX"));
                }
                return date;
            }
        }

        public RecordInvoiceTransactionAux()
        {
            this.conceptAux = new ConceptAux();
        }
    }

    public class RecordInvoiceHelper
    {
        public static RecordInvoiceResult GetRecordInvoiceVersion(int invoiceId)
        {
            RecordInvoiceResult result = new RecordInvoiceResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    List<RecordInvoice> RecordInvoiceDB = db.RecordInvoice.Where(r => r.InvoiceId == invoiceId).ToList();
                    if (RecordInvoiceDB.Count != 0)
                    {
                        RecordInvoiceDB.OrderBy(r => r.Version);
                        int lastVersion = RecordInvoiceDB.Max(r => r.Version);
                        foreach(RecordInvoice recordInv in RecordInvoiceDB)
                        {
                            RecordInvoiceAux recordInvoiceAux = new RecordInvoiceAux();
                            DataHelper.fill(recordInvoiceAux, recordInv);
                            DataHelper.fill(recordInvoiceAux.clinicAux, recordInv.Clinic);
                            
                            if(recordInv.Version == 1)
                            {
                                recordInvoiceAux.stringVersion = "Primera modificación";
                            }
                            else if(recordInv.Version == lastVersion)
                            {
                                recordInvoiceAux.stringVersion = "Última modificación";
                            }
                            else
                            {
                                recordInvoiceAux.stringVersion = "Modificación " + recordInv.Version;
                            }

                            Employee employeeDB = db.Employee.Where(e => e.UserId == recordInv.CreatedBy).FirstOrDefault();
                            if(employeeDB != null)
                            {
                                Person personDB = db.Person.Where(p => p.id == employeeDB.PersonId).FirstOrDefault();
                                if(personDB != null)
                                {
                                    recordInvoiceAux.stringCreatedBy = personDB.Name + " " + personDB.LastName;
                                }
                                else
                                {
                                    recordInvoiceAux.stringCreatedBy = recordInv.User.UserAccount;
                                }
                            }
                            else
                            {
                                recordInvoiceAux.stringCreatedBy = recordInv.User.UserAccount;
                            }

                            List<RecordTransactionInvoice> recordInvoiceTransactionList = db.RecordTransactionInvoice.Where(t => t.RecordInvoiceId == recordInv.RecordInvoiceId).ToList();

                            if(recordInvoiceTransactionList.Count != 0)
                            {
                                recordInvoiceTransactionList.OrderByDescending(t => t.TypeOfModification);
                                foreach(RecordTransactionInvoice transaction in recordInvoiceTransactionList)
                                {
                                    RecordInvoiceTransactionAux recordInvoiceTransactionAux = new RecordInvoiceTransactionAux();
                                    DataHelper.fill(recordInvoiceTransactionAux, transaction);
                                    DataHelper.fill(recordInvoiceTransactionAux.conceptAux, transaction.Concept);
                                    recordInvoiceAux.recordTransactionAux.Add(recordInvoiceTransactionAux);
                                }
                            }
                            
                            result.data_list.Add(recordInvoiceAux);
                        }
                        result.success = true;
                        result.message = "Datos cargados con éxito";
                    }
                    else
                    {
                        throw new Exception("Esta factura no contiene ediciones");
                    }
                }
                catch (Exception e)
                {
                    result.exception = e;
                    result.message = e.Message;
                    result.success = false;
                }
            }
            return result;
        }
    }
}