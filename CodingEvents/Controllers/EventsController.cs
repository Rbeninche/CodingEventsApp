using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {

        private EventDbContext _context;

        public EventsController(EventDbContext context)
        {
            _context = context;

        }
        
        [HttpGet]
        public IActionResult Index()
        {
            //Events.Add("Strange Loop");
            //Events.Add("Grace Hopper");
            //Events.Add("Code with Pride");
            List<Event> events = _context.Events.ToList();
            return View(events);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel();
            return View(addEventViewModel);

        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Type = addEventViewModel.Type
                    

                };

                _context.Events.Add(newEvent);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(addEventViewModel);
            
        }

        public IActionResult Delete()
        {
            List<Event> events = _context.Events.ToList();
            return View(events);
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                Event theevent = _context.Events.Find(eventId);
                _context.Events.Remove(theevent);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            Event theEvent = _context.Events.Find(id);

            AddEventViewModel eventToEdit = new AddEventViewModel();

            eventToEdit.Name = theEvent.Name;
            eventToEdit.Description = theEvent.Description;
            eventToEdit.ContactEmail = theEvent.ContactEmail;
            eventToEdit.Type= theEvent.Type;




            return View(eventToEdit);
        }

        [HttpPost]
        public IActionResult Edit(int id, AddEventViewModel addEventViewModel)
        {

            Event theEvent = _context.Events.Find(id);

            if (ModelState.IsValid)
            {

                theEvent.Name = addEventViewModel.Name;
                theEvent.Description = addEventViewModel.Description;
                theEvent.ContactEmail = addEventViewModel.ContactEmail;
                theEvent.Type = addEventViewModel.Type;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));


            }

            
            return View(addEventViewModel);
        }
    }
}
