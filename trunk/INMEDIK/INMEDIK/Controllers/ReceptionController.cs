using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace INMEDIK.Controllers
{
    [Authorize]
    [PSAuthorize]
    public class ReceptionController : Controller
    {
        [MenuData]
        public ActionResult Index()
        {
            if (Request.Cookies["_Authdata"] != null && !string.IsNullOrEmpty(Request.Cookies["_Authdata"].Value))
            {
                UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);
                if (CurrentUser.clinicId > -1)
                {
                    var saldoInicial = CashClosingHelper.ThereIsInitialCash(CurrentUser.clinicId, CurrentUser.Id);
                    /*si es true quiere decir que si tiene saldo inicial y puede proceder a vender*/
                    if (saldoInicial.bool_value)
                    {
                        GenericResult ocupaRetirar = WithdrawalHelper.AtWithdrawalLimit(CurrentUser.Id, CurrentUser.clinicId);
                        if (!ocupaRetirar.bool_value)
                        {
                            EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);

                            EmployeeResult res = EmployeeHelper.GetEmployeesByRol("Medic");
                            SpecialtyResult result = SpecialtyHelper.GetSpecialties();
                            NumericResult CardCommission = ParameterHelper.GetCardCommission();
                            NumericResult Tax = ParameterHelper.GetTax();
                            PaymentTypeResult paymenttyperes = PaymentHelper.GetPaymentTypes();
                            ClinicResult ClinicRes = ClinicHelper.GetClinicsSelect();
                            CartResult pendingCart = CartHelper.getPendingCart(EmployeeRess.data.id, CurrentUser.clinicId);
                            ClinicResult CurrentClinic = ClinicHelper.GetClinic(CurrentUser.clinicId);
                            PatientResult publicPatient = PatientHelper.GetPublicPatient();
                            NationalityResult NationalityRes = NationalityHelper.GetNationalitySelect();
                            if (publicPatient.success)
                            {
                                ViewBag.publicPatient = JsonUtility.ObjectToJsonJSEncode(publicPatient.data);
                            }
                            else
                            {
                                ViewBag.publicPatient = JsonUtility.ObjectToJsonJSEncode(new Object());
                            }

                            ViewBag.NationalityRes = JsonUtility.ObjectToJsonJSEncode(NationalityRes.data_list);

                            ViewBag.SpecialtiesList = JsonUtility.ObjectToJsonJSEncode(result.data_list);
                            ViewBag.MedicList = JsonUtility.ObjectToJsonJSEncode(res.data_list);
                            ViewBag.PaymentTypes = JsonUtility.ObjectToJsonJSEncode(paymenttyperes.data_list);
                            ViewBag.ClinicRes = JsonUtility.ObjectToJsonJSEncode(ClinicRes.data_list);

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
        }
        public JsonResult UpdateTemporalCart(CartAux Order, List<CartConceptAux> Concepts)
        {
            Result result = new Result();
            if (Request.Cookies["_Authdata"] != null && !string.IsNullOrEmpty(Request.Cookies["_Authdata"].Value))
            {
                UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);
                if (CurrentUser.clinicId > -1)
                {
                    EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);

                    var resDelCart = CartHelper.deleteCartOfEmployee(EmployeeRess.data.id);
                    if (resDelCart.success)
                    {
                        Order.EmployeeId = EmployeeRess.data.id;
                        Order.ClinicId = CurrentUser.clinicId;
                        result = CartHelper.UpdateTemporalCart(Order, Concepts);

                    }
                    else
                    {
                        result.message = "Error al actualizar el carrito.";
                        result.success = false;
                    }
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
        public JsonResult saveOrder(OrderAux Order, List<OrderConceptAux> Concepts, List<PaymentAux> Payments, List<OrderPromotionAux> PromotionList, List<OrderPromotionDiscountAppliedAux> PromotionsApplied, DateTime dateTime)
        {
            GenericResult result = new GenericResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            GenericResult ocupaRetirar = WithdrawalHelper.AtWithdrawalLimit(CurrentUser.Id, CurrentClinic.data.id.Value);
            if (!ocupaRetirar.bool_value)
            {
                EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);

                Order.EmployeeId = EmployeeRess.data.id;
                Order.ClinicId = CurrentClinic.data.id.Value;
                result = OrderHelper.saveOrder(Order, Concepts, Payments, PromotionList, PromotionsApplied, dateTime);
                if (result.success)
                {
                    var delCarRes = CartHelper.deleteCartOfEmployee(EmployeeRess.data.id);
                    if (!delCarRes.success)
                    {
                        return Json(new { success = delCarRes.success, message = delCarRes.message }, JsonRequestBehavior.DenyGet);
                    }
                }
            }
            else
            {
                return Json(new { needsWithdrawal = true, url = Url.Action("Index", "Withdrawal"), success = false, message = result.message }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { data = result.integer_value, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }

        public JsonResult SaveOrderPackage(OrderAux Order, List<OrderConceptAux> Concepts, List<PaymentAux> Payments, OrderAux DataOldOrder)
        {
            GenericResult result = new GenericResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            GenericResult ocupaRetirar = WithdrawalHelper.AtWithdrawalLimit(CurrentUser.Id, CurrentClinic.data.id.Value);
            if (!ocupaRetirar.bool_value)
            {
                EmployeeResult EmployeeRess = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);

                Order.EmployeeId = EmployeeRess.data.id;
                Order.ClinicId = CurrentClinic.data.id.Value;
                result = OrderHelper.SaveOrderPackage(Order, Concepts, Payments, DataOldOrder);
                if (result.success)
                {
                    var delCarRes = CartHelper.deleteCartOfEmployee(EmployeeRess.data.id);
                    if (!delCarRes.success)
                    {
                        return Json(new { success = delCarRes.success, message = delCarRes.message }, JsonRequestBehavior.DenyGet);
                    }
                }
            }
            else
            {
                return Json(new { needsWithdrawal = true, url = Url.Action("Index", "Withdrawal"), success = false, message = result.message }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { data = result.integer_value, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        public JsonResult WithdrawalNeeded()
        {
            GenericResult result = new GenericResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            result = WithdrawalHelper.AtWithdrawalLimit(CurrentUser.Id, CurrentClinic.data.id.Value);
            return Json(new { isNeeded = result.bool_value, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }
        [PSAuthorize]
        public JsonResult GetConsults(DTParameterModel model)
        {
            // Se agrega el id de la clinica para que la tabla solo muestre las consultas reservadas en tal clinica
            UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);

            TabletCartResult result = new TabletCartResult();

            if (CurrentUser.clinicId > -1)
            {
                result = TabletCartHelper.GetConsults(model, CurrentUser.clinicId);
            }
            return Json(new { data = result.data_list, recordsTotal = result.total.value, draw = model.Draw, recordsFiltered = result.total.value }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
        }

        [PSAuthorize]
        public JsonResult DeleteTabletCar(int id)
        {
            //Borrar un registro de lado de recepcion
            UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);

            GenericResult res = TabletCartHelper.DeleteTabletCar(id, CurrentUser.clinicId);
            return Json(new { success = res.success, alldeleted = res.bool_value, message = res.message }, JsonRequestBehavior.DenyGet);
        }

        [PSAuthorize]
        public JsonResult GetConsult(int id)
        {
            // Aqui se obtiene la consulta seleccionada desde el data table
            UserResult CurrentUser = UserHelper.GetUser(User.Identity.Name);

            TabletCartResult res = new TabletCartResult();

            if (CurrentUser.clinicId > -1)
            {
                res = TabletCartHelper.GetConsult(id, CurrentUser.clinicId);
            }
            return Json(new { data = res.data, message = res.message, success = res.success }, JsonRequestBehavior.AllowGet);
        }
        [PSAuthorize]
        public JsonResult GetEmployeePermised ()
        {
            GenericResult res = EmployeeHelper.GetEmployeePermised(User.Identity.Name); 
            return Json(new { success = res.success, data = res.bool_value }, JsonRequestBehavior.AllowGet); //regresa el valor de result.bool_value
        }

        public JsonResult getTicketToCancel(int idTicket)
        {
            OrderResult result = new OrderResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentUser.success)
            {
                result = OrderHelper.getOrderByTicket(idTicket, CurrentClinic.data.id.Value);

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

        public JsonResult getTicketToCancelPackage(int idTicket, OrderAux orderAux)
        {
            OrderResult result = new OrderResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentUser.success)
            {
                result = OrderHelper.getPackageByTicket(idTicket, CurrentClinic.data.id.Value, orderAux);

                if (result.data.ClinicId != CurrentClinic.data.id)
                {
                    return Json(new { success = false, message = "La orden no fue emitida en esta sucursal." }, JsonRequestBehavior.DenyGet);
                }
                if (result.data.orderPackageAuxList.Count == 0)
                {
                    return Json(new { success = false, message = "Ya se han cancelado los paquetes de este ticket." }, JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return Json(new { success = CurrentUser.success, message = CurrentUser.message }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }

        public JsonResult verifyPackageCancel(int idTicket, OrderAux orderAux)
        {
            OrderResult result = new OrderResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentUser.success)
            {
                result = OrderHelper.verifyPackageCancelled(idTicket, CurrentClinic.data.id.Value, orderAux);

                if (result.data.ClinicId != CurrentClinic.data.id)
                {
                    return Json(new { success = false, message = "La orden no fue emitida en esta sucursal." }, JsonRequestBehavior.DenyGet);
                }
                if (result.data.orderPackageAuxList.Count == 0)
                {
                    return Json(new { success = false, message = "Ya se han cancelado los paquetes de este ticket." }, JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return Json(new { success = CurrentUser.success, message = CurrentUser.message }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }

        public JsonResult getNewOrderDataPackage(int idTicket, OrderAux orderAux)
        {
            OrderResult result = new OrderResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();

            CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
            if (CurrentUser.success)
            {
                result = OrderHelper.getNewOrderDataPack(idTicket, CurrentClinic.data.id.Value, orderAux);

                if (result.data.ClinicId != CurrentClinic.data.id)
                {
                    return Json(new { success = false, message = "La orden no fue emitida en esta sucursal." }, JsonRequestBehavior.DenyGet);
                }
                if (result.data.orderPackageAuxList.Count == 0)
                {
                    return Json(new { success = false, message = "No hay paquetes por cancelar." }, JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return Json(new { success = CurrentUser.success, message = CurrentUser.message }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { data = result.data, success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [PSAuthorize]
        public JsonResult cancelOrderOrder(int idOrder, string strPass)
        {
            Result result = new Result();
            OrderResult Orderresult = new OrderResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();
            if (!string.IsNullOrEmpty(strPass))
            {
                CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
                //Traer todos los ususarios con permisos de cancelacion
                //Validar que la contraseña concuerde con uno de ellos
                //UserResult res = AuthenticationHelper.ValidateCredentials(CurrentUser.User.account, strPass, (int)CurrentClinic.data.id);
                GenericResult res = AuthenticationHelper.ValidateCancelCreden(strPass, (int)CurrentClinic.data.id);

                if (CurrentUser.success && res.success && res.bool_value)
                {
                    Orderresult = OrderHelper.getOrderByid(idOrder);
                    if (Orderresult.data.ClinicId != CurrentClinic.data.id)
                    {
                        return Json(new { success = false, message = "La orden no fue emitida en esta sucursal." }, JsonRequestBehavior.DenyGet);
                    }
                    if (Orderresult.data.IsCanceled)
                    {
                        return Json(new { success = false, message = "La orden ya esta cancelada." }, JsonRequestBehavior.DenyGet);
                    }
                    var EmployeeCancel = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);
                    result = OrderHelper.cancelOrder(idOrder, EmployeeCancel.data.id);
                }
                else
                {
                    return Json(new { success = false, message = "Contraseña invalida" }, JsonRequestBehavior.DenyGet);
                }
                return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { success = false, message = "Ingrese su contraseña para continuar" }, JsonRequestBehavior.DenyGet);
            }
        }

        public JsonResult cancelOrderPackage(OrderAux orderAux)
        {
            OrderResult result = new OrderResult();
            OrderResult Orderresult = new OrderResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();
            //if (!string.IsNullOrEmpty(strPass))
            //{
                CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
                //Traer todos los ususarios con permisos de cancelacion
                //Validar que la contraseña concuerde con uno de ellos
                ////UserResult res = AuthenticationHelper.ValidateCredentials(CurrentUser.User.account, strPass, (int)CurrentClinic.data.id);
                //GenericResult res = AuthenticationHelper.ValidateCancelCreden(strPass, (int)CurrentClinic.data.id);

                //if (CurrentUser.success && res.success && res.bool_value)
                //{
                    Orderresult = OrderHelper.getOrderByid(orderAux.id);
                    if (Orderresult.data.ClinicId != CurrentClinic.data.id)
                    {
                        return Json(new { success = false, message = "La orden no fue emitida en esta sucursal." }, JsonRequestBehavior.DenyGet);
                    }
                    if (Orderresult.data.IsCanceled)
                    {
                        return Json(new { success = false, message = "La orden ya esta cancelada." }, JsonRequestBehavior.DenyGet);
                    }
                    var EmployeeCancel = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);
                    result = OrderHelper.cancelPackage(orderAux.id, EmployeeCancel.data.id, orderAux);
                //}
                //else
                //{
                //    return Json(new { success = false, message = "Contraseña invalida" }, JsonRequestBehavior.DenyGet);
                //}
                return Json(new { success = result.success, message = result.message, data = result.data}, JsonRequestBehavior.DenyGet);
            //}
            //else
            //{
            //    return Json(new { success = false, message = "Ingrese su contraseña para continuar" }, JsonRequestBehavior.DenyGet);
            //}
        }

        public JsonResult validateCreateOrder(OrderAux orderAux)
        {
            OrderResult result = new OrderResult();
            OrderResult Orderresult = new OrderResult();
            UserResult CurrentUser = new UserResult();
            ClinicResult CurrentClinic = new ClinicResult();
            //if (!string.IsNullOrEmpty(strPass))
            //{
                CurrentUser = UserHelper.GetCurrentUserAndClinic(Request, out CurrentClinic);
                //Traer todos los ususarios con permisos de cancelacion
                //Validar que la contraseña concuerde con uno de ellos
                //UserResult res = AuthenticationHelper.ValidateCredentials(CurrentUser.User.account, strPass, (int)CurrentClinic.data.id);
                //GenericResult res = AuthenticationHelper.ValidateCancelCreden(strPass, (int)CurrentClinic.data.id);

                //if (CurrentUser.success && res.success && res.bool_value)
                //{
                    Orderresult = OrderHelper.getOrderByid(orderAux.id);
                    if (Orderresult.data.ClinicId != CurrentClinic.data.id)
                    {
                        return Json(new { success = false, message = "La orden no fue emitida en esta sucursal." }, JsonRequestBehavior.DenyGet);
                    }
                    if (Orderresult.data.IsCanceled)
                    {
                        return Json(new { success = false, message = "La orden ya esta cancelada." }, JsonRequestBehavior.DenyGet);
                    }
                    if (orderAux.AllPackSelected == true)
                    {
                        return Json(new { success = false, message = "¿Desea crear una nueva orden?." }, JsonRequestBehavior.DenyGet);
                    }
                    var EmployeeCancel = EmployeeHelper.GetEmployeeByIdUser(CurrentUser.Id);
                    result = OrderHelper.cancelPackage(orderAux.id, EmployeeCancel.data.id, orderAux);
                //}
                //else
                //{
                //    return Json(new { success = false, message = "Contraseña invalida" }, JsonRequestBehavior.DenyGet);
                //}
                return Json(new { success = result.success, message = result.message, data = result.data }, JsonRequestBehavior.DenyGet);
            //}
            //else
            //{
            //    return Json(new { success = false, message = "Ingrese su contraseña para continuar" }, JsonRequestBehavior.DenyGet);
            //}
        }

    }
}