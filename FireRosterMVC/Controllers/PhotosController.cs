using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using FireRosterMVC.Models;

namespace FireRosterMVC.Controllers
{
    public class PhotosController : Controller
    {
        private FireRosterDB db = new FireRosterDB();

        // GET: Photos
        public FileContentResult Index(int id)
        {
            Staff staff = db.StaffList.Find(id);
            return new FileContentResult(staff.Photo, "image/jpeg");
        }
    }
}