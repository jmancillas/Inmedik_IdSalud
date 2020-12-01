using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class WithdrawalAux
    {
        public int id { get; set; }
        public int number { get; set; }
        public int clinicId { get; set; }
        public int cashClosingId { get; set; }
        public decimal total { get; set; }
        public string comment { get; set; }
        public DateTime? created { get; set; }
        public string sCreated
        {
            get
            {
                return created.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(created.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                )
                .ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }
        public DateTime? updated { get; set; }
        public string sUpdated
        {
            get
            {
                return updated.HasValue ?
                TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(updated.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                )
                .ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX"))
                : "";
            }
        }
        public List<DenominationWithdrawalAux> denominationWithrawalAux { get; set; }
        public WithdrawalAux()
        {
            this.denominationWithrawalAux = new List<DenominationWithdrawalAux>();
        }
    }
    public class WithdrawalResult : Result
    {
        public WithdrawalAux data { get; set; }
        public List<WithdrawalAux> data_list { get; set; }
        public NumericResult total { get; set; }
        public WithdrawalResult()
        {
            this.data = new WithdrawalAux();
            this.data_list = new List<WithdrawalAux>();
            this.total = new NumericResult();
        }
    }
    public class DenominationWithdrawalAux
    {
        public int idWithdrawal { get; set; }
        public int idDenomination { get; set; }
        public decimal Quantity { get; set; }
        public DenominationAux denominationAux { get; set; }
        public DenominationWithdrawalAux()
        {
            denominationAux = new DenominationAux();

        }
    }
    public class DenominationWithdrawalResult : Result
    {
        public DenominationWithdrawalAux data { get; set; }
        public List<DenominationWithdrawalAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public DenominationWithdrawalResult()
        {
            this.data = new DenominationWithdrawalAux();
            this.data_list = new List<DenominationWithdrawalAux>();
            this.total = new NumericResult();
        }
    }
    public class WithdrawalHelper
    {

        public static GenericResult AtWithdrawalLimit(int userId, int clinicId)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    decimal sumaPagos = 0;
                    decimal total = 0;
                    //Busca si existe un turno abierto para el empleado en la clínica
                    CashClosing dbCashClosing = db.CashClosing.Where(c => c.ClinicId == clinicId && c.UserIdWhoOpened == userId && !c.UserIdWhoClosed.HasValue).FirstOrDefault();
                    //Existe un turno abierto
                    if(dbCashClosing != null)
                    {
                        /*obtenemos todos los pagos en efectivo no cancelados realizados durante el periodo que dura ese corte de caja*/
                        var dbPagosCorte = db.Payment.Where(p => !p.Orders.DateCanceled.HasValue && p.Created >= dbCashClosing.DateOpened && p.ClinicId == clinicId && p.Employee.UserId == userId && p.PaymentType.Name == "Efectivo");
                        //obtenemos todos los retiros relacionados al turno
                        var dbRetiros = dbCashClosing.Withdrawal.ToList();

                        sumaPagos = dbPagosCorte.Count() > 0 ? dbPagosCorte.Sum(e => e.Amount) : 0;
                        total = sumaPagos - (dbRetiros.Count() > 0 ? dbRetiros.Sum(w => w.Total) : 0);

                        ClinicResult clinic = ClinicHelper.GetClinic(clinicId);
                        if(total >= clinic.data.withdrawalAt)
                        {
                            result.bool_value = true;
                        }
                        else
                        {
                            result.bool_value = false;
                        }
                    }
                    else //No existe turno abierto
                    {
                        result.bool_value = false;
                    }
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.bool_value = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }
        public static WithdrawalResult GetWithdrawalByCashClosing(int cashClosingId)
        {
            WithdrawalResult result = new WithdrawalResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    List<Withdrawal> dbWiths = db.Withdrawal.Where(w => w.CashClosingId == cashClosingId).ToList();
                    foreach (Withdrawal dbWith in dbWiths)
                    {
                        WithdrawalAux temp = new WithdrawalAux();
                        DataHelper.fill(temp, dbWith);
                        foreach (DenominationWithdrawal dbDenomWith in dbWith.DenominationWithdrawal)
                        {
                            DenominationWithdrawalAux tempDenom = new DenominationWithdrawalAux();
                            DataHelper.fill(tempDenom, dbDenomWith);
                            DataHelper.fill(tempDenom.denominationAux, db.Denomination.FirstOrDefault(d => d.id == dbDenomWith.idDenomination));
                            result.data.denominationWithrawalAux.Add(tempDenom);
                        }
                        result.data_list.Add(temp);
                    }
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }
        public static WithdrawalResult GetWithdrawalById(int id)
        {
            WithdrawalResult result = new WithdrawalResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Withdrawal dbWith = db.Withdrawal.Where(w => w.id == id).FirstOrDefault();
                    if (dbWith != null)
                    {
                        DataHelper.fill(result.data, dbWith);
                        foreach (DenominationWithdrawal dbDenomWith in dbWith.DenominationWithdrawal)
                        {
                            DenominationWithdrawalAux temp = new DenominationWithdrawalAux();
                            DataHelper.fill(temp, dbDenomWith);
                            DataHelper.fill(temp.denominationAux, db.Denomination.FirstOrDefault(d => d.id == dbDenomWith.idDenomination));
                            result.data.denominationWithrawalAux.Add(temp);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Descuento no encontrado.";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        public static WithdrawalResult SaveWithdrawal(WithdrawalAux withdrawal)
        {
            WithdrawalResult result = new WithdrawalResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Withdrawal dbWith = new Withdrawal();
                    dbWith.ClinicId = withdrawal.clinicId;
                    dbWith.CashClosingId = withdrawal.cashClosingId;
                    dbWith.Total = withdrawal.total;
                    dbWith.Comment = withdrawal.comment;

                    /*guardamos las denominaciones en la base de datos*/
                    foreach (var item in withdrawal.denominationWithrawalAux.Where(d => d.Quantity > 0))
                    {
                        DenominationWithdrawal dbcc = new DenominationWithdrawal();
                        dbcc.idDenomination = item.idDenomination;
                        dbcc.Quantity = item.Quantity;
                        dbWith.DenominationWithdrawal.Add(dbcc);
                    }

                    db.Withdrawal.Add(dbWith);
                    db.SaveChanges();
                    db.Entry<Withdrawal>(dbWith).Reload();
                    DataHelper.fill(result.data, dbWith);
                    foreach (DenominationWithdrawal dbDenomWith in dbWith.DenominationWithdrawal)
                    {
                        DenominationWithdrawalAux tempDenom = new DenominationWithdrawalAux();
                        DataHelper.fill(tempDenom, dbDenomWith);
                        DataHelper.fill(tempDenom.denominationAux, db.Denomination.FirstOrDefault(d => d.id == dbDenomWith.idDenomination));
                        result.data.denominationWithrawalAux.Add(tempDenom);
                    }
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }
    }
}