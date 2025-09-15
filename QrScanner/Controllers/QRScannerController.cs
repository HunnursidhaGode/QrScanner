using QRCoder;
using QrScanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static QRCoder.PayloadGenerator;

namespace QrScanner.Controllers
{
    public class QRScannerController : Controller
    {
        // GET: QRScanner
        public ActionResult Index()
        {
            //craete view here using Template : Empty 
            return View(new QRCodemodel());
        }
        [HttpPost]
        public ActionResult Index(QRCodemodel qrCodeModel)
        {
            Payload payload = new Url(qrCodeModel.QRCodeText);
            QRCodeGenerator codeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = codeGenerator.CreateQrCode(payload);
            QRCoder.PngByteQRCode pngByte = new PngByteQRCode(qrCodeData);
            var QrByte = pngByte.GetGraphic(20);
            string base64Url = Convert.ToBase64String(QrByte);
            qrCodeModel.QRImageUrl = "data:image/png;base64," + base64Url;
            return View("Index", qrCodeModel);
        }
    }
}