using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using INMEDIK.Common;
namespace INMEDIK.Controllers
{
    [Authorize]
    [PSAuthorize]
    public class CancelledPackagesController : Controller
    {
        // GET: CancelledPackages
        [MenuData]
        public ActionResult Index()
        {
            UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);
            var saldoInicial = CashClosingHelper.ThereIsInitialCash(CurrentUser.clinicId, CurrentUser.Id);
            /*si es true quiere decir que si tiene saldo inicial y puede proceder a vender*/
            if (saldoInicial.bool_value)
            {
                GenericResult ocupaRetirar = WithdrawalHelper.AtWithdrawalLimit(CurrentUser.Id, CurrentUser.User.currentClinicId.Value);
                if (!ocupaRetirar.bool_value)
                {
                    EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);

                    EmployeeResult res = EmployeeHelper.GetEmployeesByRol("Medic");
                    SpecialtyResult result = SpecialtyHelper.GetSpecialties();
                    NumericResult CardCommission = ParameterHelper.GetCardCommission();
                    NumericResult Tax = ParameterHelper.GetTax();
                    PaymentTypeResult paymenttyperes = PaymentHelper.GetPaymentTypes();
                    ClinicResult ClinicRes = ClinicHelper.GetClinicsSelect();
                    CartResult pendingCart = CartHelper.getPendingCart(EmployeeRess.data.id, CurrentUser.User.currentClinicId.Value);
                    ClinicResult CurrentClinic = ClinicHelper.GetClinic(CurrentUser.User.currentClinicId.Value);
                    PatientResult publicPatient = PatientHelper.GetPublicPatient();
                    if (publicPatient.success)
                    {
                        ViewBag.publicPatient = JsonUtility.ObjectToJsonJSEncode(publicPatient.data);
                    }
                    else
                    {
                        ViewBag.publicPatient = JsonUtility.ObjectToJsonJSEncode(new Object());
                    }

                    ViewBag.SpecialtiesList = JsonUtility.ObjectToJsonJSEncode(result.data_list);
                    ViewBag.MedicList = JsonUtility.ObjectToJsonJSEncode(res.data_list);
                    ViewBag.PaymentTypes = JsonUtility.ObjectToJsonJSEncode(paymenttyperes.data_list);
                    ViewBag.ClinicRes = JsonUtility.ObjectToJsonJSEncode(ClinicRes.data_list);

                    ViewBag.Parameters = JsonUtility.ObjectToJsonJSEncode(new
                    {
                        CardCommission = CardCommission.value,
                        Tax = Tax.value,
                        clinicId = CurrentUser.User.currentClinicId.Value,
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
                    return RedirectToAction("Index", "Withdrawal");
                }
            }
            else
            {
                return RedirectToAction("OpeningCash", "CashClosing");
            }
        }

        public JsonResult GetCancelledPackages(DTParameterModel model)
        {
            CancelledPackageResult result = CancelledPackagesHelper.GetCancelledPackages(model);
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        public JsonResult cancelPackage(int idOrder, int EmployeeCancelId)
        {
            Result res = CancelledPackagesHelper.cancelPackage(idOrder, EmployeeCancelId);
            return Json(new { success = res.success, message = res.message }, JsonRequestBehavior.DenyGet);
        }
    }
}