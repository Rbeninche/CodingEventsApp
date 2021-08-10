using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.Models
{
    public class EventCategory
    {
        public string Name { get; set; }
        public int Id { get; set; }
        //public List<Event> events { get; set; }

        public EventCategory(string name)
        {
            Name = name;

        }
        public EventCategory()
        {

        }
    }
}
