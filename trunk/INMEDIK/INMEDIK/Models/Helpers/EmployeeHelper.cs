using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INMEDIK.Models.Entity;
using System.Globalization;
using System.Data.Entity;

namespace INMEDIK.Models.Helpers
{

    public class ScheduleData
    {
        public int day { get; set; }
        public List<int> hours { get; set; }

        public ScheduleData()
        {
            this.hours = new List<int>();
        }
    }

    public class ScheduleResult : Result
    {
        public ScheduleData data { get; set; }
        public List<ScheduleData> data_list { get; set; }

        public ScheduleResult()
        {
            this.data = new ScheduleData();
            this.data_list = new List<ScheduleData>();
        }
    }

    public class EmployeeAux
    {
        public EmployeeAux()
        {
            this.personAux = new PersonAux();
            this.userAux = new UserAux();
            this.clinicAux = new List<ClinicAux>();
            this.specialtyAux = new SpecialtyAux();
        }
        public int id { get; set; }
        public int personId { get; set; }
        public PersonAux personAux { get; set; }
        public int userId { get; set; }
        public UserAux userAux { get; set; }
        public int clinicId { get; set; }
        public List<ClinicAux> clinicAux { get; set; }
        public int? specialtyId { get; set; }
        public SpecialtyAux specialtyAux { get; set; }
        public string code { get; set; }
        public bool deleted { get; set; }
        public bool canCancel { get; set; }
        public string active
        {
            get
            {
                return deleted ? "No" : "Si";
            }
        }
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
        public HttpPostedFileBase signature { get; set; }
        public string signatureBytes { get; set; }

