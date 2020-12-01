using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace INMEDIK.Models.Helpers
{
    public class TabletCartAux
    {
        public int id { get; set; }
        public int EmployeeId { get; set; }
        public int PatientId { get; set; }
        public int ClinicId { get; set; }
        public DateTime Created { get; set; }

        public PatientAux PatientAux { get; set; }
        public List <TabletCartConceptAux> TabletCartConceptAux { get; set; }

        public string created_string
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(Created.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MMMM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }

        public TabletCartAux()
        {
            this.PatientAux = new PatientAux();
            this.TabletCartConceptAux = new  List<TabletCartConceptAux>();
        }
    }

    public class TabletCartResult : Result
    {
        public TabletCartAux data { get; set; }
        public List<TabletCartAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public TabletCartResult()
        {
            this.data = new TabletCartAux();
            this.data_list = new List<TabletCartAux>();
            this.total = new NumericResult();

        }
    }

    public class TabletCartConceptAux
    {
        public int Id { get; set; }
        public int TabletCartId { get; set; }
        public int ConceptId { get; set; }
        public int MedicId { get; set; }
        public int ClinicId { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime Created { get; set; }
        public string categoryName { get; set; }

        public ConceptAux ConceptAux { get; set; }

        public TabletCartConceptAux()
        {
            ConceptAux = new ConceptAux();
        }
    }

    public class TabletCartConceptResult : Result
    {
        public TabletCartConceptAux data { get; set; }
        public List<TabletCartConceptAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public TabletCartConceptResult()
        {
            this.data = new TabletCartConceptAux();
            this.data_list = new List<TabletCartConceptAux>();
            this.total = new NumericResult();
        }
    }

    public class TabletCartHelper
    {
        public static TabletCartResult GetConsult(int id, int clinicId)
        {
            TabletCartResult result = new TabletCartResult();


            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    tabletCart tabletCart = db.tabletCart.Where(c => c.id == id).FirstOrDefault();

                    if (tabletCart != null)
                    {
                        result.data = new TabletCartAux();
                        DataHelper.fill(result.data, tabletCart);
                        DataHelper.fill(result.data.PatientAux, tabletCart.Patient);
                        DataHelper.fill(result.data.PatientAux.personAux, tabletCart.Patient.Person);
                        DataHelper.fill(result.data.PatientAux.personAux.addressAux, tabletCart.Patient.Person.Address);
                        DataHelper.fill(result.data.PatientAux.personAux.addressAux.countyAux, tabletCart.Patient.Person.Address.County);


                        //tabletCartConcepts tabletConcepts = db.tabletCartConcepts.Where(q => q.TabletCartId == id).FirstOrDefault();                        
                        var tcConcepts = db.tabletCartConcepts.Where(q => q.TabletCartId == id).FirstOrDefault();
                        foreach (var tcConceptDb in tcConcepts.tabletCart.tabletCartConcepts)
                        {

                            TabletCartConceptAux tab = new TabletCartConceptAux();
                            DataHelper.fill(tab, tcConceptDb);
                            //DataHelper.fill(tab.ConceptAux, tabletConcepts);
                            /*obtenemos los datos del concepto*/
                            ConceptResult ConceptResult = ConceptHelper.GetConcept(tcConceptDb.Concept.id, clinicId);
                            tab.ConceptAux = ConceptResult.data;

                            result.data.TabletCartConceptAux.Add(tab);
                        }
                    }

                    result.success = true;

                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrio un problema inesperado " + result.exception_message;
                }

                return result;
            }
        }

        public static TabletCartResult GetConsults(DTParameterModel filter,int clinicId)
        {
            TabletCartResult result = new TabletCartResult();
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
                    var query = db.tabletCart.Where(pt=> pt.id != 0);
                    // condicion para que solo tome los registros de una clinica especifica
                    query = db.tabletCart.Where(pt => pt.ClinicId == clinicId);
                    foreach (DTColumn column in filter.Columns)
                    {
                        if (column.Data == "PatientAux.personAux.fullName" && !String.IsNullOrEmpty(column.Search.Value))
                        {
                            query = query.Where(q => (q.Patient.Person.Name + " " + q.Patient.Person.LastName).Contains(column.Search.Value));
                        }
                        //if (column.Data == "PatientAux.personAux.lastName" && !String.IsNullOrEmpty(column.Search.Value))
                        //{
                        //    query = query.Where(q => q.Patient.Person.LastName.Contains(column.Search.Value));
                        //}

                    }

                    if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderColumn))
                    {
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
                        if (orderColumn == "id")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Patient.Person.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Patient.Person.Name);
                            }
                        }

                        if (orderColumn == "PatientAux.personAux.name")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Patient.Person.Name);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Patient.Person.Name);
                            }
                        }

                        if (orderColumn == "PatientAux.personAux.lastName")
                        {
                            if (order.ToUpper() == "ASC")
                            {
                                query = query.OrderBy(q => q.Patient.Person.LastName);
                            }
                            else
                            {
                                query = query.OrderByDescending(q => q.Patient.Person.LastName);
                            }
                        }

                    }

                    result.total.value = query.Count();

                    query = query.Skip(filter.Start).Take(filter.Length);

                    foreach(tabletCart cart in query.ToList())
                    {
                        TabletCartAux aux = new TabletCartAux();
                        DataHelper.fill(aux, cart);
                        DataHelper.fill(aux.PatientAux, cart.Patient);
                        DataHelper.fill(aux.PatientAux.personAux, cart.Patient.Person);

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

        public static TabletCartResult Consults(int clinicId)
        {
            TabletCartResult result = new TabletCartResult();

            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var query = db.tabletCart.Where(pt => pt.id != 0);
                    // condicion para que solo tome los registros de una clinica especifica
                    query = db.tabletCart.Where(pt => pt.ClinicId == clinicId);

                    result.total.value = query.Count();

                    foreach (tabletCart cart in query.ToList())
                    {
                        TabletCartAux aux = new TabletCartAux();
                        DataHelper.fill(aux, cart);
                        DataHelper.fill(aux.PatientAux, cart.Patient);
                        DataHelper.fill(aux.PatientAux.personAux, cart.Patient.Person);

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

        public static TabletCartResult getPendingCart(int EmployeeId, int ClinicId)
        {
            TabletCartResult result = new TabletCartResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var cartDb = db.tabletCart.Where(c => c.EmployeeId == EmployeeId && c.ClinicId == ClinicId).FirstOrDefault();
                    if (cartDb != null)
                    {
                        /*obtenemos todos los datos que ocupamos del paciente*/
                        DataHelper.fill(result.data, cartDb);
                        DataHelper.fill(result.data.PatientAux, cartDb.Patient);
                        DataHelper.fill(result.data.PatientAux.personAux, cartDb.Patient.Person);
                        DataHelper.fill(result.data.PatientAux.personAux.addressAux, cartDb.Patient.Person.Address);
                        DataHelper.fill(result.data.PatientAux.personAux.addressAux.countyAux, cartDb.Patient.Person.Address.County);

                        if (cartDb.tabletCartConcepts.Any())
                        {
                            /*recorremos todos los conceptos guardados en el carrito*/
                            foreach (var cartConceptDb in cartDb.tabletCartConcepts.Where(c => !c.Concept.Deleted).ToList())
                            {
                                /*obtenemos todos los datos que ocupamos del concepto del carrito*/
                                TabletCartConceptAux TabletCartConceptAux = new TabletCartConceptAux();
                                DataHelper.fill(TabletCartConceptAux, cartConceptDb);

                                /*obtenemos los datos del concepto*/
                                ConceptResult ConceptResult = ConceptHelper.GetConcept(cartConceptDb.ConceptId);
                                TabletCartConceptAux.ConceptAux = ConceptResult.data;

                                result.data.TabletCartConceptAux.Add(TabletCartConceptAux);

                            }
                        }
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

        public static Result UpdateTemporalCart(TabletCartAux Order,List <TabletCartConceptAux> Consults)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    tabletCart cartDb = new tabletCart();
                    cartDb.EmployeeId = Order.EmployeeId;
                    cartDb.PatientId = Order.PatientId;
                    cartDb.ClinicId = Order.ClinicId;
                    cartDb.Created = DateTime.UtcNow;

                    db.tabletCart.Add(cartDb);                    

                    if (Consults != null)
                    {
                        foreach (var consulta in Consults)
                        {
                            tabletCartConcepts cartConceptsDb = new tabletCartConcepts();
                            cartConceptsDb.TabletCartId = cartDb.id;
                            cartConceptsDb.ConceptId = consulta.ConceptId;
                            cartConceptsDb.MedicId = consulta.MedicId;
                            cartConceptsDb.ClinicId = cartDb.ClinicId;
                            cartConceptsDb.Scheduled = consulta.Scheduled;
                            cartConceptsDb.Created = DateTime.UtcNow;

                            cartDb.tabletCartConcepts.Add(cartConceptsDb);
                        }
                    }
                    
                    db.SaveChanges();
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

        public static GenericResult DeleteTabletCar(int id,int clinicId)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<tabletCartConcepts> cartConceptsDb = db.tabletCartConcepts.Where(c => c.TabletCartId == id).AsQueryable();
                    if (cartConceptsDb.Any())
                    {
                        db.tabletCartConcepts.RemoveRange(cartConceptsDb);
                    }

                    tabletCart cartDb = db.tabletCart.Where(c => c.id == id).FirstOrDefault();
                    if (cartDb != null)
                    {
                        db.tabletCart.Remove(cartDb);
                    }

                    db.SaveChanges();
                    result.success = true;
                    result.bool_value = false;

                    int query = db.tabletCartConcepts.Count(q => q.ClinicId == clinicId);

                    if (query == 0)
                    {
                        result.bool_value = true;
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