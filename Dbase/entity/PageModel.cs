using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbase.entity
{
    public class PageModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Родительский элемент (событие)")]
        public Guid ParentEventId { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Текст")]
        public string Text { get; set; }
        [Required]
        [Display(Name = "Алиас")]
        public string Alias { get; set; }
        [Display(Name = "Скрытый")]
        public bool Disabled { get; set; }
        [Display(Name = "Парметр для сортировки")]
        public int Sort { get; set; }
    }
}
