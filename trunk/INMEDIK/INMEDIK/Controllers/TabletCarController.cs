using INMEDIK.Models.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    [Authorize]
    public class TabletCarController : Controller
    {
        public ActionResult Index()
        {
            if (Request.Cookies["_Authdata"] != null && !string.IsNullOrEmpty(Request.Cookies["_Authdata"].Value))
            {
                UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);
                if (CurrentUser.clinicId > -1)
                {
                        EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);

                        EmployeeResult res = EmployeeHelper.GetEmployeesByRol("Medic");
                        SpecialtyResult result = SpecialtyHelper.GetSpecialties();
                        NumericResult CardCommission = ParameterHelper.GetCardCommission();
                        NumericResult Tax = ParameterHelper.GetTax();
                        PaymentTypeResult paymenttyperes = PaymentHelper.GetPaymentTypes();
                        //ClinicResult ClinicRes = ClinicHelper.GetClinic(CurrentUser.clinicId);
                        TabletCartResult pendingCart = TabletCartHelper.getPendingCart(EmployeeRess.data.id, CurrentUser.clinicId);
                        ClinicResult CurrentClinic = ClinicHelper.GetClinic(CurrentUser.clinicId);
                        //PatientResult publicPatient = PatientHelper.GetPublicPatient();
                        //if (publicPatient.success)
                        //{
                        //    ViewBag.publicPatient = JsonUtility.ObjectToJsonJSEncode(publicPatient.data);
                        //}
                        //else
                        //{
                        //    ViewBag.publicPatient = JsonUtility.ObjectToJsonJSEncode(new Object());
                        //}

                        ViewBag.SpecialtiesList = JsonUtility.ObjectToJsonJSEncode(result.data_list);
                        ViewBag.MedicList = JsonUtility.ObjectToJsonJSEncode(res.data_list);
                        ViewBag.PaymentTypes = JsonUtility.ObjectToJsonJSEncode(paymenttyperes.data_list);
                        ViewBag.ClinicRes = JsonUtility.ObjectToJsonJSEncode(CurrentClinic.data);

                        ViewBag.Parameters = JsonUtility.ObjectToJsonJSEncode(new
                        {
                            CardCommission = CardCommission.value,
                            Tax = Tax.value,
                            clinicId = CurrentUser.clinicId,
                            AllowDonations = CurrentClinic.data.allowDonations,
                            minDonation = CurrentClinic.data.minDonation
                        });

                        if (pendingCart.data.PatientId != 0)
                        {
                            ViewBag.pendingCart = JsonUtility.ObjectToJsonJSEncode(pendingCart.data);
                        }
                        else
                        {
                            ViewBag.pendingCart = JsonUtility.ObjectToJsonJSEncode(new { });
                        }
                        return View();
                    
                }
                else
                {
                    return RedirectToAction("SignIn", "Authentication");
                }
            }
            else
            {
                return RedirectToAction("SignIn", "Authentication");
            }
        }
        public JsonResult UpdateTemporalCart(TabletCartAux Order, List<TabletCartConceptAux> Concepts)
        {
            Result result = new Result();
            if (Request.Cookies["_Authdata"] != null && !string.IsNullOrEmpty(Request.Cookies["_Authdata"].Value))
            {
                UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);
                if (CurrentUser.clinicId > -1)
                {
                    EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);
                    //var resDelCart = TabletCartHelper.deleteCartOfEmployee(EmployeeRess.data.id);

                    //if (resDelCart.success)
                    //{
                    //    Order.EmployeeId = EmployeeRess.data.id;
                    //    Order.ClinicId = clinicId;
                    //    result = TabletCartHelper.UpdateTemporalCart(Order, Concepts);

                    //}
                    //else
                    //{
                    //    result.message = "Error al actualizar el carrito.";
                    //    result.success = false;
                    //}

                    Order.EmployeeId = EmployeeRess.data.id;
                    Order.ClinicId = CurrentUser.clinicId;
                    result = TabletCartHelper.UpdateTemporalCart(Order, Concepts);
                }
                else
                {
                    result.message = "La sesión ha finalizado.";
                    result.success = false;
                }
            }
            else
            {
                result.message = "La sesión ha finalizado.";
                result.success = false;
            }
            return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }

        public JsonResult saveOrder(OrderAux Order, List<OrderConceptAux> Concepts, List<PaymentAux> Payments, List<OrderPromotionAux> Promotions, List<OrderPromotionDiscountAppliedAux> PromotionsApplied, DateTime dateTime)
        {
            GenericResult result = new GenericResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);

            Order.EmployeeId = EmployeeRess.data.id;
            Order.ClinicId = CurrentClinic.data.id.Value;
            result = OrderHelper.saveOrder(Order, Concepts, Payments, Promotions, PromotionsApplied, dateTime);
            if (result.success)
            {
                var delCarRes = CartHelper.deleteCartOfEmployee(EmployeeRess.data.id);
                if (!delCarRes.success)
                {
                    return Json(new { success = delCarRes.success, message = delCarRes.message }, JsonRequestBehavior.DenyGet);
                }
            }
            return Json(new { data = result.integer_value, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetConceptsOfCategory(string data = "")
        {
            Hashtable dataHash = JsonConvert.DeserializeObject<Hashtable>(data);
            ClinicResult CurrentClinic = new ClinicResult();
            ConceptResult res = new ConceptResult();

            string typed = dataHash["typed"].ToString();
            string CategoryName = dataHash["CategoryName"].ToString();
            string examType = dataHash["examType"].ToString();
            bool notExam = !dataHash.ContainsKey("examType");

            UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            int ClinicId = CurrentClinic.data.id.Value;
            if (CurrentClinic.data.id.HasValue)
            {
                if (examType == "exam")
                {
                    res = ConceptHelper.GetConceptsOfCategory(typed, CategoryName, false, false, true, ClinicId);
                }
                else if(examType == "lab")
                res = ConceptHelper.GetConceptsOfCategory(typed, CategoryName, false, true, false, ClinicId);
                else if(examType =="xray")
                {
                    res = ConceptHelper.GetConceptsOfCategory(typed, CategoryName, true, false, false, ClinicId);
                }
                
            }
            else
            {
                res.message = "No se encontro clinica.";
            }
            return Json(new { data = res.data_list, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
    }
}