using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Google.Authenticator;
using QRCoder;
using System.Web.Configuration;

namespace INMEDIK.Controllers
{
    public class AuthenticationController : Controller
    {
        private const string key = "C`8C8sVTXv'F-_=.";
        // GET: Authentication
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ClinicResult result = ClinicHelper.GetClinicsSelect();
                ViewBag.clinics = result.data_list;
                if (!result.success)
                {
                    ViewBag.errorMessage = result.message;
                }
                else if(TempData["Error"] != null)
                {
                    ViewBag.errorMessage = TempData["Error"];
                }
                return View();
            }
        }

        public ActionResult TwoFactorAuth()
        {
            if (Session["clinic"] == null || Session["account"] == null)
            {
                return RedirectToAction("Index", "Authentication");
            }
            UserResult currentUser = UserHelper.GetUser(Session["account"].ToString());
            string usrUniqueKey = key + currentUser.User.account;
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

            SetupCode code = tfa.GenerateSetupCode("Inmedik", currentUser.User.account, usrUniqueKey, 250, 250);
            string barCode = "";
            string manualCode = "";
            if (!currentUser.User.authenticator)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(String.Format("otpauth://totp/{0}?secret={1}&issuer={2}", currentUser.User.account, code.ManualEntryKey, "Inmedik"), QRCodeGenerator.ECCLevel.Q);
                Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                barCode = qrCode.GetGraphic(20);
                manualCode = code.ManualEntryKey;
            }
            ViewBag.BarcodeImage = barCode;
            ViewBag.SetupCode = manualCode;
            if (TempData.ContainsKey("error"))
            {
                ViewBag.errorMessage = TempData["error"];
            }
            return View();
        }

        public ActionResult SignInTwoFA()
        {
            if (Session["clinic"] == null || Session["account"] == null)
            {
                return RedirectToAction("Index", "Authentication");
            }
            if (!string.IsNullOrEmpty(Request.Params["code"]))
            {
                string token = Request.Params["code"];
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                UserResult currentUser = UserHelper.GetUser(Session["account"].ToString());
                string usrUniqueKey = key + currentUser.User.account;
                bool isValid = tfa.ValidateTwoFactorPIN(usrUniqueKey, token);
                if (isValid)
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (currentUser.User.rolAux.name == "TabletCart")
                    {
                        Response.Cookies["_Authdata"].Value = AuthenticationHelper.Encrypt(Session["clinic"].ToString());
                        UserHelper.SaveClinicUser(currentUser.User);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, Session["account"].ToString(),
                        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")),
                        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")).AddMinutes(1440),
                        false,
                        Session["account"].ToString(),
                        FormsAuthentication.FormsCookiePath);
                        // Encrypt the ticket.
                        string encTicket = FormsAuthentication.Encrypt(ticket);
                        // Create the cookie.
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                        {
                            HttpOnly = true,
                            Secure = FormsAuthentication.RequireSSL,
                            Path = FormsAuthentication.FormsCookiePath,
                            Domain = FormsAuthentication.CookieDomain
                        };
                        Response.AppendCookie(cookie);
                        return RedirectToAction("Index", "TabletCar");
                    }
                    else
                    {
                        Response.Cookies["_Authdata"].Value = AuthenticationHelper.Encrypt(Session["clinic"].ToString());
                        UserHelper.SaveClinicUser(currentUser.User);
                        FormsAuthentication.SetAuthCookie(Session["account"].ToString(), false);
                        UserHelper.SetAuthenticator(currentUser.Id, true);
                        Session.Remove("clinic");
                        Session.Remove("account");
                        return RedirectToAction("Index", "Dashboard");
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else
                {
                    TempData["error"] = "Código no válido.";
                }
            }
            else
            {
                return RedirectToAction("TwoFactorAuth", "Authentication");
            }
            return RedirectToAction("TwoFactorAuth", "Authentication");
        }

        public ActionResult SignIn()
        {
            //if is not authenticated, check if is trying to authenticate
            if (!string.IsNullOrEmpty(Request.Params["Account"]) && !string.IsNullOrEmpty(Request.Params["Password"]) && !string.IsNullOrEmpty(Request.Params["clinic"]))
            {
                int idclinic;
                int.TryParse(Request.Params["clinic"], out idclinic);
                //verify the credentials
                UserResult res = AuthenticationHelper.ValidateCredentials(Request.Params["Account"], Request.Params["Password"], idclinic);
                if (res.success)
                {
                    int clinicId;
                    if (!int.TryParse(Request.Params["clinic"], out clinicId))
                    {
                        ViewBag.errorMessage = "Error al obtener la clinica seleccionada.";
                        return RedirectToAction("Index", "Authentication");
                    }
                    if (!((WebConfigurationManager.AppSettings["Security.Generics.Allowed"] ?? "false") == "true"
                        && Request.Params["Password"] == WebConfigurationManager.AppSettings["Security.Generics.Pass"])
                        && WebConfigurationManager.AppSettings["Security.Generics.Authenticator"] == "true")
                    {
                        Session["clinic"] = Request.Params["clinic"];
                        Session["account"] = Request.Params["Account"];
                        return RedirectToAction("TwoFactorAuth", "Authentication");
                    }
                    else
                    {
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        Response.Cookies["_Authdata"].Value = AuthenticationHelper.Encrypt(Request.Params["clinic"].ToString());
                        var usrResult = UserHelper.GetUser(Request.Params["Account"]);
                        usrResult.User.currentClinicId = Convert.ToInt32(Request.Params["clinic"]);
                        UserHelper.SaveClinicUser(usrResult.User);
                        if (usrResult.User.rolAux.name == "TabletCart")
                        {
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, Request.Params["Account"],
                            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")),
                            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")).AddMinutes(1440),
                            false,
                            Request.Params["Account"],
                            FormsAuthentication.FormsCookiePath);
                            // Encrypt the ticket.
                            string encTicket = FormsAuthentication.Encrypt(ticket);
                            // Create the cookie.
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                            {
                                HttpOnly = true,
                                Secure = FormsAuthentication.RequireSSL,
                                Path = FormsAuthentication.FormsCookiePath,
                                Domain = FormsAuthentication.CookieDomain
                            };
                            Response.AppendCookie(cookie);
                            return RedirectToAction("Index", "TabletCar");
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(Request.Params["Account"], false);
                            return RedirectToAction("Index", "Dashboard");
                        }
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                }
                else
                {
                    //set the res.message as te error message for the view
                    TempData["Error"] = res.message;
                }

            }
            return RedirectToAction("Index", "Authentication");
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            // clear authentication cookie
            HttpCookie cookieX = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookieX.Expires = DateTime.UtcNow.AddYears(-1);
            Response.Cookies.Add(cookieX);
            // clear session cookie
            HttpCookie cookieY = new HttpCookie("ASP.NET_SessionId", "");
            cookieY.Expires = DateTime.UtcNow.AddYears(-1);
            Response.Cookies.Add(cookieY);

            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            AuthenticationHelper.logOut(userName);

            return RedirectToAction("Index");
        }
    }
}