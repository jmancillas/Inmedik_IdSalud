using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace INMEDIK.Models.Helpers
{
    public class FileDbAux
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string ContentType { get; set; }
        public DateTime Created { get; set; }
        public string sCreated
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(
               new DateTime(Created.Ticks, DateTimeKind.Utc),
               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)")
               ).ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("es-MX"));
            }
        }
        public FileDbAux()
        {
        }
    }
    public class FileEvAux
    {
        public string FileName { get; set; }
        public Stream stream { get; set; }
    }
    public class FileDbResult : Result
    {
        public FileDbAux data { get; set; }
        public List<FileDbAux> data_list { get; set; }
        public NumericResult total { get; set; }

        public FileDbResult()
        {
            this.data = new FileDbAux();
            this.data_list = new List<FileDbAux>();
            this.total = new NumericResult();
        }
    }

    public class BytesArrayResult : Result
    {
        public FileDbAux infoFile { get; set; }
        public byte[] data { get; set; }

        public BytesArrayResult()
        {
            infoFile = new FileDbAux();
            //data = new byte[]();
        }
    }
    public class FileHelper
    {
        public static GenericResult SaveFile(Stream stream, string fileName, string path)
        {
            GenericResult result = new GenericResult();

            try
            {
                string pathFile =
                    Path.Combine(path, fileName);
                using (var fileStream = System.IO.File.Create(pathFile))
                {
                    stream.CopyTo(fileStream);
                }
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.exception = ex;
                result.message = "Ocurrió un error inesperado. " + result.exception_message;
            }
            return result;
        }

        public static GenericResult SaveFileEv(Stream stream, string fileName, string path)
        {
            GenericResult result = new GenericResult();

            try
            {
                string pathFile =
                    Path.Combine(path, fileName);
                using (var fileStream = System.IO.File.Create(pathFile))
                {
                    stream.CopyTo(fileStream);
                }
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.exception = ex;
                result.message = "Ocurrió un error inesperado. " + result.exception_message;
            }
            return result;
        }

        public static GenericResult SaveFileSv(Stream stream, string fileName, string path)
        {
            GenericResult result = new GenericResult();

            try
            {
                string pathFile =
                    Path.Combine(path, fileName);
                using (var fileStream = System.IO.File.Create(pathFile))
                {
                    stream.CopyTo(fileStream);
                }
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.exception = ex;
                result.message = "Ocurrió un error inesperado. " + result.exception_message;
            }
            return result;
        }

        public static GenericResult GetFile(string fileName, string path)
        {
            GenericResult result = new GenericResult();

            try
            {
                using (Image image = Image.FromFile(Path.Combine(path, fileName)))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        result.string_value = Convert.ToBase64String(imageBytes);
                    }
                    result.success = true;
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.exception = ex;
                result.message = "Ocurrió un error inesperado. " + result.exception_message;
            }
            return result;
        }

        public static BytesArrayResult GetFileBytesArray(string fileName, string path)
        {
            BytesArrayResult result = new BytesArrayResult();

            try
            {
                result.data = System.IO.File.ReadAllBytes(Path.Combine(path, fileName));
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.exception = ex;
                result.message = "Ocurrió un error inesperado. " + result.exception_message;
            }
            return result;
        }

        public static GenericResult DeleteFile(string fileName, string path)
        {
            GenericResult result = new GenericResult();

            try
            {
                string pathFile = Path.Combine(path, fileName);
                File.Delete(pathFile);
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.exception = ex;
                result.message = "Ocurrió un error inesperado. " + result.exception_message;
            }
            return result;
        }

        public static FileDbResult GetFileDbById(int id)
        {
            FileDbResult result = new FileDbResult();
            using (dbINMEDIK db = new dbINMEDIK())
            {
                try
                {
                    var VarFileDb = db.FileDb.Where(l => l.id == id).FirstOrDefault();
                    if (VarFileDb != null)
                    {
                        DataHelper.fill(result.data, VarFileDb);
                        result.success = true;
                    }
                    else
                    {
                        result.message = "Registro no encontrado.";
                    }
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.exception = ex;
                    result.message = "Ocurrió un error inesperado. " + result.exception_message;
                }
            }
            return result;
        }
    }
}