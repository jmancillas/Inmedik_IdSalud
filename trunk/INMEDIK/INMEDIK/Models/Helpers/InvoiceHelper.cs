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
    public class InvoiceResult : Result
    {
        public InvoiceAux data { get; set; }
        public List<InvoiceAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public InvoiceResult()
        {
            this.data = new InvoiceAux();
            this.data_list = new List<InvoiceAux>();
            this.total = new NumericResult();
        }
    }
    public class InvoiceAux
    {
        public int id { get; set; }
        public string code { get; set; }
        public ClinicAux clinicAux { get; set; }
        public DateTime created { get; set; }
        public int createdBy { get; set; }
        public List<InvoiceMovementAux> invoiceMovementsAux { get; set; }
        public int deliveredId { get; set; }
        public bool isCanceled { get; set; }
        public bool disabledCancel { get; set; }
        public bool hasEdits { get; set; }
        public bool canEdit { get; set; }
        public List<RecordInvoiceAux> recordInvoiceAux { get; set; }
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
        public DateTime updated { get; set; }
        public string updated_string
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(updated.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public int updatedBy { get; set; }

        public InvoiceAux()
        {
            this.clinicAux = new ClinicAux();
            this.invoiceMovementsAux = new List<InvoiceMovementAux>();
            this.recordInvoiceAux = new List<RecordInvoiceAux>();
        }

        public bool isRequest
        {
            get
            {
                bool request;
                if (deliveredId > 0)
                {
                    request = true;
                }
                else
                {
                    request = false;
                }
                return request;
            }
        }

        public string sCancelled
        {
            get
            {
                string status;
                if (isCanceled)
                {
                    status = "Si";
                }
                else
                {
                    status = "No";
                }
                return status;
            }
        }
    }

    public class InvoiceMovementAux
    {
        public int id { get; set; }
        public ConceptAux conceptAux { get; set; }
        public StockAux stockAux { get; set; }
        public int quantity { get; set; }
        public decimal cost { get; set; }
        public decimal price { get; set; }
        public bool iva { get; set; }
        public int currentIva { get; set; }
        public int deliveredTransactionId { get; set; }
        public int? stockId { get; set; }
        public int minQuantity { get; set; }
        public bool hasSales { get; set; }
        public string batch { get; set; }
        public string sExpiredDate { get; set; }
        public int conceptId { get; set; }
        public DateTime? expiredDate { get; set; }

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

        public InvoiceMovementAux()
        {
            this.conceptAux = new ConceptAux();
            this.stockAux = new StockAux();
        }
    }

    public class tempInvoiceMovementAux
    {
        public int id { get; set; }
        public ConceptAux conceptAux { get; set; }
        public StockAux stockAux { get; set; }
        public int quantity { get; set; }
        public decimal cost { get; set; }
        public decimal price { get; set; }
        public bool iva { get; set; }
        public int currentIva { get; set; }
        public int deliveredTransactionId { get; set; }
        public int? stockId { get; set; }
        public string batch { get; set; }
        public int conceptId { get; set; }
        public DateTime? expiredDate { get; set; }

        public tempInvoiceMovementAux()
        {
            this.conceptAux = new ConceptAux();
            this.stockAux = new StockAux();
        }
    }

    public class InvoiceHelper
    {
        public static Result SaveInvoice(InvoiceAux invoice)
        {
            Result result = new Result();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }

            NumericResult taxRes = ParameterHelper.GetTax();

            if (!taxRes.success)
            {
                result.success = false;
                result.message = "Ocurrió un error al obtener el Iva configurado.";
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (!db.Invoice.Where(i => i.Code == invoice.code && i.ClinicId == invoice.clinicAux.id.Value).Any())
                    {
                        Invoice dbInvoice = db.Invoice.Create();
                        dbInvoice.Code = invoice.code;
                        dbInvoice.ClinicId = invoice.clinicAux.id.Value;
                        dbInvoice.Created = DateTime.UtcNow;
                        dbInvoice.CreatedBy = res.User.id.Value;
                        dbInvoice.DeliveredId = null;
                        dbInvoice.IsCanceled = false;
                        dbInvoice.Updated = DateTime.UtcNow;
                        dbInvoice.UpdatedBy = res.User.id.Value;
                        decimal ivaConfig = ParameterHelper.GetTax().value;
                        foreach (InvoiceMovementAux movement in invoice.invoiceMovementsAux)
                        {
                            InvoiceMovement invMovement = db.InvoiceMovement.Create();
                            var concept = db.Concept.Where(c => c.id == movement.conceptAux.id).FirstOrDefault();
                            if (concept != null)
                            {
                                concept.Cost = movement.cost;
                                concept.Price = movement.price;
                            }

                            var dbStock = db.Stock.Create();
                            dbStock.ClinicId = invoice.clinicAux.id.Value;
                            dbStock.ConceptId = movement.conceptAux.id;
                            dbStock.Code = invoice.code;
                            dbStock.Cost = movement.cost;
                            dbStock.Created = DateTime.UtcNow;
                            dbStock.Iva = movement.iva;
                            dbStock.CurrIva = Convert.ToDouble(ivaConfig);
                            dbStock.InStock = movement.quantity;
                            dbStock.Batch = movement.batch;
                            if (movement.expiredDate != null)
                            {
                                dbStock.ExpiredDate = movement.expiredDate.Value.ToUniversalTime();
                            }
                            else
                            {
                                dbStock.ExpiredDate = null;
                            }

                            db.Stock.Add(dbStock);
                            db.SaveChanges();

                            invMovement.ConceptId = movement.conceptAux.id;
                            invMovement.Cost = movement.cost;
                            invMovement.Iva = movement.iva;
                            invMovement.Quantity = movement.quantity;
                            invMovement.Price = movement.price;
                            invMovement.CurrentIva = taxRes.IntValue;
                            invMovement.DeliveredTransactionId = null;
                            invMovement.StockId = dbStock.id;
                            dbInvoice.InvoiceMovement.Add(invMovement);
                        }

                        db.Invoice.Add(dbInvoice);

                        db.SaveChanges();

                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Ya existe una factura con el código " + invoice.code + ".";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }

        public static Result EditInvoice(InvoiceAux invoice, List<InvoiceMovementAux> deleteData)
        {
            Result result = new Result();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }

            NumericResult taxRes = ParameterHelper.GetTax();

            if (!taxRes.success)
            {
                result.success = false;
                result.message = "Ocurrió un error al obtener el Iva configurado.";
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbtransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //Se busca si existen registros en record ordenado por la ultima versión
                        RecordInvoice record = db.RecordInvoice.Where(r => r.InvoiceId == invoice.id).OrderByDescending(s => s.Version).FirstOrDefault();
                        Invoice invoicedb = db.Invoice.Where(i => i.id == invoice.id).FirstOrDefault();

                        Invoice codeExist = db.Invoice.Where(i => i.Code == invoice.code).FirstOrDefault();

                        //Se valida que la factura no tenga el mismo codigo que en la edición
                        if (codeExist != null)
                        {
                            if (codeExist.id != invoice.id)
                            {
                                throw new Exception("Ya existe una factura con el codigo: " + invoice.code + ", ingrese uno diferente");
                            }
                        }
                        //Se valida si la factura esta cancelada
                        if(invoicedb.IsCanceled)
                        {
                            throw new Exception("La factura se encuentra cancelada");
                        }
                        //Se valida que la factura tenga transacciones, de lo contrario se regresa un mensaje de error
                        if (invoice.invoiceMovementsAux.Count > 0)
                        {
                            //Se usa esta variable como control para validar si se hizo algún cambio en cuanto al cabecero o las transacciones de las facturas
                            var AnythingIsEdit = 0;

                            //Se crea el registro de edición
                            RecordInvoice recordDb = db.RecordInvoice.Create();
                            recordDb.InvoiceId = invoice.id;
                            recordDb.Code = invoice.code;
                            recordDb.ClinicId = invoice.clinicAux.id.Value;
                            //Si existe un registro de record se asigna la siguiente version
                            if (record != null)
                            {
                                recordDb.Version = record.Version + 1;
                            }
                            else
                            {
                                recordDb.Version = 1;
                            }
                            recordDb.Created = DateTime.UtcNow;
                            recordDb.CreatedBy = res.Id;
                            db.RecordInvoice.Add(recordDb);

                            db.SaveChanges();

                            //Se valida si cambio el codigo de la factura o la clinica
                            if (invoicedb.Code != invoice.code || invoicedb.ClinicId != invoice.clinicAux.id)
                            {
                                invoicedb.Code = invoice.code;
                                invoicedb.ClinicId = invoice.clinicAux.id.Value;
                                invoicedb.Updated = DateTime.UtcNow;
                                invoicedb.UpdatedBy = res.Id;

                                AnythingIsEdit += 1;
                            }

                            //Se valida si se eliminaron registros de la factura
                            if (deleteData != null)
                            {
                                foreach (var deleteMovement in deleteData)
                                {
                                    Stock stockDb = db.Stock.Where(s => s.id == deleteMovement.stockId).FirstOrDefault();

                                    InvoiceMovement movementDb = db.InvoiceMovement.Where(m => m.id == deleteMovement.id).FirstOrDefault();

                                    if (movementDb != null)
                                    {
                                        //Se asigna la info correcta de la transaccion a borrar
                                        InvoiceMovementAux deleteMovementAux = new InvoiceMovementAux();
                                        DataHelper.fill(deleteMovementAux, movementDb);
                                        DataHelper.fill(deleteMovementAux.conceptAux, movementDb.Concept);
                                        DataHelper.fill(deleteMovementAux.stockAux, movementDb.Stock);

                                        //Se crea el registro de edición con los campos eliminados
                                        RecordTransactionInvoice recordTransDb = db.RecordTransactionInvoice.Create();
                                        recordTransDb.RecordInvoiceId = recordDb.RecordInvoiceId;
                                        recordTransDb.TransactionInvoiceId = deleteMovementAux.id;
                                        recordTransDb.ConceptId = deleteMovementAux.conceptAux.id;
                                        recordTransDb.Quantity = deleteMovementAux.quantity;
                                        recordTransDb.Iva = deleteMovementAux.iva;
                                        recordTransDb.Cost = deleteMovementAux.cost;
                                        recordTransDb.Price = deleteMovementAux.price;
                                        recordTransDb.Batch = deleteMovementAux.batch;
                                        recordTransDb.ExpiredDate = deleteMovementAux.stockAux.expiredDate;
                                        recordTransDb.TypeOfModification = "Eliminado";
                                        db.RecordTransactionInvoice.Add(recordTransDb);

                                        if (stockDb != null)
                                        {
                                            //Se valida que no se hayan vendido del stock del producto, si es así la transacción no se puede eliminar
                                            if (stockDb.InStock != deleteMovementAux.quantity)
                                            {
                                                throw new Exception("La transacción cuenta con ventas, no se puede eliminar");
                                            }
                                            else
                                            {
                                                db.Stock.Remove(stockDb);
                                                db.InvoiceMovement.Remove(movementDb);
                                                AnythingIsEdit += 1;
                                            }
                                        }
                                    }
                                }
                            }

                            foreach (var Movement in invoice.invoiceMovementsAux)
                            {
                                InvoiceMovement movementDb = db.InvoiceMovement.Where(m => m.id == Movement.id).FirstOrDefault();

                                tempInvoiceMovementAux movAuxOld = new tempInvoiceMovementAux();
                                tempInvoiceMovementAux movAuxNew = new tempInvoiceMovementAux();

                                //Se asigna la información original para guardarla
                                if (movementDb != null)
                                {
                                    DataHelper.fill(movAuxOld, movementDb);
                                    DataHelper.fill(movAuxOld.conceptAux, movementDb.Concept);
                                    DataHelper.fill(movAuxOld.stockAux, movementDb.Stock);

                                    //Se asigna la información nueva para guardarla
                                    DataHelper.fill(movAuxNew, Movement);

                                    //Se valida si el lote cambio, en caso de ser así se asigna un valor vacio para hacer la comparación
                                    if (Movement.batch != movAuxOld.stockAux.batch)
                                    {
                                        movAuxOld.batch = "";
                                    }
                                    else
                                    {
                                        movAuxOld.batch = movAuxOld.stockAux.batch;
                                    }

                                    //Se valida si la fecha cambio, en caso de ser así se asigna una fecha menor para hacer la comparación
                                    if (movAuxOld.stockAux.expiredDate != Movement.expiredDate)
                                    {
                                        movAuxOld.expiredDate = DateTime.Now.AddYears(-100);
                                    }
                                    else
                                    {
                                        movAuxOld.expiredDate = movAuxOld.stockAux.expiredDate;
                                    }

                                    //Se asigna el valor del concepto original y los auxiliares se hacen nulos para poder compararlos
                                    movAuxOld.conceptId = movAuxOld.conceptAux.id;
                                    movAuxOld.conceptAux = null;
                                    movAuxOld.stockAux = null;

                                    //Se asigna el valor del concepto nuevo y los auxiliares se hacen nulos para poder compararlos
                                    movAuxNew.conceptId = movAuxNew.conceptAux.id;
                                    movAuxNew.conceptAux = null;
                                    movAuxNew.stockAux = null;

                                    //Se comparan los dos objetos, si son iguales no guarda registro de edición
                                    bool theSame = ObjectManager.CompareObjects(movAuxOld, movAuxNew);
                                    if (!theSame)
                                    {
                                        RecordTransactionInvoice recordTransactionDb = db.RecordTransactionInvoice.Create();

                                        //Se guarda el registro original
                                        recordTransactionDb.RecordInvoiceId = recordDb.RecordInvoiceId;
                                        recordTransactionDb.TransactionInvoiceId = movementDb.id;
                                        recordTransactionDb.ConceptId = movementDb.Concept.id;
                                        recordTransactionDb.Quantity = movementDb.Quantity;
                                        recordTransactionDb.Iva = movementDb.Iva;
                                        recordTransactionDb.Cost = movementDb.Cost;
                                        recordTransactionDb.Price = movementDb.Price;
                                        recordTransactionDb.Batch = movementDb.Stock.Batch;
                                        if (movementDb.Stock.ExpiredDate != null)
                                        {
                                            recordTransactionDb.ExpiredDate = movementDb.Stock.ExpiredDate;
                                        }
                                        recordTransactionDb.TypeOfModification = "Original";
                                        db.RecordTransactionInvoice.Add(recordTransactionDb);
                                        db.SaveChanges();

                                        //Se guarda el registro editado
                                        recordTransactionDb.RecordInvoiceId = recordDb.RecordInvoiceId;
                                        recordTransactionDb.TransactionInvoiceId = movAuxNew.id;
                                        recordTransactionDb.ConceptId = movAuxNew.conceptId;
                                        recordTransactionDb.Quantity = movAuxNew.quantity;
                                        recordTransactionDb.Iva = movAuxNew.iva;
                                        recordTransactionDb.Cost = movAuxNew.cost;
                                        recordTransactionDb.Price = movAuxNew.price;
                                        recordTransactionDb.Batch = movAuxNew.batch;
                                        if (movAuxNew.expiredDate != null)
                                        {
                                            recordTransactionDb.ExpiredDate = movAuxNew.expiredDate.Value.ToUniversalTime();
                                        }
                                        recordTransactionDb.TypeOfModification = "Editado";
                                        db.RecordTransactionInvoice.Add(recordTransactionDb);
                                        db.SaveChanges();

                                        //Se actualiza el stock
                                        Stock stockDb = db.Stock.Where(s => s.id == Movement.stockId).FirstOrDefault();
                                        stockDb.ConceptId = movAuxNew.conceptId;
                                        stockDb.InStock = movAuxNew.quantity - (movAuxOld.quantity - stockDb.InStock);
                                        stockDb.Cost = movAuxNew.cost;
                                        stockDb.ClinicId = recordDb.ClinicId;
                                        stockDb.Iva = movAuxNew.iva;
                                        stockDb.CurrIva = movAuxNew.currentIva;
                                        stockDb.Batch = movAuxNew.batch;
                                        if (movAuxNew.expiredDate != null)
                                        {
                                            stockDb.ExpiredDate = movAuxNew.expiredDate.Value.ToUniversalTime();
                                        }
                                        else
                                        {
                                            stockDb.ExpiredDate = null;
                                        }

                                        //Se actualiza la información de las transacciones de la factura
                                        movementDb.ConceptId = movAuxNew.conceptId;
                                        movementDb.Quantity = movAuxNew.quantity;
                                        movementDb.Cost = movAuxNew.cost;
                                        movementDb.Iva = movAuxNew.iva;
                                        movementDb.Price = movAuxNew.price;

                                        AnythingIsEdit += 1;
                                    }
                                }
                                else
                                {
                                    decimal ivaConfig = ParameterHelper.GetTax().value;

                                    //Se asigna la información nueva para guardarla
                                    DataHelper.fill(movAuxNew, Movement);

                                    InvoiceMovement movementDB = db.InvoiceMovement.Create();

                                    //Se crea el stock de la nueva transaccion
                                    var dbStock = db.Stock.Create();
                                    dbStock.ClinicId = invoice.clinicAux.id.Value;
                                    dbStock.ConceptId = movAuxNew.conceptAux.id;
                                    dbStock.Cost = movAuxNew.cost;
                                    dbStock.Created = DateTime.UtcNow;
                                    dbStock.Iva = movAuxNew.iva;
                                    dbStock.CurrIva = Convert.ToDouble(ivaConfig);
                                    dbStock.InStock = movAuxNew.quantity;
                                    dbStock.Batch = movAuxNew.batch;
                                    if (movAuxNew.expiredDate != null)
                                    {
                                        dbStock.ExpiredDate = movAuxNew.expiredDate.Value.ToUniversalTime();
                                    }
                                    else
                                    {
                                        dbStock.ExpiredDate = null;
                                    }

                                    db.Stock.Add(dbStock);
                                    db.SaveChanges();

                                    //Se crea la nueva transaccion de la factura
                                    movementDB.ConceptId = movAuxNew.conceptAux.id;
                                    movementDB.Quantity = movAuxNew.quantity;
                                    movementDB.Cost = movAuxNew.cost;
                                    movementDB.Iva = movAuxNew.iva;
                                    movementDB.Price = movAuxNew.price;
                                    movementDB.InvoiceId = invoice.id;
                                    movementDB.CurrentIva = (int)ivaConfig;
                                    movementDB.DeliveredTransactionId = null;
                                    movementDB.StockId = dbStock.id;
                                    db.InvoiceMovement.Add(movementDB);
                                    db.SaveChanges();

                                    RecordTransactionInvoice recordTransactionDb = db.RecordTransactionInvoice.Create();

                                    //Se crea el registro en record de la nueva transacción
                                    recordTransactionDb.RecordInvoiceId = recordDb.RecordInvoiceId;
                                    recordTransactionDb.TransactionInvoiceId = movementDB.id;
                                    recordTransactionDb.ConceptId = movAuxNew.conceptAux.id;
                                    recordTransactionDb.Quantity = movAuxNew.quantity;
                                    recordTransactionDb.Iva = movAuxNew.iva;
                                    recordTransactionDb.Cost = movAuxNew.cost;
                                    recordTransactionDb.Price = movAuxNew.price;
                                    recordTransactionDb.Batch = movAuxNew.batch;
                                    if (movAuxNew.expiredDate != null)
                                    {
                                        recordTransactionDb.ExpiredDate = movAuxNew.expiredDate.Value.ToUniversalTime();
                                    }
                                    recordTransactionDb.TypeOfModification = "Agregado";
                                    db.RecordTransactionInvoice.Add(recordTransactionDb);
                                    db.SaveChanges();

                                    AnythingIsEdit += 1;
                                }
                            }

                            //Se valida si se hizo algún cambio en la factura, en caso contrario se arroja el siguiente error
                            if (AnythingIsEdit == 0)
                            {
                                throw new Exception("No se ha editado ningun campo de la factura");
                            }

                            db.SaveChanges();

                            dbtransaction.Commit();
                            res.success = true;
                            res.message = "Se ha guardado correctamente";
                        }
                        else
                        {
                            throw new Exception("No se puede dejar la factura sin partidas");
                        }
                    }
                    catch (Exception ex)
                    {
                        dbtransaction.Rollback();
                        res.success = false;
                        res.exception = ex;
                        res.message = "Error inesperado " + res.exception_message;
                    }
                }
            }
            return res;
        }

        public static InvoiceResult GetInvoice(int id)
        {
            InvoiceResult result = new InvoiceResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var invoice = db.Invoice.Where(i => i.id == id).FirstOrDefault();
                    if (invoice != null)
                    {
                        DataHelper.fill(result.data, invoice);
                        DataHelper.fill(result.data.clinicAux, invoice.Clinic);
                        foreach (var movement in invoice.InvoiceMovement)
                        {
                            Stock dbStock = db.Stock.Where(s => s.id == movement.StockId).FirstOrDefault();
                            InvoiceMovementAux im = new InvoiceMovementAux();
                            DataHelper.fill(im, movement);
                            DataHelper.fill(im.conceptAux, movement.Concept);
                            if (movement.Stock != null)
                            {
                                DataHelper.fill(im.stockAux, movement.Stock);
                                im.minQuantity = movement.Quantity - dbStock.InStock;
                                im.batch = movement.Stock.Batch;
                                if (movement.Stock.ExpiredDate != null)
                                {
                                    im.expiredDate = (DateTime)movement.Stock.ExpiredDate;
                                }
                                if (movement.Stock.InStock != movement.Quantity)
                                {
                                    im.hasSales = true;
                                }
                            }

                            result.data.invoiceMovementsAux.Add(im);
                        }
                        foreach (var hasStock in invoice.InvoiceMovement)
                        {
                            if (hasStock.StockId != null)
                            {
                                result.data.canEdit = true;
                            }
                            else
                            {
                                result.data.canEdit = false;
                                break;
                            }
                        }

                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Factura no encontrada.";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }

        public static InvoiceResult CancelInvoice(int InvoiceId)
        {
            InvoiceResult result = new InvoiceResult();
            UserResult res = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!res.success)
            {
                result.success = false;
                result.message = "La sesión ha finalizado.";
                return result;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Invoice invoiceDb = db.Invoice.Where(i => i.id == InvoiceId).FirstOrDefault();
                    List<InvoiceMovement> invoiceMovDb = db.InvoiceMovement.Where(m => m.InvoiceId == invoiceDb.id).ToList();

                    //Se valida que cada transacción no tenga ventas del producto y se determina si se puede cancelar o no
                    var canCancel = true;
                    foreach (var stocks in invoiceMovDb)
                    {
                        Stock stockDb = db.Stock.Where(s => s.id == stocks.StockId).FirstOrDefault();
                        if (stockDb != null)
                        {
                            if (stocks.Quantity != stockDb.InStock)
                            {
                                canCancel = false;
                                break;
                            }
                            else
                            {
                                canCancel = true;
                            }
                        }
                    }
                    if (!canCancel)
                    {
                        result.success = false;
                        result.message = "Los productos de esta factura tienen ventas, solo se puede editar.";
                    }
                    else
                    {
                        if (invoiceDb != null)
                        {
                            //Se valida si la factura esta cancelada
                            if (invoiceDb.IsCanceled)
                            {
                                result.success = false;
                                result.message = "La factura ya esta cancelada";
                            }
                            //Si la factura se creo a partir de una solicitud a almacen no se puede cancelar
                            else if (invoiceDb.DeliveredId > 0)
                            {
                                result.success = false;
                                result.message = "No se puede cancelar la factura";
                            }
                            else
                            {
                                invoiceDb.IsCanceled = true;
                                invoiceDb.Updated = DateTime.UtcNow;
                                invoiceDb.UpdatedBy = res.User.id.Value;

                                //Se buscan todos los stocks de las transacciones y se ponen en 0
                                foreach (var cancelstock in invoiceMovDb)
                                {
                                    Stock stockD = db.Stock.Where(p => p.id == cancelstock.StockId).FirstOrDefault();
                                    stockD.InStock = 0;
                                }

                                db.SaveChanges();
                                result.success = true;
                                result.message = "Se ha cancelado correctamente";
                            }
                        }
                        else
                        {
                            result.success = false;
                            result.message = "No se encuentra la factura";
                        }
                    }
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }

        public static InvoiceResult GetInvoices(DTParameterModel filter, int clinicId)
        {
            InvoiceResult result = new InvoiceResult();
            string order = "";
            string orderColumn = "";
            if (!string.IsNullOrEmpty(filter.Order.First().Dir) && !string.IsNullOrEmpty(filter.Order.First().Data))
            {
                order = filter.Order.First().Dir;
                orderColumn = filter.Order.First().Data;
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    bool userIsAdim = HttpContext.Current.User.IsInRole("Admin");
                    IQueryable<Invoice> query;
                    if (userIsAdim)
                    {
                        query = db.Invoice.AsQueryable();
                    }
                    else
                    {
                        query = db.Invoice.Where(i => i.ClinicId == clinicId);
                    }
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "code" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Code.Contains(column.Search.Value));
                        }
                        if (column.Data == "clinicAux.name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Clinic.Name.Contains(column.Search.Value));
                        }
                    }
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "code")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Code);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Code);
                            }
                        }
                        if (orderColumn == "sCancelled")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.IsCanceled);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.IsCanceled);
                            }
                        }
                        if (orderColumn == "clinicAux.name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Clinic.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Clinic.Name);
                            }
                        }
                        if (orderColumn == "created_string")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Created);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Created);
                            }
                        }
                    }

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (Invoice invoice in query.ToList())
                    {
                        InvoiceAux aux = new InvoiceAux();
                        DataHelper.fill(aux, invoice);
                        DataHelper.fill(aux.clinicAux, invoice.Clinic);
                        if (invoice.InvoiceMovement.Any(s => s.StockId == null))
                        {
                            aux.disabledCancel = true;
                        }
                        if (invoice.RecordInvoice.Count > 0)
                        {
                            aux.hasEdits = true;
                        }
                        result.data_list.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado" + result.exception_message;
                }
            }
            return result;
        }
    }
}
