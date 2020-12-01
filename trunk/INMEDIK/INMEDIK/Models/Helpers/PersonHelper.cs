using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;
using System.Globalization;

namespace INMEDIK.Models.Helpers
{
    public class PersonAux
    {
        public PersonAux()
        {
            this.addressAux = new AddressAux();
            //this.nationalityAux = new NationalityAux();
        }
        public int id { get; set; }
        public int addressId { get; set; }
        public AddressAux addressAux { get; set; }
        //public NationalityAux nationalityAux { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string secondlastname { get; set; }
        public string curp { get; set; }
        public string License { get; set; }
        public string University { get; set; }
        public string socialSecurity { get; set; }
        public string fullName
        {
            get
            {
                return (name + " " + lastName + " " + secondlastname).Trim();
            }
        }
        public DateTime birthDate { get; set; }
        public string birthDate_string
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(birthDate.ToUniversalTime().Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dddd dd/MM/yyyy", new CultureInfo("es-MX"));
            }
        }
        public int age
        {
            get
            {
                return PersonHelper.GetAge(DateTime.UtcNow, birthDate);
            }
        }
        public string sex { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string mobile { get; set; }
        public bool deleted { get; set; }
        public DateTime created { get; set; }
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
        public void fillDB(ref Person person)
        {
            person.BirthDate = this.birthDate;
            person.Name = this.name;
            person.LastName = this.lastName;
           // person.SecondLastName = this.secondlastname;
            person.Sex = this.sex;
            person.Email = this.email;
            person.PhoneNumber = this.phoneNumber;
            person.Mobile = this.mobile;
            person.University = this.University;
            person.License = this.License;
            person.Deleted = this.deleted;
            person.SocialSecurity = this.socialSecurity;
            //person.Curp = this.curp;
        }
    }

    public class PersonResult : Result
    {
        public PersonAux data { get; set; }

        public List<PersonAux> data_list { get; set; }

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
        public PersonResult()
        {
            data = new PersonAux();
            data_list = new List<PersonAux>();
            this.total = new NumericResult();
        }
    }
    public class PersonHelper
    {
        public static PersonResult GetPerson(int id)
        {
            PersonResult result = new PersonResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Person personDB = db.Person.Where(d => d.id == id).FirstOrDefault();
                    if (personDB != null)
                    {
                        DataHelper.fill(result.data, personDB);
                        DataHelper.fill(result.data.addressAux, personDB.Address);
                        DataHelper.fill(result.data.addressAux.countyAux, personDB.Address.County);
                        DataHelper.fill(result.data.addressAux.countyAux.cityAux, personDB.Address.County.City);
                        DataHelper.fill(result.data.addressAux.countyAux.cityAux.stateAux, personDB.Address.County.City.State);
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Persona no encontrada";
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

        public static int GetAge(DateTime reference, DateTime birthday)
        {
            int age = reference.Year - birthday.Year;
            if (reference < birthday.AddYears(age)) age--;

            return age;
        }

        public static PersonResult GetPersons(DTParameterModel filter, bool includePatients = true)
        {
            PersonResult result = new PersonResult();

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
                    var query = db.Person.Where(pt => !pt.Deleted);
                    if (!includePatients)
                    {
                        query = query.Where(p => !p.Patient.Any());
                    }
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
                        if (column.Data == "lastName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.LastName.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "sex" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Sex.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "email" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Email.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "phoneNumber" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.PhoneNumber.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "mobile" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Mobile.ToString().Contains(column.Search.Value));
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
                        if (orderColumn == "lastname")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.LastName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.LastName);
                            }
                        }
                        if (orderColumn == "sex")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Sex);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Sex);
                            }
                        }
                        if (orderColumn == "email")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Email);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Email);
                            }
                        }
                        if (orderColumn == "phoneNumber")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.PhoneNumber);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.PhoneNumber);
                            }
                        }
                        if (orderColumn == "mobile")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Mobile);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Mobile);
                            }
                        }

                    }
                    #endregion

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);
                    foreach (Person personDB in query.ToList())
                    {
                        PersonAux aux = new PersonAux();
                        DataHelper.fill(aux, personDB);
                        DataHelper.fill(aux.addressAux,
                                        personDB.Address);
                        DataHelper.fill(aux.addressAux.countyAux,
                                        personDB.Address.County);
                        DataHelper.fill(aux.addressAux.countyAux.cityAux,
                                        personDB.Address.County.City);
                        DataHelper.fill(aux.addressAux.countyAux.cityAux.stateAux,
                                        personDB.Address.County.City.State);
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
        public static GenericResult DeletePerson(int id)
        {
            GenericResult result = new GenericResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Person personDB = db.Person.Where(d => d.id == id).FirstOrDefault();
                    if (personDB != null)
                    {
                        personDB.Deleted = true;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Persona no encontrada";
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
        public static GenericResult SavePerson(PersonAux person)
        {
            GenericResult result = new GenericResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Person personDB;
                    if (person.id > 0)
                    {
                        personDB = db.Person.Where(d => d.id == person.id).FirstOrDefault();
                        person.fillDB(ref personDB);
                    }
                    else
                    {
                        personDB = new Person();
                        person.fillDB(ref personDB);
                        db.Person.Add(personDB);
                    }
                    db.SaveChanges();
                    result.success = true;
                    result.integer_value = personDB.id;
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