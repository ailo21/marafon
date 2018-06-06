using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dbase.entity
{
    public class EventsModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Alias { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Опубликован")]
        public bool Disabled { get; set; }    
        public SubEventModel[] SubEvent{ get; set; }

        /// <summary>
        /// Начало регистрации
        /// </summary>
        public DateTime DateStartReg { get; set; }
        /// <summary>
        /// Конец регистрации
        /// </summary>
        public DateTime DateEndReg { get; set; }
    }

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
        public DateTime DateStart { get; set; }
      
        /// <summary>
        /// стоимость участия
        /// </summary>
        public decimal Pay { get; set; }
        /// <summary>
        /// признак скрытости
        /// </summary>
        public bool Disabled { get; set; }
    }
}
