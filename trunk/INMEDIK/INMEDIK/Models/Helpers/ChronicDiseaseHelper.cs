using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;
using System.Globalization;

namespace INMEDIK.Models.Helpers
{
    public class ChronicDiseaseAux : DiseaseAux
    {
        public string code { get; set; }
    }
    public class ChronicDiseaseResult : Result
    {
        public ChronicDiseaseAux data { get; set; }

        public List<ChronicDiseaseAux> data_list { get; set; }

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
        public ChronicDiseaseResult()
        {
            data = new ChronicDiseaseAux();
            data_list = new List<ChronicDiseaseAux>();
            this.total = new NumericResult();
        }
    }

    public class ChronicDiseaseHelper
    {
        public static ChronicDiseaseResult GetChronicDiseases(bool? deleted = null)
        {
            ChronicDiseaseResult result = new ChronicDiseaseResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<ChronicDisease> diseasesDB = db.ChronicDisease;
                    if (deleted.HasValue)
                    {
                        diseasesDB = diseasesDB.Where(d => d.Deleted == deleted);
                    }

                    foreach (ChronicDisease diseaseDB in diseasesDB)
                    {
                        ChronicDiseaseAux disease = new ChronicDiseaseAux();
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
    }
}