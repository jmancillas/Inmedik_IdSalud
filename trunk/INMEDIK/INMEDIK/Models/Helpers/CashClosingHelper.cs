using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class CashClosingAux
    {
        public int id { get; set; }
        public int ClinicId { get; set; }
        public int? number { get; set; }
        public ClinicAux clinicAux { get; set; }
        public decimal InitialCash { get; set; }
        public decimal? FinalCash { get; set; }
        public decimal? RemainingOrMissing { get; set; }
        public DateTime DateOpened { get; set; }
        public int UserIdWhoOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public int? UserIdWhoClosed { get; set; }
        public decimal? TotalCash { get; set; }
        public decimal? TotalCrediCard { get; set; }
        public decimal? TotalVoucher { get; set; }
        public decimal? TotalExpense { get; set; }
        public decimal? TotalCancelation { get; set; }
        public decimal? TotalWithdrawal { get; set; }
        public decimal? TotalSell { get; set; }
        public int? TotalConsult { get; set; }
        public decimal? TotalProductSell { get; set; }
        public PersonAux PersonAux { get; set; }
        public List<WithdrawalAux> withdrawalAux { get; set; }
        public List<DenominationByCashCloseAux> denominationByCashCloseAux { get; set; }
        public List<ExpensesAux> expensesAux { get; set; }
        public List<OrderAux> OrdersCanceled { get; set; }
        public string sDateOpened
        {
            get
            {
                return
                    TimeZoneInfo.ConvertTimeFromUtc(
                    new DateTime(DateOpened.Ticks, DateTimeKind.Utc),
                    TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                    ).
                ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public string sDateClosed
        {
            get
            {
                return DateClosed.HasValue ?
                    TimeZoneInfo.ConvertTimeFromUtc(
                    new DateTime(DateClosed.Value.Ticks, DateTimeKind.Utc),
                    TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                    )
                .ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"))
                    : "";
            }
        }
        public string AvgTicket
        {
            get
            {
                string AvgTicketStr = string.Empty;

                if (TotalConsult.HasValue && TotalConsult.Value > 0)
                {
                    if (TotalProductSell.HasValue && TotalProductSell.Value > 0)
                    {
                        AvgTicketStr = Math.Round((TotalProductSell.Value / TotalConsult.Value), 2).ToString();
                    }
                    else
                    {
                        AvgTicketStr = "Sin venta de productos durante este corte.";
                    }
                }
                else
                {
                    AvgTicketStr = "Sin venta de consultas durante este corte.";
                }
                return AvgTicketStr;
            }
        }
        public CashClosingAux()
        {
            OrdersCanceled = new List<OrderAux>();
            PersonAux = new PersonAux();
            clinicAux = new ClinicAux();
            expensesAux = new List<ExpensesAux>();
            denominationByCashCloseAux = new List<DenominationByCashCloseAux>();
            withdrawalAux = new List<WithdrawalAux>();
        }

    }
    public class CashClosingResult : Result
    {
        public CashClosingAux data { get; set; }
        public List<CashClosingAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public CashClosingResult()
        {
            this.data = new CashClosingAux();
            this.data_list = new List<CashClosingAux>();
            this.total = new NumericResult();
        }
    }

    public class DenominationByCashCloseAux
    {
        public int idCashClosing { get; set; }
        public int idDenomination { get; set; }
        public decimal Quantity { get; set; }
        public DenominationAux denominationAux { get; set; }
        public DenominationByCashCloseAux()
        {
            denominationAux = new DenominationAux();

        }

    }
    public class DenominationByCashResult : Result
    {
        public DenominationByCashCloseAux data { get; set; }
        public List<DenominationByCashCloseAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public DenominationByCashResult()
        {
            this.data = new DenominationByCashCloseAux();
            this.data_list = new List<DenominationByCashCloseAux>();
            this.total = new NumericResult();
        }
    }

    public class DenominationAux
    {
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string MoneyCode { get; set; }
        public bool Active { get; set; }

        public DenominationAux()
        {
        }

    }
    public class DenominationResult : Result
    {
        public DenominationAux data { get; set; }
        public List<DenominationAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public DenominationResult()
        {
            this.data = new DenominationAux();
            this.data_list = new List<DenominationAux>();
            this.total = new NumericResult();
        }
    }

    public class CashClosingHelper
    {
        public static GenericResult ThereIsInitialCash(int clinicId, int userId)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    /*obtenemos el registro mas reciente de corde de caja en esa sucursal*/
                    var ultimoCorteDb = db.CashClosing.Where(c => c.ClinicId == clinicId && c.UserIdWhoOpened == userId
                                        && c.id == db.CashClosing.Where(c1 => c1.ClinicId == clinicId && c1.UserIdWhoOpened == userId).Max(c1 => c1.id)
                                        ).FirstOrDefault();

                    /*revizamos que no tenga fecha de corte*/
                    if (ultimoCorteDb != null && ultimoCorteDb.FinalCash == null)
                    {
                        result.bool_value = true;
                    }
                    else
                    {
                        result.bool_value = false;
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
        public static GenericResult SaveInitialCash(CashClosingAux Cash)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    /*Revizaos que no exista ya un saldo inicial SIN saldo final*/
                    var yaIniciado = ThereIsInitialCash(Cash.ClinicId, Cash.UserIdWhoOpened);

                    /*si es true quiere decir que si tiene saldo inicial por lo tanto no podemos volver a poner otro saldo inicial*/
                    if (yaIniciado.bool_value)
                    {
                        result.success = false;
                        result.message = "Ya existe un saldo inicial sin corte de caja.";
                    }
                    else
                    {
                        CashClosing newCash = new CashClosing();
                        newCash.ClinicId = Cash.ClinicId;
                        newCash.InitialCash = Cash.InitialCash;
                        newCash.DateOpened = DateTime.UtcNow;
                        newCash.UserIdWhoOpened = Cash.UserIdWhoOpened;
                        db.CashClosing.Add(newCash);
                        db.SaveChanges();
                        result.success = true;
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
        public static CashClosingResult SaveCashClosing(CashClosingAux Cash)
        {
            CashClosingResult result = new CashClosingResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    /*Revizaos que si existe un saldo inicial SIN saldo final*/
                    var yaIniciado = ThereIsInitialCash(Cash.ClinicId, Cash.UserIdWhoClosed.Value);

                    /*si es true quiere decir que si tiene saldo inicial SIN saldo final por lo tanto podemos continuar con el cierre de caja*/
                    if (yaIniciado.bool_value)
                    {
                        /*Sumamos las cantidades de las denominaciones*/
                        decimal totalLogico = 0;
                        Dictionary<string, decimal> cantitadPorTipo = new Dictionary<string, decimal>();
                        foreach (var item in Cash.denominationByCashCloseAux.Where(d => d.Quantity > 0))
                        {
                            if (item.Quantity > 0)
                            {
                                var denominationDb = db.Denomination.Where(d => d.id == item.idDenomination).FirstOrDefault();
                                totalLogico += item.Quantity * denominationDb.Value;

                                if (!cantitadPorTipo.ContainsKey(denominationDb.PaymentType.Name))
                                {
                                    cantitadPorTipo.Add(denominationDb.PaymentType.Name, 0);
                                }
                                cantitadPorTipo[denominationDb.PaymentType.Name] += item.Quantity * denominationDb.Value;
                            }
                        }
                        /*sumamos tambien las facturas ingresadas para obtener el total logico contado en el corte de caja*/
                        if (Cash.expensesAux.Count() > 0)
                        {
                            totalLogico += Cash.expensesAux.Sum(e => e.Amount);
                            cantitadPorTipo.Add("TotalExpense", Cash.expensesAux.Sum(e => e.Amount));
                        }

                        /*Verificamos que la suma de las facturas y las cantidades de las denominacion ingresadas sea igual al total ingresado*/
                        if (totalLogico == Cash.FinalCash)
                        {
                            CashClosing CashDb = db.CashClosing.Where(
                                c => c.ClinicId == Cash.ClinicId
                                && c.UserIdWhoOpened == Cash.UserIdWhoClosed
                                && c.id == db.CashClosing.Where(c1 => c1.ClinicId == Cash.ClinicId && c1.UserIdWhoOpened == Cash.UserIdWhoClosed).Max(c1 => c1.id)
                                ).FirstOrDefault();

                            /*obtenemos todos los pagos realizados durante el periodo que dura ese corte de caja*/
                            var dbPagosCorte = db.Payment.Where(p => p.Created >= CashDb.DateOpened && p.ClinicId == Cash.ClinicId && p.Employee.UserId == Cash.UserIdWhoClosed);
                            decimal sumaPagos = 0;
                            if (dbPagosCorte.Count() > 0)
                            {
                                sumaPagos = dbPagosCorte.Sum(e => e.Amount + e.Commission);
                                cantitadPorTipo.Add("TotalSell", dbPagosCorte.Sum(e => e.Amount + e.Commission));
                            }

                            /*obtenemos todos los pagos de ordenes canceladas durante el periodo que dura ese corte de caja*/
                            var dbPagosCancelados = db.Payment.Where(p => p.Orders.DateCanceled >= CashDb.DateOpened && p.Orders.ClinicId == Cash.ClinicId && p.Orders.Employee1.UserId == Cash.UserIdWhoClosed);
                            decimal sumaPagosCancelaciones = 0;
                            if (dbPagosCancelados.Count() > 0)
                            {
                                sumaPagosCancelaciones = dbPagosCancelados.Sum(e => e.Amount);
                                cantitadPorTipo.Add("TotalCancelation", dbPagosCancelados.Sum(e => e.Amount));
                            }

                            /*Obtenemos todos los retiros*/
                            decimal sumaRetiros = 0;
                            if (CashDb.Withdrawal.Count > 0)
                            {
                                sumaRetiros = CashDb.Withdrawal.Sum(w => w.Total);
                                cantitadPorTipo.Add("TotalWithdrawal", CashDb.Withdrawal.Sum(w => w.Total));
                            }

                            /*guardamos las denominaciones en la base de datos*/
                            foreach (var item in Cash.denominationByCashCloseAux.Where(d => d.Quantity > 0))
                            {
                                DenominationByCashClose dbcc = new DenominationByCashClose();
                                dbcc.idDenomination = item.idDenomination;
                                dbcc.Quantity = item.Quantity;
                                CashDb.DenominationByCashClose.Add(dbcc);
                            }

                            /*guardamos las facturas en la base de datos*/
                            foreach (var item in Cash.expensesAux.Where(d => d.Amount > 0))
                            {
                                Expenses dbexp = new Expenses();
                                dbexp.Number = item.Number;
                                dbexp.Name = item.Name;
                                dbexp.Amount = item.Amount;
                                dbexp.Created = DateTime.UtcNow;
                                dbexp.idUser = Cash.UserIdWhoClosed.Value;
                                CashDb.Expenses.Add(dbexp);
                            }

                            /*obtenemos el total de Consultas vendidas durante el corte y el total de ganancias por venta de productos*/
                            int TotalConsult = db.OrdersConcepts.Where(o => CashDb.DateOpened <= o.Created && o.Concept.Category.Name == "Consultas" && o.ClinicId == Cash.ClinicId && o.Orders.Employee.UserId == Cash.UserIdWhoClosed).Count();
                            var TotalProductSellDb = db.OrdersConcepts.Where(o => CashDb.DateOpened <= o.Created && o.Concept.Category.Name == "Productos" && o.ClinicId == Cash.ClinicId && o.Orders.Employee.UserId == Cash.UserIdWhoClosed);
                            decimal TotalProductSell = 0;

                            if (TotalProductSellDb.Count() > 0)
                            {
                                TotalProductSell = TotalProductSellDb.Sum(p => p.Total);
                            }

                            /*el total de pagos realizados menos el dinero final, nos dice si sobro o falto dinero (si es negativo, quiere decir que falto)*/
                            CashDb.RemainingOrMissing = Cash.FinalCash - sumaPagos - CashDb.InitialCash + sumaRetiros + sumaPagosCancelaciones;
                            CashDb.FinalCash = Cash.FinalCash;
                            CashDb.DateClosed = DateTime.UtcNow;
                            CashDb.UserIdWhoClosed = Cash.UserIdWhoClosed;
                            CashDb.TotalCash = !cantitadPorTipo.ContainsKey("Efectivo") ? 0 : cantitadPorTipo["Efectivo"];
                            CashDb.TotalCrediCard = !cantitadPorTipo.ContainsKey("Tarjeta") ? 0 : cantitadPorTipo["Tarjeta"];
                            CashDb.TotalVoucher = !cantitadPorTipo.ContainsKey("Vale") ? 0 : cantitadPorTipo["Vale"];
                            CashDb.TotalExpense = !cantitadPorTipo.ContainsKey("TotalExpense") ? 0 : cantitadPorTipo["TotalExpense"];
                            CashDb.TotalCancelation = !cantitadPorTipo.ContainsKey("TotalCancelation") ? 0 : cantitadPorTipo["TotalCancelation"];
                            CashDb.TotalWithdrawal = !cantitadPorTipo.ContainsKey("TotalWithdrawal") ? 0 : cantitadPorTipo["TotalWithdrawal"];
                            CashDb.TotalSell = !cantitadPorTipo.ContainsKey("TotalSell") ? 0 : cantitadPorTipo["TotalSell"];
                            CashDb.TotalConsult = TotalConsult;
                            CashDb.TotalProductSell = TotalProductSell;

                            db.SaveChanges();
                            result.success = true;
                            /*Acutalizamos data con los datos enviados por el usuario*/
                            result.data = Cash;
                            db.Entry<CashClosing>(CashDb).Reload();
                            /*Actualizamos data con los datos que se calcularon en esta funcion*/
                            DataHelper.fill(result.data, CashDb);
                            DataHelper.fill(result.data.clinicAux, CashDb.Clinic);

                            var PersonUserClosed = db.Person.Where(p => p.Employee.Where(e => e.UserId == Cash.UserIdWhoClosed.Value).Any()).FirstOrDefault();
                            DataHelper.fill(result.data.PersonAux, PersonUserClosed);

                            foreach (var orderCanDb in db.Orders.Where(o => CashDb.DateOpened <= o.DateCanceled && o.DateCanceled <= CashDb.DateClosed && o.ClinicId == Cash.ClinicId && o.Employee.UserId == Cash.UserIdWhoClosed))
                            {
                                var aux = new OrderAux();
                                DataHelper.fill(aux, orderCanDb);
                                DataHelper.fill(aux.EmployeeCancel, orderCanDb.Employee1);
                                DataHelper.fill(aux.EmployeeCancel.personAux, orderCanDb.Employee1.Person);
                                result.data.OrdersCanceled.Add(aux);
                            }
                            foreach (var withdrawalDB in CashDb.Withdrawal)
                            {
                                var aux = new WithdrawalAux();
                                DataHelper.fill(aux, withdrawalDB);
                                result.data.withdrawalAux.Add(aux);
                            }
                        }
                        else
                        {
                            result.success = false;
                            result.message = "El total ingresado ($ " + Cash.FinalCash + ") no coincide con la suma de las cantidades ingresadas.";
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.message = "No sexiste un saldo inicial para este corte de caja.";
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
        public static DenominationResult getDenominations()
        {
            DenominationResult result = new DenominationResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var denominationDbList = db.Denomination.Where(d => d.Active);
                    foreach (var denominaionDb in denominationDbList)
                    {
                        DenominationAux aux = new DenominationAux();
                        DataHelper.fill(aux, denominaionDb);
                        result.data_list.Add(aux);
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
        public static CashClosingResult getCashClosing(int id)
        {
            CashClosingResult result = new CashClosingResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var CashDb = db.CashClosing.Where(c => c.id == id).FirstOrDefault();
                    if (CashDb != null && CashDb.DateClosed != null)
                    {

                        DataHelper.fill(result.data, CashDb);
                        DataHelper.fill(result.data.clinicAux, CashDb.Clinic);

                        //CashDb.User1 = usuario que abrio la caja
                        //CashDb.User = usuario que cerro la caja
                        if (CashDb.User.Employee.FirstOrDefault() != null)
                        {
                            DataHelper.fill(result.data.PersonAux, CashDb.User.Employee.FirstOrDefault().Person);
                        }

                        /*obtenemos las cantidades de las denominaciones que se agregaron en el corte*/
                        foreach (var denominationDb in CashDb.DenominationByCashClose)
                        {
                            var aux = new DenominationByCashCloseAux();
                            DataHelper.fill(aux, denominationDb);
                            DataHelper.fill(aux.denominationAux, denominationDb.Denomination);
                            result.data.denominationByCashCloseAux.Add(aux);
                        }
                        /*obtenemos todas las facturas que se emitieron en el corte*/
                        foreach (var expenseDb in CashDb.Expenses)
                        {
                            var aux = new ExpensesAux();
                            DataHelper.fill(aux, expenseDb);
                            result.data.expensesAux.Add(aux);
                        }
                        /*buscamos todas las ordenes canceladas en el periodo del corte*/
                        foreach (var orderCanDb in db.Orders.Where(o => CashDb.DateOpened <= o.DateCanceled && o.DateCanceled <= CashDb.DateClosed && o.Employee1.UserId == CashDb.UserIdWhoClosed && o.ClinicId == CashDb.ClinicId))
                        {
                            var aux = new OrderAux();
                            DataHelper.fill(aux, orderCanDb);
                            DataHelper.fill(aux.EmployeeCancel, orderCanDb.Employee1);
                            DataHelper.fill(aux.EmployeeCancel.personAux, orderCanDb.Employee1.Person);
                            result.data.OrdersCanceled.Add(aux);
                        }
                        foreach (var withdrawalDB in CashDb.Withdrawal)
                        {
                            var aux = new WithdrawalAux();
                            DataHelper.fill(aux, withdrawalDB);
                            result.data.withdrawalAux.Add(aux);
                        }

                        result.success = true;
                    }
                    else
                    {
                        result.message = "El detalle del corte solicitado no existe.";
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
        public static CashClosingResult GetCurrentCashClosing(int userId, int clinicId)
        {
            CashClosingResult result = new CashClosingResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var CashDb = db.CashClosing.Where(c => c.UserIdWhoOpened == userId && c.ClinicId == clinicId && !c.UserIdWhoClosed.HasValue).FirstOrDefault();
                    if (CashDb != null)
                    {

                        DataHelper.fill(result.data, CashDb);

                        //CashDb.User1 = usuario que abrio la caja
                        //CashDb.User = usuario que cerro la caja
                        if (CashDb.User1.Employee.FirstOrDefault() != null)
                        {
                            DataHelper.fill(result.data.PersonAux, CashDb.User1.Employee.FirstOrDefault().Person);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "El usuario no tiene un turno iniciado.";
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
    }
}