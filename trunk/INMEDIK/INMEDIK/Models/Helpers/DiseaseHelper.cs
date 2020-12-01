using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;
using System.Globalization;

namespace INMEDIK.Models.Helpers
{
    public class DiseaseAux
    {
        public int id { get; set; } = 0;
        public string name { get; set; }
        public bool deleted { get; set; }
        public DateTime created { get; set; }
        public string created_string {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(created.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public DateTime updated { get; set; }
        public string updated_string {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(updated.Ticks, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
                ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }

        public void fillDB(ref Disease disease)
        {
            disease.Name = this.name;
            disease.Deleted = this.deleted;
        }
    }
    public class DiseaseResult : Result
    {
        public DiseaseAux data { get; set; }

        public List<DiseaseAux> data_list { get; set; }

        public int Id
        {
            get
            {
                if (success)
                {
                    return data.id;
                }
                else
                {
                    return -1;
                }
            }
        }
        public NumericResult total { get; set; }
        public DiseaseResult()
        {
            data = new DiseaseAux();
            data_list = new List<DiseaseAux>();
            this.total = new NumericResult();
        }
    }

    public class DiseaseHelper
    {
        public static DiseaseResult GetDiseases(bool? deleted = null)
        {
            DiseaseResult result = new DiseaseResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<Disease> diseasesDB = db.Disease;
                    if (deleted.HasValue)
                    {
                        diseasesDB = diseasesDB.Where(d => d.Deleted == deleted);
                    }

                    foreach (Disease diseaseDB in diseasesDB)
                    {
                        DiseaseAux disease = new DiseaseAux();
                        DataHelper.fill(disease, diseaseDB);
                        result.data_list.Add(disease);
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
        public static DiseaseResult GetDiseases(DTParameterModel filter)
        {
            DiseaseResult result = new DiseaseResult();

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
                    var query = db.Disease.Where(pt => !pt.Deleted);
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
                    } 
                    #endregion

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (Disease diseaseDB in query.ToList())
                    {
                        DiseaseAux aux = new DiseaseAux();
                        DataHelper.fill(aux, diseaseDB);
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
        public static DiseaseResult GetDisease(int id)
        {
            DiseaseResult result = new DiseaseResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Disease diseaseDB = db.Disease.Where(d => d.id == id).FirstOrDefault();
                    if (diseaseDB != null)
                    {
                        DataHelper.fill(result.data, diseaseDB);
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Enfermedad no encontrada";
                    }

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
        public static DiseaseResult SaveDisease(DiseaseAux disease)
        {
            DiseaseResult result = new DiseaseResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Disease diseaseDB;
                    if (disease.id > 0)
                    {
                        diseaseDB = db.Disease.Where(d => d.id == disease.id).FirstOrDefault();
                        disease.fillDB(ref diseaseDB);
                    }
                    else
                    {
                        diseaseDB = new Disease();
                        disease.fillDB(ref diseaseDB);
                        db.Disease.Add(diseaseDB);
                    }
                    db.SaveChanges();
                    DataHelper.fill(result.data, diseaseDB);
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
        public static GenericResult DeleteDisease(int id)
        {
            GenericResult result = new GenericResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Disease diseaseDB = db.Disease.Where(d => d.id == id).FirstOrDefault();
                    if (diseaseDB != null)
                    {
                        diseaseDB.Deleted = true;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Enfermedad no encontrada";
                    }
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
    }
}