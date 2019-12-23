using System;

namespace ExpressionTests.App.Models
{
    public interface IUser
    {
        string Username { get; set; }
    }

    public class User : IUser
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public decimal PurchaseAmountLifetime { get; set; }
        public DateTime CreationTimestamp { get; set; }
    }
}