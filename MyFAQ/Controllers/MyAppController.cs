using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFAQ.Models;

namespace MyFAQ.Controllers
{
    [Route("api/MyApp")]
    public class MyAppController : Controller
    {
        private readonly MyAppContext _context;
        public MyAppController(MyAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var db = new AppDB(_context);
            List<question> allQuestions = db.getAllQuestions();
            return Json(allQuestions);
        }
        
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var db = new AppDB(_context);
            question question = db.getQuestionById(id);
            return Json(question);
        }
        
        [HttpPost]
        public JsonResult Post([FromBody]question incomingQuestion)
        {
            if (ModelState.IsValid)
            {
                var db = new AppDB(_context);
                bool OK = db.saveQuestion(incomingQuestion);
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
                bool OK = db.editQuestion(id, incomingQuestion);
                if (OK)
                {
                    return Json("OK");
                }
            }
            return Json("Could not edit question in database!");
        }
        
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var db = new AppDB(_context);
            bool OK = db.deleteQuestion(id);
            if (!OK)
            {
                return Json("Could not delete question!");
            }
            return Json("OK");
        }
    }
}