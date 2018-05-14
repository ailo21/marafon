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
        [ValidateInput(false)]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "save-btn")]
        public ActionResult Event(Guid id, EventsViewModel back_model)
        {
            ErrorMessage message = new ErrorMessage
            {
                Title = "Информация"
            };

            back_model.EventsItem.Id = id;
            if (ModelState.IsValid)
            {
                if (_Repository.ExistEvent(id))
                {
                    //сохраняем
                    _Repository.UpdateEvent(back_model.EventsItem);
                    message.Info = "Запись обновлена";
                }
                else
                {
                    //создаем
                    _Repository.InsertEvent(back_model.EventsItem);
                    message.Info = "Запись добавлена";
                }
                message.Buttons = new ErrorMessageBtnModel[]
                {
                    new ErrorMessageBtnModel { Url = "/admin", Text = "вернуться в список" },
                    new ErrorMessageBtnModel { Url = "/admin/event/"+id, Text = "ок", Action = "false" }
                };
            }
            model.EventsItem = _Repository.GetEventItem(id);
            model.ErrorInfo = message;
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "delete_event-btn")]
        public ActionResult DeleteEvent(Guid id)
        {
            if (_Repository.DeleteEvent(id))
               return Redirect("/admin/");
            else return Redirect($"/admin/event/{id}");
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "cancel-btn")]
        public ActionResult Cancel()
        {
            return Redirect(StartUrl);
        }

    }
}