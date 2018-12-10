using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyFAQ.Models;

namespace MyFAQ.Controllers
{
    [Route("api/Home")]
    public class HomeController : Controller
    {
        private readonly MyAppContext _context;
        public HomeController(MyAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Category> Get()
        {
            var db = new AppDB(_context);
            List<Category> allCategories = db.getCategories();
            return allCategories;
        }

        [HttpPost]
        public JsonResult Post([FromBody]question incomingQuestion)
        {
            if (ModelState.IsValid)
            {
                var db = new AppDB(_context);
                bool OK = db.saveQuestionFromCustomer(incomingQuestion);
                if (OK)
                {
                    return Json("OK");
                }
            }
            return Json("Could not add question to database!");
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody]question incomingQuestion)
        {
            if (ModelState.IsValid)
            {
                var db = new AppDB(_context);
                bool OK = db.editRating(id, incomingQuestion);
                if (OK)
                {
                    return Json("OK");
                }
            }
            return Json("Could not edit question in database!");
        }
    }
}