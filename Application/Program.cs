using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExpressionTests.App.Models;

namespace Atest
{
    class Program
    {
        static void Main(string[] args)
        {
            var filters = new List<ExpressionFilter>
            {
                new ExpressionFilter
                {
                    PropertyName = nameof(User.Age),
                    Comparison = Comparison.GreaterThanOrEqual,
                    Value = 25
                },
            };

            var result = Users.Where(ExpressionCreator.ConstructAndExpressionTree<User>(filters).Compile());
        }
        private static readonly List<User> Users = new List<User>
        {
            new User
            {
                Username = "jhannes",
                FirstName = "Johan",
                LastName = "Hannes",
                Age = 20,
                CreationTimestamp = new DateTime(2019, 12, 22, 14, 1, 10),
                PurchaseAmountLifetime = 205.20m
            },
            new User
            {
                Username = "pcannes",
                FirstName = "Peter",
                LastName = "Cannes",
                Age = 30,
                CreationTimestamp = new DateTime(2018, 12, 22, 14, 1, 10),
                PurchaseAmountLifetime = 2002.20m
            },
            new User
            {
                Username = "crannes",
                FirstName = "Calr",
                LastName = "rannes",
                Age = 45,
                CreationTimestamp = new DateTime(2000, 12, 22, 14, 1, 10),
                PurchaseAmountLifetime = 0.20m
            }
        };
    }
}