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
    public class TagController : Controller
    {
        private EventDbContext _context;

        public TagController(EventDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Tag> tags = _context.Tags.ToList();
            return View(tags);
        }

        public IActionResult Add()
        {
            Tag tag = new Tag();
            return View(tag);
        }

        [HttpPost]
        public IActionResult Add(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Tags.Add(tag);
                _context.SaveChanges();
                return Redirect("/Tag/");
            }

            return View("Add", tag);
        }

        public IActionResult AddEvent(int id)
        {
            Event theEvent = _context.Events.Find(id);
            List<Tag> possibleTags = _context.Tags.ToList();

            AddEventTagViewModel viewModel = new AddEventTagViewModel(theEvent, possibleTags);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddEvent(AddEventTagViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                int eventId = viewModel.EventId;
                int tagId = viewModel.TagId;

                List<EventTag> existingItems = _context.EventTags
                   .Where(et => et.EventId == eventId)
                   .Where(et => et.TagId == tagId)
                   .ToList();

                if (existingItems.Count == 0)
                {
                    EventTag eventTag = new EventTag
                    {
                        EventId = eventId,
                        TagId = tagId
                    };
                    _context.EventTags.Add(eventTag);
                    _context.SaveChanges();
                }

                return Redirect("/Events/Detail/" + eventId);
            }

            return View(viewModel);
        }

        public IActionResult Detail(int id)
        {
            List<EventTag> eventTags = _context.EventTags
                  .Where(et => et.TagId == id)
                  .Include(et => et.Event)
                  .Include(et => et.Tag)
                  .ToList();

            return View(eventTags);
        }
    }
}
