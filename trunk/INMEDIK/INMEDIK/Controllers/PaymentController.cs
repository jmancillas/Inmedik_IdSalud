using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INMEDIK.Models.Helpers;
using System.Text;
using INMEDIK.Common;

namespace INMEDIK.Controllers
{
    public class PaymentController : Controller
    {
        [MenuData]
        [Authorize]
        [PSAuthorize]
        public ActionResult Index()
        {
            if (Request.Cookies["_Authdata"] != null && !string.IsNullOrEmpty(Request.Cookies["_Authdata"].Value))
            {
                UserResult CurrentUser = new UserResult();
                
                CurrentUser = UserHelper.GetUser(User.Identity.Name);
                if (CurrentUser.clinicId > -1)
                {
                    var saldoInicial = CashClosingHelper.ThereIsInitialCash(CurrentUser.clinicId, CurrentUser.Id);
                    /*si es true quiere decir que si tiene saldo inicial y puede proceder a vender*/
                    if (saldoInicial.bool_value)
                    {
                        GenericResult ocupaRetirar = WithdrawalHelper.AtWithdrawalLimit(CurrentUser.Id, CurrentUser.clinicId);
                        if (!ocupaRetirar.bool_value)
                        {
                            NumericResult CardCommission = ParameterHelper.GetCardCommission();
                            NumericResult Tax = ParameterHelper.GetTax();
                            PaymentTypeResult paymenttyperes = PaymentHelper.GetPaymentTypes();
                            ClinicResult CurrentClinic = ClinicHelper.GetClinic(CurrentUser.clinicId);
                            ClinicResult ClinicRes = ClinicHelper.GetClinicsSelect();

                            ViewBag.Parameters = JsonUtility.ObjectToJsonJSEncode(new
                            {
                                CardCommission = CardCommission.value,
                                Tax = Tax.value,
                                clinicId = CurrentUser.clinicId,
                                AllowDonations = CurrentClinic.data.allowDonations,
                                minDonation = CurrentClinic.data.minDonation
                            });
                            ViewBag.PaymentTypes = JsonUtility.ObjectToJsonJSEncode(paymenttyperes.data_list);
                            ViewBag.ClinicRes = JsonUtility.ObjectToJsonJSEncode(ClinicRes.data_list);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Withdrawal");
                        }
                    }
                    else
                    {
                        return RedirectToAction("OpeningCash", "CashClosing");
                    }
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
            return View();
        }

        [Authorize]
        [PSAuthorize]
        public JsonResult GetPendingOrders(DTParameterModel model)
        {
            OrderResult result = OrderHelper.GetPendingOrders(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [Authorize]
        [PSAuthorize]
        public JsonResult GetOrder(int id)
        {
            OrderResult result = OrderHelper.getOrderByid(id);
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [PSAuthorize]
        public JsonResult AddPayments(List<PaymentAux> payments, OrderAux order)
        {
            ClinicResult clinic = new ClinicResult();
            OrderResult result = new OrderResult();
            UserResult CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out clinic);
            GenericResult ocupaRetirar = WithdrawalHelper.AtWithdrawalLimit(CurrentUser.Id, clinic.data.id.Value);
            if (!ocupaRetirar.bool_value)
            {
                EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);
                GenericResult resultPayment = PaymentHelper.AddPayments(payments, order, EmployeeRess.data.id, clinic.data.id.Value);

                if (resultPayment.success)
                {
                    result = OrderHelper.getOrderByid(order.id);
                }
                else
                {
                    return Json(new { success = resultPayment.success, message = resultPayment.message }, JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return Json(new { needsWithdrawal = true, url = Url.Action("Index","Withdrawal"), success = false, message = result.message }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { data = result.data ,success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
    }
}
