using INMEDIK.Common;
using INMEDIK.Models.Helpers;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace INMEDIK.Controllers
{
    public class FilesController : Controller
    {
        [Authorize]
        [PSAuthorize]
        public JsonResult SaveImageAd(System.Web.HttpPostedFileBase File, int adId)
        {
            
            string FileName = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")).ToString("dd-MM-yyyy_HH_mm_ss") + "_" + File.FileName;
            GenericResult resultSave = FileHelper.SaveFile(
                                    File.InputStream,
                                    FileName,
                                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files")
                                );
            if (resultSave.success)
            {
                Result result = AdsHelper.AssignFileToAd(adId, FileName, File.FileName, File.ContentType);
                if (!result.success)
                {
                    FileHelper.DeleteFile(
                                FileName,
                                System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files")
                            );
                }
                return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { success = resultSave.success, message = resultSave.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
        }
        [Authorize]
        [PSAuthorize]
        public JsonResult SaveImageEv(System.Web.HttpPostedFileBase File, int evId, string Description, int? fileId)
        {
            string FileName = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")).ToString("dd-MM-yyyy_HH_mm_ss_MS") + "_" + File.FileName;
            GenericResult resultSave = FileHelper.SaveFileEv(
                                    File.InputStream,
                                    FileName,
                                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files/ElectronicFile")
                                );
            if (resultSave.success)
            {
                Result result = AdsHelper.AssignFileToEv(evId, FileName, File.FileName, File.ContentType, Description, fileId);
                if (!result.success)
                {
                    FileHelper.DeleteFile(
                                FileName,
                                System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files/ElectronicFile")
                            );
                }
                return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { success = resultSave.success, message = resultSave.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
        }

        public JsonResult SaveImageSv(HttpPostedFileBase File, int evId, string Description, int? fileId)
        {
            string FileName = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")).ToString("dd-MM-yyyy_HH_mm_ss_MS") + "_" + File.FileName;
            GenericResult resultSave = FileHelper.SaveFileSv(
                                    File.InputStream,
                                    FileName,
                                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files/ElectronicFile")
                                );
            if (resultSave.success)
            {
                Result result = AdsHelper.AssignFileToSv(evId, FileName, File.FileName, File.ContentType, Description, fileId);
                if (!result.success)
                {
                    FileHelper.DeleteFile(
                                FileName,
                                System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files/ElectronicFile")
                            );
                }
                return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { success = resultSave.success, message = resultSave.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
        }

        [Authorize]
        [PSAuthorize]
        public JsonResult SaveFileLab(System.Web.HttpPostedFileBase File, int LaboratoryId)
        {
            var labDb = LaboratoryXrayHelper.LoadLaboratoryById(LaboratoryId);
            if (labDb.data.StatusAux.Name != "Terminado")
            {
                string FileName = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")).ToString("dd-MM-yyyy_HH_mm_ss") + "_" + File.FileName;
                GenericResult resultSave =
                                FileHelper.SaveFile(
                                    File.InputStream,
                                    FileName,
                                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files")
                                );
                if (resultSave.success)
                {
                    var result = LaboratoryXrayHelper.AssignFileToLab(LaboratoryId, FileName, File.FileName, File.ContentType);

                    /*en caso de error se borra el archivo que se habia guardados*/
                    if (!result.success)
                    {
                        FileHelper.DeleteFile(
                                    FileName,
                                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files")
                                );
                        return Json(new { success = result.success, message = result.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
                    }
                }
                return Json(new { success = resultSave.success, message = resultSave.message }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { success = false, message = "No se puede cargar archivos a un laboratorio terminado." }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }
        }
        [Authorize]
        [PSAuthorize]
        public JsonResult DeleteFileLab(FileDbAux FileDbAux,int LaboratoryId)
        {
            var labDb = LaboratoryXrayHelper.LoadLaboratoryById(LaboratoryId);
            if (labDb.data.StatusAux.Name != "Terminado")
            {
                Result result = new Result();
                result = LaboratoryXrayHelper.RemoveAssignFileToLab(FileDbAux, LaboratoryId);
                if (result.success)
                {
                    var resultDelete = FileHelper.DeleteFile(
                                    FileDbAux.Name,
                                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files"));

                    /*en caso de que no lo pudiera borrar, lo volvemos a asignar al laboratorio*/
                    if (!resultDelete.success)
                    {
                        LaboratoryResult resultReAsign = LaboratoryXrayHelper.AssignFileToLab(LaboratoryId, FileDbAux.Name, FileDbAux.OriginalName, FileDbAux.ContentType);
                        resultDelete.message = "El archivo no pudo ser eliminado. " + resultDelete.message;
                        return Json(new { success = resultDelete.success, message = resultDelete.message }, JsonRequestBehavior.DenyGet);
                    }

                    /*En caso de que todo saliera bien, se botiene el laboratorio de nuevo para que se recarge del lado del cliente*/
                    var LabResult = LaboratoryXrayHelper.LoadLaboratoryById(LaboratoryId);
                    return Json(new { data = LabResult.data, success = LabResult.success, message = LabResult.message }, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(new { success = result.success, message = result.message }, JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "No se puede borrar archivos a un laboratorio terminado." }, "application/json", Encoding.UTF8, JsonRequestBehavior.DenyGet);
            }

        }
        [Authorize]
        [PSAuthorize]
        public FileResult DownLoadFileLab(string data)
        {
            FileDbAux FileDbAux = new JavaScriptSerializer().Deserialize<FileDbAux>(data);

            FileDbResult fileResult = FileHelper.GetFileDbById(FileDbAux.id);
            if (fileResult.success)
            {
                BytesArrayResult result = new BytesArrayResult();
                result = FileHelper.GetFileBytesArray(
                                    fileResult.data.Name,
                                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Files"));
                if (result.success)
                {
                    return File(result.data, /*System.Net.Mime.MediaTypeNames.Application.Octet*/fileResult.data.ContentType, fileResult.data.Name);
                }
                else
                {
                    throw result.exception;
                }
                
            }
            else
            {
                throw fileResult.exception;
            }
        }
        [Authorize]
        [PSAuthorize]
        public FileResult DownLoadTemplateLab(string data)
        {
            BytesArrayResult result = new BytesArrayResult();
            result = FileHelper.GetFileBytesArray(
                                data,
                                System.Web.HttpContext.Current.Server.MapPath("~/Content/templates/lab"));
            if (result.success)
            {
                return File(result.data, System.Net.Mime.MediaTypeNames.Application.Octet/*fileResult.data.ContentType*/, data);
            }
            else
            {
                throw result.exception;
            }
        }
    }
}