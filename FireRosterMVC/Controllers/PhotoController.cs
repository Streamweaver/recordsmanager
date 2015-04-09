using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Drawing;
using System.IO;
using FireRosterMVC.Models;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;

namespace FireRosterMVC.Controllers
{
    public class PhotoController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        private ISupportedImageFormat format = new JpegFormat { Quality = 70 };

        // GET: Photos
        public ActionResult Index(int id, int? height, int? width)
        {
            int h = (height ?? 325);
            int w = (width ?? 325);

            Staff staff = db.StaffList.Find(id);
            if (staff == null)
            {
                return new HttpNotFoundResult();
            }
            if (staff.Photo != null)
            {
                Size size = new Size(w, h);
                byte[] rawImg;
                using (MemoryStream inStream = new MemoryStream(staff.Photo))
                {
                    using (MemoryStream outStream = new MemoryStream())
                    {
                        using (ImageFactory imageFactory = new ImageFactory())
                        {
                            imageFactory.Load(inStream)
                                .Constrain(size)
                                .Format(format)
                                .Save(outStream);
                        }
                        rawImg = outStream.ToArray();
                    }
                }
                return new FileContentResult(rawImg, "image/jpeg");
            }
            else
            {
                return null;
            }
        }

        // GET: Photos
        public ActionResult GetSize(int id)
        {
            Staff staff = db.StaffList.Find(id);
            if (staff.Photo != null)
            {
                var img = new WebImage(staff.Photo);
                img.Resize(100, 100, true, true);
                var imgBytes = img.GetBytes();

                return File(imgBytes, "image/" + img.ImageFormat);
            }
            else
            {
                return null;
            }
        }
    }
}