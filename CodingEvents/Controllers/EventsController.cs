using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            List<Event> events = _context.Events.Include(e => e.Category).ToList();
            return View(events);
        }

      
        [HttpGet]
        public IActionResult Add()
        {
            List<EventCategory> categories = _context.Categories.ToList();
            AddEventViewModel addEventViewModel = new AddEventViewModel(categories);
            return View(addEventViewModel);

        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            List<EventCategory> categories = _context.Categories.ToList();
            AddEventViewModel addEventViewMode = new AddEventViewModel(categories);
            if (ModelState.IsValid)
            {
                EventCategory theCategory = _context.Categories.Find(addEventViewModel.CategoryId);
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Category = theCategory
                    

                };

                _context.Events.Add(newEvent);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(addEventViewMode);
            
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

            List<EventCategory> categories = _context.Categories.ToList();
            AddEventViewModel eventToEdit = new AddEventViewModel(categories);

            eventToEdit.Name = theEvent.Name;
            eventToEdit.Description = theEvent.Description;
            eventToEdit.ContactEmail = theEvent.ContactEmail;
            eventToEdit.CategoryId= theEvent.Category.Id;




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
                theEvent.CategoryId = addEventViewModel.CategoryId;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));


            }

            
            return View(addEventViewModel);
        }

        public IActionResult Detail(int id)
        {
            Event theEvent = _context.Events
               .Include(e => e.Category)
               .Single(e => e.Id == id);

            List<EventTag> eventTags = _context.EventTags
                .Where(et => et.EventId == id)
                .Include(et => et.Tag)
                .ToList();

            EventDetailViewModel viewModel = new EventDetailViewModel(theEvent, eventTags);
            return View(viewModel);
        }
    }
}
