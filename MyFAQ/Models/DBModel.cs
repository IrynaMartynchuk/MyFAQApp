using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyFAQ.Models
{
    public class Question
    {
        [Key]
        public int id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public int thumbup { get; set; }
        public int thumbdown { get; set; }
        public DateTime date { get; set; }
        public virtual Category category { get; set; }
        public virtual Customer customer { get; set; }
    }

    public class Category
    {
        [Key]
        public string title { get; set; }
        public virtual List<Question> questions { get; set; }
    }

    public class Customer
    {
        [Key]
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public virtual List<Question> questions { get; set; }
    }

    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options)
        : base(options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
