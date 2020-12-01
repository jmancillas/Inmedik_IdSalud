using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    public class WithdrawalController : Controller
    {
        // GET: Withdrawal
        [MenuData]
        public ActionResult Index()
        {
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (WithdrawalHelper.AtWithdrawalLimit(CurrentUser.Id, CurrentClinic.data.id.Value).bool_value)
            {
                DenominationResult result = new DenominationResult();
                result = CashClosingHelper.getDenominations();
                ViewBag.DenominationsList = JsonUtility.ObjectToJsonJSEncode(result.data_list.Where(d => d.Name != "CC" && d.Name != "Vales"));
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }
        
        [Authorize]
        public JsonResult SaveWithdrawal(WithdrawalAux withdrawal)
        {
            CashClosingResult cashclosing = new CashClosingResult();
            WithdrawalResult result = new WithdrawalResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);

            withdrawal.clinicId = CurrentClinic.data.id.Value;
            cashclosing = CashClosingHelper.GetCurrentCashClosing(CurrentUser.Id, CurrentClinic.data.id.Value);
            withdrawal.cashClosingId = cashclosing.data.id;
            result = WithdrawalHelper.SaveWithdrawal(withdrawal);
            
            return Json(new { Clinica = CurrentClinic.data, Usuario = cashclosing.data.PersonAux.fullName,  Retiro = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
    }
}