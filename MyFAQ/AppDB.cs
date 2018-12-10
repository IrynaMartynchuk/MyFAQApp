using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyFAQ.Models;

namespace MyFAQ
{
    public class AppDB
    {
        private readonly MyAppContext _context;
        public AppDB(MyAppContext context)
        {
            _context = context;
        }

        public List<Category> getCategories()
        {
            List<Category> allCategories = _context.Categories.Select(q => new Category()
            {
                title = q.title,
                questions = q.questions
            }).
                                      ToList();
            return allCategories;
        }

        public List<question> getAllQuestions()
        {
            List<question> allQuestions = _context.Questions.Where(q => q.answer != null).Select(q => new question()
            {
                id = q.id,
                question_ = q.question,
                answer = q.answer,
                thumbup = q.thumbup,
                thumbdown = q.thumbdown,
                categoryTitle = q.category.title,
                date = q.date
            }).
                                      ToList();
            return allQuestions;
        }

        public List<question> getAllNotAnswered()
        {
            List<question> allQuestions = _context.Questions.Where(q => q.answer == null).Select(q => new question()
            {
                id = q.id,
                question_ = q.question,
                answer = q.answer,
                categoryTitle = q.category.title,
                customerName = q.customer.name,
                customerEmail = q.customer.surname,
                customerSurname = q.customer.email,
                date = q.date
            }).ToList();
            return allQuestions;
        }

        public question getQuestionById(int id)
        {
            Question selected_question = _context.Questions.Include(k => k.category).FirstOrDefault(q => q.id == id);

            var question = new question()
            {
                id = selected_question.id,
                question_ = selected_question.question,
                answer = selected_question.answer,
                thumbup = selected_question.thumbup,
                thumbdown = selected_question.thumbdown,
                categoryTitle = selected_question.category.title
            };
            return question;
        }

        public bool saveQuestionFromCustomer(question incomingQuestion)
        {
            var question = new Question
            {
                question = incomingQuestion.question_,
                answer = incomingQuestion.answer,
                thumbup = 0,
                thumbdown = 0,
                date = DateTime.Today
            };
            Category foundCategory = _context.Categories.FirstOrDefault(q => q.title == "DIFFERENT QUESTIONS");

            question.category = foundCategory;
            
            Customer foundCustomer = _context.Customers.Find(incomingQuestion.customerEmail);
            if (foundCustomer == null)
            {
                var newCustomer = new Customer
                {
                    name = incomingQuestion.customerName,
                    surname = incomingQuestion.customerSurname,
                    email = incomingQuestion.customerEmail
                };
                question.customer = newCustomer;
            }
            else
            {
                question.customer = foundCustomer;
            }

            try
            {
                _context.Questions.Add(question);
                _context.SaveChanges();
            }
            catch (Exception error)
            {
                return false;
            }
            return true;
        }

        public bool saveQuestion(question incomingQuestion)
        {
            var question = new Question
            {
                question = incomingQuestion.question_,
                answer = incomingQuestion.answer,
                thumbup = 0,
                thumbdown = 0,
                date = DateTime.Now
            };
            
            Category foundCategory = _context.Categories.Find(incomingQuestion.categoryTitle);
            if (foundCategory == null)
            {
                var newCategory = new Category
                {
                    title = incomingQuestion.categoryTitle
                };
                question.category = newCategory;
            }
            else
            {
                question.category = foundCategory;
            }

            try
            {
                _context.Questions.Add(question);
                _context.SaveChanges();
            }
            catch (Exception error)
            {
                return false;
            }
            return true;
        }

        public bool editQuestion(int id, question incomingQuestion)
        {
            Question foundQuestion = _context.Questions.FirstOrDefault(q => q.id == id);
            if (foundQuestion == null)
            {
                return false;
            }
            foundQuestion.question = incomingQuestion.question_;
            foundQuestion.answer = incomingQuestion.answer;
            foundQuestion.thumbup = foundQuestion.thumbup + incomingQuestion.thumbup;
            foundQuestion.thumbdown = incomingQuestion.thumbdown + incomingQuestion.thumbdown;

            Category foundCategory = _context.Categories.Find(incomingQuestion.categoryTitle);
            if (foundCategory == null)
            {
                var newCategory = new Category
                {
                    title = incomingQuestion.categoryTitle
                };
                foundQuestion.category = newCategory;
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception error)
            {
                return false;
            }
            return true;
        }

        public bool editRating(int id, question incomingQuestion)
        {
            Question foundQuestion = _context.Questions.FirstOrDefault(q => q.id == id);
            if (foundQuestion == null)
            {
                return false;
            }
                foundQuestion.thumbup = foundQuestion.thumbup + incomingQuestion.thumbup;
            
                foundQuestion.thumbdown = foundQuestion.thumbdown + incomingQuestion.thumbdown;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception error)
            {
                return false;
            }
            return true;
        }

        public bool deleteQuestion(int id)
        {
            try
            {
                Question question = _context.Questions.Find(id);
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }
            catch (Exception error)
            {
                return false;
            }
            return true;
        }
    }
}