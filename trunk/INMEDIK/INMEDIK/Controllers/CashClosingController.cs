using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace INMEDIK.Controllers
{
    public class CashClosingController : Controller
    {
        [MenuData]
        [Authorize]
        public ActionResult OpeningCash()
        {
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();
            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            var saldoInicial = CashClosingHelper.ThereIsInitialCash(CurrentClinic.data.id.Value, CurrentUser.Id);
            /*si es true, lo enviamos a la pantalla de recepcion*/
            if (saldoInicial.bool_value)
            {
                return RedirectToAction("Index","Reception");
            }
            else
            {
                return View();
            }
        }
        [MenuData]
        [Authorize]
        public ActionResult ClosingCash()
        {
            bool isDetail = false;
            int? idCorte = null;
            ViewBag.isDetail = "false";
            ViewBag.DetailError = string.Empty;
            ViewBag.successDetail = "false";

            if (Request.Params["id"] != null)
            {
                ViewBag.isDetail = "true";
                isDetail = true;
                idCorte = Convert.ToInt32(Request.Params["id"]);
                var Cash = CashClosingHelper.getCashClosing(idCorte.Value);
                if (Cash.success)
                {
                    ViewBag.CorteDetalle = JsonUtility.ObjectToJsonJSEncode(Cash.data);
                    ViewBag.successDetail = "true";
                    ViewBag.DetailError = string.Empty;
                }
                else
                {
                    ViewBag.CorteDetalle = JsonUtility.ObjectToJsonJSEncode(new { });
                    ViewBag.successDetail = "false";
                    ViewBag.DetailError = Cash.message;
                }
            }
            else
            {
                ViewBag.CorteDetalle = JsonUtility.ObjectToJsonJSEncode(new { });
                ViewBag.isDetail = "false";
            }
            
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();
            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            var saldoInicial = CashClosingHelper.ThereIsInitialCash(CurrentClinic.data.id.Value, CurrentUser.Id);
            /*si es true, lo enviamos a la pantalla de corte de caja*/
            if (saldoInicial.bool_value || isDetail)
            {
                DenominationResult result = new DenominationResult();
                result = CashClosingHelper.getDenominations();
                NumericResult RefreshTimeCashClosed = ParameterHelper.GetRefreshTimeCashClosed();
                ViewBag.DenominationsList = JsonUtility.ObjectToJsonJSEncode(result.data_list);
                ViewBag.refreshTime = RefreshTimeCashClosed.value;
                return View();
            }
            else
            {
                return RedirectToAction("OpeningCash", "CashClosing");
            }
        }
        [Authorize]
        public JsonResult SaveInitialCash(CashClosingAux Cash)
        {
            GenericResult result = new GenericResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);

            Cash.ClinicId = CurrentClinic.data.id.Value;
            Cash.InitialCash = Cash.InitialCash;
            Cash.UserIdWhoOpened = CurrentUser.Id;
            result = CashClosingHelper.SaveInitialCash(Cash);
            if (!result.success)
            {
                return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { data = result.integer_value, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        [Authorize]
        public JsonResult SaveCashClosing(CashClosingAux Cash)
        {
            CashClosingResult result = new CashClosingResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);

            Cash.UserIdWhoClosed = CurrentUser.Id;
            Cash.ClinicId = CurrentClinic.data.id.Value;
            result = CashClosingHelper.SaveCashClosing(Cash);
            if (!result.success)
            {
                return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
            }
            FormsAuthentication.SignOut();
            AuthenticationHelper.logOut(System.Web.HttpContext.Current.User.Identity.Name);
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        [Authorize]
        public JsonResult getDenominations()
        {
            DenominationResult result = new DenominationResult();
            result = CashClosingHelper.getDenominations();
            return Json(new { data = result.data_list, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);

        }
    }
}