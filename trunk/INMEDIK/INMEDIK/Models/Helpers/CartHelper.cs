using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INMEDIK.Models.Helpers
{
    public class CartAux
    {
        public int EmployeeId { get; set; }
        public int PatientId { get; set; }
        public int ClinicId { get; set; }
        public DateTime Created { get; set; }
        public PatientAux PatientAux { get; set; }

        public List<CartConceptAux> CartConceptAux { get; set; }

        public CartAux()
        {
            PatientAux = new PatientAux();
            CartConceptAux = new List<CartConceptAux>();
        }
    }

    public class CartResult : Result
    {
        public CartAux data { get; set; }
        public List<CartAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public CartResult()
        {
            this.data = new CartAux();
            this.data_list = new List<CartAux>();
            this.total = new NumericResult();
        }
    }

    public class CartConceptAux
    {
        public int Id { get; set; }
        public int EmployeeCartId { get; set; }
        public int ConceptId { get; set; }
        public int? MedicId { get; set; }
        public int ClinicId { get; set; }
        public int Quantity { get; set; }
        public DateTime? Scheduled { get; set; }
        public string Medicname { get; set; }
        public string Decree { get; set; }
        public DateTime Created { get; set; }
        public string categoryName { get; set; }

        public ConceptAux ConceptAux { get; set; }

        public CartConceptAux()
        {
            ConceptAux = new ConceptAux();
        }
    }

    //public class CartPackageAux : CartConceptAux
    //{
    //    public PackageAux packageAux { get; set; }
    //    public CartPackageAux()
    //    {
    //        this.packageAux = new PackageAux();
    //    }
    //}

    public class CartConceptResult : Result
    {
        public CartConceptAux data { get; set; }
        public List<CartConceptAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public CartConceptResult()
        {
            this.data = new CartConceptAux();
            this.data_list = new List<CartConceptAux>();
            this.total = new NumericResult();
        }
    }

    public class CartHelper
    {
        public static CartResult getPendingCart(int EmployeeId, int ClinicId)
        {
            CartResult result = new CartResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var cartDb = db.Cart.Where(c => c.EmployeeId == EmployeeId && c.ClinicId == ClinicId).FirstOrDefault();
                    if(cartDb != null)
                    {
                        /*obtenemos todos los datos que ocupamos del paciente*/
                        DataHelper.fill(result.data,cartDb);
                        DataHelper.fill(result.data.PatientAux, cartDb.Patient);
                        DataHelper.fill(result.data.PatientAux.personAux, cartDb.Patient.Person);
                        DataHelper.fill(result.data.PatientAux.personAux.addressAux, cartDb.Patient.Person.Address);
                        DataHelper.fill(result.data.PatientAux.personAux.addressAux.countyAux, cartDb.Patient.Person.Address.County);

                        if (cartDb.CartConcepts.Any())
                        {
                            /*recorremos todos los conceptos guardados en el carrito*/
                            foreach(var cartConceptDb in cartDb.CartConcepts.Where(c => !c.Concept.Deleted).ToList())
                            {
                                /*obtenemos todos los datos que ocupamos del concepto del carrito*/
                                CartConceptAux CartConceptAux = new CartConceptAux();
                                DataHelper.fill(CartConceptAux, cartConceptDb);

                                /*obtenemos los datos del concepto*/
                                ConceptResult ConceptResult = ConceptHelper.GetConcept(cartConceptDb.ConceptId, ClinicId);
                                CartConceptAux.ConceptAux = ConceptResult.data;

                                result.data.CartConceptAux.Add(CartConceptAux);

                            }
                        }

                        if(cartDb.CartPackage.Any())
                        {
                            foreach (var cartPackage in cartDb.CartPackage.Where(c => !c.Package.Deleted).ToList())
                            {
                                CartConceptAux cartPack = new CartConceptAux();
                                DataHelper.fill(cartPack, cartPackage);
                                cartPack.Quantity = 1;
                                PackageResult packageResult = ConceptHelper.GetPackage(cartPackage.PackageId);
                                cartPack.ConceptAux = packageResult.data;
                                result.data.CartConceptAux.Add(cartPack);
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
        public static Result UpdateTemporalCart(CartAux Order, List<CartConceptAux> Concepts)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    Cart cartDb = new Cart();
                    cartDb.EmployeeId = Order.EmployeeId;
                    cartDb.PatientId = Order.PatientId;
                    cartDb.ClinicId = Order.ClinicId;
                    cartDb.Created = DateTime.UtcNow;

                    db.Cart.Add(cartDb);

                    if (Concepts != null)
                    {
                        foreach (var concept in Concepts)
                        {
                            if (concept.categoryName != "Paquetes")
                            {
                                CartConcepts cartConceptsDb = new CartConcepts();
                                cartConceptsDb.EmployeeCartId = Order.EmployeeId;
                                cartConceptsDb.ConceptId = concept.ConceptId;
                                cartConceptsDb.MedicId = concept.MedicId;
                                cartConceptsDb.ClinicId = concept.ClinicId;
                                cartConceptsDb.Quantity = concept.Quantity;
                                cartConceptsDb.Scheduled = concept.Scheduled;
                                cartConceptsDb.Medicname = (concept.Medicname ?? "");
                                cartConceptsDb.Decree = (concept.Decree ?? "");
                                cartConceptsDb.Created = DateTime.UtcNow;

                                db.CartConcepts.Add(cartConceptsDb);
                            }
                            else
                            {
                                CartPackage cartPackageDb = new CartPackage();
                                cartPackageDb.EmployeeCartId = Order.EmployeeId;
                                cartPackageDb.PackageId = concept.ConceptId;
                                cartPackageDb.MedicId = concept.MedicId;
                                cartPackageDb.ClinicId = concept.ClinicId;
                                cartPackageDb.Scheduled = concept.Scheduled.Value;
                                cartPackageDb.Medicname = (concept.Medicname ?? "");
                                cartPackageDb.Created = DateTime.UtcNow;

                                db.CartPackage.Add(cartPackageDb);
                            }
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
        public static Result deleteCartOfEmployee(int EmployeeId)
        {
            Result result = new Result();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    IQueryable<CartConcepts> cartConceptsDb = db.CartConcepts.Where(c => c.EmployeeCartId == EmployeeId).AsQueryable();
                    if (cartConceptsDb.Any())
                    {
                        db.CartConcepts.RemoveRange(cartConceptsDb);
                    }

                    IQueryable<CartPackage> cartPackageDb = db.CartPackage.Where(cp => cp.EmployeeCartId == EmployeeId).AsQueryable();
                    if(cartPackageDb.Any())
                    {
                        db.CartPackage.RemoveRange(cartPackageDb);
                    }

                    Cart cartDb = db.Cart.Where(c => c.EmployeeId == EmployeeId).FirstOrDefault();
                    if (cartDb != null)
                    {
                        db.Cart.Remove(cartDb);
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

    }
}
