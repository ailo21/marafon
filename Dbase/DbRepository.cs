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
        public bool GetEventList()
        {
            using (var db = new CMSdb(_context))
            {
                var query = db.Events.OrderBy(o => o.DDate);
                return false;
            }
        }
    }
}
