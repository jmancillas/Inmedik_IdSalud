using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class ReturnAux
    {
        public int? id { get; set; }
        public string Reason { get; set; }
        public int CreatedId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(Created).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public string sUpdated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(Updated).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }
        public UserAux UserAuxCrea { get; set; }
        public UserAux UserAuxUpda { get; set; }
        public int UpdatedId { get; set; }
        public int AlmBrancRequestToDeliveryId { get; set; }
        public List<ReturnProductsAux> ReturnProductsAux { get; set; }
        public bool confirmed { get; set; }
        public int? clinicId { get; set; }

        public ReturnAux()
        {
            UserAuxCrea = new UserAux();
            UserAuxUpda = new UserAux();
            ReturnProductsAux = new List<ReturnProductsAux>();
        }

        public void fill(IReturn ReturnDb)
        {
            id = ReturnDb.Id;
            Reason = ReturnDb.Reason;
            CreatedId = ReturnDb.CreatedId;
            UpdatedId = ReturnDb.UpdatedId;
            Created = ReturnDb.Created;
            Updated = ReturnDb.Updated;
            UserAuxCrea.account = ReturnDb.User.UserAccount;
            UserAuxUpda.account = ReturnDb.User1.UserAccount;
            foreach (var itemStock in ReturnDb.IReturnProducts)
            {
                var aux = new ReturnProductsAux();
                aux.fill(itemStock);
                ReturnProductsAux.Add(aux);
            }
            confirmed = ReturnDb.Confirmed;
            clinicId = ReturnDb.ClinicId;
        }

    }

    public class ReturnProductsAux
    {
        public int? id { get; set; }
        public string Reason { get; set; }
        public string Batch { get; set; }
        public string Name { get; set; }
        public int InStock { get; set; }
        public int Quantity { get; set; }
        public int ConceptId { get; set; }
        public int IReturnId { get; set; }
        public DateTime Created { get; set; }
        public int AlmStockId { get; set; }
        public int StockId { get; set; }
        public int? AlmBranchRequestToDeliveryTransactionId { get; set; }
        public string sCreated { get { return DateTimeHelper.GetDateTimeFromDateTimeUTC(Created).ToString("dd/MM/yyyy HH:mm", new CultureInfo("es-MX")); } }

        public ReturnProductsAux()
        {

        }

        public void fill(IReturnProducts ReturnDb)
        {
            id = ReturnDb.Id;
            Reason = ReturnDb.Reason;
            Quantity = ReturnDb.Quantity;
            ConceptId = ReturnDb.ConceptId;
            IReturnId = ReturnDb.IReturnId;
            AlmStockId = ReturnDb.AlmStockId;
            Created = ReturnDb.Created;
            Batch = ReturnDb.AlmStock.Batch;
            Name = ReturnDb.Concept.Name;
            InStock = ReturnDb.Quantity;
            AlmBranchRequestToDeliveryTransactionId = ReturnDb.AlmBranchRequestToDeliveryTransactionId;

        }

    }

    public class ReturnResult : Result
    {
        public ReturnAux data { get; set; }
        public List<ReturnAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public int Id
        {
            get
            {
                if (success)
                {
                    return data.id.Value;
                }
                else
                {
                    return -1;
                }
            }
        }

        public ReturnResult()
        {
            data = new ReturnAux();
            data_list = new List<ReturnAux>();
            total = new NumericResult();
        }
    }

    public class AllProductExistAux
    {
        public int id { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public int? stockId { get; set; }
        //public int code { get; set; }
        public int conceptId { get; set; }
        public string name { get; set; }
        public decimal cost { get; set; }
        public decimal price { get; set; }
        public decimal discount { get; set; }
        public bool iva { get; set; }
        public int inStock { get; set; }
        public string batch { get; set; }
        public int clinicId { get; set; }

        public AllProductExistAux()
        {

        }
    }

    public class AllProductExistResult : Result
    {
        public AllProductExistAux data { get; set; }
        public List<AllProductExistAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public AllProductExistResult()
        {
            this.data = new AllProductExistAux();
            this.data_list = new List<AllProductExistAux>();
            this.total = new NumericResult();
        }
    }

    public class ReturnHelper
    {
        public static ReturnResult GetReturns(DTParameterModel model)
        {
            ReturnResult result = new ReturnResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<IReturn> query = db.IReturn;
                    foreach (DTColumn column in model.Columns)
                    {
                        bool columnHasValue = !string.IsNullOrEmpty(column.Search.Value);
                        switch (column.Data)
                        {
                            case "id":
                                query = columnHasValue ? query.Where(u => u.Id.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "Reason":
                                query = columnHasValue ? query.Where(u => u.Reason.Contains(column.Search.Value)) : query;
                                break;
                            case "UserAuxUpda.name":
                                query = columnHasValue ? query.Where(u => (u.User1.UserAccount).Contains(column.Search.Value)) : query;
                                break;
                            default:
                                break;
                        }

                        if (!string.IsNullOrEmpty(model.Order.First().Dir) && !string.IsNullOrEmpty(model.Order.First().Data))
                        {
                            string order = model.Order.First().Dir;
                            string orderColumn = model.Order.First().Data;

                            switch (orderColumn)
                            {
                                case "id":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Id) : query.OrderByDescending(q => q.Id);
                                    break;
                                case "Reason":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Reason) : query.OrderByDescending(q => q.Reason);
                                    break;
                                case "UserAuxCrea.name":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.User.UserAccount) : query.OrderByDescending(q => q.User.UserAccount);
                                    break;
                                case "sCreated":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Created) : query.OrderByDescending(q => q.Created);
                                    break;
                                case "UserAuxUpda.name":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.User1.UserAccount) : query.OrderByDescending(q => q.User1.UserAccount);
                                    break;
                                case "sUpdated":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Updated) : query.OrderByDescending(q => q.Updated);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    result.total.value = query.Count();

                    query = query.Skip(model.Start).Take(model.Length);
                    foreach (IReturn Return in query.ToList())
                    {
                        ReturnAux aux = new ReturnAux();
                        DataHelper.fill(aux, Return);
                        aux.UserAuxUpda.fill(Return.User1);
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

        public static AllProductExistResult GetStock(DTParameterModel model, int clinicId)
        {
            AllProductExistResult result = new AllProductExistResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<vwAllProductsExist> query = db.vwAllProductsExist.Where(vw => vw.ClinicId == clinicId);
                    foreach (DTColumn column in model.Columns)
                    {
                        bool columnHasValue = !string.IsNullOrEmpty(column.Search.Value);
                        switch (column.Data)
                        {
                            case "id":
                                query = columnHasValue ? query.Where(u => u.Id.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "conceptId":
                                query = columnHasValue ? query.Where(u => u.ConceptId.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "inStock":
                                query = columnHasValue ? query.Where(u => u.InStock.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "batch":
                                query = columnHasValue ? query.Where(u => u.Batch.Contains(column.Search.Value)) : query;
                                break;
                            case "categoryId":
                                query = columnHasValue ? query.Where(u => u.CategoryId.ToString().Contains(column.Search.Value)) : query;
                                break;
                            case "name":
                                query = columnHasValue ? query.Where(u => u.Name.Contains(column.Search.Value)) : query;
                                break;
                            case "clinicId":
                                query = columnHasValue ? query.Where(u => u.ClinicId.ToString().Contains(column.Search.Value)) : query;
                                break;
                            default:
                                break;
                        }

                        if (!string.IsNullOrEmpty(model.Order.First().Dir) && !string.IsNullOrEmpty(model.Order.First().Data))
                        {
                            string order = model.Order.First().Dir;
                            string orderColumn = model.Order.First().Data;

                            switch (orderColumn)
                            {
                                case "id":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Id) : query.OrderByDescending(q => q.Id);
                                    break;
                                case "conceptId":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.ConceptId) : query.OrderByDescending(q => q.ConceptId);
                                    break;
                                case "inStock":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.InStock) : query.OrderByDescending(q => q.InStock);
                                    break;
                                case "batch":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Batch) : query.OrderByDescending(q => q.Batch);
                                    break;
                                case "categoryId":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.CategoryId) : query.OrderByDescending(q => q.CategoryId);
                                    break;
                                case "name":
                                    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Name) : query.OrderByDescending(q => q.Name);
                                    break;
                                //case "sUpdated":
                                //    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Updated) : query.OrderByDescending(q => q.Updated);
                                //    break;
                                //case "sCreated":
                                //    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.Created) : query.OrderByDescending(q => q.Created);
                                //    break;
                                //case "SupplierName":
                                //    query = order.ToUpper() == "ASC" ? query.OrderBy(q => q.SupplierName) : query.OrderByDescending(q => q.SupplierName);
                                //    break;
                                default:
                                    break;
                            }
                        }
                    }
                    result.total.value = query.Count();

                    query = query.Skip(model.Start).Take(model.Length);
                    foreach (vwAllProductsExist item in query.ToList())
                    {
                        AllProductExistAux aux = new AllProductExistAux();
                        DataHelper.fill(aux, item);
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

        public static Result SaveReturn(ReturnAux Return, int clinicId)
        {
            Result result = new Result();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                var now = DateTime.UtcNow;
                UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
                try
                {
                    IReturn ReturnDb;

                    if (Return.id == null)
                    {
                        //en caso de ser un registro nuevo, creamos un new
                        ReturnDb = new IReturn();
                        ReturnDb.Created = now;
                        ReturnDb.CreatedId = userRes.Id;
                        ReturnDb.ClinicId = clinicId;
                    }
                    else
                    {
                        //en caso de tener id diferente de null, buscamos el registro en la base de datos para modificarlo y borramos los productos que se agregaran
                        ReturnDb = db.IReturn.Where(d => d.Id == Return.id.Value).FirstOrDefault();

                        if (!ReturnDb.Confirmed)
                        {
                            //devolvemos todos los productos al stock
                            foreach (var productDb in ReturnDb.IReturnProducts)
                            {
                                if (productDb.StockId.HasValue)
                                {
                                    productDb.Stock.InStock = productDb.Stock.InStock + productDb.Quantity;
                                }
                            }
                            db.IReturnProducts.RemoveRange(ReturnDb.IReturnProducts);
                        }
                        else
                        {
                            result.success = false;
                            result.message = "La devolucíon se encuentra con estatus de confirmada";
                        }
                    }

                    if (!ReturnDb.Confirmed)
                    {
                        ReturnDb.ClinicId = clinicId;
                        ReturnDb.Reason = Return.Reason;
                        ReturnDb.Updated = now;
                        ReturnDb.UpdatedId = userRes.Id;

                        foreach (var itemProd in Return.ReturnProductsAux.Where(d => d.Quantity > 0))
                        {
                            var aux = new IReturnProducts();
                            aux.Quantity = itemProd.Quantity;
                            aux.Reason = itemProd.Reason;
                            aux.ConceptId = itemProd.ConceptId;
                            aux.AlmStockId = itemProd.AlmStockId;
                            aux.StockId = itemProd.StockId == 0 ? (int?)null : itemProd.StockId;
                            aux.Created = now;

                            //reducimos la cantidad dada de baja del stock
                            if (itemProd.StockId > 0)
                            {
                                var stockDb = db.Stock.Where(s => s.id == itemProd.StockId).First();
                                stockDb.InStock = stockDb.InStock - itemProd.Quantity;
                            }

                            ReturnDb.IReturnProducts.Add(aux);
                        }

                        if (Return.id == null)
                        {
                            db.IReturn.Add(ReturnDb);
                        }

                        db.SaveChanges();
                        result.success = true;
                        result.message = "Se guardo correctamente la información";
                    }
                    else
                    {
                        result.success = false;
                        result.message = "La devolucíon se encuentra con estatus de confirmada";
                    }
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurríó un error inesperado {result.exception_message}";
                }
            }

            return result;
        }

        public static ReturnResult GetReturn(int id)
        {
            ReturnResult result = new ReturnResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IReturn ReturnDb = db.IReturn.Where(d => d.Id == id).FirstOrDefault();
                    result.data.fill(ReturnDb);
                    result.success = true;
                }
                catch (Exception e)
                {
                    result.success = false;
                    result.exception = e;
                    result.message = $"Ocurríó un error inesperado {result.exception_message}";
                }
            }

            return result;
        }
    }

}