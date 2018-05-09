using Dbase.entity;
using Prypo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prypo.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : CoreController
    {
        EventsViewModel model;
        FilterModel filter;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ViewBag.Title = "[adm]Событие";
            model = new EventsViewModel();
        }
        // GET: Admin 
        public ActionResult Index()
        {
            ViewBag.Title = "[adm]События";
            filter = GetFilter();
            model.ListEvents = _Repository.GetEventList(filter);
            ViewBag.Message = "test";
            return View(model);
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "insert-btn")]
        public ActionResult EventInsert()
        {
            string query = HttpUtility.UrlDecode(Request.Url.Query);
            query = AddFilterParam(query, "page", String.Empty);

            return Redirect(StartUrl + "Event/" + Guid.NewGuid() + "/" + query);
        }
        [HttpGet]
        public ActionResult Event(Guid id)
        {
            model.EventsItem = _Repository.GetEventItem(id);
            return View(model);
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "save-btn")]
        public ActionResult Event(EventsViewModel back_model)
        {
            if (ModelState.IsValid)
            {
                _Repository.InsertEvent(back_model.EventsItem);
            }
                
            return View(model);
        }
    }
}