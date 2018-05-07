using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbase.entity
{
    public class SubEventModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Alias { get; set; }
        public string Text { get; set; }
        /// <summary>
        /// Время начала события
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Начало регистрации
        /// </summary>
        public DateTime DateStartReg { get; set; }
        /// <summary>
        /// Конец регистрации
        /// </summary>
        public DateTime DateEndReg { get; set; }
        /// <summary>
        /// стоимость участия
        /// </summary>
        public decimal Pay { get; set; }
    }
}
