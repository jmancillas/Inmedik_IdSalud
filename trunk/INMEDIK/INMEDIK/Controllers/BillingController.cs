using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace INMEDIK.Controllers
{
    public class BillingController : Controller
    {
        // GET: Facturacion
        [PSAuthorize]
        [MenuData]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerPagosFacturables(DTParameterModel model)
        {
            Resultado<TablaTicket> resultado = PagoFamiliaService.ObtenerTickesFacturables(model);
            return Json(new { data = resultado.Instancia.data_list, recordsTotal = resultado.Instancia.total_rows.integer_value, draw = model.Draw, recordsFiltered = resultado.Instancia.total_rows.integer_value, total_sum = resultado.Instancia.total_sum.double_value });
        }

        public JsonResult FacturarTicket(int idTicket)
        {
            int idUsuario = Convert.ToInt32(Membership.GetUser().ProviderUserKey);
            Resultado<GenericModel> result = FacturaService.FacturarTicket(idTicket, idUsuario);
            return Json(new { success = result.Exito, message = result.Mensaje });
        }

        public FileStreamResult GenerarFacturaImpresa(int idFactura)
        {
            Resultado<GenericModel> result = FacturaService.GenerarFacturaImpresa(idFactura);
            byte[] byteArray = PdfManager.HtmlToPdF(result.Instancia.string_value);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public JsonResult GenerarFacturaImpresaBase64(int idFactura)
        {
            Resultado<GenericModel> result = FacturaService.GenerarFacturaImpresa(idFactura);
            byte[] byteArray = PdfManager.HtmlToPdF(result.Instancia.string_value);
            ;
            return Json(new { data = "data:application/pdf;base64," + Convert.ToBase64String(byteArray), success = result.Exito, message = result.Mensaje });
        }

        public JsonResult ObtenerLogFactura(int idTicket)
        {
            Resultado<List<LogFacturacion>> result = FacturaService.ObtenerLogFacturacion(idTicket);
            return Json(new { success = result.Exito, message = result.Mensaje, data = result.Instancia });
        }

        public JsonResult EnviarParaCancelacionNS(int idFactura)
        {
            Resultado<GenericModel> result = FacturaService.EnviarParaCancelacionNS(idFactura);
            return Json(new { success = result.Exito, message = result.Mensaje });
        }

        public JsonResult EstadoDeCancelacion(int idFactura)
        {
            Resultado<GenericModel> result = FacturaService.EstadoDeCancelacion(idFactura);
            return Json(new { success = result.Exito, message = result.Mensaje, montoTicket = result.Instancia.double_value });
        }

        public JsonResult SATConsultaCFDI_Pruebas(int idFactura)
        {
            Resultado<ConsultaCFDI> result = FacturaService.SATConsultaCFDI_Pruebas(idFactura);
            return Json(new { success = result.Exito, message = result.Mensaje });
        }
    }
}