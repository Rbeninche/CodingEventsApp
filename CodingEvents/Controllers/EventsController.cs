﻿using CodingEvents.Data;
using CodingEvents.Models;
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
            ViewBag.events = EventData.GetAll();
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();

        }

        [HttpPost]
        [Route("/Events/Add")]
        public IActionResult NewEvents(Event newEvent)
        {
            EventData.Add(newEvent);
            return Redirect("/Events");
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