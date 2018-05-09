using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbase.entity
{
    /// <summary>
    /// Постраничный вывод сущностей
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Paged<T>
    {
        /// <summary>
        /// Список сущностей
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Пейджер
        /// </summary>
        public PagerModel Pager { get; set; }
    }


    /// <summary>
    /// Пейджер
    /// </summary>
    public class PagerModel
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageNum { get; set; } = 1;

        /// <summary>
        /// Кол-во эл-тов на странице
        /// </summary>
        public int PageSize { get; set; } = 15;

        /// <summary>
        /// Общее кол-во эл-тов
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Кол-во страниц
        /// </summary>
        public int PageCount
        {
            get
            {
                return (TotalCount / PageSize) + (TotalCount % PageSize > 0 ? 1 : 0);
            }
        }

    }
}
