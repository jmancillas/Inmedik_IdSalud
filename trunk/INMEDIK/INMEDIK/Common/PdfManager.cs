using INMEDIK.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using TuesPechkin;

namespace INMEDIK.Common
{
    public class PdfManager
    {
        private static IConverter _converter = null;
        private static object syncRoot = new object();
        private static IConverter converter
        {
            get
            {
                if(_converter == null)
                {
                    lock (syncRoot)
                    {
                        if (_converter == null)
                        {
                            _converter = new ThreadSafeConverter(
                                        new RemotingToolset<PdfToolset>(
                                            new WinAnyCPUEmbeddedDeployment(
                                                new TempFolderDeployment())));
                        }
                    }
                }
                return _converter;
            }
        }

        public static byte[] HtmlToPdF(string htmlString)
        {
            //string html = String.Format("<html><body>{0}</body></html>", htmlString);

            var document = new HtmlToPdfDocument
            {
                GlobalSettings =
                    {
                        ProduceOutline = true,

                    },
                Objects = {
                        new ObjectSettings {
                            HtmlText = htmlString,
                            WebSettings = new WebSettings{
                                DefaultEncoding = "UTF-8"
                            }
                        }
                }
            };
            byte[] pdf = converter.Convert(document);
            return pdf;
            //string pdf_page_size = "A4";
            //string pdf_orientation = "Portrait";
            //int webPageWidth = 1024;
            //int webPageHeight = 0;

            //PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
            //    pdf_page_size, true);

            //PdfPageOrientation pdfOrientation =
            //    (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
            //    pdf_orientation, true);



            //// instantiate a html to pdf converter object
            //HtmlToPdf converter = new HtmlToPdf();

            //// set converter options
            //converter.Options.PdfPageSize = pageSize;
            //converter.Options.PdfPageOrientation = pdfOrientation;
            //converter.Options.WebPageWidth = webPageWidth;
            //converter.Options.WebPageHeight = webPageHeight;

            //// create a new pdf document converting a html string
            //SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlString);

            //// save pdf document
            //byte[] pdf = doc.Save();


            //// close pdf document
            //doc.Close();

            //// return resulted pdf document
            //return pdf;
        }                                          

        public static byte[] HtmlToPdFWide(string htmlString)
        {
            string html = String.Format("<html><body>{0}</body></html>", htmlString);

            var document = new HtmlToPdfDocument
            {
                GlobalSettings =
                    {
                        ProduceOutline = true,
                        Orientation = GlobalSettings.PaperOrientation.Landscape
                    },
                Objects = {
                        new ObjectSettings {
                            HtmlText = html,
                            WebSettings = new WebSettings{
                                DefaultEncoding = "UTF-8"
                            }
                        }
                }
            };
            byte[] pdf = converter.Convert(document);
            return pdf;
            //string pdf_page_size = "A4";
            //string pdf_orientation = "Landscape";
            //int webPageWidth = 1024;
            //int webPageHeight = 0;

            //PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
            //    pdf_page_size, true);

            //PdfPageOrientation pdfOrientation =
            //    (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
            //    pdf_orientation, true);



            //// instantiate a html to pdf converter object
            //HtmlToPdf converter = new HtmlToPdf();

            //// set converter options
            //converter.Options.PdfPageSize = pageSize;
            //converter.Options.PdfPageOrientation = pdfOrientation;
            //converter.Options.WebPageWidth = webPageWidth;
            //converter.Options.WebPageHeight = webPageHeight;

            //// create a new pdf document converting a html string
            //SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlString);

            //// save pdf document
            //byte[] pdf = doc.Save();


            //// close pdf document
            //doc.Close();

            //// return resulted pdf document
            //return pdf;
        }

        public static byte[] HtmlToPdFNoMargin(string htmlString)
        {
            string html = String.Format("<html><body>{0}</body></html>", htmlString);

            var document = new HtmlToPdfDocument
            {
                GlobalSettings =
                    {
                        ProduceOutline = false,
                        Margins =
                            {
                                Left = 0.2,
                                Top = 0,
                                Bottom = 0,
                                Right = -0.1,
                                Unit = Unit.Centimeters
                            }

                    },
                Objects = {
                        new ObjectSettings {
                            HtmlText = html,
                            WebSettings = new WebSettings{
                                DefaultEncoding = "UTF-8"
                            }
                        }
                }
            };
            byte[] pdf = converter.Convert(document);
            return pdf;
        }
    }
                                            
}