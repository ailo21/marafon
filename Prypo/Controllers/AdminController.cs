﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prypo.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin        
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            ViewBag.Message = "test";
            return View();
        }
    }
}