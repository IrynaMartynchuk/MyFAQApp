using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyFAQ.Models;

namespace MyFAQ.Controllers
{
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        private readonly MyAppContext _context;
        public CustomerController(MyAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<question> Get()
        {
            var db = new AppDB(_context);
            List<question> allQuestions = db.getAllNotAnswered();
            return allQuestions;
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody]question incomingQuestion)
        {
            if (ModelState.IsValid)
            {
                var db = new AppDB(_context);
                bool OK = db.editQuestion(id, incomingQuestion);
                if (OK)
                {
                    return Json("OK");
                }
            }
            return Json("Could not edit question in database!");
        }

    }
}