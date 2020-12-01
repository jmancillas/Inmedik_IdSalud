using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace INMEDIK.Controllers
{
    public class OrderController : Controller
    {
        [Authorize]
        [PSAuthorize]
        [MenuData]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [PSAuthorize]
        public JsonResult GetOrders(DTParameterModel model, ReportFilter filter)
        {
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            vwSalesResult result = new vwSalesResult();
            if (currentUser.User.rolAux.name != "Admin")
            {
                EmployeeResult currentEmployee = EmployeeHelper.GetEmployeeByIdUser(currentUser.Id);
                result = OrderHelper.GetOrders(model, currentEmployee.data.clinicAux.Select(c => (int)c.id).ToList(), filter);
            }
            else
            {
                result = OrderHelper.GetOrders(model, filter);
            }

            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }
        [Authorize]
        [PSAuthorize]
        public JsonResult GetCancellations(DTParameterModel model)
        {
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            CancellationsResult result = new CancellationsResult();
            if (currentUser.User.rolAux.name != "Admin")
            {
                EmployeeResult currentEmployee = EmployeeHelper.GetEmployeeByIdUser(currentUser.Id);
                result = OrderHelper.GetCancellations(model, currentEmployee.data.clinicAux.Select(c => (int)c.id).ToList());
            }
            else
            {
                result = OrderHelper.GetCancellations(model);
            }
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [PSAuthorize]
        public JsonResult GetOrder(int idOrder)
        {
            ClinicResult CurrentClinic = new ClinicResult();
            OrderResult result = OrderHelper.getOrderByid(idOrder);
            Decimal iva = ParameterHelper.GetTax().value;
            return Json(new { data = result.data, tax = iva, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [PSAuthorize]
        public JsonResult GetOrderCanceledPack(int idOrder)
        {
            ClinicResult CurrentClinic = new ClinicResult();
            OrderResult result = OrderHelper.getpackageByid(idOrder);
            Decimal iva = ParameterHelper.GetTax().value;
            return Json(new { data = result.data, tax = iva, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [PSAuthorize]
        public JsonResult GetTicket(int idTicket)
        {
            ClinicResult CurrentClinic = new ClinicResult();
            UserResult CurrentUser = new UserResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentUser.success)
            {
                OrderResult result = OrderHelper.getTicketByid(idTicket, CurrentClinic.data.id.Value);
                return Json(new { data = result.data, success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { success = CurrentUser.success, message = CurrentUser.message }, JsonRequestBehavior.DenyGet);
            }


        }

        [Authorize]
        [PSAuthorize]
        public JsonResult GetRestricted(DTParameterModel model, ReportFilter filter)
        {
            Order_ConceptResult result = new Order_ConceptResult();
            UserResult currentUser = UserHelper.GetUser(User.Identity.Name);
            if (currentUser.User.rolAux.name != "Admin")
            {
                EmployeeResult currentEmployee = EmployeeHelper.GetEmployeeByIdUser(currentUser.Id);
                result = OrderHelper.GetRestricted(model, filter, currentEmployee.data.clinicAux.Select(c => (int)c.id).ToList());
            }
            else
            {
                result = OrderHelper.GetRestricted(model, filter);
            }
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        public JsonResult getOrderToCancel(int idOrder)
        {
            OrderResult result = new OrderResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentUser.success)
            {
                result = OrderHelper.getOrderByid(idOrder);
                if (result.data.ClinicId != CurrentClinic.data.id)
                {
                    return Json(new { success = false, message = "La orden no fue emitida en esta sucursal." }, JsonRequestBehavior.DenyGet);
                }
                if (result.data.IsCanceled)
                {
                    return Json(new { success = false, message = "La orden ya esta cancelada." }, JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return Json(new { success = CurrentUser.success, message = CurrentUser.message }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }


    }
}