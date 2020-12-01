using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;
using System.Globalization;
using System.Data.Entity;

namespace INMEDIK.Models.Helpers
{
    public class PatientAux
    {
        public PatientAux()
        {
            this.personAux = new PersonAux();
            this.LaboratoryAux = new List<LaboratoryAux>();
            this.clinicAux = new ClinicAux();
            this.ahfaux = new AHFAux();
            this.appAux = new APPAux();
            this.apnpAux = new APNPAux();
            this.identificationAux = new IdentificationAux();
        }
        public int id { get; set; }
        public int personId { get; set; }
        public PersonAux personAux { get; set; }
        public string reference { get; set; }
        public DateTime created { get; set; }
        public bool deleted { get; set; }
        public ClinicAux clinicAux { get; set; }
        public IdentificationAux identificationAux { get; set; }
        public AHFAux ahfaux { get; set; }
        public APPAux appAux { get; set; }
        public APNPAux apnpAux { get; set; }

        public List<LaboratoryAux> LaboratoryAux { get; set; }
        public string created_string
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(created.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dddd dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public DateTime updated { get; set; }
        public string updated_string
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(updated.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dddd dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public void fillDB(ref Patient patient)
        {
            patient.Reference = this.reference;
            patient.Deleted = this.deleted;
            
        }
    }
    public class PatientResult : Result
    {
        public PatientAux data { get; set; }

        public List<PatientAux> data_list { get; set; }

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
        public PatientResult()
        {
            data = new PatientAux();
            data_list = new List<PatientAux>();
            this.total = new NumericResult();
        }
    }
    public class PatientHelper
    {
        public static PatientResult GetPublicPatient()
        {
            PatientResult result = new PatientResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Patient publicPatient = db.Patient.Where(d => d.Person.Name == "PÚBLICO" && d.Person.LastName == "GENERAL").FirstOrDefault();
                    if (publicPatient != null)
                    {
                        DataHelper.fill(result.data, publicPatient);
                        DataHelper.fill(result.data.personAux, publicPatient.Person);
                        DataHelper.fill(result.data.personAux.addressAux,
                                        publicPatient.Person.Address);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux,
                                        publicPatient.Person.Address.County);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux.cityAux,
                                        publicPatient.Person.Address.County.City);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux.cityAux.stateAux,
                                        publicPatient.Person.Address.County.City.State);
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Paciente no encontrado";
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
        public static PatientResult GetPatient(int id)
        {
            PatientResult result = new PatientResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Patient patientDB = db.Patient.Where(d => d.id == id).FirstOrDefault();
                    if (patientDB != null)
                    {
                        DataHelper.fill(result.data, patientDB);
                        DataHelper.fill(result.data.personAux, patientDB.Person);
                        DataHelper.fill(result.data.personAux.addressAux,
                                        patientDB.Person.Address);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux,
                                        patientDB.Person.Address.County);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux.cityAux,
                                        patientDB.Person.Address.County.City);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux.cityAux.stateAux,
                                        patientDB.Person.Address.County.City.State);
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;

