using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dbase.entity
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool Disabled { get; set; }    
        public SubEventModel[] SubEvent{ get; set; }
    }
}