        public void fillDB(ref Employee employee)
        {
            employee.Code = this.code;
            employee.Deleted = this.deleted;
        }
    }
    public class EmployeeResult : Result
    {
        public EmployeeAux data { get; set; }
        public List<EmployeeAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public EmployeeResult()
        {
            this.data = new EmployeeAux();
            this.data_list = new List<EmployeeAux>();
            this.total = new NumericResult();
        }
    }
    public class EmployeeHelper
    {
        public static GenericResult GetEmployeePermised(string account)
        {
            GenericResult result = new GenericResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Employee employeeDB = db.Employee.Where(d => d.User.UserAccount == account).FirstOrDefault();//toma como variable la cuenta del usuario y alamacena en employeeDB

                    if (employeeDB != null)
                    {
                        if (employeeDB.User.Role.FirstOrDefault().Name == "Admin")
                        {
                            result.bool_value = true;
                        }
                        else
                        {
                            result.bool_value = !employeeDB.canCancel.HasValue ? false : employeeDB.canCancel.Value; //utilizando employeeDB identifica si el usuario tiene permisos o no y la guarda en result.bool_value
                        }
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
        public static EmployeeResult GetEmployee(int id)
        {
            EmployeeResult result = new EmployeeResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Employee employeeDB = db.Employee.Where(d => d.id == id).FirstOrDefault();
                    if (employeeDB != null)
                    {
                        //Employee
                        DataHelper.fill(result.data, employeeDB);
                        //Person
                        DataHelper.fill(result.data.personAux,
                                        employeeDB.Person);
                        DataHelper.fill(result.data.personAux.addressAux,
                                        employeeDB.Person.Address);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux,
                                        employeeDB.Person.Address.County);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux.cityAux,
                                        employeeDB.Person.Address.County.City);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux.cityAux.stateAux,
                                        employeeDB.Person.Address.County.City.State);
                        //User
                        result.data.userAux.fill(employeeDB.User);
                        GenericResult signature =
                                FileHelper.GetFile(
                                    employeeDB.id + "_Signature.jpg",
                                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Images")
                                );
                        if (signature.success)
                        {
                            result.data.signatureBytes = signature.string_value;
                        }
                        //Clinic
                        foreach (Clinic clinicDB in employeeDB.Clinic)
                        {
                            ClinicAux aux = new ClinicAux();
                            DataHelper.fill(aux, clinicDB);
                            result.data.clinicAux.Add(aux);
                        }
                        //Specialty
                        if (employeeDB.Specialty != null)
                        {
                            DataHelper.fill(result.data.specialtyAux, employeeDB.Specialty);
                        }

                        result.success = true;
                    }
                    else
                    {
                        result.success = false;

                        result.message = "Empleado no encontrado";
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
        public static EmployeeResult GetEmployeeByIdUser(int id)
        {
            EmployeeResult result = new EmployeeResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {

                    Employee employeeDB = db.Employee.Where(e => e.UserId == id).FirstOrDefault();
                    if (employeeDB != null)
                    {
                        //Employee
                        DataHelper.fill(result.data, employeeDB);
                        //Person
                        DataHelper.fill(result.data.personAux,
                                        employeeDB.Person);
                        DataHelper.fill(result.data.personAux.addressAux,
                                        employeeDB.Person.Address);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux,
                                        employeeDB.Person.Address.County);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux.cityAux,
                                        employeeDB.Person.Address.County.City);
                        DataHelper.fill(result.data.personAux.addressAux.countyAux.cityAux.stateAux,
                                        employeeDB.Person.Address.County.City.State);
                        //User
                        result.data.userAux.fill(employeeDB.User);
                        GenericResult signature =
                                FileHelper.GetFile(
                                    employeeDB.id + "_Signature.jpg",
                                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Images")
                                );
                        if (signature.success)
                        {
                            result.data.signatureBytes = signature.string_value;
                        }
                        //Clinic
                        foreach (Clinic clinicDB in employeeDB.Clinic)
                        {
                            ClinicAux aux = new ClinicAux();
                            DataHelper.fill(aux, clinicDB);
                            result.data.clinicAux.Add(aux);
                        }
                        //Specialty
                        if (employeeDB.Specialty != null)
                        {
                            DataHelper.fill(result.data.specialtyAux, employeeDB.Specialty);
                        }

                        result.success = true;
                    }
                    else
                    {
                        result.success = false;

                        result.message = "Empleado no encontrado";
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
        public static ScheduleResult GetEmployeeSchedule(int id)
        {
            ScheduleResult result = new ScheduleResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    if (db.Employee.Where(e => e.id == id).Any())
                    {
                        var days = db.DayOfWeek.ToList();
                        foreach (var day in days)
                        {
                            ScheduleData data = new ScheduleData();
                            data.day = day.DayNumber;
                            foreach (var time in db.Schedule.Where(s => s.EmployeeId == id && s.DayOfWeekId == day.id).Select(s => s.HoursOfDay))
                            {
                                data.hours.Add(time.HourOrder);
                            }
                            result.data_list.Add(data);
                        }
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Empleado no encontrado";
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
        public static Result SaveEmployeeSchedule(int idEmployee, List<ScheduleData> schedule)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
                    if (userRes.User.isDemo.HasValue && userRes.User.isDemo.Value)
                    {
                        throw new Exception("Su usuario no tiene permisos para realizar esta acción.");
                    }
                    var employee = db.Employee.Where(e => e.id == idEmployee).FirstOrDefault();
                    if (employee != null)
                    {
                        employee.Schedule.Clear();
                        foreach (ScheduleData element in schedule)
                        {
                            var day = db.DayOfWeek.Where(d => d.DayNumber == element.day).FirstOrDefault();
                            foreach (var hour in db.HoursOfDay.Where(hd => element.hours.Contains(hd.HourOrder)))
                            {
                                Schedule dbSchedule = new Schedule();
                                dbSchedule.DayOfWeekId = day.id;
                                dbSchedule.HourOfDayId = hour.id;
                                employee.Schedule.Add(dbSchedule);
                            }
                        }
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Empleado no encontrado.";
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

        public static EmployeeResult GetEmployeeViews(int id)
        {
            EmployeeResult result = new EmployeeResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Employee employeeDB = db.Employee.Where(d => d.id == id).FirstOrDefault();
                    if (employeeDB != null)
                    {
                        //Employee
                        DataHelper.fill(result.data, employeeDB);

                        //User
                        result.data.userAux.fill(employeeDB.User);
                        foreach (MenuView menuView in employeeDB.User.MenuView)
                        {
                            MenuViewAux aux = new MenuViewAux();//carga menuviews
                            DataHelper.fill(aux, menuView);
                            result.data.userAux.menuViewAux.Add(aux);
                        }

                        result.success = true;
                    }
                    else
                    {
                        result.success = false;

                        result.message = "Empleado no encontrado";
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
        public static GenericResult SaveEmployeeViews(EmployeeAux employee)
        {
            GenericResult result = new GenericResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
                    if (userRes.User.isDemo.HasValue && userRes.User.isDemo.Value)
                    {
                        throw new Exception("Su usuario no tiene permisos para realizar esta acción.");
                    }
                    Employee employeeDB = db.Employee.Where(d => d.id == employee.id).FirstOrDefault();
                    if (employeeDB != null)
                    {

                        employeeDB.User.MenuView.Clear();

                        foreach (var views in employee.userAux.menuViewAux)
                        {
                            employeeDB.User.MenuView.Add(db.MenuView.Where(m => m.id == views.id).FirstOrDefault());

                        }
                        employeeDB.canCancel = employee.canCancel;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;

                        result.message = "Empleado no encontrado";
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
        public static EmployeeResult GetEmployees(DTParameterModel filter)
        {
            EmployeeResult result = new EmployeeResult();

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
                    var query = db.Employee.AsQueryable();

                    UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
                    //Si eres usuario demo te debe mostrar usuarios creados por ti o a ti mismo
                    if (userRes.User.isDemo.HasValue && userRes.User.isDemo.Value)
                    {
                        query = query.Where(q => q.User.CreatedBy == userRes.Id || q.UserId == userRes.Id);
                    }
                    #region filtros
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "id" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.id.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "personAux.name" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Person.Name.Contains(column.Search.Value));
                        }
                        if (column.Data == "personAux.lastName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Person.LastName.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "personAux.sex" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.Person.Sex.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "userAux.account" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.User.UserAccount.ToString().Contains(column.Search.Value));
                        }
                        if (column.Data == "userAux.rolAux.description" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => q.User.Role.FirstOrDefault().Description.ToString().Contains(column.Search.Value));
                        }
                    }
                    #endregion
                    #region orden
                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
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
                        if (orderColumn == "userAux.rolAux.name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.User.Role.FirstOrDefault().Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.User.Role.FirstOrDefault().Name);
                            }
                        }
                        if (orderColumn == "userAux.account")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.User.UserAccount);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.User.UserAccount);
                            }
                        }
                        if (orderColumn == "active")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Deleted);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Deleted);
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
                    foreach (Employee employeeDB in query.ToList())
                    {
                        EmployeeAux aux = new EmployeeAux();
                        DataHelper.fill(aux, employeeDB);
                        if (employeeDB.Specialty != null)
                        {
                            DataHelper.fill(aux.specialtyAux,
                                            employeeDB.Specialty);
                        }
                        DataHelper.fill(aux.personAux,
                                        employeeDB.Person);
                        DataHelper.fill(aux.personAux.addressAux,
                                        employeeDB.Person.Address);
                        DataHelper.fill(aux.personAux.addressAux.countyAux,
                                        employeeDB.Person.Address.County);
                        DataHelper.fill(aux.personAux.addressAux.countyAux.cityAux,
                                        employeeDB.Person.Address.County.City);
                        DataHelper.fill(aux.personAux.addressAux.countyAux.cityAux.stateAux,
                                        employeeDB.Person.Address.County.City.State);
                        //User
                        aux.userAux.fill(employeeDB.User);
                        //Clinic
                        foreach (Clinic clinicDB in employeeDB.Clinic)
                        {
                            ClinicAux clinic = new ClinicAux();
                            DataHelper.fill(clinic, clinicDB);
                            aux.clinicAux.Add(clinic);
                        }

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
        public static GenericResult DeleteEmployee(int id)
        {
            GenericResult result = new GenericResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
                    if (userRes.User.isDemo.HasValue && userRes.User.isDemo.Value)
                    {
                        throw new Exception("Su usuario no tiene permisos para realizar esta acción.");
                    }
                    Employee employeeDB = db.Employee.Where(d => d.id == id).FirstOrDefault();
                    if (employeeDB != null)
                    {
                        employeeDB.Deleted = true;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Empleado no encontrado";
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
        public static GenericResult ToggleEmployee(int id)
        {
            GenericResult result = new GenericResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
                    if (userRes.User.isDemo.HasValue && userRes.User.isDemo.Value)
                    {
                        throw new Exception("Su usuario no tiene permisos para realizar esta acción.");
                    }
                    Employee employeeDB = db.Employee.Where(d => d.id == id).FirstOrDefault();
                    if (employeeDB != null)
                    {
                        employeeDB.Deleted = !employeeDB.Deleted;
                        db.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "Empleado no encontrado";
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
        public static GenericResult SaveEmployee(EmployeeAux employee)
        {
            GenericResult result = new GenericResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                using (DbContextTransaction dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        UserResult userRes = UserHelper.GetUser(HttpContext.Current.User.Identity.Name);
                        if (userRes.User.isDemo.HasValue && userRes.User.isDemo.Value)
                        {
                            throw new Exception("Su usuario no tiene permisos para realizar esta acción.");
                        }
                        Employee employeeDB;

                        if (employee.id > 0)
                        {
                            employeeDB = db.Employee.Where(d => d.id == employee.id).FirstOrDefault();
                            employee.fillDB(ref employeeDB);

                            Person person = employeeDB.Person;
                            employee.personAux.fillDB(ref person);
                            person.SecondLastName = employee.personAux.secondlastname;

                            Address address = person.Address;
                            employee.personAux.addressAux.fillDB(ref address);

                            User user = employeeDB.User;
                            employee.userAux.fillDB(ref user);

                            employeeDB.User.Role.Clear();
                            Role roleDB = db.Role.Where(i => i.id == employee.userAux.rolAux.id).FirstOrDefault();
                            employeeDB.User.Role.Add(roleDB);

                            employeeDB.Clinic.Clear();
                            foreach (var clinic in employee.clinicAux)
                            {
                                employeeDB.Clinic.Add(db.Clinic.Where(w => w.id == clinic.id).FirstOrDefault());
                                user.CurrentClinicId = clinic.id;
                            }

                            if (employee.specialtyAux.id > 0)
                            {
                                Specialty specialty = db.Specialty.Where(s => s.id == employee.specialtyAux.id).FirstOrDefault();
                                employeeDB.Specialty = specialty;
                            }
                        }
                        else
                        {
                            if (db.User.Any(u => u.UserAccount == employee.userAux.account))
                            {
                                result.success = false;
                                result.message = "Ya existe un usuario con la cuenta " + employee.userAux.account + ". Por favor utilice una diferente.";
                                return result;
                            }
                            employeeDB = db.Employee.Create();
                            Person person;
                            Address address;
                            User user;

                            person = db.Person.Create();
                            employee.personAux.fillDB(ref person);
                            person.SecondLastName = employee.personAux.secondlastname;

                            address = db.Address.Create();
                            employee.personAux.addressAux.fillDB(ref address);

                            user = db.User.Create();
                            employee.userAux.fillDB(ref user);

                            if (employee.specialtyAux.id > 0)
                            {
                                Specialty specialty = db.Specialty.Where(s => s.id == employee.specialtyAux.id).FirstOrDefault();
                                employeeDB.Specialty = specialty;
                            }

                            person.Address = address;
                            person.License = person.License;
                            person.University = person.University;
                            employee.fillDB(ref employeeDB);
                            employeeDB.Person = person;
                            employeeDB.User = user;
                            Role roleDB = db.Role.Where(i => i.id == employee.userAux.rolAux.id).FirstOrDefault();
                            employeeDB.User.Role.Add(roleDB);

                            foreach (var menuView in roleDB.MenuView)
                            {
                                employeeDB.User.MenuView.Add(menuView);
                            }

                            foreach (var clinic in employee.clinicAux)
                            {
                                employeeDB.Clinic.Add(db.Clinic.Where(w => w.id == clinic.id).FirstOrDefault());
                                user.CurrentClinicId = clinic.id;
                            }

                            if (!employeeDB.User.MenuView.Any())
                            {
                                employeeDB.User.MenuView.Add(db.MenuView.FirstOrDefault(m => m.Controller == "Dashboard"));
                            }

                            db.Employee.Add(employeeDB);
                        }

                        db.SaveChanges();

                        if (employee.signature != null)
                        {
                            GenericResult saveSignature =
                                FileHelper.SaveFile(
                                    employee.signature.InputStream,
                                    employeeDB.id + "_Signature.jpg",
                                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Images")
                                );
                            if (!saveSignature.success)
                            {
                                throw saveSignature.exception;
                            }
                        }
                        result.success = true;
                        dbTransaction.Commit();
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
        public static EmployeeResult GetMedicsBySpecialty(int idSpecialty)
        {
            EmployeeResult result = new EmployeeResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    List<Employee> employeesDB = db.Employee.Where(e => e.User.Role.Any(r => r.Name == "Medic") && e.SpecialtyId == idSpecialty).ToList();
                    foreach (var employeeDB in employeesDB)
                    {
                        EmployeeAux aux = new EmployeeAux();
                        DataHelper.fill(aux, employeeDB);
                        DataHelper.fill(aux.personAux, employeeDB.Person);
                        result.data_list.Add(aux);
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
        public static EmployeeResult GetMedicsBySpecialtyList(string typed, int specialtyId)
        {
            EmployeeResult result = new EmployeeResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.Employee.Where(em => !em.Deleted && ((em.Person.Name + " " + em.Person.LastName).Contains(typed) || em.id.ToString() == typed) && em.SpecialtyId == specialtyId);
                    result.total.value = query.Count();
                    query = query.Take(15);

                    foreach(Employee employeeListDB in query.ToList())
                    {
                        EmployeeAux aux = new EmployeeAux();
                        DataHelper.fill(aux, employeeListDB);
                        DataHelper.fill(aux.personAux, employeeListDB.Person);
                        DataHelper.fill(aux.specialtyAux, employeeListDB.Specialty);
                        result.data_list.Add(aux);
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
        public static EmployeeResult GetEmployeesByRol(string RoleName)
        {
            EmployeeResult result = new EmployeeResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    List<Employee> employeesDB = db.Employee.Where(e => e.User.Role.Any(r => r.Name == RoleName)).ToList();
                    foreach (var employeeDB in employeesDB)
                    {
                        EmployeeAux aux = new EmployeeAux();
                        DataHelper.fill(aux, employeeDB);
                        DataHelper.fill(aux.personAux, employeeDB.Person);
                        result.data_list.Add(aux);
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

        public static EmployeeResult GetEmployeesSelect()
        {
            EmployeeResult result = new EmployeeResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    List<Employee> employeesDB = db.Employee.Where(e => !e.Deleted).OrderBy(e => e.Person.Name + e.Person.LastName).ToList();
                    foreach (var employeeDB in employeesDB)
                    {
                        EmployeeAux aux = new EmployeeAux();
                        DataHelper.fill(aux, employeeDB);
                        DataHelper.fill(aux.personAux, employeeDB.Person);
                        result.data_list.Add(aux);
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