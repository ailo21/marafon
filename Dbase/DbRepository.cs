using Dbase.entity;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbase
{
    public partial class DbRepository
    {
        /// <summary>
        /// Контекст подключения
        /// </summary>
        private string _context = null;

        /// <summary>
        /// ip-адрес
        /// </summary>
        private string _ip = string.Empty;
        private string _currentUserId = string.Empty;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DbRepository()
        {
            _context = "DefaultConnection";
            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
        }

        public DbRepository(string connectionString, string userId, string ip)
        {
            _context = connectionString;
            //_domain = (!string.IsNullOrEmpty(DomainUrl)) ? getSiteId(DomainUrl) : "";
            _ip = ip;
            _currentUserId = userId;           


            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
        }
        /// <summary>
        /// список событий
        /// </summary>
        /// <returns></returns>
        public Paged<EventsModel> GetEventList(FilterModel filter)
        {
            using (var db = new CMSdb(_context))
            {
                var query = db.Events
                              .OrderBy(o => o.DDate);

                int itemsCount = query.Count();

                var list = query.Skip(filter.Size * (filter.Page - 1))
                                .Take(filter.Size).Select(s=>new EventsModel() {
                                                      Id=s.Id,
                                                      Title=s.CTitle,
                                                      SubEvent=s.SubEvents.Select(e=>new SubEventModel() {
                                                          Id=e.Id,
                                                          Title=e.CTitle
                                                      }).ToArray()                                  
                                                  });

                return new Paged<EventsModel>()
                {
                    Items = list.ToArray(),
                    Pager = new PagerModel()
                    {
                        PageNum = filter.Page,
                        PageSize = filter.Size,
                        TotalCount = itemsCount
                    }
                };
            }
        }
        public EventsModel GetEventItem(Guid id)
        {
            using (var db = new CMSdb(_context))
            {
                var query = db.Events.Where(w => w.Id == id);
                if (query.Any())
                {
                    return query.Select(s => new EventsModel {
                        Id=s.Id,
                        Alias=s.CAlias,
                        Title=s.CTitle,
                        Date=s.DDate,
                        Disabled=s.BDisabled,
                        Text=s.CText
                    }).Single();
                }
                return null;
            }
        }
        public bool InsertEvent(EventsModel ev)
        {
            using (var db = new CMSdb(_context))
            {
                return db.Events
                  .Value(p => p.Id, ev.Id)
                  .Value(p => p.CTitle, ev.Title)
                  .Value(p => p.CAlias, ev.Alias)
                  .Value(p => p.CText, ev.Text)
                  .Value(p => p.BDisabled, ev.Disabled)
                  .Value(p => p.DDate, ev.Date)
                  .Insert() > 0;
            }            
        }
        public bool UpdateEvent(EventsModel ev)
        {
            using (var db = new CMSdb(_context))
            {
                using (var tr = db.BeginTransaction()) {
                    db.Events.Where(w => w.Id==ev.Id)                        
                      .Set(p => p.CTitle, ev.Title)
                      .Set(p => p.CAlias, ev.Alias)
                      .Set(p => p.CText, ev.Text)
                      .Set(p => p.BDisabled, ev.Disabled)
                      .Set(p => p.DDate, ev.Date)
                      .Update();
                    tr.Commit();
                    return true;
                }                   
            }
        }
        /// <summary>
        /// определяет существует ли такое событие
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ExistEvent(Guid id)
        {
            using (var db = new CMSdb(_context))
            {
                return db.Events.Where(w => w.Id == id).Any();
            }
        }
        public bool DeleteEvent(Guid id)
        {
            using (var db = new CMSdb(_context))
            {
                return db.Events.Where(w => w.Id == id).Delete() > 0;
            }
        }

    }
}