                        result.message = "Paciente no encontrado";
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
        public static PatientResult GetPatients(DTParameterModel filter)
        {
            PatientResult result = new PatientResult();

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
                    var query = db.Patient.Where(pt => !pt.Deleted);
                    #region filtros
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "personAux.name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Person.Name.Contains(column.Search.Value));
                        }
                        if (column.Data == "id" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.id.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "personAux.lastName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Person.LastName.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "personAux.sex" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Person.Sex.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "personAux.birthDate_string" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Person.BirthDate.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "reference" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Reference.ToString().Contains(column.Search.Value));
                        }
                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
                        if (orderColumn == "personAux.name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Person.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Person.Name);
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
                        if (orderColumn == "personAux.lastname")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Person.LastName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Person.LastName);
                            }
                        }
                        if (orderColumn == "personAux.sex")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Person.Sex);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Person.Sex);
                            }
                        }
                        if (orderColumn == "personAux.birthDate_string")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Person.BirthDate);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Person.BirthDate);
                            }
                        }
                        if (orderColumn == "reference")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Reference);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Reference);
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
                    foreach (Patient patientDB in query.ToList())
                    {
                        PatientAux aux = new PatientAux();
                        DataHelper.fill(aux, patientDB);
                        DataHelper.fill(aux.personAux, patientDB.Person);
                        DataHelper.fill(aux.personAux.addressAux,
                                        patientDB.Person.Address);
                        DataHelper.fill(aux.personAux.addressAux.countyAux,
                                        patientDB.Person.Address.County);
                        DataHelper.fill(aux.personAux.addressAux.countyAux.cityAux,
                                        patientDB.Person.Address.County.City);
                        DataHelper.fill(aux.personAux.addressAux.countyAux.cityAux.stateAux,
                                        patientDB.Person.Address.County.City.State);
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
        public static PatientResult GetPatientsList(string data)
        {            
            PatientResult result = new PatientResult();
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
                    //WaitingTimes waitingData = new WaitingTimes();
                    //waitingData.AttendingTimePatientRecS = DateTime.UtcNow;
                    //db.WaitingTimes.Add(waitingData);
                    //db.SaveChanges();

                    var query = db.Patient.Where(pt => !pt.Deleted && ((pt.Person.Name + " " + pt.Person.LastName).Contains(data) || pt.id.ToString() == data));
                    result.total.value = query.Count();
                    query = query.Take(10);

                    foreach (Patient patientDB in query.ToList())
                    {
                        PatientAux aux = new PatientAux();
                        DataHelper.fill(aux, patientDB);
                        DataHelper.fill(aux.personAux, patientDB.Person);
                        DataHelper.fill(aux.personAux.addressAux, patientDB.Person.Address);
                        DataHelper.fill(aux.personAux.addressAux.countyAux, patientDB.Person.Address.County);

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
        public static PatientResult GetPatientsListF(string data, int clinicId)
        {
            PatientResult result = new PatientResult();
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
                    var patientsIds = db.vwPatientsId.ToList();
                    List<int> Ids = new List<int>();
                    foreach (var item in patientsIds)
                    {
                        Ids.Add(item.id);
                    }

                    var query = db.Patient.Where(pt => ((pt.Person.Name + " " + pt.Person.LastName).Contains(data) || pt.id.ToString() == data) && Ids.Contains(pt.id));
                    result.total.value = query.Count();
                    query = query.Take(10);

                    foreach (Patient patientDB in query.ToList())
                    {
                        PatientAux aux = new PatientAux();
                        DataHelper.fill(aux, patientDB);
                        DataHelper.fill(aux.personAux, patientDB.Person);
                        DataHelper.fill(aux.personAux.addressAux, patientDB.Person.Address);
                        DataHelper.fill(aux.personAux.addressAux.countyAux, patientDB.Person.Address.County);

                        //if (patientDB.Person.Nationality != null)
                        //{
                        //    DataHelper.fill(aux.personAux.nationalityAux, patientDB.Person.Nationality);
                        //}
                        if (patientDB.AHF.Count > 0)
                        {
                            DataHelper.fill(aux.ahfaux, patientDB.AHF.Last());
                            DataHelper.fill(aux.ahfaux.commentsAux, patientDB.AHF.First().AHFComments);
                            foreach (var family in patientDB.AHF.Last().AHFFamily)
                            {
                                AHFFamilyAux temp = new AHFFamilyAux();
                                DataHelper.fill(temp, family);

                                foreach (var disease in family.AHFDisease)
                                {
                                    AHFDiseaseAux diseasetemp = new AHFDiseaseAux();
                                    DataHelper.fill(diseasetemp, disease);
                                    temp.diseaseAux.Add(diseasetemp);
                                }
                                aux.ahfaux.familyAux.Add(temp);
                            }
                        }
                        EmployeeAux eAux = new EmployeeAux();

                        Employee employeeDb = db.Employee.Where(e => e.UserId == res.User.id.Value).FirstOrDefault();
                        if (employeeDb != null)
                        {
                            DataHelper.fill(eAux, employeeDb);
                            DataHelper.fill(eAux.personAux, employeeDb.Person);
                        }

                        Clinic clinicDb = db.Clinic.Where(c => c.id == clinicId).FirstOrDefault();


                        aux.identificationAux.created_date = DateTime.UtcNow.ToString("dd/MM/yyyy", new CultureInfo("es-MX"));
                        //aux.identificationAux.expFolio = patientAux.id.ToString();
                        aux.identificationAux.medicId = eAux.id;
                        aux.identificationAux.medicName = eAux.personAux.fullName;
                        aux.identificationAux.patientFullName = aux.personAux.name + " " + aux.personAux.lastName + " " + aux.personAux.secondlastname;
                        aux.identificationAux.curp = aux.personAux.curp;
                        aux.identificationAux.birthdate_string = aux.personAux.birthDate.ToString("dd/MM/yyyy", new CultureInfo("es-MX"));
                        if (aux.personAux.sex == "M")
                        {
                            aux.identificationAux.sex_string = "Masculino";
                        }
                        else if (aux.personAux.sex == "F")
                        {
                            aux.identificationAux.sex_string = "Femenino";
                        }
                        else
                        {
                            result.data.identificationAux.sex_string = "No definido";
                        }
                        //if (aux.personAux.nationalityAux != null)
                        //{
                        //    aux.identificationAux.nationality_string = aux.personAux.nationalityAux.name;
                        //}
                        //else
                        //{
                        //    aux.identificationAux.nationality_string = "S/N";
                        //}
                        aux.identificationAux.age_string = aux.personAux.age.ToString();
                        if (clinicDb != null)
                        {
                            aux.identificationAux.clinicName = clinicDb.Name;
                            aux.identificationAux.clinicId = clinicDb.id;
                        }
                        else
                        {
                            aux.identificationAux.clinicName = "S/N";
                            aux.identificationAux.clinicId = 1;
                        }
                        //aux.identificationAux.consultId = consultAux.id;

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
        public static GenericResult DeletePatient(int id)
        {
            GenericResult result = new GenericResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Patient patientDB = db.Patient.Where(d => d.id == id).FirstOrDefault();
                    if (patientDB != null)
                    {
                        patientDB.Deleted = true;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Paciente no encontrado";
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
        public static PatientResult SavePatient(PatientAux patient)
        {
            PatientResult result = new PatientResult();


            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Patient patientDB;

                    if (patient.id > 0)
                    {
                        patientDB = db.Patient.Where(d => d.id == patient.id).FirstOrDefault();
                        patient.fillDB(ref patientDB);

                        Person person = patientDB.Person;
                        patient.personAux.fillDB(ref person);
                        person.Curp = patient.personAux.curp;
                        person.SecondLastName = patient.personAux.secondlastname;
                        //patientDB.Person.NationalityId = patient.personAux.nationalityAux.id;

                        Address address = person.Address;
                        patient.personAux.addressAux.fillDB(ref address);
                    }
                    else
                    {
                        patientDB = db.Patient.Create();
                        Person person;
                        Address address;
                        if (patient.personAux.id > 0)
                        {
                            person = db.Person.Where(p => p.id == patient.personAux.id).FirstOrDefault();
                            patient.personAux.fillDB(ref person);
                            person.Curp = patient.personAux.curp;
                            person.SecondLastName = patient.personAux.secondlastname;
                            address = db.Address.Where(a => a.id == patient.personAux.addressId).FirstOrDefault();
                        }
                        else
                        {
                            person = db.Person.Create();
                            patient.personAux.fillDB(ref person);
                            person.Curp = patient.personAux.curp;
                            person.SecondLastName = patient.personAux.secondlastname;
                            address = db.Address.Create();
                        }
                        patient.personAux.addressAux.fillDB(ref address);

                        person.Address = address;
                        patient.fillDB(ref patientDB);
                        patientDB.Person = person;
                        //patientDB.Person.NationalityId = patient.personAux.nationalityAux.id;
                        db.Patient.Add(patientDB);
                    }
                    db.SaveChanges();
                    patientDB = db.Patient.Where(d => d.id == patientDB.id).FirstOrDefault();
                    if (patientDB != null)
                    {
                        DataHelper.fill(result.data, patientDB);
                        DataHelper.fill(result.data.personAux, patientDB.Person);
                        DataHelper.fill(result.data.personAux.addressAux,
                                        patientDB.Person.Address);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux,
                                        patientDB.Person.Address.County);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux.cityAux,
                                        patientDB.Person.Address.County.City);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux.cityAux.stateAux,
                                        patientDB.Person.Address.County.City.State);
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
        public static GenericResult CreateBulkPatients(List<PatientAux> patients)
        {
            GenericResult result = new GenericResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (PatientAux patient in patients)
                        {
                            Patient patientDB = db.Patient.Create();
                            Person person;
                            Address address;
                            if (patient.personAux.id > 0)
                            {
                                person = db.Person.Where(p => p.id == patient.personAux.id).FirstOrDefault();
                                patient.personAux.fillDB(ref person);
                                address = db.Address.Where(a => a.id == patient.personAux.addressId).FirstOrDefault();
                            }
                            else
                            {
                                person = db.Person.Create();
                                patient.personAux.fillDB(ref person);
                                address = db.Address.Create();
                            }
                            patient.personAux.addressAux.fillDB(ref address);

                            person.Address = address;
                            patient.fillDB(ref patientDB);
                            patientDB.Person = person;
                            db.Patient.Add(patientDB);

                            db.SaveChanges();
                        }
                        dbTransaction.Commit();
                        result.success = true;
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        result.success = false;
                        result.exception = e;
                        result.message = "Ocurrió un error inesperado. " + result.exception_message;
                    }
                }
            }

            return result;
        }
        public static GenericResult GetPatientAge(int idPatient)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Patient patientDB = db.Patient.Where(d => d.id == idPatient).FirstOrDefault();
                    if (patientDB != null)
                    {
                        int years = DateTime.UtcNow.Year - patientDB.Person.BirthDate.Year;
                        int months = 0;

                        if (patientDB.Person.BirthDate.Month > DateTime.UtcNow.Month)
                        {
                            months = 12 - patientDB.Person.BirthDate.Month + DateTime.UtcNow.Month;
                            years--;
                        }
                        else
                        {
                            months = DateTime.UtcNow.Month - patientDB.Person.BirthDate.Month;
                        }

                        result.string_value = years + " años " + months + " meses";
                        result.success = true;
                    }
                    else
                    {
                        result.message = "no se encontro paciente";
                        result.success = false;
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