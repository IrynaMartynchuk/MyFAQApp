

using System;

namespace MyFAQ.Models
{
    public class question
    {
        public int id { get; set; }
        public string question_ { get; set; }
        public string answer { get; set; }
        public string categoryTitle { get; set; }
        public string customerName { get; set; }
        public string customerSurname { get; set; }
        public string customerEmail { get; set; }
        public int thumbup { get; set; }
        public int thumbdown { get; set; }
        public DateTime date { get; set; }
    }
}
