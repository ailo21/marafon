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
            model.PageList = _Repository.GetPageList(id,null);
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


        #region page        
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "insert-page")]
        public ActionResult PageInsert(Guid id)
        {
            string query = HttpUtility.UrlDecode(Request.Url.Query);
            query = AddFilterParam(query, "parent", id.ToString());

            return Redirect(StartUrl + "page/" + Guid.NewGuid() + "/" + query);
        }

        [HttpGet]
        public ActionResult Page(Guid id)
        {
            model.Page = _Repository.GetPageItem(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "page-save-btn")]
        public ActionResult Page(Guid id, EventsViewModel back_model)
        {
            ErrorMessage message = new ErrorMessage
            {
                Title = "Информация"
            };
            back_model.Page.Id = id;
            if (ModelState.IsValid)
            {
                if (_Repository.ExistPage(id))
                {
                    _Repository.UpdatePage(back_model.Page);
                    message.Info = "Запись обновлена";
                }
                else
                {
                    #region идентификатор родителя
                    var urlString = ((System.Web.HttpRequestWrapper)Request).UrlReferrer.OriginalString;
                    Uri uri = new Uri(urlString);
                    string queryString = uri.Query;
                    Guid Parent = Guid.Parse(System.Web.HttpUtility.ParseQueryString(queryString).Get("parent"));
                    back_model.Page.ParentEventId = Parent; 
                    #endregion

                    _Repository.InsertPage(back_model.Page);
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
        [MultiButton(MatchFormKey = "action", MatchFormValue = "page-delete-btn")]
        public ActionResult DeletePAge(Guid id)
        {
            if (_Repository.DeletePage(id))
                return Redirect("/admin/");
            else return Redirect($"/admin/event/{id}");
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "page-cancel-btn")]
        public ActionResult CancelPage(Guid id)
        {
            string newurl= String.Format("/admin/event/{0}", _Repository.GetParentEventId(id));
            return Redirect(StartUrl);
        }
        #endregion


        #region subevent
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "subevent-page")]
        public ActionResult SubEventInsert(Guid id)
        {
            string query = HttpUtility.UrlDecode(Request.Url.Query);
            query = AddFilterParam(query, "parent", id.ToString());

            return Redirect(StartUrl + "subevent/" + Guid.NewGuid() + "/" + query);
        }
        [HttpGet]
        public ActionResult SubEvent(Guid id)
        {
            //model.Page = _Repository.GetPageItem(id);
            return View(model);
        }

        #endregion
    }
}