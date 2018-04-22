using System;

namespace GabDemo.Models
{
    public class Customer
    {
        public string CustomerId => Guid.NewGuid().ToString();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}