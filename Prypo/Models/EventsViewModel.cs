using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dbase.entity;

namespace Prypo.Models
{
    public class EventsViewModel: CoreViewModel
    {
        public Paged<EventsModel> ListEvents{get;set;}
        public EventsModel EventsItem { get; set; }
        public PageModel Page { get; set; }
        public PageModel[] PageList { get; set; }
    }
}