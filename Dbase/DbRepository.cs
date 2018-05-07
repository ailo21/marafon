using Dbase.entity;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbase
{
    public class DbRepository
    {
        /// <summary>
        /// Контекст подключения
        /// </summary>
        private string _context = null;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DbRepository()
        {
            _context = "DefaultConnection";
            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
        }
        /// <summary>
        /// список событий
        /// </summary>
        /// <returns></returns>
        public EventModel[] GetEventList()
        {
            using (var db = new CMSdb(_context))
            {
                var query = db.Events
                              .OrderBy(o => o.DDate)
                              .Select(s=>new EventModel() {
                                  Id=s.Id,
                                  Title=s.CTitle,
                                  SubEvent=s.SubEvents.Select(e=>new SubEventModel() {
                                      Id=e.Id,
                                      Title=e.CTitle
                                  }).ToArray()                                  
                              });
                if (query.Any())
                    return query.ToArray();
                return null;
            }
        }

        public bool InsertEvent(EventModel ev)
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
            return false;
        }

    }
}
