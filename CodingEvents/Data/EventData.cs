using CodingEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.Data
{
    public class EventData
    {
        private static Dictionary<int, Event> Events = new Dictionary<int, Event>();

        //Add Events
        public static void Add(Event newEvent)
        {
            Events.Add(newEvent.Id, newEvent);
        }

        //retrive the Events

        public static IEnumerable<Event> GetAll()
        {
            return Events.Values;
        }

        //remove an event
        public static void Remove(int id)
        {
            Events.Remove(id);
        }


    }
}
