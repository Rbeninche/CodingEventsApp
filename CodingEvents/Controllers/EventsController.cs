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
        
        [HttpGet]
        public IActionResult Index()
        {
            //Events.Add("Strange Loop");
            //Events.Add("Grace Hopper");
            //Events.Add("Code with Pride");
            List<Event> events = new List<Event>(EventData.GetAll());
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
                    ContactEmail = addEventViewModel.ContactEmail

                };

                EventData.Add(newEvent);
                return Redirect("/Events");

            }
            return View(addEventViewModel);
            
        }

        public IActionResult Delete()
        {
            ViewBag.events = EventData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                EventData.Remove(eventId);
            }

            return Redirect("/Events");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.name = EventData.GetAll().Where(x => x.Id == id).FirstOrDefault().Name;
            ViewBag.description = EventData.GetAll().Where(x => x.Id == id).FirstOrDefault().Description;
            ViewBag.id = EventData.GetAll().Where(x => x.Id == id).FirstOrDefault().Id;

            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, Event newEvent)
        {
            var evt = EventData.GetAll().Where(x => x.Id == id).FirstOrDefault();

            evt.Name = newEvent.Name;
            evt.Description = newEvent.Description;

            return Redirect("/Events");
        }
    }
}
