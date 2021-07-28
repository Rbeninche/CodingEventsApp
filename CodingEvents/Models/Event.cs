using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.Models
{
    public class Event
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ContactEmail { get; set; }

        public string Location { get; set; }

        public bool RegistrationRequired { get; set; }

        public int NumberOfAttendees { get; set; }

        public int Id { get; }
        static private int nextId = 1;

        public Event(string name, string description, string contactEmail, string location, bool registration, int attendees)
        {
            Name = name;
            Description = description;
            ContactEmail = contactEmail;
            Location = location;
            RegistrationRequired = registration;
            NumberOfAttendees = attendees;
            Id = nextId;
            nextId++;
        }

        public Event()
        {
            Id = nextId;
            nextId++;
        }


        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Event @event &&
                   Id == @event.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
