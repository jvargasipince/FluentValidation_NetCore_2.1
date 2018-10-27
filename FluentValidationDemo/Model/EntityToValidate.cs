using System;

namespace FluentValidationDemo.Model
{
    public class EntityToValidate
    {
        
        public string FullName { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool HasDiscount { get; set; }

        public decimal Discount { get; set; }

    }
}
