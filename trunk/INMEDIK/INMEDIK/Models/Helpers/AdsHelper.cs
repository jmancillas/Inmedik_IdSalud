using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Models.Helpers
{
    public class AdsAux
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool activeAlways { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public List<KeywordAux> keywordAux { get; set; }

        public string periodActive
        {
            get
            {
                if (this.activeAlways)
                {
                    return "SIEMPRE";
                }
                else
                {
                    return
                         (fromDate.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(fromDate.Value.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy", new CultureInfo("es-MX")) : "")
                    + "-" +
                    (fromDate.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(toDate.Value.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy", new CultureInfo("es-MX")) : "");
                }
            }
        }
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
        public string updated_string
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(updated.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public AdsAux()
        {
            this.keywordAux = new List<KeywordAux>();
        }
    }
    public class KeywordAux
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class KeywordResult : Result
    {
        public KeywordAux data { get; set; }
        public List<KeywordAux> data_list { get; set; }

        public KeywordResult()
        {
            this.data = new KeywordAux();
            this.data_list = new List<KeywordAux>();
        }
    }
    public class AdResult : Result
    {
        public AdsAux data { get; set; }
        public List<AdsAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public AdResult()
        {
            this.data = new AdsAux();
            this.data_list = new List<AdsAux>();
            this.total = new NumericResult();
        }
    }

    public class AdsHelper
    {
        public static GenericResult SaveAd(AdsAux ad)
        {
            GenericResult result = new GenericResult();
            var now = DateTime.UtcNow;
            UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
            if (!ad.activeAlways)
            {
                ad.toDate = ad.toDate.Value.Date.AddDays(1).AddSeconds(-1);
            }
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Advertisement currentAd;
                    if (ad.id != 0)
                    {
                        currentAd = db.Advertisement.Where(a => a.id == ad.id).FirstOrDefault();
                        currentAd.Name = ad.name;
                        currentAd.ActiveAlways = ad.activeAlways;
                        if (!ad.activeAlways)
                        {
                            currentAd.FromDate = ad.fromDate;
                            currentAd.ToDate = ad.toDate;
                        }
                        currentAd.Updated = now;
                        currentAd.UpdatedBy = userRes.Id;
                    }
                    else
                    {
                        currentAd = db.Advertisement.Create();
                        currentAd.Name = ad.name;
                        currentAd.ActiveAlways = ad.activeAlways;
                        if (!ad.activeAlways)
                        {
                            currentAd.FromDate = ad.fromDate;
                            currentAd.ToDate = ad.toDate;
                        }
                        currentAd.Created = now;
                        currentAd.Updated = now;
                        currentAd.CreatedBy = userRes.Id;
                        currentAd.UpdatedBy = userRes.Id;
                        db.Advertisement.Add(currentAd);
                    }
                    currentAd.Keywords.Clear();
                    foreach (KeywordAux keyword in ad.keywordAux)
                    {
                        var key = db.Keywords.Where(k => k.Name.ToUpper() == keyword.name.ToUpper()).FirstOrDefault();
                        if (key == null)
                        {
                            key = db.Keywords.Create();
                            key.Name = keyword.name;
                        }
                        currentAd.Keywords.Add(key);
                    }

                    db.SaveChanges();
                    result.integer_value = currentAd.id;
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        public static KeywordResult GetKeywords(string typed)
        {
            KeywordResult result = new KeywordResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Keywords.Where(k => k.Name.Contains(typed) || typed.Contains(k.Name));
                    foreach (Keywords keyword in query)
                    {
                        KeywordAux aux = new KeywordAux();
                        DataHelper.fill(aux, keyword);
                        result.data_list.Add(aux);
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        public static GenericResult DeleteAd(int id)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Advertisement adDb = db.Advertisement.Where(a => a.id == id).FirstOrDefault();
                    if (adDb != null)
                    {
                        adDb.Deleted = true;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Enfermedad no encontrada";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        public static AdResult GetAd(int id)
        {
            AdResult result = new AdResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var ad = db.Advertisement.Where(a => a.id == id).FirstOrDefault();
                    DataHelper.fill(result.data, ad);
                    foreach (Keywords keyword in ad.Keywords)
                    {
                        KeywordAux aux = new KeywordAux();
                        DataHelper.fill(aux, keyword);
                        result.data.keywordAux.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        public static AdResult GetAds(DTParameterModel filter)
        {
            AdResult result = new AdResult();
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
                    var query = db.Advertisement.Where(pt => !pt.Deleted);
                    #region filtros
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Name.Contains(column.Search.Value));
                        }
                        if (column.Data == "id" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.id.ToString().Contains(column.Search.Value));
                        }
                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Name);
                            }
                        }
                        if (orderColumn == "id")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.id);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.id);
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
                        if (orderColumn == "updated_string")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Updated);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Updated);
                            }
                        }
                        if (orderColumn == "periodActive")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.ActiveAlways).ThenBy(q => q.FromDate);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.ActiveAlways).ThenByDescending(q => q.FromDate);
                            }
                        }
                    }
                    #endregion

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (Advertisement adDB in query.ToList())
                    {
                        AdsAux aux = new AdsAux();
                        DataHelper.fill(aux, adDB);
                        result.data_list.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado. " + result.exception_message;
                }
            }
            return result;
        }

        public static Result AssignFileToAd(int id, string FileName, string OriginalName, string ContentType)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var adDb = db.Advertisement.Where(a => a.id == id).FirstOrDefault();
                    if (adDb != null)
                    {

                        FileDb FileDb = adDb.FileDb.FirstOrDefault();
                        if (FileDb != null)
                        {
                            FileHelper.DeleteFile(
                                FileDb.Name,
                                System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files")
                            );
                            adDb.FileDb.Remove(FileDb);
                            db.FileDb.Remove(FileDb);
                        }
                        FileDb = new FileDb();
                        FileDb.Name = FileName;
                        FileDb.OriginalName = OriginalName;
                        FileDb.ContentType = ContentType;
                        FileDb.Created = DateTime.UtcNow;
                        adDb.FileDb.Add(FileDb);
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.message = "Registro no encontrado.";
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

        public static Result AssignFileToEv(int Evid, string FileName, string OriginalName, string ContentType, string Description, int? fileId)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var evDb = db.EvolutionNote.Where(a => a.Id == Evid).FirstOrDefault();
                    //EvFile evFileDb = db.EvFile.Where(e => e.EvolutionNoteId == Evid && e.OriginalName == OriginalName).FirstOrDefault();

                    //if (evFileDb != null)
                    if (fileId != null)
                    {
                        EvFile evFileDb = db.EvFile.Where(e => e.Id == fileId).FirstOrDefault();
                        FileHelper.DeleteFile(
                            evFileDb.Name,
                            System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files/ElectronicFile")
                        );
                        evDb.EvFile.Remove(evFileDb);
                        db.EvFile.Remove(evFileDb);
                        //}
                        //if (evDb != null)
                        //{
                    }
                    else
                    {
                        result.message = "Registro no encontrado.";
                    }
                    if (evDb != null)
                    {
                        EvFile evfileDb = db.EvFile.Create();
                        evfileDb.Name = FileName;
                        evfileDb.EvolutionNoteId = Evid;
                        evfileDb.OriginalName = OriginalName;
                        evfileDb.ContentType = ContentType;
                        evfileDb.Created = DateTime.UtcNow;
                        evfileDb.Description = Description;
                        db.EvFile.Add(evfileDb);
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

        public static Result AssignFileToSv(int Evid, string FileName, string OriginalName, string ContentType, string Description, int? fileId)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var esvDb = db.Service.FirstOrDefault(s=>s.id == Evid);
                    EvFile evFileDb = db.EvFile.Where(e => e.ServiceId == Evid && e.OriginalName == OriginalName).FirstOrDefault();

                    if (evFileDb != null)
                    {
                        if (fileId != null)
                        {
                            evFileDb = db.EvFile.Where(e => e.Id == fileId).FirstOrDefault();
                            FileHelper.DeleteFile(
                                evFileDb.Name,
                                System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files")
                            );
                            esvDb.EvFile.Remove(evFileDb);
                            db.EvFile.Remove(evFileDb);
                        }
                        else
                        {
                            result.message = "Registro no encontrado.";
                        }
                    }
                    if (esvDb != null)
                    {
                        EvFile evfileDb = db.EvFile.Create();
                        evfileDb.Name = FileName;
                        evfileDb.ServiceId = Evid;
                        evfileDb.OriginalName = OriginalName;
                        evfileDb.ContentType = ContentType;
                        evfileDb.Created = DateTime.UtcNow;
                        evfileDb.Description = Description;
                        db.EvFile.Add(evfileDb);
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
        public static FileDbResult GetFileDbByAdId(int adId)
        {
            FileDbResult result = new FileDbResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var VarFileDb = db.Advertisement.Where(a => a.id == adId).Select(a => a.FileDb.FirstOrDefault()).FirstOrDefault();
                    if (VarFileDb != null)
                    {
                        DataHelper.fill(result.data, VarFileDb);
                        result.success = true;
                    }
                    else
                    {
                        result.message = "Registro no encontrado.";
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

        public static FileDbResult GetFileDbByEvId(int evId)
        {
            FileDbResult result = new FileDbResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var VarFileDb = db.EvFile.Where(a => a.Id == evId).FirstOrDefault();
                    if (VarFileDb != null)
                    {
                        DataHelper.fill(result.data, VarFileDb);
                        result.success = true;
                    }
                    else
                    {
                        result.message = "Registro no encontrado.";
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

        public static AdResult GetActiveAds()
        {
            AdResult result = new AdResult();
            var now = DateTime.UtcNow;
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Advertisement.Where(pt => !pt.Deleted && (pt.ActiveAlways || (pt.FromDate < now && pt.ToDate >= now)));
                    foreach (Advertisement adDB in query.ToList())
                    {
                        AdsAux aux = new AdsAux();
                        DataHelper.fill(aux, adDB);
                        foreach (Keywords keyword in adDB.Keywords)
                        {
                            KeywordAux auxKey = new KeywordAux();
                            DataHelper.fill(auxKey, keyword);
                            aux.keywordAux.Add(auxKey);
                        }
                        result.data_list.Add(aux);
                    }
                    result.success = true;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Error inesperado. " + result.exception_message;
                }
            }
            return result;
        }
    }
}